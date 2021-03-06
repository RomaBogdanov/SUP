﻿using System;
using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.EnumerationClasses;
using System.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.ViewModels.AddUpdateViewModel;
using SupRealClient.Views.AddUpdateView;
using System.Collections.Generic;

namespace SupRealClient.Views
{
    public class DoorsListModel<T> : Base4ModelAbstr<T>
        where T : Door, new()
    {
        protected override DataTable Table
        { get { return DoorsWrapper.CurrentTable().Table; } }

        public DoorsListModel()
        {
            DoorsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            AddUpdateAbstrModel model = new AddDoorModel();
            AddUpdateBaseViewModel viewModel = new AddUpdateDoorViewModel
            {
                Model = model
            };
            AddUpdateDoorWindView view = new AddUpdateDoorWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        public override void Update()
        {
            AddUpdateAbstrModel model = new UpdateDoorModel(CurrentItem);
            AddUpdateBaseViewModel viewModel = new AddUpdateDoorViewModel
            {
                Model = model
            };
            AddUpdateDoorWindView view = new AddUpdateDoorWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                    from doors in Table.AsEnumerable()
                    where doors.Field<int>("f_door_id") != 0
                    select new T
                    {
                        Id = doors.Field<int>("f_door_id"),
                        DoorNum = doors.Field<string>("f_door_num"),
                        Descript = doors.Field<string>("f_descript"),
                        SpaceInId = doors.Field<int>("f_space_in"),
                        SpaceIn = doors.Field<int>("f_space_in") == 0 ?
                            "" : SpacesWrapper.CurrentTable()
                            .Table.AsEnumerable().FirstOrDefault(
                            arg => arg.Field<int>("f_space_id") ==
                            doors.Field<int>("f_space_in"))["f_num_real"].ToString(),
                        SpaceOutId = doors.Field<int>("f_space_out"),
                        SpaceOut = doors.Field<int>("f_space_out") == 0 ?
                            "" : SpacesWrapper.CurrentTable()
                            .Table.AsEnumerable().FirstOrDefault(
                            arg => arg.Field<int>("f_space_id") ==
                            doors.Field<int>("f_space_out"))["f_num_real"].ToString(),
                        AccessPointIdHi = doors.Field<int>("f_access_point_id_hi"),
                        AccessPointIdLo = doors.Field<int>("f_access_point_id_lo"),
                        AccessPoint = (doors.Field<int>("f_access_point_id_hi") == 0 &&
                            doors.Field<int>("f_access_point_id_lo") == 0) ?
                            "" : AccessPointsWrapper.CurrentTable()
                            .Table.AsEnumerable().FirstOrDefault(
                            arg => arg.Field<int>("f_object_id_hi") ==
                            doors.Field<int>("f_access_point_id_hi") &&
                            arg.Field<int>("f_object_id_lo") ==
                            doors.Field<int>("f_access_point_id_lo")
                            )["f_access_point_name"].ToString(),
                    });
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.DoorNum };
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>
            {
                { "DoorNum", "Номер" },
                { "Descript", "Описание" },
            };
        }
    }
}
