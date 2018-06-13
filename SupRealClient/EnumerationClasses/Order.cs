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
using SupRealClient.Annotations;
using SupRealClient.TabsSingleton;

namespace SupRealClient.EnumerationClasses
{
    public class Order : IdEntity
    {
        public Order()
        {
            OrderElements.CollectionChanged += OrderElements_CollectionChanged;
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
            /*if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    ((OrderElement) item).OrderId = Id;
                }
            }*/
        }

        private int organizationId;
        private int agreeId;

        public int Number { get; set; } = 0;
        public int TypeId { get; set; } = 0;
        public string Type { get; set; } = "";
        public string RegNumber { get; set; } = "";
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int CatcherId { get; set; } = 0; // Id провожающего
        public string Catcher { get; set; } = ""; // провожающий
        public string OrderType { get; set; } = ""; // тип заявки
        public string Passes { get; set; } = "";
        public int SignedId { get; set; } = 0; // Id подписывающего лица
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
        /// Список привязанных к заявке посетителей.
        /// </summary>
        public ObservableCollection<OrderElement> OrderElements { get; set; } =
            new ObservableCollection<OrderElement>();

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
        private int visitorId;
        private int organizationId;
        private int catcherId;

        /// <summary>
        /// Уникальный номер заявки
        /// </summary>
        public int OrderId { get; set; } = 0;

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

        public bool IsDeleted { get; set; }

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
    }
}
