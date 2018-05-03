﻿using System.Linq;
using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Organizations1View.xaml
    /// </summary>
    public partial class Base2View : UserControl
    {
        Base1ViewModel viewModel = new Base1ViewModel();

        public Base2View()
        {
            DataContext = viewModel;
        }

        public void SetViewModel(Base1ModelAbstr model)
        {
            ((Base1ViewModel)DataContext).SetModel(model);
            InitializeComponent();
            tbxSearch.Focus();
        }

        public DataGrid BaseTab
        {
            get { return baseTab; }
            set
            {
                baseTab = value;
                baseTab.Focus();
            }
        }

        public void SetDefaultColumn()
        {
            if (baseTab.Columns.Count > 0)
            {
                baseTab.CurrentColumn = baseTab.Columns[0];
            }
        }

        private void BaseTab_OnKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Down)
            {
                SelectSearchBox();
            }
        }

        public void SelectSearchBox()
        {
            tbxSearch.Focus();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key== Key.Up)
            {
                btnUp.Command.Execute(null);
            }
            else if (e.Key==Key.Down)
            {
                btnDown.Command.Execute(null);
            }
            /*else if(e.Key==Key.Enter)
            {
                btnEdit.Command.Execute(null);
            }*/
        }
    }
}
