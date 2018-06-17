using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AndoverPersonsManager
{
    public partial class ZonesForm : Form
    {
        private readonly ProgramData _programData;
        private PersonCore _person;

        public ZonesForm(ProgramData programData, PersonCore person)
        {
            _programData = programData;
            _person = person;

            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            _person.Areas = new List<AreaCore>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                var area = checkedListBox1.Items[i] as AreaCore;
                if (area != null && checkedListBox1.GetItemChecked(i))
                {
                    _person.Areas.Add(area);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ZonesForm_Load(object sender, EventArgs e)
        {
            foreach (var area in _programData.AreaCores)
            {
                int idx = checkedListBox1.Items.Add(area);
                if (_person.Areas.FirstOrDefault(a =>
                    a.Area.ObjectIdHi == area.Area.ObjectIdHi &&
                    a.Area.ObjectIdLo == area.Area.ObjectIdLo) != null)
                {
                    checkedListBox1.SetItemChecked(idx, true);
                }
            }
        }
    }
}
