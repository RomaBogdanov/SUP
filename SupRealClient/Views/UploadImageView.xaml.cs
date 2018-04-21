using SupRealClient.ViewModels;
using System.Windows;

namespace SupRealClient.Views
{
    /// <summary>
    /// Interaction logic for UploadImageView.xaml
    /// </summary>
    public partial class UploadImageView : Window
    {
        public UploadImageView()
        {
            InitializeComponent();

            DataContext = new UploadImageViewModel();
        }
    }
}
