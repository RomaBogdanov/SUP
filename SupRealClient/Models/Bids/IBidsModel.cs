using System;
using System.Collections.ObjectModel;
using System.Windows;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.ViewModels;
using SupRealClient.ViewModels.AddUpdateViewModel;
using SupRealClient.Views;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SupRealClient.Models
{
    public interface IBidsModel
    {
        /// <summary>
        /// Срабатывает при обновлении данных.
        /// </summary>
        event Action OnRefresh;

        ObservableCollection<Order> SingleOrdersSet { get; set; }
        ObservableCollection<Order> TemporaryOrdersSet { get; set; }
        ObservableCollection<Order> VirtueOrdersSet { get; set; }
        ObservableCollection<Order> OrdersSet { get; set; }
        Order CurrentSingleOrder { get; set; }
        Order CurrentTemporaryOrder { get; set; }
        Order CurrentVirtueOrder { get; set; }
        Order CurrentOrder { get; set; }
        OrderType OrderType { get; set; }
        bool IsCanAddRows { get; set; }
        Visibility IsAddUpdVisib { get; set; }
        OrderElement UpdateVisitor { get; set; }

        void AddPerson();


        /// <summary>
        /// Обработка нажатия кнопки "Начало"
        /// </summary>
        void Begin();
        void Cancel();
        void Delay();
        void Edit();
        void End();
        void Further();
        void New();
        void Next();
        /// <summary>
        /// Обрабатывает нажатие кнопки Ок.
        /// </summary>
        void Ok();
        void Prev();
        void Reload();
        void Search();
        void Agreer();
        void Signer();
        //void SignerTemp();
        void UpdatePerson();
        void DeletePerson();
    }

    public abstract class BidsModelBase : IBidsModel
    {
        /// <summary>
        /// Данное поле необходимо для храниния информации о максимальном номере
        /// заявки, которая есть сейчас, чтобы создавать новую.
        /// </summary>
        protected static int maxOrderNumber;

        public ObservableCollection<Order> SingleOrdersSet { get; set; }
        public ObservableCollection<Order> TemporaryOrdersSet { get; set; }
        public ObservableCollection<Order> VirtueOrdersSet { get; set; }
        public ObservableCollection<Order> OrdersSet { get; set; }
        public Order CurrentSingleOrder { get; set; }
        public Order CurrentTemporaryOrder { get; set; }
        public Order CurrentVirtueOrder { get; set; }
        public Order CurrentOrder { get; set; }
        public OrderType OrderType { get; set; }

        public bool IsCanAddRows { get; set; }

        public Visibility IsAddUpdVisib { get; set; }
        public OrderElement UpdateVisitor { get; set; }

        public virtual event Action OnRefresh;

        /// <summary>
        /// Обработка нажатия кнопки "Начало".
        /// </summary>
        public void Begin()
        {
            switch (OrderType)
            {
                case OrderType.Temp:
                {
                    if (TemporaryOrdersSet.Count > 0)
                    {
                        CurrentTemporaryOrder = TemporaryOrdersSet[0];
                    }
                    break;
                }
                case OrderType.Single:
                {
                    if (SingleOrdersSet.Count > 0)
                    {
                        CurrentSingleOrder = SingleOrdersSet[0];
                    }

                    break;
                }

                default:
                    throw new Exception("Unexpected Case");
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки "Предыдущий".
        /// </summary>
        public void Prev()
        {
            switch (OrderType)
            {
                case OrderType.Temp:
                {
                    if (TemporaryOrdersSet.Count > 0 && TemporaryOrdersSet
                            .IndexOf(CurrentTemporaryOrder) > 0)
                    {
                        CurrentTemporaryOrder = TemporaryOrdersSet[
                            TemporaryOrdersSet.IndexOf(CurrentTemporaryOrder) - 1];
                    }
                    break;
                }
                case OrderType.Single:
                {
                    if (SingleOrdersSet.Count > 0 &&
                        SingleOrdersSet.IndexOf(CurrentSingleOrder) > 0)
                    {
                        CurrentSingleOrder = SingleOrdersSet[
                            SingleOrdersSet.IndexOf(CurrentSingleOrder) - 1];
                    }

                    break;
                }

                default:
                    throw new Exception("Unexpected Case");
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки "Следующий".
        /// </summary>
        public void Next()
        {
            switch (OrderType)
            {
                case OrderType.Temp:
                {
                    if (TemporaryOrdersSet.Count > 0 && TemporaryOrdersSet
                            .IndexOf(CurrentTemporaryOrder) <
                        TemporaryOrdersSet.Count - 1)
                    {
                        CurrentTemporaryOrder = TemporaryOrdersSet[
                            TemporaryOrdersSet.IndexOf(CurrentTemporaryOrder) + 1];
                    }
                    break;
                }
                case OrderType.Single:
                {
                    if (SingleOrdersSet.Count > 0 &&
                        SingleOrdersSet.IndexOf(CurrentSingleOrder) <
                        SingleOrdersSet.Count - 1)
                    {
                        CurrentSingleOrder = SingleOrdersSet[
                            SingleOrdersSet.IndexOf(CurrentSingleOrder) + 1];
                    }

                    break;
                }

                default:
                    throw new Exception("Unexpected Case");
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки "Конец".
        /// </summary>
        public void End()
        {
            switch (OrderType)
            {
                case OrderType.Temp:
                {
                    if (TemporaryOrdersSet.Count > 0)
                    {
                        CurrentTemporaryOrder = TemporaryOrdersSet[
                            TemporaryOrdersSet.Count - 1];
                    }
                    break;
                }
                case OrderType.Single:
                {
                    if (SingleOrdersSet.Count > 0)
                    {
                        CurrentSingleOrder = SingleOrdersSet[
                            SingleOrdersSet.Count - 1];
                    }
                    break;
                }

                default:
                    throw new Exception("Unexpected Case");
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки "Далее".
        /// </summary>
        public void Further()
        {
            //todo: обработать команду
        }

        /// <summary>
        /// Обработка нажатия кнопки "Отложить".
        /// </summary>
        public void Delay()
        {
            //todo: обработать команду
        }

        /// <summary>
        /// Обработка нажатия кнопки "Новый".
        /// </summary>
        public void New()
        {
            //todo: наверно, надо удалить, потому что обработка на уровне ViewModel
        }

        /// <summary>
        /// Обработка нажатия кнопки "Правка".
        /// </summary>
        public void Edit()
        {
            //todo: наверно, надо удалить, потому что обработка на уровне ViewModel
        }

        /// <summary>
        /// Обработка нажатия кнопки "Принять".
        /// </summary>
        public virtual void Ok() { }

        /// <summary>
        /// Обработка нажатия кнопки "Отмена".
        /// </summary>
        public void Cancel()
        {
            //todo: наверно, надо удалить, потому что обработка на уровне ViewModel
        }

        /// <summary>
        /// Обработка нажатия кнопки "Найти".
        /// </summary>
        public void Search()
        {
            //todo: наверно, надо удалить, потому что обработка на уровне ViewModel
        }

        /// <summary>
        /// Обработка нажатия кнопки "Обновить".
        /// </summary>
        public void Reload()
        {
            //todo: наверно, надо удалить, потому что обработка на уровне ViewModel
        }

        /// <summary>
        /// Обработка нажатия кнопок добавления нового человека в заявку.
        /// </summary>
        public void AddPerson()
        {
            switch (OrderType)
            {
                case OrderType.Temp:
                    AddPersonInTempOrder();
                    break;
                case OrderType.Single:
                    AddPersonInSingleOrder();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Обработка нажатия кнопок редактирования нового человека в заявку.
        /// </summary>
        public void UpdatePerson()
        {
            if (UpdateVisitor == null)
            {
                MessageBox.Show("Не выбран посетитель для редактирования данных по нему");
                return;
            }
            AddUpdateAbstrModel model = new UpdateBidModel(UpdateVisitor);
	        AddUpdateBaseViewModel viewModel = new AddUpdateBidsViewModel(model);
            AddUpdateBidWindView view = new AddUpdateBidWindView
            {
                DataContext = viewModel
            };
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            int i = CurrentSingleOrder.OrderElements.IndexOf(UpdateVisitor);
            if (view.WindowResult == null) return;
            CurrentSingleOrder.OrderElements[i] = (view.WindowResult as OrderElement);
            UpdateVisitor = CurrentSingleOrder.OrderElements[i];
            OnRefresh?.Invoke();
        }

        /// <summary>
        /// Обработка нажатия кнопок удаления нового человека из заявки.
        /// </summary>
        public void DeletePerson()
        {
            UpdateVisitor.IsDeleted = true;
            CurrentSingleOrder.OrderElements.Remove(UpdateVisitor);
        }

        /// <summary>
        /// Обработка нажатия кнопки для добавления согласующего заявку.
        /// </summary>
        public void Agreer()
        {
            VisitorsModelResult result = ViewManager.Instance.OpenWindowModal(
                "VisitorsListWindViewOk", null) as VisitorsModelResult;
            if (result == null) return;
            CurrentTemporaryOrder.AgreeId = result.Id;
            CurrentTemporaryOrder.Agree = result.Name;
            OnRefresh?.Invoke();
        }

        /// <summary>
        /// Обработка нажатия кнопки "Правка".
        /// </summary>
        public void Signer()
        {
            VisitorsModelResult result = ViewManager.Instance.OpenWindowModal(
                "VisitorsListWindViewOk", null) as VisitorsModelResult;
            if (result == null) return;
            switch (OrderType)
            {
                case OrderType.Temp:
                    {
                        CurrentTemporaryOrder.SignedId = result.Id;
                        CurrentTemporaryOrder.Signed = result.Name;
                        break;
                    }
                case OrderType.Single:
                    {
                        CurrentSingleOrder.SignedId = result.Id;
                        CurrentSingleOrder.Signed = result.Name;
                        break;
                    }

                default:
                    break;
            }

            OnRefresh?.Invoke();
        }
        
        /// <summary>
        /// Процедура обрабатывающая добавление персоны в разовой заявке.
        /// </summary>
        protected void AddPersonInSingleOrder()
        {
            AddUpdateAbstrModel model = new AddSingleBidModel(true, CurrentSingleOrder.From, CurrentSingleOrder.From);
	        AddUpdateBaseViewModel viewModel = new AddUpdateBidsViewModel(model);
            AddUpdateBidWindView view = new AddUpdateBidWindView();
            view.Title = "Добавить посетителя";
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
            if (res == null) return;
            if (CurrentSingleOrder.OrderElements == null)
                CurrentSingleOrder.OrderElements = new ObservableCollection<OrderElement>();
            CurrentSingleOrder.OrderElements.Add((OrderElement)res);
            OnRefresh?.Invoke();
        }

        /// <summary>
        /// Процедура обрабатывающая добавление персоны во временной заявке.
        /// </summary>
        protected void AddPersonInTempOrder()
        {
            AddUpdateAbstrModel model = new AddSingleBidModel(false, CurrentTemporaryOrder.From, CurrentTemporaryOrder.To);
			AddUpdateBaseViewModel viewModel = new AddUpdateBidsViewModel(model)
			{
                Model = model
            };
            AddUpdateBidWindView view = new AddUpdateBidWindView();
            view.Title = "Добавить посетителя";
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
            if (res == null) return;
            if (CurrentTemporaryOrder.OrderElements == null)
                CurrentTemporaryOrder.OrderElements = new ObservableCollection<OrderElement>();
            CurrentTemporaryOrder.OrderElements.Add((OrderElement)res);
            OnRefresh?.Invoke();
        }
    }
}