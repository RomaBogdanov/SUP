using AndoverLib;
using System.Collections.Generic;

namespace AndoverPersonsManager
{
    public class ProgramData
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }

        public List<Container> Containers { get; set; }
        public List<Device> Devices { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<Door> Doors { get; set; }
        public List<Area> Areas { get; set; }
        public List<DoorList> DoorLists { get; set; }
        public List<Personnel> Personnels { get; set; }
        public List<AreaLink> AreaLinks { get; set; }

        public List<ContainerCore> ContainerCores { get; set; }
        public List<AreaCore> AreaCores { get; set; }
        public List<PersonCore> PersonCores { get; set; }
    }
}
