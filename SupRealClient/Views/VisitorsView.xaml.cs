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

            Loaded += (s, e) =>
            {
                if (DataContext == null)
                {
                    DataContext = new VisitsViewModel(this);
                }
            };
        }
    }
}
