﻿using SupRealClient.Common.Interfaces;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

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

}
