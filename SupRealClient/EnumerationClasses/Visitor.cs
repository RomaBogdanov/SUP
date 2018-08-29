using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using RegulaLib;
using SupRealClient.Models;
using SupRealClient.TabsSingleton;

namespace SupRealClient.EnumerationClasses
{
    public class Visitor : IdEntity, ICloneable
    {
		private string _сomment;
		private int organizationId;
	    private ObservableCollection<VisitorsMainDocument> _mainDocuments;

		public string FullName { get; set; }
        public string Family { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string BirthDate { get; set; }

        public int OrganizationId
        {
            get { return organizationId;}
            set
            {
                organizationId = value;
                /*DataRow row = OrganizationsWrapper.CurrentTable()
                    .Table.AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_org_id") ==
                        organizationId);*/ //["f_org_name"]; //["f_full_org_name"];;
                Organization = OrganizationsHelper.GenerateFullName(organizationId, true);
            }
        }
        public string Organization { get; set; }
	    public bool OrganizationIsBasic { get; set; } = true;
		public string Comment
		{
			get => _сomment;
			set
			{
				_сomment = value;
				int i = 0;
			}
		}
        public bool IsAccessDenied { get; set; }
        public bool IsCanHaveVisitors { get; set; }
        public bool IsNotFormular { get; set; }
        public string Telephone { get; set; }
        public int NationId { get; set; }
        public string Nation { get; set; }
        public int DocumentId { get; set; }
        public string DocType { get; set; }
        public string DocSeria { get; set; }
        public string DocNum { get; set; }
        public DateTime DocDate { get; set; }
        public string DocCode { get; set; }
        public string DocPlace { get; set; }
        public bool IsAgree { get; set; }
        public DateTime AgreeToDate { get; set; }
        public string Operator { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public bool IsRightSign { get; set; }
        public bool IsAgreement { get; set; }
        public int CabinetId { get; set; }
        public string Cabinet { get; set; }

		/// <summary>
		/// RegulaLib.CPerson
		/// </summary>
		public CPerson Person { get; set; }
		public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Card2> Cards { get; set; }
        public ObservableCollection<VisitorsMainDocument> MainDocuments { get; set; }
        public ObservableCollection<VisitorsDocument> Documents { get; set; }
		/// <summary>
		/// Список активных элементов заявок, касающихся данного посетителя
		/// </summary>
		public ObservableCollection<OrderElement> OrderElements { get; set; }



        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
