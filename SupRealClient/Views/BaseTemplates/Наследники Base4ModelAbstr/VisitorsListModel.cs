using System;
using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using System.Data;
using SupRealClient.Common;
using System.Collections.Generic;
using System.Windows;
using SupRealClient.Common.Interfaces;
using SupRealClient.Models;
using SupRealClient.EnumerationClasses;

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

	    private bool _isRequeiredCanHaveVisitors;
	    private bool _isRequiredCanSign;
	    private bool _isRequiredCanAgree;
		public bool IsRequeiredCanHaveVisitors
		{
			get => _isRequeiredCanHaveVisitors;
			set
			{
				_isRequeiredCanHaveVisitors = value;
				DoQuery();
				Begin();
			}
		}

	    public bool IsRequiredCanSign
	    {
		    get { return _isRequiredCanSign; }
		    set
		    {
			    _isRequiredCanSign = value;
			    DoQuery();
				Begin();
		    }
	    }

	    public bool IsRequiredCanAgree
	    {
		    get { return _isRequiredCanAgree; }
		    set
		    {
			    _isRequiredCanAgree = value;
				DoQuery();
				Begin();
			}
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

            //todo: создание нового view - костыль для открытия в режиме редактирования. Позже нужно удалить.
            ViewManager.Instance.OpenWindow("VisitorsView", this.Parent ?? new VisitorsListWindView(null));                       
        }

        public override void Farther()
        {
            SetAt(searchResult.Next());
        }

        
        public override void Update()
        {
	        if (CurrentItem != null)
	        {
				
				IWindow parent = Parent;
		        if (parent == null) //todo: создание нового view - костыль для открытия в режиме редактирования. Позже нужно удалить.
				{
			        parent = new VisitorsListWindView(null);
		        }
		        if (parent is VisitorsListWindView parentView)
		        {
			        parentView.base4.modeEdit = true;
			        parentView.base4.baseTab.CurrentItem = currentItem;
		        }
				ViewManager.Instance.OpenWindow("VisitorsView", parent);
	        }
        }

        public override void Watch()
        {
            if (CurrentItem != null)
            {
				
	            IWindow parent = Parent;
	            if (parent == null) //todo: создание нового view - костыль для открытия в режиме редактирования. Позже нужно удалить.
				{
		            parent = new VisitorsListWindView(null);
	            }
	            if (parent is VisitorsListWindView view)
	            {
		            view.base4.modeWatch = true;
		            view.base4.baseTab.CurrentItem = currentItem;
	            }
				ViewManager.Instance.OpenWindow("VisitorsView", parent);
            }
        }

        public override void Synonyms()
        {
            if (CurrentItem != null)
            {
                Organization currentOrg = OrganizationsHelper.GetOrganization(CurrentItem.OrganizationId, true);
                int? res = ViewManager.Instance.OpenSynonims(currentOrg);                
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
                    Comment = visitors.Field<string>("f_vr_text"),
					Family = visitors.Field<string>("f_family"),
					Name = visitors.Field<string>("f_fst_name"),
					Patronymic = visitors.Field<string>("f_sec_name"),
					Organization = OrganizationsHelper.
								   GenerateFullName(visitors.Field<int>("f_org_id"), true),
					IsAccessDenied = CommonHelper.StringToBool(visitors.Field<string>(
						"f_persona_non_grata")),
					IsCanHaveVisitors = CommonHelper.StringToBool(visitors.Field<string>(
						"f_can_have_visitors")),
	                IsNotFormular = CommonHelper.StringToBool(visitors.Field<string>("f_no_formular")),
					Telephone = visitors.Field<string>("f_phones"),
					Nation = (string)CountriesWrapper.CurrentTable()
						.Table.AsEnumerable()
						.FirstOrDefault(arg => arg.Field<int>("f_cntr_id") ==
						visitors.Field<int>("f_cntr_id"))?["f_cntr_name"],
					DocType = (string)DocumentsWrapper.CurrentTable().Table
						.AsEnumerable().FirstOrDefault(arg =>
						arg.Field<int>("f_doc_id") ==
						visitors.Field<int>("f_doc_id"))?["f_doc_name"],
					DocSeria = visitors.Field<string>("f_doc_seria"),
					DocNum = visitors.Field<string>("f_doc_num"),
					DocDate = visitors.Field<DateTime>("f_doc_date"),
					DocCode = visitors.Field<string>("f_doc_code"),
					DocPlace = visitors.Field<string>("f_doc_org"),
					IsAgree = CommonHelper.StringToBool(visitors.Field<string>(
						"f_personal_data_agreement")),
					AgreeToDate = visitors.Field<DateTime>("f_personal_data_last_date"),
					Position = visitors.Field<string>("f_job"),
					IsRightSign = CommonHelper.StringToBool(visitors.Field<string>(
						"f_can_sign_orders")),
					IsAgreement = CommonHelper.StringToBool(visitors.Field<string>(
						"f_can_adjust_orders")),
					Cabinet = (string)CabinetsWrapper.CurrentTable()
						.Table.AsEnumerable().FirstOrDefault(arg =>
						arg.Field<int>("f_cabinet_id") ==
						visitors.Field<int>("f_cabinet_id"))?["f_cabinet_desc"],
	                OrganizationIsBasic = OrganizationsHelper.GetBasicParametr(visitors.Field<int>("f_org_id"), true)
				}
                );
	        FilterSet();
        }

		private void FilterSet()
	    {

		    for (int i = 0; i < Set.Count; i++)
		    {
				if (IsRequeiredCanHaveVisitors && Set[i].IsCanHaveVisitors == false)
				{
					Set.RemoveAt(i);
					i--;
					continue;
				}

			    if (IsRequiredCanSign && Set[i].IsRightSign == false)
			    {
				    Set.RemoveAt(i);
				    i--;
					continue;
			    }

			    if (IsRequiredCanAgree && Set[i].IsAgreement == false)
			    {
				    Set.RemoveAt(i);
				    i--;
			    }
			}
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
