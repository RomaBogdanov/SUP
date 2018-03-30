using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SupRealClient
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateCardView.xaml
    /// </summary>
    public partial class AddUpdateCardView : Window
    {
        public AddUpdateCardView(IAddUpdateCardModel model)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new AddUpdateCardViewModel();
            ((AddUpdateCardViewModel)DataContext).SetModel(model);
            InitializeComponent();
        }

        private void Handling_OnClose()
        {
            this.Close();
        }
    }
}
