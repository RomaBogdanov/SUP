namespace SupHost
{
    /// <summary>
    /// TableBehavior для Кабинеты
    /// </summary>
    partial class VisCabinetsTableBehavior : VisitorsDBTableBehavior
    {
        public VisCabinetsTableBehavior()
        {
            this.StandartSetup("vis_cabinets", "f_cabinet_id");
        }
    }

    /// <summary>
    /// TableWrapper для Кабинеты
    /// </summary>
	partial class VisCabinetsTableWrapper : AbstractTableWrapper
    {
        public VisCabinetsTableWrapper()
        { 
			this.getTableBehavior = new VisCabinetsTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для Пропуска
    /// </summary>
    partial class VisCardsTableBehavior : VisitorsDBTableBehavior
    {
        public VisCardsTableBehavior()
        {
            this.StandartSetup("vis_cards", "f_card_id");
        }
    }

    /// <summary>
    /// TableWrapper для Пропуска
    /// </summary>
	partial class VisCardsTableWrapper : AbstractTableWrapper
    {
        public VisCardsTableWrapper()
        { 
			this.getTableBehavior = new VisCardsTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для Страны
    /// </summary>
    partial class VisCountriesTableBehavior : VisitorsDBTableBehavior
    {
        public VisCountriesTableBehavior()
        {
            this.StandartSetup("vis_countries", "f_cntr_id");
        }
    }

    /// <summary>
    /// TableWrapper для Страны
    /// </summary>
	partial class VisCountriesTableWrapper : AbstractTableWrapper
    {
        public VisCountriesTableWrapper()
        { 
			this.getTableBehavior = new VisCountriesTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для Документы
    /// </summary>
    partial class VisDocumentsTableBehavior : VisitorsDBTableBehavior
    {
        public VisDocumentsTableBehavior()
        {
            this.StandartSetup("vis_documents", "f_doc_id");
        }
    }

    /// <summary>
    /// TableWrapper для Документы
    /// </summary>
	partial class VisDocumentsTableWrapper : AbstractTableWrapper
    {
        public VisDocumentsTableWrapper()
        { 
			this.getTableBehavior = new VisDocumentsTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для ??
    /// </summary>
    partial class VisOrderElementsTableBehavior : VisitorsDBTableBehavior
    {
        public VisOrderElementsTableBehavior()
        {
            this.StandartSetup("vis_order_elements", "f_oe_id");
        }
    }

    /// <summary>
    /// TableWrapper для ??
    /// </summary>
	partial class VisOrderElementsTableWrapper : AbstractTableWrapper
    {
        public VisOrderElementsTableWrapper()
        { 
			this.getTableBehavior = new VisOrderElementsTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для ??
    /// </summary>
    partial class VisOrdersTableBehavior : VisitorsDBTableBehavior
    {
        public VisOrdersTableBehavior()
        {
            this.StandartSetup("vis_orders", "f_ord_id");
        }
    }

    /// <summary>
    /// TableWrapper для ??
    /// </summary>
	partial class VisOrdersTableWrapper : AbstractTableWrapper
    {
        public VisOrdersTableWrapper()
        { 
			this.getTableBehavior = new VisOrdersTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для Организации
    /// </summary>
    partial class VisOrganizationsTableBehavior : VisitorsDBTableBehavior
    {
        public VisOrganizationsTableBehavior()
        {
            this.StandartSetup("vis_organizations", "f_org_id");
        }
    }

    /// <summary>
    /// TableWrapper для Организации
    /// </summary>
	partial class VisOrganizationsTableWrapper : AbstractTableWrapper
    {
        public VisOrganizationsTableWrapper()
        { 
			this.getTableBehavior = new VisOrganizationsTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для Регионы
    /// </summary>
    partial class VisRegionsTableBehavior : VisitorsDBTableBehavior
    {
        public VisRegionsTableBehavior()
        {
            this.StandartSetup("vis_regions", "f_region_id");
        }
    }

    /// <summary>
    /// TableWrapper для Регионы
    /// </summary>
	partial class VisRegionsTableWrapper : AbstractTableWrapper
    {
        public VisRegionsTableWrapper()
        { 
			this.getTableBehavior = new VisRegionsTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для ??
    /// </summary>
    partial class VisSprCardstatesTableBehavior : VisitorsDBTableBehavior
    {
        public VisSprCardstatesTableBehavior()
        {
            this.StandartSetup("vis_spr_cardstates", "f_state_id");
        }
    }

    /// <summary>
    /// TableWrapper для ??
    /// </summary>
	partial class VisSprCardstatesTableWrapper : AbstractTableWrapper
    {
        public VisSprCardstatesTableWrapper()
        { 
			this.getTableBehavior = new VisSprCardstatesTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для Посетители
    /// </summary>
    partial class VisVisitorsTableBehavior : VisitorsDBTableBehavior
    {
        public VisVisitorsTableBehavior()
        {
            this.StandartSetup("vis_visitors", "f_visitor_id");
        }
    }

    /// <summary>
    /// TableWrapper для Посетители
    /// </summary>
	partial class VisVisitorsTableWrapper : AbstractTableWrapper
    {
        public VisVisitorsTableWrapper()
        { 
			this.getTableBehavior = new VisVisitorsTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для ??
    /// </summary>
    partial class VisVisitsTableBehavior : VisitorsDBTableBehavior
    {
        public VisVisitsTableBehavior()
        {
            this.StandartSetup("vis_visits", "f_visit_id");
        }
    }

    /// <summary>
    /// TableWrapper для ??
    /// </summary>
	partial class VisVisitsTableWrapper : AbstractTableWrapper
    {
        public VisVisitsTableWrapper()
        { 
			this.getTableBehavior = new VisVisitsTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для Зоны
    /// </summary>
    partial class VisZonesTableBehavior : VisitorsDBTableBehavior
    {
        public VisZonesTableBehavior()
        {
            this.StandartSetup("vis_zones", "f_zone_id");
        }
    }

    /// <summary>
    /// TableWrapper для Зоны
    /// </summary>
	partial class VisZonesTableWrapper : AbstractTableWrapper
    {
        public VisZonesTableWrapper()
        { 
			this.getTableBehavior = new VisZonesTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для ??
    /// </summary>
    partial class VisCabinetsZonesTableBehavior : VisitorsDBTableBehavior
    {
        public VisCabinetsZonesTableBehavior()
        {
            this.StandartSetup("vis_cabinets_zones", "f_cabinet_zone_id");
        }
    }

    /// <summary>
    /// TableWrapper для ??
    /// </summary>
	partial class VisCabinetsZonesTableWrapper : AbstractTableWrapper
    {
        public VisCabinetsZonesTableWrapper()
        { 
			this.getTableBehavior = new VisCabinetsZonesTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для Типы зон
    /// </summary>
    partial class VisZoneTypesTableBehavior : VisitorsDBTableBehavior
    {
        public VisZoneTypesTableBehavior()
        {
            this.StandartSetup("vis_zone_types", "f_zone_type_id");
        }
    }

    /// <summary>
    /// TableWrapper для Типы зон
    /// </summary>
	partial class VisZoneTypesTableWrapper : AbstractTableWrapper
    {
        public VisZoneTypesTableWrapper()
        { 
			this.getTableBehavior = new VisZoneTypesTableBehavior(); 
		}
    }
    /// <summary>
    /// TableBehavior для Департаменты
    /// </summary>
    partial class VisDepartmentTableBehavior : VisitorsDBTableBehavior
    {
        public VisDepartmentTableBehavior()
        {
            this.StandartSetup("vis_departaments", "f_dep_id");
        }
    }

    /// <summary>
    /// TableWrapper для Департаменты
    /// </summary>
	partial class VisDepartmentTableWrapper : AbstractTableWrapper
    {
        public VisDepartmentTableWrapper()
        { 
			this.getTableBehavior = new VisDepartmentTableBehavior(); 
		}
    }
}