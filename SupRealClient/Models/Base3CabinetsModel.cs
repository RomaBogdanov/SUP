using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Common.Interfaces;
using System.Data;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using SupRealClient.Views;

namespace SupRealClient.Models
{
    class Base3CabinetsModel : Base3ModelAbstr
    {
        public Base3CabinetsModel(IBase1ViewModel viewModel, IWindow parent)
        {
            this.viewModel = viewModel;
            this.parent = parent;
            CabinetsWrapper documentsWrapper = CabinetsWrapper.CurrentTable();
            table = documentsWrapper.Table;
            tabConnector = documentsWrapper.Connector;
            tabName = documentsWrapper.Table.TableName;
            documentsWrapper.OnChanged += Query;
            this.Query();
        }

        public override void EnterCurrentItem(object item)
        {
            this.viewModel.NumItem = (item as Cabinet).Id;
        }

        public override void Add()
        {
            //ViewManager.Instance.Add(new AddItemDocumentsModel(), parent);
            AddUpdateCabinetView addUpdateCabinetView = new AddUpdateCabinetView();
            addUpdateCabinetView.Show();
        }

        public override void Update()
        {
            //ViewManager.Instance.Update(new UpdateItemDocumentsModel((Document)this.viewModel.CurrentItem), parent);
            AddUpdateCabinetView addUpdateCabinetView = 
                new AddUpdateCabinetView(this.viewModel.CurrentItem as Cabinet);
            addUpdateCabinetView.Show();
        }

        public override void Begin()
        {
            if (this.viewModel.Set.Count() > 0)
            {
                this.viewModel.CurrentItem = this.viewModel.Set.First();
                this.viewModel.NumItem =
                    (this.viewModel.CurrentItem as Cabinet).Id;
                this.viewModel.SelectedIndex = 0;
            }
            else
            {
                this.viewModel.NumItem = -1;
            }
        }

        public override void End()
        {
            this.viewModel.CurrentItem = this.viewModel.Set.Last();
            this.viewModel.NumItem =
                (this.viewModel.CurrentItem as Cabinet).Id;
            this.viewModel.SelectedIndex = this.viewModel.Set.Count() - 1;
        }

        public override void Farther()
        {
            //throw new NotImplementedException();
        }

        public override void Searching(string pattern)
        {
            /*var indSet = this.viewModel.Set
                .Select((arg, index) =>
                new { index, at = (arg as Document).DocName.StartsWith(pattern) });
            this.viewModel.SelectedIndex =
                indSet.FirstOrDefault(arg1 => arg1.at == true) != null ?
                indSet.FirstOrDefault(arg1 => arg1.at == true).index :
                this.viewModel.SelectedIndex;*/
        }

        protected override void Query()
        {
            var cabinets = from cabs in table.AsEnumerable()
                            select new Cabinet()
                            {
                                Id = cabs.Field<int>("f_cabinet_id"),
                                CabNum = cabs.Field<string>("f_cabinet_num"),
                                Descript = cabs.Field<string>("f_cabinet_desc"),
                                DoorNum = cabs.Field<string>("f_door_num")
                            };
            this.viewModel.Set = cabinets;
            if (viewModel.NumItem == -1)
            {
                this.Begin();
            }
            else
            {
                try
                {
                    this.viewModel.CurrentItem = cabinets.First(
                        arg => arg.Id == this.viewModel.NumItem);
                }
                catch (Exception)
                {
                    this.Begin();
                }
            }
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>() { { "f_cabinet_desc", "Описание" } };
        }

        public override void Watch()
        {
            throw new NotImplementedException();
        }
    }
}
