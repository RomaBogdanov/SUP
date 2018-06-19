using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
using SupRealClient.Annotations;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Views.Visitor
{
    /// <summary>
    /// Interaction logic for AddZone.xaml
    /// </summary>
    public partial class AddZone
    {
        public AddZone()
        {
            InitializeComponent();
        }
    }
    /*
    public class AddZoneViewModel : INotifyPropertyChanged
    {
        private AddZoneModel model;

        public AddZoneModel Model
        {
            get { return model; }
            set
            {
                model = value;
                
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }*/

    public class AddZoneModel : Base4ModelAbstr<Order>
    {
        //private int visitorId;

        public AddZoneModel(ObservableCollection<Order> orders)
        {
            //visitorId = visitor_id;
            //Query();
            Set = orders;
        }

        protected override DataTable Table
        {
            get { return OrderElementsWrapper.CurrentTable().Table; }
        }

        public override void Add()
        {
            ViewManager.Instance.OpenWindow("Base4CardsWindView", null);
        }

        public override void Update()
        {
            
        }

        protected override void DoQuery()
        {
            /*Set = new ObservableCollection<OrderElement>((
                (ObservableCollection<OrderElement>) OrderElementsWrapper
                .CurrentTable().StandartQuery())
                .Where(arg => arg.VisitorId == visitorId));*/
        }

        protected override BaseModelResult GetResult()
        {
            throw new NotImplementedException();
        }
    }
}
