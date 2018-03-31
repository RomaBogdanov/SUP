using SupRealClient.Common.Data;
using System;

namespace SupRealClient.Models
{
    public interface IAddUpdateCardModel
    {
        event Action OnClose;
        CardData Data { get; }
        void Ok(CardData data);
        void Cancel();
        void ChangeState();
    }
}
