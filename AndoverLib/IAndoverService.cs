using System.Collections.Generic;
using System.ServiceModel;

namespace AndoverLib
{
    [ServiceContract]
    public interface IAndoverService
    {
        [OperationContract]
        string Ping();

        [OperationContract]
        List<Container> GetContainers();

        [OperationContract]
        List<Device> GetDevices();

        [OperationContract]
        List<Area> GetAreas();

        [OperationContract]
        List<Personnel> GetPersons();

        [OperationContract]
        List<Schedule> GetSchedules();

        [OperationContract]
        List<AreaLink> GetAreaLinks();

        [OperationContract]
        List<Door> GetDoors();

        [OperationContract]
        List<DoorList> GetDoorLists();

        [OperationContract]
        void ExportPersons(List<Personnel> persons);

        [OperationContract]
        bool ExportPersonsDmp(List<PersonInfo> persons);

        [OperationContract]
        CAndoverAgentCallback ExportPersonDmp(PersonInfo person);
    }
}
