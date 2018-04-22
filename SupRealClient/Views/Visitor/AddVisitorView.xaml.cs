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

namespace SupRealClient.Views.Visitor
{
    /// <summary>
    /// Interaction logic for AddVisitor.xaml
    /// </summary>
    public partial class AddVisitorView
    {
        public AddVisitorView()
        {
            InitializeComponent();
        }
    }

    public class AddVisitorsViewModel
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
    }
}
