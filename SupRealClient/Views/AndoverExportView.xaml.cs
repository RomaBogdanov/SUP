using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SupRealClient.Views
{
	/// <summary>
	/// Логика взаимодействия для AndoverExportView.xaml
	/// </summary>
	public partial class AndoverExportView 
	{
		public AndoverExportView()
		{
			InitializeComponent();
			
		}

		public EAndoverExportItem AndoverExportItem => (EAndoverExportItem) StackPanel.Children.Cast<CheckBox>().Select((item, index) => item.IsChecked.Value ? 1 << index : 0).Sum();

		private void OkClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void CancelClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}

	[Flags]
	public enum EAndoverExportItem
	{
		None = 0,
		Doors = 1,
		Areas = 2,
		Personnel = 4,
		Schedules = 8
	}
}
