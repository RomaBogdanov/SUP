using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using System.Data;

namespace SupRealClient.Views
{
    public class VisitorsListModel<T> : Base4ModelAbstr<T>
        where T : EnumerationClasses.Visitor, new()
    {
        public VisitorsListModel()
        {
            VisitorsWrapper.CurrentTable().OnChanged += Query;
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

        #region BtnHandlers

        public override void Add()
        {
            Visitor.AddVisitorView wind = new Visitor.AddVisitorView();
            wind.Show();
        }

        public override void Farther()
        {
            System.Windows.Forms.MessageBox.Show("Farther");
        }

        public override void Search()
        {
            System.Windows.Forms.MessageBox.Show("Search");
        }

        public override void Update()
        {
            System.Windows.Forms.MessageBox.Show("Update");
        }

        #endregion

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.Name };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from visitors in VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                where visitors.Field<int>("f_visitor_id") != 0
                select new T
                {
                    Id = visitors.Field<int>("f_visitor_id"),
                    FullName = visitors.Field<string>("f_full_name"),
                    Organization = (string)OrganizationsWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_org_id") == 
                        visitors.Field<int>("f_org_id"))["f_full_org_name"],
                    Comment = visitors.Field<string>("f_vr_text")
                }
                );
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_visitor_id");
        }

        protected override DataTable Table
        {
            get
            {
                return VisitorsWrapper.CurrentTable().Table;
            }
        }
    }
}
