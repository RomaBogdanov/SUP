using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SUPClient
{
    public class FullOrder
    {
        private event Action OrderChange;

        public DataTable VisitorsTab { get; set; }
        public DataTable OrganizationsTab { get; set; }
        public DataTable OrdersTab { get; set; }
        public DataTable OrderElementsTab { get; set; }

        public DataRow Order { get; set; }
        public DataRow PersonSigned { get; set; }
        public DataRow OrganizationSigned { get; set; }
        public DataRow PersonAdjusted { get; set; }
        public DataRow OrganizationAdjusted { get; set; }
        public DataRow OrderElements { get; set; }
        public DataRow Visitor { get; set; }
        public DataRow VisitorOrganization { get; set; }

        public string OrderID
        {
            get { return this.Order["f_ord_id"].ToString(); }
            set
            {
                this.Order["f_ord_id"] = value;
            }
        }

        public DateTime OrderDate
        {
            get { return (DateTime)Order["f_ord_date"]; }
            set
            {
                this.Order["f_ord_date"] = value;
            }
        }

        public string ReceiveOrganization
        {
            get { return (string)OrganizationSigned["f_full_org_name"]; }
            set
            {
                if ((string)OrganizationSigned["f_full_org_name"] != value && value != "")
                {
                    var a = from b in OrganizationsTab.AsEnumerable()
                            where b.Field<string>("f_full_org_name") == value
                            select b;
                    if (a.Count() > 0)
                    {
                        this.OrganizationSigned = a.ElementAt(0);
                        this.PersonSigned["f_org_id"] = a.ElementAt(0)["f_org_id"];
                        this.OrderChange();
                    }
                    else
                    {
                        this.OrganizationSigned["f_full_org_name"] = value;
                    }
                }
            }
        }

        public string ReceivePerson
        {
            get { return (string)PersonSigned["f_full_name"]; }
            set
            {
                if ((string)this.PersonSigned["f_full_name"] != value && value != "")
                {
                    var a = from b in VisitorsTab.AsEnumerable()
                            where b.Field<string>("f_full_name") == value
                            select b;
                    if (a.Count() > 0)
                    {
                        this.PersonSigned = a.ElementAt(0);
                        this.Order["f_signed_by"] = a.ElementAt(0)["f_visitor_id"];
                        this.OrderChange();
                    }
                    else
                    {
                        this.PersonSigned["f_full_name"] = value;
                    }
                }
            }
        }

        public string ReceivePersonTelephone
        {
            get { return (string)PersonSigned["f_phones"]; }
            set
            {
                this.PersonSigned["f_phones"] = value;
            }
        }

        public string AdjustPerson
        {
            get { return (string)PersonAdjusted["f_full_name"]; }
            set
            {
                if ((string)this.PersonAdjusted["f_full_name"] != value && value != "")
                {
                    var a = from b in VisitorsTab.AsEnumerable()
                            where b.Field<string>("f_full_name") == value
                            select b;
                    if (a.Count() > 0)
                    {
                        this.PersonAdjusted = a.ElementAt(0);
                        this.Order["f_adjusted_with"] = a.ElementAt(0)["f_visitor_id"];
                        this.OrderChange();
                    }
                    else
                    {
                        this.PersonAdjusted["f_full_name"] = value;
                    }
                }
            }
        }

        public string AdjustPersonTelephone
        {
            get { return (string)PersonAdjusted["f_phones"]; }
            set
            {
                this.PersonAdjusted["f_phones"] = value;
            }
        }

        public string Pass
        {
            get { return (string)OrderElements["f_passes"]; }
            set
            {
                this.OrderElements["f_passes"] = value;
            }
        }

        public DateTime From
        {
            get { return (DateTime)Order["f_date_from"]; }
            set
            {
                Order["f_date_from"] = value;
            }
        }

        public DateTime To
        {
            get { return (DateTime)Order["f_date_to"]; }
            set
            {
                Order["f_date_to"] = value;
            }
        }

        public string FullName
        {
            get { return (string)Visitor["f_full_name"]; }
            set { Visitor["f_full_name"] = value; }
        }

        public string Family
        {
            get { return (string)Visitor["f_family"]; }
            set
            {
                if ((string)this.Visitor["f_full_name"] != value && value != "")
                {
                    var a = from b in VisitorsTab.AsEnumerable()
                            where b.Field<string>("f_full_name") == value
                            select b;
                    if (a.Count() > 0)
                    {
                        this.Visitor = a.ElementAt(0);
                        this.OrderElements["f_visitor_id"] = a.ElementAt(0)["f_visitor_id"];
                        this.OrderChange();
                    }
                    else
                    {
                        Visitor["f_family"] = value;
                    }
                }
            }
        }

        public string FirstName
        {
            get { return (string)Visitor["f_fst_name"]; }
            set
            {
                Visitor["f_fst_name"] = value;
            }
        }

        public string SecondName
        {
            get { return (string)Visitor["f_sec_name"]; }
            set
            {
                Visitor["f_sec_name"] = value;
            }
        }

        public string Organization
        {
            get { return (string)VisitorOrganization["f_full_org_name"]; }
            set
            {
                if ((string)VisitorOrganization["f_full_org_name"] != value && value != "")
                {
                    var a = from b in OrganizationsTab.AsEnumerable()
                            where b.Field<string>("f_full_org_name") == value
                            select b;
                    if (a.Count() > 0)
                    {
                        this.VisitorOrganization = a.ElementAt(0);
                        this.Visitor["f_org_id"] = a.ElementAt(0)["f_org_id"];
                        this.OrderChange();
                    }
                    else
                    {
                        VisitorOrganization["f_full_org_name"] = value;
                    }
                }
            }
        }

        public string Job
        {
            get { return (string)Visitor["f_job"]; }
            set
            {
                Visitor["f_job"] = value;
            }
        }

        public string DocSeria
        {
            get { return (string)Visitor["f_doc_seria"]; }
            set
            {
                Visitor["f_doc_seria"] = value;
            }
        }

        public string DocNumber
        {
            get { return (string)Visitor["f_doc_num"]; }
            set
            {
                Visitor["f_doc_num"] = value;
            }
        }

        public string Phone
        {
            get { return (string)Visitor["f_phones"]; }
            set
            {
                Visitor["f_phones"] = value;
            }
        }

        public string Status
        {
            get { return (string)Order["f_notes"]; }
            set
            {
                Order["f_notes"] = value;
            }
        }

        public FullOrder(Action action)
        {
            this.OrderChange = action;
        }

    }
}
