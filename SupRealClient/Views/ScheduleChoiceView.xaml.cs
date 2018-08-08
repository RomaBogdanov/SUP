using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace SupRealClient.Views
{
	/// <summary>
	/// Логика взаимодействия для ScheduleChoiceView.xaml
	/// </summary>
	public partial class ScheduleChoiceView
	{
		public string SelectedSchedule { get; private set; }

		public ScheduleChoiceView(List<string> schedulesList)
		{
			InitializeComponent();
			View.ItemsSource = schedulesList;

		}

		private void OkButton_OnClick(object sender, RoutedEventArgs e)
		{
			SelectedSchedule = View.SelectedItem.ToString();
			Close();
		}

		private void CancelButton_OnClick(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
