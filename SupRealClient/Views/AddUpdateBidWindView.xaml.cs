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
using System.Diagnostics;
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

	    private readonly List<UIElement> _enterUiElementsSequence;
	    private UIElement _previousEnterUiElement = null;

        public object WindowResult { get; set; }

        public AddUpdateBidWindView()
        {
            InitializeComponent();
	        _enterUiElementsSequence = new List<UIElement>
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

		    if (_previousEnterUiElement !=null && _previousEnterUiElement.Equals(sender))
		    {
			    UIElement newFocus = _previousEnterUiElement;
			    while (true)
			    {
				    int index = _enterUiElementsSequence.FindIndex(x => x.Equals(newFocus));
				    index = (index + 1) % _enterUiElementsSequence.Count;
					newFocus = _enterUiElementsSequence[index];
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
			    _previousEnterUiElement = (UIElement)sender;
			}
	    }
    }
}
