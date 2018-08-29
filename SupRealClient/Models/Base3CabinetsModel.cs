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
using SupRealClient.Common;

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
                new AddUpdateCabinetView(this.viewModel.CurrentItem as Cabinet, "Изменение кабинета");
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

        protected override void Query()
        {
            var cabinets = from cabs in table.AsEnumerable()
                           where cabs.Field<int>("f_cabinet_id") != 0 &&
                           CommonHelper.NotDeleted(cabs)
                           select new Cabinet()
                            {
                                Id = cabs.Field<int>("f_cabinet_id"),
                                CabNum = cabs.Field<string>("f_cabinet_num"),
                                Descript = cabs.Field<string>("f_cabinet_desc"),
                                DoorNum = cabs.Field<string>("f_door_num")
                            };
            this.viewModel.Set = new System.Collections.ObjectModel.ObservableCollection<object>(cabinets);
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

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_cabinet_id");
        }

        public override void Watch()
        {
            throw new NotImplementedException();
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "CabNum", "f_cabinet_num" },
                { "Descript", "f_cabinet_desc" },
                { "DoorNum", "f_door_num" },
            };
        }
    }
}
