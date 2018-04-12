using SupRealClient.Common.Interfaces;
using SupRealClient.Models;
using SupRealClient.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SupRealClient
{
    // TODO - подумать, как реализовать более грамотно
    // TODO - можно завести словать из связок: ViewModel -> Window, передавать интерфейс или сам ViewModel через generic и открывать нужное окно основываясь на словаре

    public class ViewManager : IViewManager
    {
        private static IViewManager viewManager;

        private IDictionary<string, IWindow> windows =
            new Dictionary<string, IWindow>();

        public static IViewManager Instance
        {
            get { return viewManager = viewManager ?? new ViewManager(); }
        }

        /// <summary>
        /// Добавить поле в таблицу
        /// </summary>
        /// <param name="model"></param>
        public void Add(IAddItem1Model model, IWindow parent)
        {
            ReopenWindow("AddItem1View." + model.ToString(),
                new AddItem1View(model), parent);
        }

        /// <summary>
        /// Добавить поле в таблицу
        /// </summary>
        /// <param name="model"></param>
        /// TODO - Пока криво - потом переделать
        public void AddObject(object model, IWindow parent)
        {
            if (model is AddOrgsModel)
            {
                ReopenWindow("AddOrgsView",
                    new AddUpdateOrgsView(model as AddOrgsModel), parent);
            }
            else if (model is AddCardModel)
            {
                ReopenWindow("AddCardView",
                    new AddUpdateCardView(model as AddCardModel), parent);
            }
        }

        /// <summary>
        /// Редактировать поле таблицы
        /// </summary>
        /// <param name="model"></param>
        public void Update(IAddItem1Model model, IWindow parent)
        {
            ReopenWindow("UpdateItem1View." + model.ToString(),
                new AddItem1View(model), parent);
        }

        /// <summary>
        /// Редактировать поле таблицы
        /// </summary>
        /// <param name="model"></param>
        /// TODO - Пока криво - потом переделать
        public void UpdateObject(object model, IWindow parent)
        {
            if (model is UpdateOrgsModel)
            {
                ReopenWindow("UpdateOrgsView",
                    new AddUpdateOrgsView(model as UpdateOrgsModel), parent);
            }
            else if (model is UpdateCardModel)
            {
                ReopenWindow("UpdateCardView",
                    new AddUpdateCardView(model as UpdateCardModel), parent);
            }
        }

        /// <summary>
        /// Поиск в таблице
        /// </summary>
        /// <param name="searchHelper"></param>
        public void Search(ISearchHelper searchHelper, IWindow parent)
        {
            ReopenWindow("Search1View." + searchHelper.ToString(),
                new Search1View(searchHelper), parent);
        }

        /// <summary>
        /// Открыть окно
        /// </summary>
        /// <param name="name"></param>
        public void OpenWindow(string name)
        {
            IWindow window = windows.ContainsKey(name) ?
                windows[name] : CreateWindow(name);
            if (window == null)
            {
                return;
            }
            if (!windows.ContainsKey(name))
            {
                windows[name] = window;
            }
            OpenWindow(window);
        }

        /// <summary>
        /// Закрыть окно
        /// </summary>
        /// <param name="window"></param>
        /// <param name="withChildren"></param>
        public void CloseWindow(IWindow window, bool withChildren,
            CancelEventArgs e)
        {
            // Перед закрытием окна, закрываем все его дочерние окна рекурсивно
            if (withChildren)
            {
                foreach (var wnd in windows.Values.
                    Where(w => w.ParentWindow == window))
                {
                    CloseWindow(wnd, withChildren, e);
                }
            }
            window.CloseWindow(e);
        }

        /// <summary>
        /// Минимизировать/раскрыть дочерние окна
        /// </summary>
        /// <param name="window"></param>
        public void SetChildrenState(Window window, bool isMain)
        {
            if (window == null)
            {
                return;
            }
            if (window.WindowState != WindowState.Minimized &&
                window.WindowState != WindowState.Normal)
            {
                return;
            }
            if (isMain)
            {
                foreach (var wnd in windows.Values)
                {
                    (wnd as Window).WindowState = window.WindowState;
                }
            }
            else
            {
                foreach (var wnd in windows.Values.
                    Where(w => w.ParentWindow == window))
                {
                    SetChildrenState(wnd as Window, isMain);
                    (wnd as Window).WindowState = window.WindowState;
                }
            }
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        public void ExitApp()
        {
            // Закрываем все окна при выходе
            for (int i = windows.Values.Count - 1; i >= 0; i--)
            {
                Close(windows.Values.ElementAt(i));
            }
        }

        private void ReopenWindow(string name, IWindow window, IWindow parent)
        {
            if (window == null)
            {
                return;
            }
            if (windows.ContainsKey(name))
            {
                Hide(windows[name]);
                windows.Remove(name);
            }
            window.ParentWindow = parent;
            windows[name] = window;
            OpenWindow(window);
        }

        private void OpenWindow(IWindow window)
        {
            if (window == null)
            {
                return;
            }
            if (window.IsRealClose)
            {
                window.IsRealClose = false;
                (window as Window).Show();
            }
            (window as Window).Activate();
        }

        private IWindow CreateWindow(string name)
        {
            switch (name)
            {
                case "DocumentsWindView":
                    return new DocumentsWindView();
                case "NationsWindView":
                    return new NationsWindView();
                case "CardsWindView":
                    return new CardsWindView();
                case "OrganizationsWindView":
                    return new OrganizationsWindView();
                case "ZonesWindView":
                    return new ZonesWindView();
                case "CabinetsWindView":
                    return new CabinetsWindView();
                case "LogsWindView":
                    return new LogsWindView();
                case "MainOrganisationStructureView":
                    return new MainOrganisationStructureView();
                case "ChildOrgsView":
                    return new ChildOrgsView();
                case "BaseOrgsView":
                    return new BaseOrgsView();
            }

            return null;
        }

        private void Hide(IWindow window)
        {
            (window as Window).Close();
            window.IsRealClose = true;
        }

        private void Close(IWindow window)
        {
            window.Unsuscribe();
            (window as Window).Close();
            window.IsRealClose = true;
        }
    }
}
