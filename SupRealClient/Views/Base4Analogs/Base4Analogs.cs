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
    /// Логика взаимодействия для Base4CabinetsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4CabinetsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4CabinetsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Cabinet> viewModel =
            new Base4ViewModel<EnumerationClasses.Cabinet>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Visible,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new CabinetsListModel<EnumerationClasses.Cabinet>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4DocumentsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4DocumentsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4DocumentsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Document> viewModel =
            new Base4ViewModel<EnumerationClasses.Document>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new DocumentsListModel<EnumerationClasses.Document>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4NationsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4NationsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4NationsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Nation> viewModel =
            new Base4ViewModel<EnumerationClasses.Nation>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new NationsListModel<EnumerationClasses.Nation>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4OrganizationsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4OrganizationsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4OrganizationsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Organization> viewModel =
            new Base4ViewModel<EnumerationClasses.Organization>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new OrganizationsListModel<EnumerationClasses.Organization>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4RegionsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4RegionsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4RegionsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Region> viewModel =
            new Base4ViewModel<EnumerationClasses.Region>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new RegionsListModel<EnumerationClasses.Region>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4ZonesWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4ZonesWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4ZonesWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Zone> viewModel =
            new Base4ViewModel<EnumerationClasses.Zone>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Visible,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new ZonesListModel<EnumerationClasses.Zone>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4BaseOrgsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4BaseOrgsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4BaseOrgsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Organization> viewModel =
            new Base4ViewModel<EnumerationClasses.Organization>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new BaseOrganizationsListModel<EnumerationClasses.Organization>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4ChildOrgsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4ChildOrgsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4ChildOrgsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Organization> viewModel =
            new Base4ViewModel<EnumerationClasses.Organization>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new ChildOrganizationsListModel<EnumerationClasses.Organization>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4CardsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4CardsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4CardsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Card> viewModel =
            new Base4ViewModel<EnumerationClasses.Card>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new CardsListModel<EnumerationClasses.Card>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4OrganizationsLargeWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4OrganizationsLargeWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4OrganizationsLargeWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Organization> viewModel =
            new Base4ViewModel<EnumerationClasses.Organization>
            {
                OkCaption = "OK",
                OkVisibility = Visibility.Hidden,
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new OrganizationsListModel<EnumerationClasses.Organization>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            viewModel.ScrollCurrentItem = base4.ScrollIntoViewCurrentItem;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4SpacesWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4SpacesWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4SpacesWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Space> viewModel =
            new Base4ViewModel<EnumerationClasses.Space>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new SpacesListModel<EnumerationClasses.Space>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4DoorsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4DoorsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4DoorsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Door> viewModel =
            new Base4ViewModel<EnumerationClasses.Door>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new DoorsListModel<EnumerationClasses.Door>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4AreasWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4AreasWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4AreasWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Area> viewModel =
            new Base4ViewModel<EnumerationClasses.Area>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new AreasListModel<EnumerationClasses.Area>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4AreasSpacesWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4AreasSpacesWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4AreasSpacesWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.AreaSpace> viewModel =
            new Base4ViewModel<EnumerationClasses.AreaSpace>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new AreaSpacesListModel<EnumerationClasses.AreaSpace>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4AccessPointsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4AccessPointsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4AccessPointsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.AccessPoint> viewModel =
            new Base4ViewModel<EnumerationClasses.AccessPoint>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new AccessPointsListModel<EnumerationClasses.AccessPoint>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4KeysWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4KeysWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4KeysWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.RealKey> viewModel =
            new Base4ViewModel<EnumerationClasses.RealKey>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new RealKeysListModel<EnumerationClasses.RealKey>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4SchedulesWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4SchedulesWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4SchedulesWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Schedule> viewModel =
            new Base4ViewModel<EnumerationClasses.Schedule>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new SchedulesListModel<EnumerationClasses.Schedule>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4AccessLevelsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4AccessLevelsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4AccessLevelsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.AccessLevel> viewModel =
            new Base4ViewModel<EnumerationClasses.AccessLevel>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new AccessLevelsListModel<EnumerationClasses.AccessLevel>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4CarsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4CarsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4CarsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Car> viewModel =
            new Base4ViewModel<EnumerationClasses.Car>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new CarsListModel<EnumerationClasses.Car>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4EquipmentsWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4EquipmentsWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4EquipmentsWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.Equipment> viewModel =
            new Base4ViewModel<EnumerationClasses.Equipment>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new EquipmentsListModel<EnumerationClasses.Equipment>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4KeyCasesWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4KeyCasesWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4KeyCasesWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.KeyCase> viewModel =
            new Base4ViewModel<EnumerationClasses.KeyCase>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new KeyCasesListModel<EnumerationClasses.KeyCase>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для Base4KeyHoldersWindView.xaml - базовая часть для всех View
    /// </summary>
    public partial class Base4KeyHoldersWindView : IWindow
    {
        public bool CanMinimize { get; private set; } = true;

        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Base4KeyHoldersWindView";

        public IWindow ParentWindow { get; set; }

        public object WindowResult { get; set; }

        public void AfterInitialize()
        {
            this.Closing += Window_Closing;
            this.StateChanged += Window_StateChanged;
            this.Loaded += Window_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Base4ViewModel<EnumerationClasses.KeyHolder> viewModel =
            new Base4ViewModel<EnumerationClasses.KeyHolder>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
				WatchVisibility = Visibility.Hidden,
                Parent = this,
                Model = new KeyHoldersListModel<EnumerationClasses.KeyHolder>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base4.DataContext = viewModel;

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

        private void Handling_OnClose(object result)
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
				
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }

}
