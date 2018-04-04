namespace SupHost
{

    partial class VisZonesTableBehavior : VisitorsDBTableBehavior
    {
        public VisZonesTableBehavior()
        {
            this.StandartSetup("vis_zones", "f_zone_id");
        }
    }

	partial class VisZonesTableWrapper : AbstractTableWrapper
    {
        public VisZonesTableWrapper()
        { 
			this.getTableBehavior = new VisZonesTableBehavior(); 
		}
    }

    partial class VisCabinetsZonesTableBehavior : VisitorsDBTableBehavior
    {
        public VisCabinetsZonesTableBehavior()
        {
            this.StandartSetup("vis_cabinets_zones", "f_cabinet_zone_id");
        }
    }

	partial class VisCabinetsZonesTableWrapper : AbstractTableWrapper
    {
        public VisCabinetsZonesTableWrapper()
        { 
			this.getTableBehavior = new VisCabinetsZonesTableBehavior(); 
		}
    }

    partial class VisZoneTypesTableBehavior : VisitorsDBTableBehavior
    {
        public VisZoneTypesTableBehavior()
        {
            this.StandartSetup("vis_zone_types", "f_zone_type_id");
        }
    }

	partial class VisZoneTypesTableWrapper : AbstractTableWrapper
    {
        public VisZoneTypesTableWrapper()
        { 
			this.getTableBehavior = new VisZoneTypesTableBehavior(); 
		}
    }
}