using System;
using SupRealClient.Common.Interfaces;
using System.Windows;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for VisitorsView.xaml
    /// </summary>
    public partial class VisitorsView
    {
        private static VisitorsView visitorsView;
	    private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

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
	                DataContext = new VisitsViewModel(this);
					(DataContext as VisitsViewModel).MoveNextFocusingElement += MovingNextFocusingElement;
					this.Height = (this.DataContext as VisitsViewModel).WinSet.Height;
                    this.Width = (this.DataContext as VisitsViewModel).WinSet.Width;
                    this.Left = (this.DataContext as VisitsViewModel).WinSet.Left;
                    this.Top = (this.DataContext as VisitsViewModel).WinSet.Top;

                    parentWindowChecking();

                    //(this.DataContext as VisitsViewModel).Model.OnClose += Handling_OnClose;
                }
            };
        }

	    private void MovingNextFocusingElement(string e)
	    {
		    if (e == "OrganizationsList")
		    {
			    checkBoxNoForm.Focus();
		    }
	    }

	    public void NewVisitor()
        {
            ((VisitsViewModel)this.DataContext).Model= new NewVisitsModel();
        }

        private void MetroWindow_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {            
            parentWindowChecking();            
        }

        private void MetroWindow_Activated(object sender, System.EventArgs e)
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
                        if (currentItem is EnumerationClasses.Visitor && butEdit.IsEnabled)
                        {
                            int IdSel = (currentItem as EnumerationClasses.Visitor).Id;
                            // -2, т.к Id начинается с 1, и далее вызываем команду некст, т.е. это еще +1
                            (((VisitsViewModel)this.DataContext).Model as VisitsModel).selectedIndex = IdSel - 2;
                            (this.DataContext as VisitsViewModel).NextCommand.Execute(null);
                            (this.DataContext as VisitsViewModel).EditCommand.Execute(null);                            
                        }
                            
                        (this.ParentWindow as SupRealClient.Views.VisitorsListWindView).base4.modeEdit = false;
                    }
                    else if (butNew.IsEnabled)
                        (this.DataContext as VisitsViewModel).NewCommand.Execute(null);
                }
                    

                if (this.Visibility == Visibility.Hidden)
                    this.DataContext = new VisitsViewModel(this);
            }
        }

		private void MoveNextFocusControl(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				((UIElement)sender).MoveFocus(_focusMover);
				e.Handled = true;
			}
		}

	    private void MoveNextFocusControl_ToName(object sender, KeyEventArgs e)
	    {
		    if (e.Key == Key.Enter)
		    {
			    MoveNextFocusControl_ToName((sender as FrameworkElement).Name);
		    }
	    }

	    private void MoveNextFocusControl_ToName(string nameControl)
	    {
			    switch (nameControl)
			    {
				    case "textbox_Patronymic":
				    {
					    button_LoadOrganization.Focus();
				    }
					    break;
				    case "checkBoxNoForm":
				    {
					    checkBox_Consent.Focus();
				    }
					    break;

				    case "checkBox_Consent":
				    {
					    button_Ok.Focus();
				    }
					    break;
					default:
						break;
			    }
	    }
	}
}
