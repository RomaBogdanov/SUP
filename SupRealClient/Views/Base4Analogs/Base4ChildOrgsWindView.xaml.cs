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

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4ChildOrgsWindView.xaml
    /// </summary>
    public partial class Base4ChildOrgsWindView
    {
        public Base4ChildOrgsWindView()
        {
            InitializeComponent();
            base4.tbxSearch.Focus();
            AfterInitialize();
            ((Base4ViewModel<EnumerationClasses.Organization>)base4.DataContext)
                .OkVisibility = Visibility.Hidden;
            ((Base4ViewModel<EnumerationClasses.Organization>)base4.DataContext)
                .ScrollCurrentItem = base4.ScrollIntoViewCurrentItem;
            base4.btnUpdate.Visibility = Visibility.Collapsed; // Скрыть кнопку "Правка".
            ChangeCommandKeyGestureCtrlD();
            
            base4.Focus();
        }

        void ChangeCommandKeyGestureCtrlD()
        {
            foreach (InputBinding inputBinding in (base4 as UserControl)?.InputBindings)
            {
                KeyGesture keyGesture = inputBinding.Gesture as KeyGesture;
                if (keyGesture != null && keyGesture.Key == Key.D && keyGesture.Modifiers == ModifierKeys.Control)
                {
                    inputBinding.Command = ((Base4ViewModel<EnumerationClasses.Organization>)base4.DataContext).Remove;
                    break;
                }
            }
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Тип",
                Binding = new Binding("Type")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("Name")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Страна",
                Binding = new Binding("Country")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Регион",
                Binding = new Binding("Region")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            //base4.btnUpdate.Content = "Удалить";

        }
        private void SetDefaultColumn()
        {
            base4.baseTab.CurrentColumn = base4.baseTab.Columns[0];
        }

    }
}
