using SupRealClient.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        }        

        public List<TabControlItems> ListTabs { get; set; }

        private void MetroWindow_Initialized(object sender, EventArgs e)
        {
            this.DataContext = new ViewModels.BidsViewModel() { BidsModel = new Models.BidsModel() }; // Контекст данных.
            this.Height = (this.DataContext as ViewModels.BidsViewModel).WinSet.Height;
            this.Width = (this.DataContext as ViewModels.BidsViewModel).WinSet.Width;
            this.Left = (this.DataContext as ViewModels.BidsViewModel).WinSet.Left;
            this.Top = (this.DataContext as ViewModels.BidsViewModel).WinSet.Top;

            ListTabs = new List<TabControlItems>();
            ListTabs.Add(new TabControlItems() { ID = 1, Name = "Разовые заявки" });
            ListTabs.Add(new TabControlItems() { ID = 2, Name = "Временные заявки" });
            ListTabs.Add(new TabControlItems() { ID = 3, Name = "Заявки на основании" });
            ListTabs.Add(new TabControlItems() { ID = 4, Name = "Список заявок" });

            //listBoxTabs.ItemsSource = ListTabs;
        }

        private void btnTabSelect_Click(object sender, RoutedEventArgs e)
        {
            object clickedButton = (sender as Button).Tag;
            mainTabControl.SelectedIndex = int.Parse(clickedButton.ToString());
        }
    }

    public class TabControlItems
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
