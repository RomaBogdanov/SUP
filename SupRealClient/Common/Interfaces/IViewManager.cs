using SupRealClient.Models;
using System.ComponentModel;
using System.Windows;

namespace SupRealClient.Common.Interfaces
{
    /// <summary>
    /// Менеджер View
    /// </summary>
    public interface IViewManager
    {
        /// <summary>
        /// Добавить поле в таблицу
        /// </summary>
        /// <param name="model"></param>
        /// <param name="parent"></param>
        void Add(IAddItem1Model model, IWindow parent);

        /// <summary>
        /// Добавить поле в таблицу
        /// </summary>
        /// <param name="model"></param>
        /// <param name="parent"></param>
        void AddObject(object model, IWindow parent);

        /// <summary>
        /// Редактировать поле таблицы
        /// </summary>
        /// <param name="model"></param>
        /// <param name="parent"></param>
        void Update(IAddItem1Model model, IWindow parent);

        /// <summary>
        /// Редактировать поле таблицы
        /// </summary>
        /// <param name="model"></param>
        /// <param name="parent"></param>
        void UpdateObject(object model, IWindow parent);

        /// <summary>
        /// Поиск в таблице
        /// </summary>
        /// <param name="searchHelper"></param>
        /// <param name="parent"></param>
        void Search(ISearchHelper searchHelper, IWindow parent);

        /// <summary>
        /// Открыть окно
        /// </summary>
        /// <param name="name"></param>
        void OpenWindow(string name, IWindow parent = null);

        /// <summary>
        /// Открыть окно
        /// </summary>
        /// <param name="name"></param>
        object OpenWindowModal(string name, IWindow parent = null);

        /// <summary>
        /// Открыть окно
        /// </summary>
        void OpenWindow(IWindow window, IWindow parent);

        /// <summary>
        /// Закрыть окно
        /// </summary>
        /// <param name="window"></param>
        /// <param name="withChildren"></param>
        void CloseWindow(IWindow window, bool withChildren, CancelEventArgs e);

        /// <summary>
        /// Минимизировать/раскрыть дочерние окна
        /// </summary>
        /// <param name="window"></param>
        void SetChildrenState(Window window, bool isMain);

        /// <summary>
        /// Выход из приложения
        /// </summary>
        void ExitApp();
    }
}
