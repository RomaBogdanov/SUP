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

	/// <summary>
	/// ??
	/// </summary>
	partial class CabinetsWrapper : TableWrapper
	{
		static CabinetsWrapper currentTable;

        public static CabinetsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new CabinetsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private CabinetsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisCabinets);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class ZonesWrapper : TableWrapper
	{
		static ZonesWrapper currentTable;

        public static ZonesWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new ZonesWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private ZonesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisZones);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class CabinetsZonesWrapper : TableWrapper
	{
		static CabinetsZonesWrapper currentTable;

        public static CabinetsZonesWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new CabinetsZonesWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private CabinetsZonesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisCabinetsZones);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class ZoneTypesWrapper : TableWrapper
	{
		static ZoneTypesWrapper currentTable;

        public static ZoneTypesWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new ZoneTypesWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private ZoneTypesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisZoneTypes);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class DepartmentWrapper : TableWrapper
	{
		static DepartmentWrapper currentTable;

        public static DepartmentWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new DepartmentWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private DepartmentWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisDepartment);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class DepartmentSectionWrapper : TableWrapper
	{
		static DepartmentSectionWrapper currentTable;

        public static DepartmentSectionWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new DepartmentSectionWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

        private DepartmentSectionWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisDepartmentSection);
            this.Subscribe();
        }
    }

}
