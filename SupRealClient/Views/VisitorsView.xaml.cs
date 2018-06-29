using SupRealClient.Common.Interfaces;
using System.Windows;

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

                    parentWindowChecking();

                    //(this.DataContext as VisitsViewModel).Model.OnClose += Handling_OnClose;
                }
            };
        }

        public void NewVisitor()
        {
            ((VisitsViewModel)this.DataContext).Model= new NewVisitsModel();
        }

        private void MetroWindow_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {            
            parentWindowChecking();            
        }

        void parentWindowChecking()
        {
            if (this.DataContext != null && this.ParentWindow is SupRealClient.Views.VisitorsListWindView)
            {
                if (this.Visibility == Visibility.Visible)
                {                    
                    if ((this.ParentWindow as SupRealClient.Views.VisitorsListWindView).base4.modeEdit)
                    {
                        object currentItem = (this.ParentWindow as SupRealClient.Views.VisitorsListWindView).base4.baseTab.CurrentItem;
                        if (currentItem is EnumerationClasses.Visitor)
                        {
                            int IdSel = (currentItem as EnumerationClasses.Visitor).Id;
                            // -2, т.к Id начинается с 1, и далее вызываем команду некст, т.е. это еще +1
                            (((VisitsViewModel)this.DataContext).Model as VisitsModel).selectedIndex = IdSel - 2;
                            (this.DataContext as VisitsViewModel).NextCommand.Execute(null);
                            (this.DataContext as VisitsViewModel).EditCommand.Execute(null);                            
                        }
                            
                        (this.ParentWindow as SupRealClient.Views.VisitorsListWindView).base4.modeEdit = false;
                    }
                    else
                        (this.DataContext as VisitsViewModel).NewCommand.Execute(null);
                }
                    

                if (this.Visibility == Visibility.Hidden)
                    this.DataContext = new VisitsViewModel(this);
            }
        }
    }
}
