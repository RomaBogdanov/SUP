using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for AssigningZonesView.xaml
    /// </summary>
    public partial class AssigningZonesView
    {
        public AssigningZonesView()
        {
            InitializeComponent();
            AfterInitialize();
        }

	    private void DataGridAreasRight_OnMouseDown(object sender, MouseButtonEventArgs e)
	    {
			HitTestResult result = VisualTreeHelper.HitTest(this, e.GetPosition(this));
		    if (result.VisualHit.GetType() != typeof(DataGridRow))
		    {
			    DataGridAreasRight.UnselectAllCells();
			    e.Handled = true;
		    }

		}

	    private void DataGridAreasLeft_OnMouseDown(object sender, MouseButtonEventArgs e)
	    {
			HitTestResult result = VisualTreeHelper.HitTest(this, e.GetPosition(this));
		    if (result.VisualHit.GetType() != typeof(DataGridRow))
		    {
				DataGridAreasLeft.UnselectAllCells();
			    e.Handled = true;
		    }

		}

	    private void DataGridTemplateLeft_OnMouseDown(object sender, MouseButtonEventArgs e)
	    {
			HitTestResult result = VisualTreeHelper.HitTest(this, e.GetPosition(this));
		    if (result.VisualHit.GetType() != typeof(DataGridRow))
		    {
				DataGridTemplateLeft.UnselectAllCells();
			    e.Handled = true;
		    }

		}

	    private void DataGridTemplateRight_OnMouseDown(object sender, MouseButtonEventArgs e)
	    {
		    HitTestResult result = VisualTreeHelper.HitTest(this, e.GetPosition(this));
		    if (result.VisualHit.GetType() != typeof(DataGridRow))
		    {
				DataGridTemplateRight.UnselectAllCells();
			    e.Handled = true;
		    }
		}

		/// <summary>
		/// Обработка нажатия клавиш в окне
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				Close();
				e.Handled = true;
			} else if (e.Key == Key.Space)
			{
				ExecuteDataGridInputCommand(DataGridAreasLeft);
				ExecuteDataGridInputCommand(DataGridAreasRight);
				ExecuteDataGridInputCommand(DataGridTemplateLeft);
				ExecuteDataGridInputCommand(DataGridTemplateRight);
				e.Handled = true;
			}
			else if (e.Key == Key.Down)
			{
				if (DataGridTemplateLeft.SelectedItem != null && DataGridTemplateLeft.SelectedIndex == (DataGridTemplateLeft.Items.Count - 1) &&
				    DataGridAreasLeft.Items.Count > 0)
				{
					DataGridAreasLeft.SelectedIndex = 0;
					SelectRow((DataGridRow)DataGridAreasLeft.ItemContainerGenerator.ContainerFromIndex(DataGridAreasLeft.SelectedIndex));
				} else if (DataGridTemplateRight.SelectedItem != null && 
				           DataGridTemplateRight.SelectedIndex == (DataGridTemplateRight.Items.Count - 1) &&
				           DataGridAreasRight.Items.Count > 0)
				{
					DataGridAreasRight.SelectedIndex = 0;
					SelectRow((DataGridRow)DataGridAreasRight.ItemContainerGenerator.ContainerFromIndex(DataGridAreasRight.SelectedIndex));
				}
				else
				{
					SelectNext(DataGridAreasLeft);
					SelectNext(DataGridAreasRight);
					SelectNext(DataGridTemplateLeft);
					SelectNext(DataGridTemplateRight);
				}
			} else if (e.Key == Key.Up)
			{
				if (DataGridAreasLeft.SelectedItem != null && 
				    DataGridAreasLeft.SelectedIndex == 0 && DataGridTemplateLeft.Items.Count > 0)
				{
					DataGridTemplateLeft.SelectedIndex = DataGridTemplateLeft.Items.Count -1;
					SelectRow((DataGridRow)DataGridTemplateLeft.ItemContainerGenerator.ContainerFromIndex(DataGridTemplateLeft.SelectedIndex));
				} else if (DataGridAreasRight.SelectedItem != null &&
				           DataGridAreasRight.SelectedIndex == 0 && DataGridTemplateRight.Items.Count > 0)
				{
					DataGridTemplateRight.SelectedIndex = DataGridTemplateRight.Items.Count - 1;
					SelectRow((DataGridRow)DataGridTemplateRight.ItemContainerGenerator.ContainerFromIndex(DataGridTemplateRight.SelectedIndex));
				}
				else
				{
					SelectPrevious(DataGridAreasLeft);
					SelectPrevious(DataGridAreasRight);
					SelectPrevious(DataGridTemplateLeft);
					SelectPrevious(DataGridTemplateRight);
				}
			}

			else if (e.Key == Key.Left)
			{
				if (DataGridAreasRight.SelectedItem != null && DataGridAreasLeft.Items.Count >0)
				{
					DataGridAreasLeft.SelectedIndex =0;
					SelectRow((DataGridRow)DataGridAreasLeft.ItemContainerGenerator.ContainerFromIndex(DataGridAreasLeft.SelectedIndex));
				}
				if (DataGridTemplateRight.SelectedItem != null && DataGridTemplateLeft.Items.Count > 0)
				{
					DataGridTemplateLeft.SelectedIndex = 0;
					SelectRow((DataGridRow)DataGridTemplateLeft.ItemContainerGenerator.ContainerFromIndex(DataGridTemplateLeft.SelectedIndex));
				}
			}
			else if (e.Key == Key.Right)
			{
				if (DataGridAreasLeft.SelectedItem != null && DataGridAreasRight.Items.Count > 0)
				{
					DataGridAreasRight.SelectedIndex = 0;
					SelectRow((DataGridRow)DataGridAreasRight.ItemContainerGenerator.ContainerFromIndex(DataGridAreasRight.SelectedIndex));
				}
				if (DataGridTemplateLeft.SelectedItem != null && DataGridTemplateRight.Items.Count > 0)
				{
					DataGridTemplateRight.SelectedIndex = 0;
					SelectRow((DataGridRow)DataGridTemplateRight.ItemContainerGenerator.ContainerFromIndex(DataGridTemplateRight.SelectedIndex));
				}
			}
		}

	    private void SelectRow(DataGridRow selectedRow)
	    {
		    FocusManager.SetIsFocusScope(selectedRow, true);
		    FocusManager.SetFocusedElement(selectedRow, selectedRow);
		}

	    private bool SelectNext(DataGrid dataGrid)
	    {
		    if (dataGrid.SelectedItem != null)
		    {
			    dataGrid.SelectedIndex = dataGrid.SelectedIndex < (dataGrid.Items.Count - 1) ? dataGrid.SelectedIndex + 1 : dataGrid.SelectedIndex;
				SelectRow((DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex));
			    return true;
		    }

		    return false;
	    }

	    private bool SelectPrevious(DataGrid dataGrid)
	    {
		    if (dataGrid.SelectedItem != null)
		    {
			    dataGrid.SelectedIndex = dataGrid.SelectedIndex > 0 ? dataGrid.SelectedIndex - 1 : dataGrid.SelectedIndex;
				SelectRow((DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex));
				return true;
		    }

		    return false;
	    }

	    private void ExecuteDataGridInputCommand(DataGrid dataGrid)
	    {
		    if (dataGrid.SelectedItem != null)
		    {
			    dataGrid.InputBindings[0].Command.Execute(dataGrid.SelectedItem);
		    }
		}

	    private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
	    {
		    if (sender is DataGrid dataGrid)
		    {
			    if (dataGrid.SelectedItem == null)
			    {
					return;
			    }
		    }

			if (sender == null) return;

			if (!sender.Equals(DataGridAreasLeft))
		    {
			    DataGridAreasLeft.UnselectAllCells();
			}
		    if (!sender.Equals(DataGridAreasRight))
		    {
			    DataGridAreasRight.UnselectAllCells();
			}
		    if (!sender.Equals(DataGridTemplateLeft))
		    {
			    DataGridTemplateLeft.UnselectAllCells();
			}
		    if (!sender.Equals(DataGridTemplateRight))
		    {
			    DataGridTemplateRight.UnselectAllCells();
			}
		}
    }
}
