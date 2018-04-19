using SupRealClient.ViewModels;
using System.Windows.Controls;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Authorize1View.xaml
    /// </summary>
    public partial class Authorize1View : UserControl
    {
        public Authorize1View()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            (this.DataContext as Authorize1ViewModel).Reset();
        }
    }
}
