using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SupRealClient.EnumerationClasses
{
    public class Visitor : IdEntity, ICloneable
    {
        public string FullName { get; set; }
        public string Family { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Organization { get; set; }
        public string Comment { get; set; }
        public bool IsAccessDenied { get; set; }
        public bool IsCanHaveVisitors { get; set; }
        public bool IsNotFormular { get; set; }
        public string Telephone { get; set; }
        public string Nation { get; set; }
        public string DocType { get; set; }
        public string DocSeria { get; set; }
        public string DocNum { get; set; }
        public DateTime DocDate { get; set; }
        public string DocPlace { get; set; }
        public bool IsAgree { get; set; }
        public DateTime AgreeToDate { get; set; }
        public string Operator { get; set; }
        public string Department { get; set; }
        public string DepartmentUnit { get; set; }
        public string DepartmentUnitUnit { get; set; }
        public string Position { get; set; }
        public bool IsRightSign { get; set; }
        public bool IsAgreement { get; set; }
        public string Cabinet { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Card2> Cards { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
