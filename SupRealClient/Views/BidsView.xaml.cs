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

namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for BidsView.xaml
    /// </summary>
    public partial class BidsView
    {
        public BidsView()
        {
            InitializeComponent();
            this.DataContext = new BidsViewModel(); // Контекст данных.
            this.Height = (this.DataContext as BidsViewModel).Settings.Height;
            this.Width = (this.DataContext as BidsViewModel).Settings.Width;
            this.Left = (this.DataContext as BidsViewModel).Settings.Left;
            this.Top = (this.DataContext as BidsViewModel).Settings.Top;
        }

        private void BidsView_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }    
}
