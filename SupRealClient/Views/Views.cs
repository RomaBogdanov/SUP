using SupRealClient.Common.Interfaces;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

/*
ФАЙЛ СГЕНЕРИРОВАН АВТОМАТИЧЕСКИ
ИЗМЕНЕНИЯ НЕ ВНОСИТЬ!!!!!
*/

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddItem1View.xaml - базовая часть для всех View
    /// </summary>
    public partial class AddItem1View : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "AddItem1View";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для AddUpdateCardView.xaml - базовая часть для всех View
    /// </summary>
    public partial class AddUpdateCardView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "AddUpdateCardView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для AddUpdateOrgsView.xaml - базовая часть для всех View
    /// </summary>
    public partial class AddUpdateOrgsView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "AddUpdateOrgsView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для CardsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class CardsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "CardsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для DocumentsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class DocumentsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "DocumentsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для NationsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class NationsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "NationsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для OrganizationsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class OrganizationsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "OrganizationsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Search1View.xaml - базовая часть для всех View
    /// </summary>
    public partial class Search1View : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Search1View";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для ZonesWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class ZonesWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "ZonesWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для CabinetsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class CabinetsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "CabinetsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для LogsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class LogsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "LogsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для MainOrganisationStructureView.xaml - базовая часть для всех View
    /// </summary>
    public partial class MainOrganisationStructureView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "MainOrganisationStructureView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для BaseOrgsView.xaml - базовая часть для всех View
    /// </summary>
    public partial class BaseOrgsView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "BaseOrgsView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для ChildOrgsView.xaml - базовая часть для всех View
    /// </summary>
    public partial class ChildOrgsView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "ChildOrgsView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для VisitorsListWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class VisitorsListWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "VisitorsListWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для VisitorsView.xaml - базовая часть для всех View
    /// </summary>
    public partial class VisitorsView : IWindow
    {
        public bool CanMinimize { get; private set; } = false;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "VisitorsView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для AddUpdateCabinetView.xaml - базовая часть для всех View
    /// </summary>
    public partial class AddUpdateCabinetView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "AddUpdateCabinetView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для VisitorsDocumentView.xaml - базовая часть для всех View
    /// </summary>
    public partial class VisitorsDocumentView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "VisitorsDocumentView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для DocumentImagesView.xaml - базовая часть для всех View
    /// </summary>
    public partial class DocumentImagesView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "DocumentImagesView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для VisitorsMainDocumentView.xaml - базовая часть для всех View
    /// </summary>
    public partial class VisitorsMainDocumentView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "VisitorsMainDocumentView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для FullNameView.xaml - базовая часть для всех View
    /// </summary>
    public partial class FullNameView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "FullNameView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для AddUpdateRegionView.xaml - базовая часть для всех View
    /// </summary>
    public partial class AddUpdateRegionView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "AddUpdateRegionView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для SynonimView.xaml - базовая часть для всех View
    /// </summary>
    public partial class SynonimView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "SynonimView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для BidsView.xaml - базовая часть для всех View
    /// </summary>
    public partial class BidsView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "BidsView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CreateColumns();
        }

        public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                this.Hide();
            }
        }

        public void Unsuscribe()
        {
            this.Closing -= this.Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

        private void Handling_OnClose()
        {
            this.Close();
        }

        private void Handling_OnClose(object result = null)
        {
            WindowResult = result;
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefaultColumn();
        }

        partial void CreateColumns();

        partial void SetDefaultColumn();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

}
