﻿using System;
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

        public object WindowResult { get; set; }

        public AddUpdateBidWindView()
        {
            InitializeComponent();
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

        private void btnSelectBid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
	            btnSelectOrganization.Focus();
                e.Handled = true;
            }
        }

	    private void btnSelectOrganization_PreviewKeyDown(object sender, KeyEventArgs e)
	    {
		    if (e.Key == Key.Enter)
		    {
			    btnSelectCatcher.Focus();
			    e.Handled = true;
		    }
		}

        private void btnSelectCatcher_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                dpTimeFrom.Focus();
                e.Handled = true;
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
