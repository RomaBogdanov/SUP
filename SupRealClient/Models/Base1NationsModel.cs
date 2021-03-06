﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Interfaces;
using SupRealClient.Common;

namespace SupRealClient.Models
{
    class Base1NationsModel : Base1ModelAbstr
    {
        public Base1NationsModel(IBase1ViewModel viewModel, IWindow parent)
        {
            this.viewModel = viewModel;
            this.parent = parent;
            CountriesWrapper countriesWrapper = CountriesWrapper.CurrentTable();
            table = countriesWrapper.Table;
            tabConnector = countriesWrapper.Connector;
            tabName = countriesWrapper.Table.TableName;
            countriesWrapper.OnChanged += Query;
            this.Query();
        }

        public override void Add()
        {
            ViewManager.Instance.Add(new AddItemNationsModel(), parent);
        }

        public override void Begin()
        {
            if (this.viewModel.Set.Count() > 0)
            {
                this.viewModel.CurrentItem = this.viewModel.Set.First();
                this.viewModel.NumItem =
                    (this.viewModel.CurrentItem as Nation).Id;
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
                (this.viewModel.CurrentItem as Nation).Id;
            this.viewModel.SelectedIndex = this.viewModel.Set.Count() - 1;
        }

        public override void EnterCurrentItem(object item)
        {
            this.viewModel.NumItem = (item as Nation).Id;
        }

        public override void Update()
        {
            ViewManager.Instance.Update(new UpdateItemNationsModel((Nation)this.viewModel.CurrentItem), parent);
        }

        protected override void Query()
        {
            var nations = from nats in table.AsEnumerable()
                          where nats.Field<int>("f_cntr_id") != 0 &&
                          CommonHelper.NotDeleted(nats)
                          select new Nation()
                            {
                                Id = nats.Field<int>("f_cntr_id"),
                                CountryName = nats.Field<string>("f_cntr_name"),
                                Deleted = CommonHelper.StringToBool(
                                    nats.Field<string>("f_deleted")),
                                RecDate = nats.Field<DateTime>("f_rec_date"),
                                RecOperator = nats.Field<int>("f_rec_operator")
                          };
            this.viewModel.Set = new System.Collections.ObjectModel.ObservableCollection<object>(nations);
            if (viewModel.NumItem == -1)
            {
                this.Begin();
            }
            else
            {
                try
                {
                    this.viewModel.CurrentItem = nations.First(
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
            return new Dictionary<string, string>()
            {
                { "f_cntr_name", "Название" }
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_cntr_id");
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "CountryName", "f_cntr_name" },
            };
        }
    }
}
