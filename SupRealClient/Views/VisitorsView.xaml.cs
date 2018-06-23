using SupRealClient.Common.Interfaces;

namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for VisitorsView.xaml
    /// </summary>
    public partial class VisitorsView
    {
        private static VisitorsView visitorsView;

        public static VisitorsView Instance
        {
            get
            {
                if (visitorsView == null)
                {
                    visitorsView = new VisitorsView();
                }

                return visitorsView;
            }
        }

        public VisitorsView(bool isNew = false)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                if (this.DataContext == null)
                {
                    this.DataContext = new VisitsViewModel(this);

                    this.Height = (this.DataContext as VisitsViewModel).WinSet.Height;
                    this.Width = (this.DataContext as VisitsViewModel).WinSet.Width;
                    this.Left = (this.DataContext as VisitsViewModel).WinSet.Left;
                    this.Top = (this.DataContext as VisitsViewModel).WinSet.Top;

                    //(this.DataContext as VisitsViewModel).Model.OnClose += Handling_OnClose;
                }
            };
        }

        public void NewVisitor()
        {
            ((VisitsViewModel)this.DataContext).Model= new NewVisitsModel();
        }
    }
}
