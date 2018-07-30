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

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }

    public class Door : IdEntity
    {
        public string DoorNum { get; set; }
        public string Descript { get; set; }
        public int SpaceInId { get; set; }
        public string SpaceIn { get; set; }
        public int SpaceOutId { get; set; }
        public string SpaceOut { get; set; }
        public int AccessPointIdHi { get; set; }
        public int AccessPointIdLo { get; set; }
        public string AccessPoint { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    /// <summary>
    /// Описание области доступа.
    /// </summary>
    public class Area : IdEntity
    {
        public int ObjectIdHi { get; set; }
        public int ObjectIdLo { get; set; }
        public string Name { get; set; }
        public string Descript { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class AreaOrderElement : IdEntity
    {
        public int OrderElementId { get; set; }
        public int AreaIdHi { get; set; }
        public int AreaIdLo { get; set; }
    }

    public class AreaSpace : IdEntity
    {
        public int AreaIdHi { get; set; }
        public int AreaIdLo { get; set; }
        public string Area { get; set; }
        public int SpaceId { get; set; }
        public string Space { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class AccessPoint : IdEntity
    {
        public string Name { get; set; }
        public string Descript { get; set; }
        public string SpaceIn { get; set; }
        public string SpaceOut { get; set; }
    }

    public class CardArea : IdEntity
    {
        public int CardIdHi { get; set; }
        public int CardIdLo { get; set; }
        public int AreaIdHi { get; set; }
        public int AreaIdLo { get; set; }
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
        public int ObjectIdHi { get; set; }
        public int ObjectIdLo { get; set; }
        public string Name { get; set; }
        public string Descript { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class AccessLevel : IdEntity
    {
        public int AreaIdHi { get; set; }
        public int AreaIdLo { get; set; }
        public string Area { get; set; }
        public string Name { get; set; }
        public int ScheduleIdHi { get; set; }
        public int ScheduleIdLo { get; set; }
        public string Schedule { get; set; }
        public string AccessLevelNote { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
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
