using System.ComponentModel;

namespace SupRealClient.Common.Interfaces
{
    /// <summary>
    /// Интерфейс, предоставляемый окнами для управления ими
    /// </summary>
    public interface IWindow
    {
        bool IsRealClose { get; set; }

        /// <summary>
        /// Название окна
        /// </summary>
        string WindowName { get; }

        /// <summary>
        /// Родительское окно
        /// </summary>
        IWindow ParentWindow { get; set; }

        /// <summary>
        /// Закрытие окна
        /// </summary>
        /// <param name="e"></param>
        void CloseWindow(CancelEventArgs e);

        void Unsuscribe();
    }
}
