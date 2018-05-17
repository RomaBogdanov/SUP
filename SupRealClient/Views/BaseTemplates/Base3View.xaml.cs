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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SupRealClient.ViewModels;
using SupRealClient.Models;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.Views
{    
    /// <summary>
    /// Логика взаимодействия для Base3View.xaml
    /// </summary>
    public partial class Base3View : UserControl
    {
        Base1ViewModel viewModel = new Base3ViewModel();

        public Base3View()
        {
            DataContext = viewModel;
        }

        public void Init()
        {
            InitializeComponent();
        }

        public void SetViewModel(Base3ModelAbstr model)
        {
            ((Base3ViewModel)DataContext).SetModel(model);
            InitializeComponent();
            tbxSearch.Focus();
        }

        public DataGrid BaseTab
        {
            get { return baseTab; }
            set
            {
                baseTab = value;
                BaseTab.Focus();
            }
        }

        public void SetDefaultColumn()
        {
            if (baseTab.Columns.Count > 0)
            {
                baseTab.CurrentColumn = baseTab.Columns[0];
            }
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up & !BaseTab.IsKeyboardFocusWithin)
            {
                btnUp.Command.Execute(null);
                e.Handled = true;
            }
            else if (e.Key == Key.Down & !BaseTab.IsKeyboardFocusWithin)
            {
                btnDown.Command.Execute(null);
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                btnUpdate.Command.Execute(null);
            }
            else if (e.Key == Key.Insert)
            {
                ((ISuperBaseViewModel)DataContext).Add.Execute(null);
            }
            else if (e.Key == Key.Home)
            {
                ((ISuperBaseViewModel)DataContext).Begin.Execute(null);
            }
            else if (e.Key == Key.End)
            {
                ((ISuperBaseViewModel)DataContext).End.Execute(null);
            }
        }
    }
}
