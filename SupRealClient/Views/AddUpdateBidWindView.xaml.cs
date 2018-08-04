﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SupRealClient.Views
{
	/// <summary>
	/// Логика взаимодействия для AddUpdateBidWindView.xaml
	/// </summary>
	public partial class AddUpdateBidWindView
    {
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

	    private void UiElement_PreviewKeyDown(object sender, KeyEventArgs e)
	    {
		    if (e.Key != Key.Enter)
		    {
				return;
		    }

		    bool isSenderButton = sender is Button;


			if (_previousEnterUiElement !=null && (!isSenderButton || _previousEnterUiElement.Equals(sender)))
		    {
			    if (!isSenderButton)
			    {
				    _previousEnterUiElement = (UIElement) sender;
			    }
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

	    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
	    {
		    if (e.Key == Key.Escape)
		    {
			    Close();
			    e.Handled = true;
		    }
	    }
    }
}
