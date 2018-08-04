using AndoverLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndoverPersonsManager
{
    public class PersonCore
    {
        private readonly Personnel _personnel;
        private string _path = "";

        public PersonCore()
        {
            Areas = new List<AreaCore>();
        }

        public PersonCore(Personnel personnel, List<Container> containers,
            List<Area> areas, List<AreaLink> areaLinks)
        {
            _personnel = personnel;

            FirstName = _personnel.FirstName;
            LastName = _personnel.LastName;
            CardNumber = !_personnel.NonABACardNumber.HasValue ? "" :
                _personnel.NonABACardNumber.Value.ToString();

            Containers = ContainerHelper.GetContainers(containers, _personnel.OwnerIdHi, _personnel.OwnerIdLo);

            Areas = new List<AreaCore>();
            foreach (var areaLink in areaLinks.Where(l =>
                l.PersonIdHi == _personnel.ObjectIdHi && l.PersonIdLo == _personnel.ObjectIdLo))
            {
                var area = areas.FirstOrDefault(a =>
                    a.ObjectIdHi == areaLink.AreaIdHi && a.ObjectIdLo == areaLink.AreaIdLo);
                if (area != null)
                {
                    Areas.Add(new AreaCore(area, containers));
                }
            }
        }

        public Personnel Personnel
        {
            get
            {
                return _personnel;
            }
        }

        public List<ContainerCore> Containers { get; set; }
        public List<AreaCore> Areas { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }

        public string Path
        {
            get
            {
                if (Containers != null)
                {
                    return ContainerHelper.GetPath(Containers);
                }
                else
                {
                    return _path;
                }
            }
            set
            {
                if (Containers == null)
                {
                    _path = value;
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Path);
            sb.Append(" '");
            sb.Append(LastName);
            sb.Append(" ");
            sb.Append(FirstName);
            sb.Append("' Карта № ");
            sb.Append(CardNumber);
            return sb.ToString();
        }

        public PersonInfo Convert()
        {
            return new PersonInfo
            {
                FirstName = FirstName,
                LastName = LastName,
                CardNum = CardNumber,
                Containers = Path.Split(new [] { '\\' }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                Areas = Areas.Select(a => a.Path + a.Name).ToList(),
            };
        }
    }
}
