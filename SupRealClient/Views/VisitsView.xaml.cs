using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SupRealClient.Models;

namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for VisitsView.xaml
    /// </summary>
    public partial class VisitsView
    {
        public VisitsView()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is Visit)
            {
                var visit = (Visit) sender;

                visit.ToString();
            }
        }
    }

    public class VisitsViewModel
    {
        public bool Enable
        { get; set; }

        public string Family
        { get; set; }

        public string Name
        { get; set; }

        public string Patronymic
        { get; set; }

        public string Organization
        { get; set; }

        public string Comment
        { get; set; }

        public bool IsAccessDenied
        { get; set; }

        public bool IsCanHaveVisitors
        { get; set; }

        public bool IsNotFormular
        { get; set; }

        // TODO: Сделать вставки под фотки
        // ================================

        // ================================

        public string Telephone
        { get; set; }

        public string Nation
        { get; set; }

        public string DocType
        { get; set; }

        public string DocSeria
        { get; set; }

        public string DocNum
        { get; set; }

        public DateTime DocDate
        { get; set; }

        public string DocPlace
        { get; set; }

        public bool IsAgree
        { get; set; }

        public DateTime AgreeToDate
        { get; set; }

        public string Operator
        { get; set; }

        //TODO: Сделать вкладки по пропуску
        // ================================

        // ================================

        //TODO: Сделать вкладки по заявкам
        // ================================

        // ================================


        public string Department
        { get; set; }

        public string DepartmentUnit
        { get; set; }

        /// <summary>
        /// TODO: Посмотреть, что можно сделать.
        /// </summary>
        public string DepartmentUnitUnit
        { get; set; }

        public string Position
        { get; set; }

        public bool IsRightSign
        { get; set; }

        public bool IsAgreement
        { get; set; }

        public string Cabinet
        { get; set; }
    }
}
