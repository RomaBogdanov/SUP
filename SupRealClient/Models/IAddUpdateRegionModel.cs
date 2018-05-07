using SupRealClient.EnumerationClasses;
using System;

namespace SupRealClient.Models
{
    public interface IAddUpdateRegionModel
    {
        event Action OnClose;
        Region Data { get; }
        void Ok(Region data);
        void Cancel();
    }
}
