using SupRealClient.Common.Data;
using System;

namespace SupRealClient.Models
{
    public interface IAddItem1Model
    {
        event Action OnClose;
        FieldData Data { get; }
        void Ok(FieldData data);
        void Cancel();
    }
}
