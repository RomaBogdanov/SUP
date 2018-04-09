using System.ComponentModel;

namespace SupRealClient.Models.OrganizationStructure.Interfaces
{
    public interface IModel : INotifyPropertyChanged
    {
        string Description { get; set; }
        bool Save { get; set; }
    }
}
