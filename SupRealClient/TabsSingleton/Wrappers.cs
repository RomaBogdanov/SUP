using SupContract;

/*
ФАЙЛ СГЕНЕРИРОВАН АВТОМАТИЧЕСКИ
ИЗМЕНЕНИЯ НЕ ВНОСИТЬ!!!!!
*/

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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private CardsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisCards);
            this.Subscribe();
        }
    }

	/// <summary>
	/// Страны
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private CountriesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisCountries);
            this.Subscribe();
        }
    }

	/// <summary>
	/// Документы
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private DocumentsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisDocuments);
            this.Subscribe();
        }
    }

	/// <summary>
	/// Организации
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private SprCardstatesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisSprCardstates);
            this.Subscribe();
        }
    }

	/// <summary>
	/// Посетители
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private VisitsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisVisits);
            this.Subscribe();
        }
    }

	/// <summary>
	/// Кабинеты
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private CabinetsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisCabinets);
            this.Subscribe();
        }
    }

	/// <summary>
	/// Зоны
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private ZoneTypesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisZoneTypes);
            this.Subscribe();
        }
    }

	/// <summary>
	/// Подразделения
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

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
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
	partial class OrdersWrapper : TableWrapper
	{
		static OrdersWrapper currentTable;

        public static OrdersWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new OrdersWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private OrdersWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisOrders);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class OrderElementsWrapper : TableWrapper
	{
		static OrderElementsWrapper currentTable;

        public static OrderElementsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new OrderElementsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private OrderElementsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisOrderElements);
            this.Subscribe();
        }
    }

	/// <summary>
	/// Изображения
	/// </summary>
	partial class ImagesWrapper : TableWrapper
	{
		static ImagesWrapper currentTable;

        public static ImagesWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new ImagesWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private ImagesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisImages);
            this.Subscribe();
        }
    }

	/// <summary>
	/// Регионы
	/// </summary>
	partial class RegionsWrapper : TableWrapper
	{
		static RegionsWrapper currentTable;

        public static RegionsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new RegionsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private RegionsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisRegions);
            this.Subscribe();
        }
    }

	/// <summary>
	/// Типы заявок
	/// </summary>
	partial class SprOrderTypesWrapper : TableWrapper
	{
		static SprOrderTypesWrapper currentTable;

        public static SprOrderTypesWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new SprOrderTypesWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private SprOrderTypesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisSprOrderTypes);
            this.Subscribe();
        }
    }

	/// <summary>
	/// Документы посетителей
	/// </summary>
	partial class VisitorsDocumentsWrapper : TableWrapper
	{
		static VisitorsDocumentsWrapper currentTable;

        public static VisitorsDocumentsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new VisitorsDocumentsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private VisitorsDocumentsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisVisitorsDocuments);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class ImageDocumentWrapper : TableWrapper
	{
		static ImageDocumentWrapper currentTable;

        public static ImageDocumentWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new ImageDocumentWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private ImageDocumentWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisImageDocument);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class SpacesWrapper : TableWrapper
	{
		static SpacesWrapper currentTable;

        public static SpacesWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new SpacesWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private SpacesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisSpaces);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class DoorsWrapper : TableWrapper
	{
		static DoorsWrapper currentTable;

        public static DoorsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new DoorsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private DoorsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisDoors);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class AreasWrapper : TableWrapper
	{
		static AreasWrapper currentTable;

        public static AreasWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new AreasWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private AreasWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisAreas);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class AreasExtWrapper : TableWrapper
	{
		static AreasExtWrapper currentTable;

        public static AreasExtWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new AreasExtWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private AreasExtWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisAreasExt);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class AreasSpacesWrapper : TableWrapper
	{
		static AreasSpacesWrapper currentTable;

        public static AreasSpacesWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new AreasSpacesWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private AreasSpacesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisAreasSpaces);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class AccessPointsWrapper : TableWrapper
	{
		static AccessPointsWrapper currentTable;

        public static AccessPointsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new AccessPointsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private AccessPointsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisAccessPoints);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class AccessPointsExtWrapper : TableWrapper
	{
		static AccessPointsExtWrapper currentTable;

        public static AccessPointsExtWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new AccessPointsExtWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private AccessPointsExtWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisAccessPointsExt);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class KeysWrapper : TableWrapper
	{
		static KeysWrapper currentTable;

        public static KeysWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new KeysWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private KeysWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisKeys);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class SchedulesWrapper : TableWrapper
	{
		static SchedulesWrapper currentTable;

        public static SchedulesWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new SchedulesWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private SchedulesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisSchedules);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class SchedulesExtWrapper : TableWrapper
	{
		static SchedulesExtWrapper currentTable;

        public static SchedulesExtWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new SchedulesExtWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private SchedulesExtWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisSchedulesExt);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class AccessLevelWrapper : TableWrapper
	{
		static AccessLevelWrapper currentTable;

        public static AccessLevelWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new AccessLevelWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private AccessLevelWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisAccessLevel);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class CarsWrapper : TableWrapper
	{
		static CarsWrapper currentTable;

        public static CarsWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new CarsWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private CarsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisCars);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class EquipmentWrapper : TableWrapper
	{
		static EquipmentWrapper currentTable;

        public static EquipmentWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new EquipmentWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private EquipmentWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisEquipment);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class KeyCasesWrapper : TableWrapper
	{
		static KeyCasesWrapper currentTable;

        public static KeyCasesWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new KeyCasesWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private KeyCasesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisKeyCases);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class KeyHoldersWrapper : TableWrapper
	{
		static KeyHoldersWrapper currentTable;

        public static KeyHoldersWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new KeyHoldersWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private KeyHoldersWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisKeyHolders);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class AreaOrderElementWrapper : TableWrapper
	{
		static AreaOrderElementWrapper currentTable;

        public static AreaOrderElementWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new AreaOrderElementWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private AreaOrderElementWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisAreaOrderElement);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class CardAreaWrapper : TableWrapper
	{
		static CardAreaWrapper currentTable;

        public static CardAreaWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new CardAreaWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private CardAreaWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisCardArea);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class TemplatesWrapper : TableWrapper
	{
		static TemplatesWrapper currentTable;

        public static TemplatesWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new TemplatesWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private TemplatesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisTemplates);
            this.Subscribe();
        }
    }

	/// <summary>
	/// ??
	/// </summary>
	partial class TemplatesAreasWrapper : TableWrapper
	{
		static TemplatesAreasWrapper currentTable;

        public static TemplatesAreasWrapper CurrentTable()
        {
            if (currentTable == null)
            {
                currentTable = new TemplatesAreasWrapper();
                wrappers.Add(currentTable);
            }
            return currentTable;
        }

		public override void Dispose()
        {
            base.Dispose();
            currentTable = null;
        }

        private TemplatesAreasWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisTemplatesAreas);
            this.Subscribe();
        }
    }

}
