using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using System.Data;

namespace SupRealClient.Views
{
    public class CabinetsListModel<T> : Base4ModelAbstr<T>
        where T : Cabinet, new()
    {
        public CabinetsListModel()
        {
            CabinetsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        public override void Add()
        {
            ViewManager.Instance.OpenWindow(new AddUpdateCabinetView(), Parent);
        }

        public override void Update()
        {
            ViewManager.Instance.OpenWindow(new AddUpdateCabinetView(CurrentItem), Parent);
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.Descript};
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
    from cabs in CabinetsWrapper.CurrentTable().Table.AsEnumerable()
    where cabs.Field<int>("f_cabinet_id") != 0
    select new T
    {
        Id = cabs.Field<int>("f_cabinet_id"),
        CabNum = cabs.Field<string>("f_cabinet_num"),
        Descript = cabs.Field<string>("f_cabinet_desc"),
        DoorNum = cabs.Field<string>("f_door_num")
    }
    );
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>()
            {
                { "f_cabinet_desc", "Описание" }
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_cabinet_id");
        }

        protected override DataTable Table
        {
            get
            {
                return CabinetsWrapper.CurrentTable().Table;
            }
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
