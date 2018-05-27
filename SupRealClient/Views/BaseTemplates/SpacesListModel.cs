using System;
using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.EnumerationClasses;
using System.Data;
using SupRealClient.TabsSingleton;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.ViewModels.AddUpdateViewModel;

namespace SupRealClient.Views
{
    public class SpacesListModel<T> : Base4ModelAbstr<T>
        where T : Space, new()
    {
        protected override DataTable Table
        { get { return SpacesWrapper.CurrentTable().Table; } }

        public SpacesListModel()
        {
            SpacesWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            AddUpdateAbstrModel model = new AddSpaceModel();
            AddUpdateBaseViewModel viewModel = new AddUpdateBaseViewModel
                {
                    Model = model
                };
            AddUpdateSpaceView view = new AddUpdateSpaceView();
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
                from spaces in Table.AsEnumerable()
                where spaces.Field<int>("f_space_id") != 0
                select new T
                {
                    Id = spaces.Field<int>("f_space_id"),
                    NumReal = spaces.Field<string>("f_num_real"),
                    NumBuild = spaces.Field<string>("f_num_build"),
                    Descript = spaces.Field<string>("f_descript"),
                    Note = spaces.Field<string>("f_note")
                });
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.NumReal };

        }
    }
}
