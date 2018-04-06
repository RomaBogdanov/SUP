using SupClientConnectionLib.ServiceRef;

namespace SupRealClient.TabsSingleton
{
	/// <summary>
	/// Пользователи без пароля
	/// </summary>
	partial class UsersWrapper : TableWrapper
	{
		static UsersWrapper currentTable;

        public static UsersWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new UsersWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private UsersWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisClientUsers);
            this.Subscribe();
        }
    }

	/// <summary>
	/// Логи сервера
	/// </summary>
	partial class LogsWrapper : TableWrapper
	{
		static LogsWrapper currentTable;

        public static LogsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new LogsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private LogsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisClientLogs);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class CardsWrapper : TableWrapper
	{
		static CardsWrapper currentTable;

        public static CardsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new CardsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private CardsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisCards);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class CountriesWrapper : TableWrapper
	{
		static CountriesWrapper currentTable;

        public static CountriesWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new CountriesWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private CountriesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisCountries);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class DocumentsWrapper : TableWrapper
	{
		static DocumentsWrapper currentTable;

        public static DocumentsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new DocumentsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private DocumentsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisDocuments);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class OrganizationsWrapper : TableWrapper
	{
		static OrganizationsWrapper currentTable;

        public static OrganizationsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new OrganizationsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private OrganizationsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisOrganizations);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class SprCardstatesWrapper : TableWrapper
	{
		static SprCardstatesWrapper currentTable;

        public static SprCardstatesWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new SprCardstatesWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private SprCardstatesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisSprCardstates);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class VisitorsWrapper : TableWrapper
	{
		static VisitorsWrapper currentTable;

        public static VisitorsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new VisitorsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private VisitorsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisVisitors);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class VisitsWrapper : TableWrapper
	{
		static VisitsWrapper currentTable;

        public static VisitsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new VisitsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private VisitsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisVisits);
            this.Subscribe();
        }
    }

}
