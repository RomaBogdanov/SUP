namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for VisitorsView.xaml
    /// </summary>
    public partial class VisitorsView
    {
        public VisitorsView()
        {
            InitializeComponent();

            DataContext = new VisitsViewModel(this);
        }
    }
}
