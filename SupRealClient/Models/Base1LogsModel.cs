using SupClientConnectionLib;
using SupRealClient.Common.Interfaces;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SupRealClient.Models
{
    class Base1LogsModel : Base1ModelAbstr
    {
        public Base1LogsModel(IBase1ViewModel viewModel, IWindow parent)
        {
            this.viewModel = viewModel;
            this.parent = parent;
            LogsWrapper logsWrapper = LogsWrapper.CurrentTable();
            table = logsWrapper.Table;
            tabConnector = logsWrapper.Connector;
            tabName = logsWrapper.Table.TableName;
            logsWrapper.OnChanged += Query;
            this.Query();
        }

        public override void EnterCurrentItem(object item)
        {
            // TODO - сделать новую модель для лога
            this.viewModel.NumItem = (int)(item as LogItem).Id;
        }

        public override void Add()
        {
        }

        public override void Update()
        {
        }

        public override void Begin()
        {
            if (this.viewModel.Set.Count() > 0)
            {
                this.viewModel.CurrentItem = this.viewModel.Set.First();
                this.viewModel.NumItem =
                    (int)(this.viewModel.CurrentItem as LogItem).Id;
                this.viewModel.SelectedIndex = 0;
            }
            else
            {
                this.viewModel.NumItem = -1;
            }
        }

        public override void End()
        {
            this.viewModel.CurrentItem = this.viewModel.Set.Last();
            this.viewModel.NumItem =
                (int)(this.viewModel.CurrentItem as LogItem).Id;
            this.viewModel.SelectedIndex = this.viewModel.Set.Count() - 1;
        }

        protected override void Query()
        {
            var logs = from l in table.AsEnumerable()
                       where //l.Field<object>("f_rec_operator") != null &&
                       Authorizer.AppAuthorizer.Id.Equals(l.Field<object>("f_rec_operator"))
                       select new LogItem
                            {
                                Id = l.Field<long>("f_log_id"),
                                Severity = l.Field<string>("f_log_severety"),
                                Message = l.Field<string>("f_log_message"),
                                RecDate = l.Field<DateTime>("f_rec_date"),
                                RecOperator = l.Field<object>("f_rec_operator") != null ? l.Field<int>("f_rec_operator") : -1,
                            };
            this.viewModel.Set =
                new System.Collections.ObjectModel.ObservableCollection<object>(logs);
            if (viewModel.NumItem == -1)
            {
                this.Begin();
            }
            else
            {
                try
                {
                    this.viewModel.CurrentItem = logs.First(
                        arg => arg.Id == this.viewModel.NumItem);
                }
                catch (Exception)
                {
                    this.Begin();
                }
            }
        }

        public override IDictionary<string, string> GetFields()
        {
            return new Dictionary<string, string>
            {
                { "f_log_severety", "Уровень" },
                { "f_log_message", "Сообщение" },
            };
        }

        public override long GetId(int index)
        {
            return Rows[index].Field<long>("f_log_id");
        }

        public override void SetAt(long id)
        {
            for (int i = 0; i < this.viewModel.Set.Count(); i++)
            {
                if ((this.viewModel.Set.ElementAt(i) as LogItem).Id == id)
                {
                    this.viewModel.SelectedValue = this.viewModel.Set.ElementAt(i);
                    break;
                }
            }
        }

        protected override IDictionary<string, string> GetColumns()
        {
            return new Dictionary<string, string>()
            {
                { "RecDate", "f_rec_date" },
                { "Severity", "f_log_severety" },
                { "Message", "f_log_message" },
            };
        }
    }
}
