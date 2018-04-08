using SupRealClient.EnumerationClasses;
using System;

namespace SupRealClient.Models
{
    public interface IAddUpdateOrgsModel
    {
        event Action OnClose;
        Organization Data { get; }
        void Ok(Organization data);
        void Cancel();
    }
}
