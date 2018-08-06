using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using System.Data;
using SupRealClient.Common;
using System.Collections.Generic;

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
            //Visitor.AddVisitorView wind = new Visitor.AddVisitorView();
            //wind.Show();
            //ViewManager.Instance.OpenWindow("VisitorsViewNew");
            /*VisitorsView.Instance.Show();
            VisitorsView.Instance.NewVisitor();*/
            //object res = ViewManager.Instance.OpenWindowModal("VisitorsView");
            ViewManager.Instance.OpenWindow("VisitorsView", this.Parent);
        }

        public override void Farther()
        {
            SetAt(searchResult.Next());
        }

        
        public override void Update()
        {
            if (CurrentItem != null)
            { 
                ViewManager.Instance.OpenWindow("VisitorsView", this.Parent);
            }
        }

        #endregion

        protected override BaseModelResult GetResult()
        {
            return new VisitorsModelResult
            {
                Id = CurrentItem.Id,
                Name = CurrentItem.FullName,
                OrganizationId = CurrentItem.OrganizationId,
                Organization = CurrentItem.Organization,
				IsBlock = CurrentItem.IsAccessDenied,
				IsCardIssue = true // todo: получить из данных пользователя
            };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from visitors in VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                where visitors.Field<int>("f_visitor_id") != 0 &&
                CommonHelper.NotDeleted(visitors)
                select new T
                {
                    Id = visitors.Field<int>("f_visitor_id"),
                    FullName = visitors.Field<string>("f_full_name"),
                    OrganizationId = visitors.Field<int>("f_org_id"),
                    Comment = visitors.Field<string>("f_vr_text")
                }
                );
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_visitor_id");
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>
            {                
                { "FullName", "ФИО" },
                { "Organization", "Организация" },
                { "Comment", "Примечание" },
            };
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
