using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using SupRealClient.Annotations;
using SupRealClient.TabsSingleton;
using SupRealClient.Common;

namespace SupRealClient.EnumerationClasses
{
    public class Order : IdEntity
    {
        public Order()
        {
            OrderElements = new ObservableCollection<OrderElement>();
            
            OnChangeId += Order_OnChangeId;
        }

        private void Order_OnChangeId()
        {
            foreach (OrderElement orderElement in OrderElements)
            {
                orderElement.OrderId = Id;
            }
        }

        private void OrderElements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        ((OrderElement) item).OrderId = Id;
                        AddedOrderElements.Add((OrderElement)item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        DeletedOrderElements.Add((OrderElement)item);
                    }
                    // перемещаем в список удалённых.
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new Exception("Unexpected Case");
            }
        }

        private int organizationId;
        private int agreeId;
        /// <summary>
        /// Номер заявки.
        /// </summary>
        public int Number { get; set; } = 0;
        /// <summary>
        /// Номер типа заявки.
        /// </summary>
        public int TypeId { get; set; } = 0;

        /// <summary>
        /// Название типа заявки.
        /// </summary>
        public string Type
        {
            get
            {
                return SprOrderTypesWrapper.CurrentTable().Table.AsEnumerable()
                    .FirstOrDefault(arg => arg.Field<int>("f_order_type_id") ==
                    TypeId)["f_order_text"].ToString();
            }
            set { }
        }

        /// <summary>
        /// Выводимый пользователю и показываемый ему номер заявки.
        /// </summary>
        public string RegNumber
        {
            get
            {
                if (Type.Length == 0)
                {
                    return Number.ToString();
                }

                return Number + "-" + Type[0];
            }
            set
            {
                string num = Regex.Match(value, (@"^\d*")).Value;
                if (num != "") Number = Convert.ToInt32(num);

            }
        }
        /// <summary>
        /// Дата подачи заявки.
        /// </summary>
        public DateTime OrderDate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        
        public int CatcherId { get; set; } = 0; // Id провожающего
        public string Catcher { get; set; } = ""; // провожающий
        
        public string OrderType { get; set; } = ""; // тип заявки
        public string Passes { get; set; } = "";

        private int signedId;
        /// <summary>
        /// Id подписывающего лица
        /// </summary>
        public int SignedId
        {
            get { return signedId;}
            set
            {
                signedId = value;
                Signed = VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                    .FirstOrDefault(arg => arg.Field<int>("f_visitor_id") ==
                    signedId)["f_full_name"].ToString();
            }
        }
        public string Signed { get; set; } = ""; // Полное имя подписывающего
        public int AgreeId // Id согласовавшего
        {
            get { return agreeId; }
            set
            {
                agreeId = value;
                Agree = VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                    .FirstOrDefault(arg => arg.Field<int>("f_visitor_id") == 
                    agreeId)["f_visitor_id"].ToString();
            }
        } 
        public string Agree { get; set; } = ""; // Полное имя согласовавшего
        public string Note { get; set; } = "";// Примечание
        public int OrganizationId
        {
            get { return organizationId; }
            set
            {
                organizationId = value;
                Organization = OrganizationsWrapper.CurrentTable().Table
                    .AsEnumerable().FirstOrDefault(arg => 
                    arg.Field<int>("f_org_id") == organizationId)["f_org_name"]
                    .ToString();
            }
        }
        public string Organization { get; set; } = "";
        /// <summary>
        /// Показывает, что заявка не активна.
        /// </summary>
        public bool IsDisable { get; set; }
        /// <summary>
        /// Специализированое свойство предназначенное для того, чтобы помещать,
        /// что эту заявку надо выбрать. Изначально вставлено, чтобы удобно было
        /// работать с назначениями заявок, на которые надо ориентироваться при
        /// выдаче пропуска.
        /// </summary>
        public bool IsChecked { get; set; }

        private ObservableCollection<OrderElement> orderElements;

        /// <summary>
        /// Список привязанных к заявке посетителей.
        /// </summary>
        public ObservableCollection<OrderElement> OrderElements
        {
            get { return orderElements;}
            set
            {
                orderElements = value;
                orderElements.CollectionChanged += OrderElements_CollectionChanged;
            }
        }/* =
            new ObservableCollection<OrderElement>();*/

        /// <summary>
        /// Список элементов под удаление.
        /// </summary>
        public  ObservableCollection<OrderElement> DeletedOrderElements { get; set; }=
            new ObservableCollection<OrderElement>();

        public ObservableCollection<OrderElement> AddedOrderElements { get; set; } =
            new ObservableCollection<OrderElement>();
    }

    /// <summary>
    /// Описание посетителя по заявке.
    /// </summary>
    public class OrderElement : IdEntity, INotifyPropertyChanged, ICloneable
    {
        private int orderId;
        private int visitorId;
        private int organizationId;
        private int catcherId;

        /// <summary>
        /// Уникальный номер заявки
        /// </summary>
        public int OrderId
        {
            get { return orderId;}
            set
            {
                orderId = value;
                // todo: пока неактуальный код, но без необходимости не удалять.
                // todo: создавался для использования из под формы посетители, но пока вроде
                // todo: функционал реализуется средствами формы.
                /*DataRow row = OrdersWrapper.CurrentTable().Table.Rows.Find(orderId);
                string type = SprOrderTypesWrapper.CurrentTable().Table.AsEnumerable()
                    .FirstOrDefault(arg => arg.Field<int>("f_order_type_id") ==
                    (int)row["f_order_type_id"])["f_order_text"].ToString();
                int number = row.Field<int>("f_reg_number");
                type = type.Length > 0 ? type : " ";
                Order = number.ToString() + type[0];*/
            }
        }

        public string Order { get; set; }

        public int VisitorId
        {
            get { return visitorId;}
            set
            {
                visitorId = value;
                DataRow row = VisitorsWrapper.CurrentTable().Table
                    .AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_visitor_id") == visitorId);
                Visitor = row["f_full_name"].ToString();
                OrganizationId = (int)row["f_org_id"];
            }
        }

        private string visitor = "";

        public string Visitor {
            get { return visitor; }
            set
            {
                visitor = value;
                OnPropertyChanged();
            }
        }
        public int OrganizationId
        {
            get { return organizationId; }
            set
            {
                organizationId = value;
                Organization = OrganizationsWrapper.CurrentTable().Table
                    .AsEnumerable().FirstOrDefault(arg =>
                        arg.Field<int>("f_org_id") == organizationId)["f_org_name"]
                    .ToString();
            }
        }
        public string Organization { get; set; } = "";

        public int CatcherId
        {
            get { return catcherId; }
            set
            {
                catcherId = value;
                Catcher = VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                    .FirstOrDefault(arg => arg.Field<int>("f_visitor_id") ==
                                           catcherId)["f_full_name"].ToString();
                
            }
        } // Id сопровождающего

        private string catcher = "";
        public string Catcher
        {
            get { return catcher;}
            set
            {
                catcher = value;
                OnPropertyChanged();
            }
        } // сопровождающий

        private DateTime from= DateTime.MinValue;
        public DateTime From
        {
            get { return from;}
            set
            {
                from = value;
                OnPropertyChanged();
            }
        }

        private DateTime to = DateTime.MinValue;

        public DateTime To
        {
            get { return to;}
            set
            {
                to = value;
                OnPropertyChanged();
            }
        }

        private string passes = "";

        public string Passes
        {
            get { return passes;}
            set
            {
                passes = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Показывает, что элемент не активен.
        /// </summary>
        public bool IsDisable { get; set; }

        //public bool IsDeleted { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public ObservableCollection<Area> Areas { get; set; } = 
            new ObservableCollection<Area>();

        public ObservableCollection<Area> AddedAreas { get; set; } =
            new ObservableCollection<Area>();

        public ObservableCollection<Area> DeletedAreas { get; set; } =
            new ObservableCollection<Area>();
    }
}
