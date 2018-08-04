using System;
using System.Windows.Controls;
using System.Windows.Input;

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
			comboMenu.SelectionChanged += ComboBox_SelectionChanged; // Подписываемся на событие выбора вкладки.
		}

		private void MetroWindow_Initialized(object sender, EventArgs e)
		{
			this.DataContext = new ViewModels.BidsViewModel() {BidsModel = new Models.BidsModel()}; // Контекст данных.
			this.Height = (this.DataContext as ViewModels.BidsViewModel).WinSet.Height;
			this.Width = (this.DataContext as ViewModels.BidsViewModel).WinSet.Width;
			this.Left = (this.DataContext as ViewModels.BidsViewModel).WinSet.Left;
			this.Top = (this.DataContext as ViewModels.BidsViewModel).WinSet.Top;
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			mainTabControl.SelectedIndex = comboMenu.SelectedIndex;
		}

		/// <summary>
		/// Закрытие окна по нажатию Esc
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				Close();
				e.Handled = true;
			}
		}
	}
}