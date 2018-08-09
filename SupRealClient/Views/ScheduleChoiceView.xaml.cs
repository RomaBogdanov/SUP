using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using SupRealClient.EnumerationClasses;
using DataGrid = System.Windows.Controls.DataGrid;

namespace SupRealClient.Views
{
	/// <summary>
	/// Логика взаимодействия для ScheduleChoiceView.xaml
	/// </summary>
	public partial class ScheduleChoiceView
	{
		public List<CAreaSchedule> areaSchedulesList { get; set; }

		public ScheduleChoiceView(IEnumerable<CAreaSchedule> areaSchedulesList)
		{
			InitializeComponent();
			DataGrid.ItemsSource = areaSchedulesList;

		}
		private void OkButton_OnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void CancelButton_OnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}
