using System.Reflection;
using System.Windows;

namespace SupRealClient.ViewModels
{
    /// <summary>
    /// ViewModel для окна "О программе".
    /// </summary>
    class AboutWindowViewModel
    {
        /// <summary>
        /// Название приложения.
        /// </summary>
        public string ApplicationName { get; set; }
        
        /// <summary>
        /// Разработчик.
        /// </summary>
        public string Developer { get; set; }

        /// <summary>
        /// Версия сборки.
        /// </summary>
        public System.Version AppVersion { get; set; }

        /// <summary>
        /// Ссылка на сайт разработчика.
        /// </summary>
        public string WebPage { get; set; }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public AboutWindowViewModel()
        {
            ApplicationName = "SUP";
            Developer = "ИП Богданов";
            AppVersion = Assembly.GetExecutingAssembly().GetName().Version;
            WebPage = "http://www.yandex.com";
        }

        private RelayCommand _goToWebPage;
        /// <summary>
        /// Переход на сайт.
        /// </summary>
        public RelayCommand GoToWebSite
        {
            get
            {
                return _goToWebPage ??
                    (_goToWebPage = new RelayCommand(async obj =>
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(WebPage);
                        }
                        catch (System.Exception ex)
                        {
                            //await InfoBox.ShowMessageAsync("Ошибка", "Не реализован переход");
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }));
            }
        }



        /*
        private static class InfoBox
        {
            public async static Task<MessageDialogResult> ShowMessageAsync(string title, 
                string Message, 
                MessageDialogStyle style = MessageDialogStyle.Affirmative, 
                MetroDialogSettings settings = null)
            {
                return await ((MetroWindow)(Application.Current.MainWindow)).ShowMessageAsync(title, Message, style, settings);
            }
        }*/
    }
}
