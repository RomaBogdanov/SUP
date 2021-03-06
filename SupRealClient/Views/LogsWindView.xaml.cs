﻿using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для LogsWindView.xaml
    /// </summary>
    public partial class LogsWindView : Window
    {
        Base1ViewModel viewModel = new Base1ViewModel();

        public LogsWindView()
        {
            DataContext = viewModel;
            Base1ModelAbstr b = new Base1LogsModel(
                (Base1ViewModel)this.DataContext, this);
            b.OnClose += Handling_OnClose;
            this.SetViewModel(b);
            AfterInitialize();
        }

        public DataGrid BaseTab
        {
            get { return baseTab; }
            set
            {
                baseTab = value;
            }
        }

        private void SetViewModel(Base1ModelAbstr model)
        {
            ((Base1ViewModel)DataContext).SetModel(model);
            InitializeComponent();
        }

        partial void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Дата",
                Binding = new Binding("RecDate")
            };
            this.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Уровень",
                Binding = new Binding("Severity")
            };
            this.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Сообщение",
                Binding = new Binding("Message")
            };
            this.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Машина",
                Binding = new Binding("Machine")
            };
            this.BaseTab.Columns.Add(dataGridTextColumn);
        }

        partial void SetDefaultColumn()
        {
            this.BaseTab.CurrentColumn = this.BaseTab.Columns[1];
        }
    }
}
