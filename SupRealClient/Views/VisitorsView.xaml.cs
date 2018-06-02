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
                if (this.DataContext == null)
                {
                    this.DataContext = new VisitsViewModel(this);

                    this.Height = (this.DataContext as VisitsViewModel).WinSet.VisitorsHeight;
                    this.Width = (this.DataContext as VisitsViewModel).WinSet.VisitorsWidth;
                }
            };
        }
    }
}
