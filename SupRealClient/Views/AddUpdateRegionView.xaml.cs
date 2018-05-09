using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateOrgsView.xaml
    /// </summary>
    public partial class AddUpdateRegionView
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        public AddUpdateRegionView(IAddUpdateRegionModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddUpdateRegionViewModel();
            ((AddUpdateRegionViewModel)DataContext).SetModel(model);
            InitializeComponent();

            AfterInitialize();
            NameTextBox.Focus();
        }

        /// <summary>
        /// Конструктор - заглушка
        /// </summary>
        public AddUpdateRegionView()
        {
            InitializeComponent();
            NameTextBox.Focus();
            DataContext = new AddUpdateOrgsViewModel();
        }

        private void TextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((UIElement)sender).MoveFocus(_focusMover);
            }
        }
    }
}
