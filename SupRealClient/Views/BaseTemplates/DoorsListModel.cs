using System;
using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.EnumerationClasses;
using System.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.ViewModels.AddUpdateViewModel;
using SupRealClient.Views.AddUpdateView;

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
            AddUpdateBaseViewModel viewModel = new AddUpdateBaseViewModel
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
            throw new NotImplementedException();
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
                        SpaceIn = doors.Field<int>("f_space_in"),
                        SpaceOut = doors.Field<int>("f_space_out"),
                        AccessPointId = doors.Field<int>("f_access_point_id"),
                        KeyId = doors.Field<int>("f_key_id")
                    });
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.DoorNum };
        }
    }
}
