using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using System.Data;
using SupRealClient.Common;
using System.Windows;

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
            if (CurrentItem != null)
            {
                ViewManager.Instance.OpenWindow(new AddUpdateCabinetView(CurrentItem), Parent);
            }
        }

        public override bool Remove()
        {
            if ((from vis in VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                 where vis.Field<int>("f_cabinet_id") == currentItem.Id &&
                 CommonHelper.NotDeleted(vis)
                 select vis).Any())
            {
                MessageBox.Show("Кабинет невозможно удалить, т.к. он связана с посетителями!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if ((from cz in CabinetsZonesWrapper.CurrentTable().Table.AsEnumerable()
                 where cz.Field<int>("f_cabinet_id") == currentItem.Id
                 select cz).Any())
            {
                MessageBox.Show("Кабинет невозможно удалить, т.к. он связана с посетителями!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            DataRow row =
                CabinetsWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.Descript};
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
    from cabs in CabinetsWrapper.CurrentTable().Table.AsEnumerable()
    where cabs.Field<int>("f_cabinet_id") != 0 &&
    CommonHelper.NotDeleted(cabs)
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
