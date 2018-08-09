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
    }
}
