﻿using System;
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
    class Base3ZonesModel : Base3ModelAbstr
    {
        CabinetsZonesWrapper cabinetsZones = CabinetsZonesWrapper.CurrentTable();
        ZoneTypesWrapper zoneTypes = ZoneTypesWrapper.CurrentTable();
        CabinetsWrapper cabinets = CabinetsWrapper.CurrentTable();

        public Base3ZonesModel(IBase1ViewModel viewModel, IWindow parent)
        {
            this.viewModel = viewModel;
            this.parent = parent;
            ZonesWrapper documentsWrapper = ZonesWrapper.CurrentTable();
            table = documentsWrapper.Table;
            tabConnector = documentsWrapper.Connector;
            tabName = documentsWrapper.Table.TableName;
            documentsWrapper.OnChanged += Query;
            cabinetsZones.OnChanged += Query;
            zoneTypes.OnChanged += Query;
            cabinets.OnChanged += Query;
            this.Query();
        }

        public override void EnterCurrentItem(object item)
        {
            this.viewModel.NumItem = (item as Zone).Id;
        }

        public override void Add()
        {
            //ViewManager.Instance.Add(new AddItemDocumentsModel(), parent);
            AddUpdateZoneWindView addUpdateZoneWindView = new AddUpdateZoneWindView();
            addUpdateZoneWindView.Show();
        }

        public override void Update()
        {
            if ((Zone)this.viewModel.CurrentItem != null)
            {
                //ViewManager.Instance.Update(new UpdateItemDocumentsModel((Document)this.viewModel.CurrentItem), parent);
                AddUpdateZoneWindView addUpdateZoneWindView =
                    new AddUpdateZoneWindView((Zone)this.viewModel.CurrentItem);
                addUpdateZoneWindView.Show();
            }
        }

        public override void Begin()
        {
            if (this.viewModel.Set.Count() > 0)
            {
                this.viewModel.CurrentItem = this.viewModel.Set.First();
                this.viewModel.NumItem =
                    (this.viewModel.CurrentItem as Zone).Id;
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
                (this.viewModel.CurrentItem as Zone).Id;
            this.viewModel.SelectedIndex = this.viewModel.Set.Count() - 1;
        }

        protected override void Query()
        {
            var zoneDoors = from cabs in cabinets.Table.AsEnumerable()
                            join cabszns in cabinetsZones.Table.AsEnumerable()
                            on cabs.Field<int>("f_cabinet_id") equals cabszns.Field<int>("f_cabinet_id")
                            where cabszns.Field<int>("f_zone_id") != 0 &&
                            CommonHelper.NotDeleted(cabs)
                            select new
                            {
                                Id = cabszns.Field<int>("f_zone_id"),
                                Door = cabs.Field<string>("f_door_num")
                            };
            var znsDoors = zoneDoors.GroupBy(x => x.Id)
                .Select(g => new { g.Key, Door = string
                .Join(", ", g.Select(x => x.Door)) });

            var zones = from typezns in zoneTypes.Table.AsEnumerable()
                        join zns in table.AsEnumerable()
                        on typezns.Field<int>("f_zone_type_id") equals zns.Field<int>("f_zone_type_id")
                        join zd in znsDoors
                         on zns.Field<int>("f_zone_id") equals zd.Key
                        select new Zone
                        {
                            Id = zns.Field<int>("f_zone_id"),
                            ZoneNum = zns.Field<int>("f_zone_num"),
                            Type = typezns.Field<string>("f_zone_type_name"),
                            Name = zns.Field<string>("f_zone_name"),
                            RelatedDoors = zd.Door
                        };
            /*var zones2 = from z in zones
                         join zd in znsDoors
                         on z.Id equals zd.Key
                         select z.RelatedDoors = zd.Door;*/
            this.viewModel.Set = new System.Collections.ObjectModel.ObservableCollection<object>(zones);
            if (viewModel.NumItem == -1)
            {
                this.Begin();
            }
            else
            {
                try
                {
                    this.viewModel.CurrentItem = zones.First(
                        arg => arg.Id == this.viewModel.NumItem);
                }
                catch (Exception)
                {
                    this.Begin();
                }
            }
        }

        // TODO - переделать без повторов кода
        public override DataRow[] Rows
        {
            get
            {
                var zoneDoors = from cabs in cabinets.Table.AsEnumerable()
                                join cabszns in cabinetsZones.Table.AsEnumerable()
                                on cabs.Field<int>("f_cabinet_id") equals cabszns.Field<int>("f_cabinet_id")
                                select new
                                {
                                    Id = cabszns.Field<int>("f_zone_id"),
                                    Door = cabs.Field<string>("f_door_num")
                                };
                var znsDoors = zoneDoors.GroupBy(x => x.Id)
                    .Select(g => new {
                        g.Key,
                        Door = string
                    .Join(", ", g.Select(x => x.Door))
                    });
                return (from typezns in zoneTypes.Table.AsEnumerable()
                        join zns in table.AsEnumerable()
                        on typezns.Field<int>("f_zone_type_id") equals zns.Field<int>("f_zone_type_id")
                        join zd in znsDoors
                        on zns.Field<int>("f_zone_id") equals zd.Key
                        select zns).AsEnumerable().ToArray();
            }
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                //{ "f_zone_type_name", "Тип зоны" },
                { "f_zone_num", "Номер" },
                { "f_zone_name", "Название" },
                //{ "f_door_num", "Привязанные двери" },
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_zone_id");
        }

        public override void Watch()
        {
            throw new NotImplementedException();
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                //{ "Type", "f_zone_type_name" },
                { "ZoneNum", "f_zone_num" },
                { "Name", "f_zone_name" },
                //{ "RelatedDoors", "f_door_num" },
            };
        }
    }
}
