using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using SupRealClient.TabsSingleton;
using SupRealClient.ViewModels;

namespace SupRealClient.Models
{
    public class BidsModel : BidsModelBase
    {
        public override event Action OnRefresh;

        public BidsModel()
        {
            IsAddUpdVisib = Visibility.Hidden;
            Query();
        }

        private void Query()
        {
            OrdersSet = (ObservableCollection<Order>) OrdersWrapper.CurrentTable().StandartQuery();

            SingleOrdersSet = new ObservableCollection<Order>(OrdersSet.Where(
                arg => arg.Type == "Разовая"));
            if (SingleOrdersSet.Count > 0)
            {
                CurrentSingleOrder = SingleOrdersSet[0];
            }
            TemporaryOrdersSet = new ObservableCollection<Order>(OrdersSet.Where(
                arg => arg.Type == "Временная"));
            if (TemporaryOrdersSet.Count > 0)
            {
                CurrentTemporaryOrder = TemporaryOrdersSet[0];
            }
            VirtueOrdersSet = new ObservableCollection<Order>(OrdersSet.Where(
                arg => arg.Type == "На основании"));
            if (VirtueOrdersSet.Count > 0)
            {
                CurrentVirtueOrder = VirtueOrdersSet[0];
            }

        }
    }
}