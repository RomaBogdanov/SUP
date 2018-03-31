using SupRealClient.Common.Data;
using System;

namespace SupRealClient.Models
{
    public interface IAddUpdateOrgsModel
    {
        event Action OnClose;
        OrganizationData Data { get; }
        void Ok(OrganizationData data);
        void Cancel();
    }
}
