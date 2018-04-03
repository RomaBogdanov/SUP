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
            => currentTable = currentTable ?? new UsersWrapper();

        private UsersWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisClientUsers);
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
            => currentTable = currentTable ?? new CardsWrapper();

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
            => currentTable = currentTable ?? new CountriesWrapper();

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
            => currentTable = currentTable ?? new DocumentsWrapper();

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
            => currentTable = currentTable ?? new OrganizationsWrapper();

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
            => currentTable = currentTable ?? new SprCardstatesWrapper();

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
            => currentTable = currentTable ?? new VisitorsWrapper();

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
            => currentTable = currentTable ?? new VisitsWrapper();

        private VisitsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisVisits);
            this.Subscribe();
        }
    }

}
