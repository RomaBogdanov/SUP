using AndoverLib;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Forms;
using System.Xml;

namespace AndoverPersonsManager
{
    public partial class LoadDataForm : Form
    {
        private readonly ProgramData _programData;
        private bool _loaded = false;

        public LoadDataForm(ProgramData programData)
        {
            _programData = programData;

            InitializeComponent();
        }

        private void LoadData()
        {
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
                new EndpointAddress("http://localhost:7001/AndoverHost"));
            IAndoverService wcfClient = myChannelFactory.CreateChannel();

            _programData.Containers = wcfClient.GetContainers();
            //_programData.Devices = wcfClient.GetDevices();
            //_programData.Schedules = wcfClient.GetSchedules();
            //_programData.Doors = wcfClient.GetDoors();
            _programData.Areas = wcfClient.GetAreas();
            //_programData.DoorLists = wcfClient.GetDoorLists();
            _programData.Personnels = wcfClient.GetPersons();
            _programData.AreaLinks = wcfClient.GetAreaLinks();

            _programData.ContainerCores = new List<ContainerCore>();
            foreach (var cont in _programData.Containers)
            {
                _programData.ContainerCores.Add(new ContainerCore(cont));
            }

            _programData.AreaCores = new List<AreaCore>();
            foreach (var area in _programData.Areas)
            {
                _programData.AreaCores.Add(new AreaCore(area, _programData.Containers));
            }

            _programData.PersonCores = new List<PersonCore>();
            foreach (var pers in _programData.Personnels)
            {
                _programData.PersonCores.Add(new PersonCore(pers,
                    _programData.Containers, _programData.Areas, _programData.AreaLinks));
            }
            _programData.PersonCores.Sort((p1, p2) => p1.ToString().CompareTo(p2.ToString()));
        }

        private void LoadDataForm_Shown(object sender, EventArgs e)
        {
            if (_loaded)
            {
                return;
            }
            _loaded = true;
            try
            {
                LoadData();
                _programData.Valid = true;
            }
            catch (Exception ex)
            {
                _programData.Valid = false;
                _programData.ErrorMessage = ex.Message;
            }
            this.Close();
        }
    }
}
