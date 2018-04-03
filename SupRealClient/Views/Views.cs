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

		public void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

		public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                Handling_OnClose();
            }
        }
		
        private void Handling_OnClose()
        {
            this.Hide();
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

		public void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

		public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                Handling_OnClose();
            }
        }
		
        private void Handling_OnClose()
        {
            this.Hide();
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

		public void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

		public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                Handling_OnClose();
            }
        }
		
        private void Handling_OnClose()
        {
            this.Hide();
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

		public void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

		public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                Handling_OnClose();
            }
        }
		
        private void Handling_OnClose()
        {
            this.Hide();
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

		public void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

		public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                Handling_OnClose();
            }
        }
		
        private void Handling_OnClose()
        {
            this.Hide();
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

		public void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

		public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                Handling_OnClose();
            }
        }
		
        private void Handling_OnClose()
        {
            this.Hide();
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

		public void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

		public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                Handling_OnClose();
            }
        }
		
        private void Handling_OnClose()
        {
            this.Hide();
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

		public void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

		public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                Handling_OnClose();
            }
        }
		
        private void Handling_OnClose()
        {
            this.Hide();
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

		public void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewManager.Instance.CloseWindow(this, true, e);
        }

		public void CloseWindow(CancelEventArgs e)
        {
            if (!IsRealClose)
            {
                IsRealClose = true;
                e.Cancel = true;
                Handling_OnClose();
            }
        }
		
        private void Handling_OnClose()
        {
            this.Hide();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ViewManager.Instance.SetChildrenState(sender as Window, false);
        }
    }

}
