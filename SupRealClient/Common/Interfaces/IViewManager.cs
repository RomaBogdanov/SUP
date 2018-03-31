using SupRealClient.Models;

namespace SupRealClient.Common.Interfaces
{
    /// <summary>
    /// Менеджер View
    /// </summary>
    public interface IViewManager
    {
        void Add(IAddItem1Model model);
        void AddObject(object model);
        void Update(IAddItem1Model model);
        void UpdateObject(object model);
        void Search(ISearchHelper searchHelper);
    }
}
