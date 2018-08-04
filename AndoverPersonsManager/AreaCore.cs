using AndoverLib;
using System.Collections.Generic;
using System.Text;

namespace AndoverPersonsManager
{
    public class AreaCore
    {
        private readonly Area _area;

        public AreaCore(Area area, List<Container> containers)
        {
            _area = area;

            Name = _area.UiName;

            Containers = ContainerHelper.GetContainers(containers, _area.OwnerIdHi, _area.OwnerIdLo);
        }

        public Area Area
        {
            get
            {
                return _area;
            }
        }

        public string Path
        {
            get
            {
                if (Containers != null)
                {
                    return ContainerHelper.GetPath(Containers);
                }
                return "";
            }
        }

        public string Name { get; set; }
        public List<ContainerCore> Containers { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Path);
            sb.Append(" '");
            sb.Append(Name);
            sb.Append("'");
            return sb.ToString();
        }
    }
}
