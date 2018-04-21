using SupRealClient.Common.Interfaces;
using System;
using System.ComponentModel;
using System.Windows;

namespace SupRealClient.Views
{
	/// <summary>
	/// Логика взаимодействия для AddItem1View.xaml - базовая часть для всех View
	/// </summary>
	public partial class AddItem1View : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "AddItem1View";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для AddUpdateCardView.xaml - базовая часть для всех View
	/// </summary>
	public partial class AddUpdateCardView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "AddUpdateCardView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для AddUpdateOrgsView.xaml - базовая часть для всех View
	/// </summary>
	public partial class AddUpdateOrgsView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "AddUpdateOrgsView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для CardsWindView.xaml - базовая часть для всех View
	/// </summary>
	public partial class CardsWindView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "CardsWindView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для DocumentsWindView.xaml - базовая часть для всех View
	/// </summary>
	public partial class DocumentsWindView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "DocumentsWindView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для NationsWindView.xaml - базовая часть для всех View
	/// </summary>
	public partial class NationsWindView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "NationsWindView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для OrganizationsWindView.xaml - базовая часть для всех View
	/// </summary>
	public partial class OrganizationsWindView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "OrganizationsWindView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для Search1View.xaml - базовая часть для всех View
	/// </summary>
	public partial class Search1View : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "Search1View";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для ZonesWindView.xaml - базовая часть для всех View
	/// </summary>
	public partial class ZonesWindView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "ZonesWindView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для CabinetsWindView.xaml - базовая часть для всех View
	/// </summary>
	public partial class CabinetsWindView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "CabinetsWindView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для LogsWindView.xaml - базовая часть для всех View
	/// </summary>
	public partial class LogsWindView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "LogsWindView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для MainOrganisationStructureView.xaml - базовая часть для всех View
	/// </summary>
	public partial class MainOrganisationStructureView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "MainOrganisationStructureView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для BaseOrgsView.xaml - базовая часть для всех View
	/// </summary>
	public partial class BaseOrgsView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "BaseOrgsView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для ChildOrgsView.xaml - базовая часть для всех View
	/// </summary>
	public partial class ChildOrgsView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "ChildOrgsView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для VisitorsListWindView.xaml - базовая часть для всех View
	/// </summary>
	public partial class VisitorsListWindView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "VisitorsListWindView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

	/// <summary>
	/// Логика взаимодействия для UploadImageView.xaml - базовая часть для всех View
	/// </summary>
	public partial class UploadImageView : IWindow
	{
        public bool IsRealClose { get; set; } = true;

        public string WindowName { get; private set; } = "UploadImageView";

        public IWindow ParentWindow { get; set; }

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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

}
