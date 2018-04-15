using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.Models
{
    public class Human
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }

        public string Initials => $"{SecondName} {FirstName.First()}.{ThirdName.First()}.";
    }
}
