using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.EnumerationClasses
{
    public class Space : IdEntity
    {
        public string NumReal { get; set; }
        public string NumBuild { get; set; }
        public string Descript { get; set; }
        public string Note { get; set; }

    }

    public class Door : IdEntity
    {
        public string DoorNum { get; set; }
        public string Descript { get; set; }
        public int SpaceIn { get; set; }
        public int SpaceOut { get; set; }
        public int AccessPointId { get; set; }
    }

    public class Area : IdEntity
    {
        public string Name { get; set; }
        public string Descript { get; set; }
    }

    public class AreaSpace : IdEntity
    {
        public int AreaId { get; set; }
        public int SpaceId { get; set; }
    }

    public class AccessPoint : IdEntity
    {
        public string Name { get; set; }
        public string Descript { get; set; }
        public string SpaceIn { get; set; }
        public string SpaceOut { get; set; }
    }

    public class RealKey : IdEntity
    {
        public string Name { get; set; }
        public string Descript { get; set; }
        public int DoorId { get; set; } // номер двери, к которой привязан ключ
        public int KeyHolderId { get; set; } // номер ключницы, в которой находится ключ
        public int KeyCaseId { get; set; } // номер пенала, в котором находится ключ
    }

    public class Schedule : IdEntity
    {
        // Неверно совершенно. Переделать.
    }

    public class AccessLevel : IdEntity
    {
        public int AreaId { get; set; }
        public string Name { get; set; }
        public int ScheduleId { get; set; }
        public string AccessLevelNote { get; set; }
    }

    public class Car : IdEntity
    {
        public string CarMark { get; set; }
        public string CarNumber { get; set; }
        public int OrgId { get; set; }
        public int VisitorId { get; set; } // имя заявителя
        public string Color { get; set; } // цвет машины
    }

    public class Equipment : IdEntity
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string EquipNum { get; set; }
        public string Direct { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int OrgId { get; set; }
        public int VisId { get; set; }
    }

    public class KeyCase : IdEntity
    {
        public string InnerCode { get; set; }
        public string KeyHolder { get; set; }
        public int CellNum { get; set; }
        public string Descript { get; set; }
    }

    public class KeyHolder: IdEntity
    {
        public string KeyHolderNum { get; set; }
        public string Descript { get; set; }
        public int Count { get; set; }
    }
}
