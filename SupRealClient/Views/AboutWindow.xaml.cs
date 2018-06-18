

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow
    {
        public AboutWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.AboutWindowViewModel();
        }
    }
}
