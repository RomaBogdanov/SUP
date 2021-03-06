﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4DocumentsWindView.xaml
    /// </summary>
    public partial class Base4DocumentsWindView
    {
        public Base4DocumentsWindView(Visibility okVisibility)
        {
            InitializeComponent();
            base4.tbxSearch.Focus();
            AfterInitialize();
                        
            ((Base4ViewModel<EnumerationClasses.Document>)base4.DataContext)
                .OkVisibility = okVisibility;
            ((Base4ViewModel<EnumerationClasses.Document>)base4.DataContext)
                .ScrollCurrentItem = base4.ScrollIntoViewCurrentItem;
            base4.btnRemove.Visibility = Visibility.Collapsed; // Скрываем кнопку "Удалить".
            base4.Focus();
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("DocName")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
        }

        private void SetDefaultColumn()
        {
            base4.baseTab.CurrentColumn = base4.baseTab.Columns[0];
        }
    }
}
