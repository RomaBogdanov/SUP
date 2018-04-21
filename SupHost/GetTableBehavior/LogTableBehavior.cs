using SupHost.Connectors;
using System;

namespace SupHost
{
    class LogTableBehavior : BaseTableBehavior
    {
        private Connector connector = LogConnector.CurrentConnector;

        public LogTableBehavior()
        {
            this.StandartSetup("vis_log", "f_log_id");
        }

        public override void UpdateRow()
        {
            throw new NotImplementedException("Обновление данных лога не поддерживается");
        }

        public override void DeleteRow()
        {
            throw new NotImplementedException("Удаление данных лога не поддерживается");
        }

        protected override Connector GetConnector()
        {
            return connector;
        }
    }
}
