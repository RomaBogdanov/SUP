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
using System.ComponentModel;
using  SupRealClient;
using SupRealClient.Behaviour;
using SupRealClient.Models.AddUpdateModel;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateBidWindView.xaml
    /// </summary>
    public partial class AddUpdateBidWindView
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private readonly TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

	    private List<UIElement> enterUiElementsSequence;
	    private UIElement previousEnterUiElement = null;

        public object WindowResult { get; set; }

        public AddUpdateBidWindView()
        {
            InitializeComponent();
	        enterUiElementsSequence = new List<UIElement>
	        {
		        btnSelectBid,
		        btnSelectOrganization,
		        tbPosition,
		        btnSelectCatcher,
		        dpTimeFrom,
		        dpTimeTo,
		        btnSelectPass,
		        checkDisable,
		        btnOK
	        };
		}        

        public void Handling_OnClose(object result)
        {
            WindowResult = result;
            this.Close();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnSelectBid.Focus();
        }

	    private void uiElement_PreviewKeyDown(object sender, KeyEventArgs e)
	    {
		    if (e.Key != Key.Enter)
		    {
				return;
		    }

		    if (previousEnterUiElement !=null && previousEnterUiElement.Equals(sender))
		    {
			    UIElement newFocus = previousEnterUiElement;
			    while (true)
			    {
				    int index = enterUiElementsSequence.FindIndex(x => x.Equals(newFocus));
				    index = (index + 1) % enterUiElementsSequence.Count;
					newFocus = enterUiElementsSequence[index];
				    newFocus?.Focus();

				    if (newFocus != null && !newFocus.IsEnabled)
				    {
						continue;
				    }

				    break;
			    }
			   
			    e.Handled = true;
		    }
		    else
		    {
			    previousEnterUiElement = (UIElement)sender;
			}
	    }

        private void btnSelectBid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
	            if (!Equals(previousEnterUiElement, btnSelectBid))
	            {
		            previousEnterUiElement = btnSelectBid;
	            }
	            else
	            {
		            previousEnterUiElement = null;
					btnSelectOrganization.Focus();
					e.Handled = true;
				}
            }
        }

	    private void btnSelectOrganization_PreviewKeyDown(object sender, KeyEventArgs e)
	    {
		    if (e.Key == Key.Enter)
		    {
				if (!Equals(previousEnterUiElement, btnSelectOrganization))
				{
					previousEnterUiElement = btnSelectOrganization;
				}
				else
				{
					previousEnterUiElement = null;
					btnSelectCatcher.Focus();
					e.Handled = true;
				}
			}
		}

        private void btnSelectCatcher_PreviewKeyDown(object sender, KeyEventArgs e)
        {
	        bool test = Equals((UIElement) sender, btnSelectCatcher); 
            if (e.Key == Key.Enter)
            {
				if (!Equals(previousEnterUiElement, btnSelectCatcher))
				{
					previousEnterUiElement = btnSelectCatcher;
				}
				else
				{
					previousEnterUiElement = null;
					dpTimeFrom.Focus();
					e.Handled = true;
				}
			}
        }

        private void UIdef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(_focusMover);
                    e.Handled = true;
                }
            }
        }

        private void btnSelectPass_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                checkDisable.Focus();
                e.Handled = true;
            }
        }

        private void btnOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnOK.Command.Execute(null);
            }
        }
    }
}
