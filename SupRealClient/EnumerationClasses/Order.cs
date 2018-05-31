using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupRealClient.TabsSingleton;

namespace SupRealClient.EnumerationClasses
{
    public class Order : IdEntity
    {
        private int organizationId;
        private int agreeId;

        public int Number { get; set; } = 0;
        public int TypeId { get; set; } = 0;
        public string Type { get; set; } = "";
        public string RegNumber { get; set; } = "";
        public DateTime From { get; set; } = DateTime.Now;
        public DateTime To { get; set; } = DateTime.Now;
        public int CatcherId { get; set; } = 0;
        public string Catcher { get; set; } = "";
        public string OrderType { get; set; } = "";
        public string Passes { get; set; } = "";
        public int SignedId { get; set; } = 0; // Id подписывающего лица
        public string Signed { get; set; } = ""; // Полное имя подписывающего
        public int AgreeId // Id согласовавшего
        {
            get { return agreeId; }
            set
            {
                agreeId = value;
                Agree = VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                    .FirstOrDefault(arg => arg.Field<int>("f_visitor_id") == 
                    agreeId)["f_visitor_id"].ToString();
            }
        } 
        public string Agree { get; set; } = ""; // Полное имя согласовавшего
        public string Note { get; set; } = "";// Примечание
        public int OrganizationId
        {
            get { return organizationId; }
            set
            {
                organizationId = value;
                Organization = OrganizationsWrapper.CurrentTable().Table
                    .AsEnumerable().FirstOrDefault(arg => 
                    arg.Field<int>("f_org_id") == organizationId)["f_org_name"]
                    .ToString();
            }
        }
        public string Organization { get; set; } = "";


    }
}
