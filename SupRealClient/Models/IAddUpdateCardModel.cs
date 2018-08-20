using SupRealClient.EnumerationClasses;
using System;

namespace SupRealClient.Models
{
    public interface IAddUpdateCardModel
    {
        event Action OnClose;
        Card Data { get; }
        void Ok(Card data);
        void Cancel();
        int? ChangeState();
    }
}
