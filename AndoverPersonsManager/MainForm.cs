using AndoverLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace AndoverPersonsManager
{
    public partial class MainForm : Form
    {
        private readonly ProgramData _programData = new ProgramData();
        private bool _loaded = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (_loaded)
            {
                return;
            }
            LoadData();
        }

        private void LoadData()
        {
            _loaded = true;
            var form = new LoadDataForm(_programData);
            form.ShowDialog();

            if (!_programData.Valid)
            {
                MessageBox.Show(_programData.ErrorMessage, "Не удалось загрузить данные из Andover",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listBox1.Items.Clear();
            foreach (var pers in _programData.PersonCores)
            {
                int idx = listBox1.Items.Add(pers);
            }

            UpdateControls();
        }

        private void UpdateControls()
        {
            PersonCore pers = null;
            if (listBox1.SelectedItems.Count == 1)
            {
                pers = listBox1.SelectedItems[0] as PersonCore;
            }
            buttonEdit.Enabled = pers != null;
            buttonDelete.Enabled = pers != null && pers.Personnel == null;
            buttonUnload.Enabled = listBox1.SelectedItems.Count > 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new PersonForm(_programData, null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Add(form.Person);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            PersonCore pers = null;
            if (listBox1.SelectedItems.Count == 1)
            {
                pers = listBox1.SelectedItems[0] as PersonCore;
            }
            if (pers == null)
            {
                return;
            }
            var form = new PersonForm(_programData, pers);
            if (form.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items[listBox1.SelectedIndex] = form.Person;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            PersonCore pers = null;
            if (listBox1.SelectedItems.Count == 1)
            {
                pers = listBox1.SelectedItems[0] as PersonCore;
            }
            if (pers == null)
            {
                return;
            }
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void buttonUnload_Click(object sender, EventArgs e)
        {
            var containers = new List<string>();
            containers.AddRange(_programData.ContainerCores.Select(c => c.Name));
            var info = new List<PersonInfo>();
            foreach (PersonCore pers in listBox1.SelectedItems)
            {
                var pi = pers.Convert();
                if (containers.FirstOrDefault(c =>
                    c.ToUpper() == pi.Containers[pi.Containers.Count - 1].ToUpper()) == null)
                {
                    pi.CreateFolder = true;
                    containers.Add(pi.Containers[pi.Containers.Count - 1]);
                }
                info.Add(pi);
            }

            var binding = new WSDualHttpBinding()
            {
                MaxReceivedMessageSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxArrayLength = 2147483647,
                    MaxStringContentLength = 2147483647
                }
            };
            var myChannelFactory = new ChannelFactory<IAndoverService>(
                binding,
                new EndpointAddress(ConfigurationManager.AppSettings["Endpoint"]));
            IAndoverService wcfClient = myChannelFactory.CreateChannel();
           
            bool result = wcfClient.ExportPersonsDmp(info);

            if (result)
            {
                MessageBox.Show("Данные выгружены в Andover", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Выгрузка в Andover не удалась", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Thread.Sleep(1000);
            LoadData();
        }
    }
}
