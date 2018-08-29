using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using SupRealClient.TabsSingleton;
using SupRealClient.EnumerationClasses;
using System.Data;
using SupRealClient.Models;
using SupRealClient.Common;
using System.Windows;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Views
{
    public class OrganizationsListModel<T> : Base4ModelAbstr<T>
        where T : Organization, new()
    {
	    public Func<bool> TestOrganization_ToSin { get; set; }


	    public OrganizationsListModel()
        {
            OrganizationsWrapper.CurrentTable().OnChanged += Query;
            Query();
            Begin();
        }

	    public override void Ok()
	    {
			//if (TestOrganization_ToSin != null)
			//{
			// if (TestOrganization_ToSin.Invoke() == true)
			// {
			//  int baseOrgID = ((Organization) CurrentItem).SynId;
			//  Organization baseOrg =
			//   OrganizationsHelper.GetOrganization(baseOrgID, true);
			//  if (MessageBox.Show(
			//       "При подтверждении выбора данной организации, в дальнейшем будет выбрано назние организации" +
			//       Environment.NewLine + " \"" + baseOrg.Name + "\"", "Внимание", MessageBoxButton.OKCancel,
			//       MessageBoxImage.Exclamation) == MessageBoxResult.OK)
			//  {
			//   CurrentItem =(T) baseOrg;

			//  }
			//  else
			//  {
			//   return;
			//  }
			// }
			//}

			if (CurrentItem is Organization)
			{
				if (((Organization)CurrentItem).SynId>0)
				{
					int baseOrgID = ((Organization)CurrentItem).SynId;
					Organization baseOrg =
						OrganizationsHelper.GetOrganization(baseOrgID, true);
					if (MessageBox.Show(
						    "При подтверждении выбора будет назначена основная организация" +
						    Environment.NewLine + " \"" + baseOrg.Name + "\"", "Внимание", MessageBoxButton.OKCancel,
						    MessageBoxImage.Exclamation) == MessageBoxResult.OK)
					{
						CurrentItem = (T)baseOrg;

					}
					else
					{
						return;
					}
				}
			}

			base.Ok();
	    }

		#region BtnHandlers

		public override void Add()
        {
            AddOrgsModel model =new AddOrgsModel();
            var wind = new AddUpdateOrgsView(model);
            wind.ShowDialog();

            //ViewManager.Instance.AddObject(new AddOrgsModel(), Parent);
        }

        public override void Update()
        {
            if (CurrentItem != null)
            {
                UpdateOrgsModel model = new UpdateOrgsModel(CurrentItem);
                var wind = new AddUpdateOrgsView(model);
                wind.ShowDialog();

                //ViewManager.Instance.UpdateObject(new UpdateOrgsModel(CurrentItem), Parent);
            }
        }

        public override void RightClick()
        {
            int? res = ViewManager.Instance.OpenSynonims(CurrentItem);
            if (res.HasValue)
            {
                SetAt(res.Value);
            }
        }

        #endregion

        public override bool Remove()
        {
            if ((from vis in VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                 where vis.Field<int>("f_org_id") == currentItem.Id &&
                 CommonHelper.NotDeleted(vis)
                 select vis).Any())
            {
                MessageBox.Show("Организацию невозможно удалить, т.к. она связана с посетителями!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if ((from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                 where orgs.Field<int>("f_syn_id") == currentItem.Id &&
                 CommonHelper.NotDeleted(orgs)
                 select orgs).Any())
            {
                MessageBox.Show("Перед удалением организации удалите её синонимы!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            DataRow row =
                OrganizationsWrapper.CurrentTable().Table.Rows.Find(currentItem.Id);
            row["f_deleted"] = CommonHelper.BoolToString(true);

            return true;
        }

        protected override BaseModelResult GetResult()
        {
            return new BaseModelResult { Id = CurrentItem.Id, Name = CurrentItem.FullName };
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                where orgs.Field<int>("f_org_id") != 0 &&
                CommonHelper.NotDeleted(orgs)
                select new T
                {
                    Id = orgs.Field<int>("f_org_id"),
                    Type = orgs.Field<string>("f_org_type"),
                    Name = OrganizationsHelper.UntrimName(
                        orgs.Field<string>("f_org_name")),
                    FullName = OrganizationsHelper.
                        GenerateFullName(orgs.Field<int>("f_org_id")),
                    Comment = orgs.Field<string>("f_comment"),
                    CountryId = orgs.Field<int>("f_cntr_id"),
                    Country = orgs.Field<int>("f_cntr_id") == 0 ? 
                        "" : CountriesWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_cntr_id") ==
                        orgs.Field<int>("f_cntr_id"))["f_cntr_name"].ToString(),
                    RegionId = orgs.Field<int>("f_region_id"),
                    Region = orgs.Field<int>("f_region_id") == 0 ? 
                        "" : RegionsWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_region_id") ==
                        orgs.Field<int>("f_region_id"))["f_region_name"].ToString(),
                    SynId = orgs.Field<int>("f_syn_id"),
					IsBasic = CommonHelper.StringToBool(orgs.Field<string>("f_is_basic"))
				});
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<int>("f_org_id");
        }

        public override DataRow[] Rows
        {
            get
            {
                return (from orgs in Table.AsEnumerable()
                        where orgs.Field<int>("f_org_id") != 0 select orgs).
                        AsEnumerable().ToArray();
            }
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>
            {
                { "Type", "Тип" },
                { "Name", "Название организации" },
                { "Comment", "Примечание" },
                { "FullName", "Основное название" },
                { "Country", "Страна" },
                { "Region", "Регион" },
            };
        }

        protected override DataTable Table
        {
            get
            {
                return OrganizationsWrapper.CurrentTable().Table;
            }
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>
            {
                { "Type", "f_org_type" },
                { "Name", "f_org_name" },
                { "Comment", "f_comment" },
            };
        }
    }

    public class AddChildOrganizationsListModel<T> : OrganizationsListModel<T>
        where T : Organization, new()
    {
        protected IWindow parent;

        public AddChildOrganizationsListModel(IWindow parent)
        {
            this.parent = parent;
            OnClose += Handling_OnClose; 
        }        

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                where orgs.Field<int>("f_org_id") != 0 &&
                      orgs.Field<string>("f_has_free_access").ToString().ToUpper() != "Y" &&
                   orgs.Field<int?>("f_syn_id") == 0 &&
                   orgs.Field<string>("f_is_basic").ToString().ToUpper() != "Y" &&
                   CommonHelper.NotDeleted(orgs)
                select new T
                {
                    Id = orgs.Field<int>("f_org_id"),
                    Type = orgs.Field<string>("f_org_type"),
                    Name = OrganizationsHelper.UntrimName(
                        orgs.Field<string>("f_org_name")),
                    FullName = OrganizationsHelper.
                        GenerateFullName(orgs.Field<int>("f_org_id")),
                    Comment = orgs.Field<string>("f_comment"),
                    CountryId = orgs.Field<int>("f_cntr_id"),
                    Country = orgs.Field<int>("f_cntr_id") == 0 ?
                        "" : CountriesWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_cntr_id") ==
                        orgs.Field<int>("f_cntr_id"))["f_cntr_name"].ToString(),
                    RegionId = orgs.Field<int>("f_region_id"),
                    Region = orgs.Field<int>("f_region_id") == 0 ?
                        "" : RegionsWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_region_id") ==
                        orgs.Field<int>("f_region_id"))["f_region_name"].ToString(),
                    SynId = orgs.Field<int>("f_syn_id"),
					IsBasic = CommonHelper.StringToBool(orgs.Field<string>("f_is_basic"))
				});
        }

        public override void Ok()
        {
            int currentId = CurrentItem.Id;

            OrganizationsWrapper organizations =
                OrganizationsWrapper.CurrentTable();
            DataRow row = organizations.Table.Rows.Find(currentId);
            row["f_has_free_access"] = "Y";
            Close();

            // TODO - установка CurrentItem - работает, но возможно потом лучше переделать
            Base4ViewModel<Organization> OrgViewModel = (parent as Base4ChildOrgsWindView)?.base4.DataContext as Base4ViewModel<Organization>;
            if (OrgViewModel != null)
            {
                OrgViewModel.CurrentItem = OrgViewModel.Set.FirstOrDefault(
                                                                r =>
                                                                    r.Id == currentId);
            }
        }
        
        void Handling_OnClose(object result)
        {
            var wind = Parent as IWindow;
            if (wind != null)
            {
                wind.WindowResult = result;
                (Parent as Window)?.Close();
            }            
        }
    }

    public class AddBaseOrganizationsListModel<T> : OrganizationsListModel<T>
      where T : Organization, new()
    {
        protected IWindow parent;

        public AddBaseOrganizationsListModel(IWindow parent)
        {
            this.parent = parent;
            OnClose += Handling_OnClose;
        }

        protected override void DoQuery()
        {
            Set = new ObservableCollection<T>(
                from orgs in OrganizationsWrapper.CurrentTable().Table.AsEnumerable()
                where orgs.Field<int>("f_org_id") != 0 &&
                      orgs.Field<string>("f_is_basic").ToString().ToUpper() != "Y" &&
                      orgs.Field<int?>("f_syn_id") == 0 &&
                      orgs.Field<string>("f_has_free_access").ToString().ToUpper() != "Y" &&
                      CommonHelper.NotDeleted(orgs)
                select new T
                {
                    Id = orgs.Field<int>("f_org_id"),
                    Type = orgs.Field<string>("f_org_type"),
                    Name = OrganizationsHelper.UntrimName(
                        orgs.Field<string>("f_org_name")),
                    FullName = OrganizationsHelper.
                        GenerateFullName(orgs.Field<int>("f_org_id")),
                    Comment = orgs.Field<string>("f_comment"),
                    CountryId = orgs.Field<int>("f_cntr_id"),
                    Country = orgs.Field<int>("f_cntr_id") == 0 ?
                        "" : CountriesWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_cntr_id") ==
                        orgs.Field<int>("f_cntr_id"))["f_cntr_name"].ToString(),
                    RegionId = orgs.Field<int>("f_region_id"),
                    Region = orgs.Field<int>("f_region_id") == 0 ?
                        "" : RegionsWrapper.CurrentTable()
                        .Table.AsEnumerable().FirstOrDefault(
                        arg => arg.Field<int>("f_region_id") ==
                        orgs.Field<int>("f_region_id"))["f_region_name"].ToString(),
                    SynId = orgs.Field<int>("f_syn_id"),
					IsBasic = CommonHelper.StringToBool(orgs.Field<string>("f_is_basic"))
				});
        }

        public override void Ok()
        {
            int currentId = CurrentItem.Id;

            OrganizationsWrapper organizations =
                 OrganizationsWrapper.CurrentTable();
            DataRow row = organizations.Table.Rows.Find(currentId);
            row["f_is_basic"] = "Y";
            Close();

            // TODO - установка CurrentItem - работает, но возможно потом лучше переделать
            Base4ViewModel<Organization> OrgViewModel = (parent as Base4ChildOrgsWindView)?.base4.DataContext as Base4ViewModel<Organization>;
            if (OrgViewModel != null)
            {
                OrgViewModel.CurrentItem = OrgViewModel.Set.FirstOrDefault(
                                                                r =>
                                                                    r.Id == currentId);
            }
        }
        
        void Handling_OnClose(object result)
        {
            var wind = Parent as IWindow;
            if (wind != null)
            {
                wind.WindowResult = result;
                (Parent as Window)?.Close();
            }
        }
    }
}
