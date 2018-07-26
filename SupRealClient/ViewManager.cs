using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using SupRealClient.TabsSingleton;
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

        private static Authorize1View authorizeView;

        public static IViewManager Instance
        {
            get { return viewManager = viewManager ?? new ViewManager(); }
        }

        public static Authorize1View AuthorizeView
        {
            get { return authorizeView ?? (authorizeView = new Authorize1View()); }
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
            else if (model is AddItemRegionsModel)
            {
                ReopenWindow("AddRegionView",
                    new AddUpdateRegionView(model as AddItemRegionsModel), parent);
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
            else if (model is UpdateItemRegionsModel)
            {
                ReopenWindow("UpdateRegionView",
                    new AddUpdateRegionView(model as UpdateItemRegionsModel), parent);
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
        public void OpenWindow(string name, IWindow parent = null)
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
            window.ParentWindow = parent;
            OpenWindow(window);
        }

        /// <summary>
        /// Открыть окно
        /// </summary>
        /// <param name="name"></param>
        public object OpenWindowModal(string name, IWindow parent = null)
        {
            IWindow window = windows.ContainsKey(name) ?
                windows[name] : CreateWindow(name);
            if (window == null)
            {
                return null;
            }
            if (!windows.ContainsKey(name))
            {
                windows[name] = window;
            }
            window.ParentWindow = parent;
            return OpenWindowModal(window);
        }

        /// <summary>
        /// Открыть окно
        /// </summary>
        public void OpenWindow(IWindow window, IWindow parent)
        {
            ReopenWindow(window.WindowName, window, parent);
        }

        public object OpenRegions(int countryId)
        {
            var window = new Base4RegionsWindView(Visibility.Visible);
            window.SetCountry(countryId);
            return OpenWindowModal(window);
        }

        public int? OpenSynonims(Organization organization)
        {
            var window = new SynonimView(organization);
            window.ShowDialog();
            return window.WindowResult as int?;
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
                    if (wnd.CanMinimize)
                    {
                        (wnd as Window).WindowState = window.WindowState;
                    }
                }
            }
            else
            {
                foreach (var wnd in windows.Values.
                    Where(w => w.ParentWindow == window))
                {
                    SetChildrenState(wnd as Window, isMain);
                    if (wnd.CanMinimize)
                    {
                        (wnd as Window).WindowState = window.WindowState;
                    }
                }
            }
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        public void ExitApp()
        {
            TableWrapper.Reset();
            //InputProvider.GetInputProvider().Dispose();
            // Закрываем все окна при выходе
            for (int i = windows.Values.Count - 1; i >= 0; i--)
            {
                Close(windows.Values.ElementAt(i));
            }
            windows.Clear();
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

                //Открытие окна в модальном режиме
                if (window.GetType().ToString().Contains("AddUpdateOrgsView") ||
                    window.GetType().ToString().Contains("OrganizationsWindView"))
                    (window as Window).ShowDialog();
                else
                    (window as Window).Show();
            }
            (window as Window).Activate();
        }

        private object OpenWindowModal(IWindow window)
        {
            if (window != null && window.IsRealClose)
            {
                window.IsRealClose = false;
                (window as Window).ShowDialog();
                return window.WindowResult;
            }
            return null;
        }

        private IWindow CreateWindow(string name)
        {
            switch (name)
            {
                case nameof(DocumentsWindView):
                    return new DocumentsWindView();
                case "NationsWindView":
                    return new NationsWindView();
                case "CardsWindView":
                    return new CardsWindView();
                case "Base4CardsWindView":
                    return new Base4CardsWindView();
                case "OrganizationsWindView":
                    return new OrganizationsWindView();
                case "Base4ZonesWindView":
                    return new Base4ZonesWindView();
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
                case "VisitorsView":
                    return new VisitorsView();
                case "VisitorsViewNew":
                    return new VisitorsView(true);
                case @"VisitorsListWindView":
                    return new VisitorsListWindView(Visibility.Hidden);
                case @"VisitorsListWindViewOk":
                    return new VisitorsListWindView(Visibility.Visible);
                case "Base4CabinetsWindView":
                    return new Base4CabinetsWindView(Visibility.Hidden);
                case "Base4CabinetsWindViewOk":
                    return new Base4CabinetsWindView(Visibility.Visible);
                case "Base4DocumentsWindView":
                    return new Base4DocumentsWindView(Visibility.Hidden);
                case "Base4DocumentsWindViewOk":
                    return new Base4DocumentsWindView(Visibility.Visible);
                case "Base4NationsWindView":
                    return new Base4NationsWindView(Visibility.Hidden);
                case "Base4NationsWindViewOk":
                    return new Base4NationsWindView(Visibility.Visible);
                case "Base4OrganizationsWindView":
                    return new Base4OrganizationsWindView();
                case "Base4OrganizationsLargeWindView":
                    return new Base4OrganizationsLargeWindView();
                //Base4OrganizationsLargeWindView
                case "Base4RegionsWindView":
                    return new Base4RegionsWindView(Visibility.Hidden);
                case "Base4BaseOrgsWindView":
                    return new Base4BaseOrgsWindView();
                case "Base4ChildOrgsWindView":
                    return new Base4ChildOrgsWindView();
                case "Base4SpacesWindView":
                    return new Base4SpacesWindView();
                case "Base4DoorsWindView":
                    return new Base4DoorsWindView();
                case "Base4AreasWindView":
                    return new Base4AreasWindView();
                case "Base4AreasSpacesWindView":
                    return new Base4AreasSpacesWindView();
                case "Base4AccessPointsWindView":
                    return new Base4AccessPointsWindView();
                case "Base4KeysWindView":
                    return new Base4KeysWindView();
                case "Base4KeyCasesWindView":
                    return new Base4KeyCasesWindView();
                case "Base4KeyHoldersWindView":
                    return new Base4KeyHoldersWindView();
                case "Base4SchedulesWindView":
                    return new Base4SchedulesWindView();
                case "Base4AccessLevelsWindView":
                    return new Base4AccessLevelsWindView();
                case "Base4CarsWindView":
                    return new Base4CarsWindView();
                case "Base4EquipmentsWindView":
                    return new Base4EquipmentsWindView();
                case "BidsView":
                    return new BidsView(); // Окно "Заявки".
                default:
                    break;
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
