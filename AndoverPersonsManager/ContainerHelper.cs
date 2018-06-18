using AndoverLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndoverPersonsManager
{
    public static class ContainerHelper
    {
        public static List<ContainerCore> GetContainers(List<Container> containers, int? idHi, int? idLo)
        {
            var result = new List<ContainerCore>();
            var container = containers.FirstOrDefault(c =>
                c.ObjectIdHi == (idHi.HasValue ? idHi.Value : 0) &&
                c.ObjectIdLo == (idLo.HasValue ? idLo.Value : 0));
            while (container != null)
            {
                result.Insert(0, new ContainerCore(container));
                container = containers.FirstOrDefault(c =>
                    c.ObjectIdHi == (container.OwnerIdHi.HasValue ? container.OwnerIdHi.Value : 0) &&
                    c.ObjectIdLo == (container.OwnerIdLo.HasValue ? container.OwnerIdLo.Value : 0));

            }
            return result;
        }

        public static string GetPath(List<ContainerCore> containers)
        {
            var sb = new StringBuilder();
            foreach (var cont in containers)
            {
                sb.Append(cont);
                sb.Append("\\");
            }
            return sb.ToString();
        }
    }
}
