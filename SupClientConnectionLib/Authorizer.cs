using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupClientConnectionLib
{
    /// <summary>
    /// 
    /// </summary>
    public class Authorizer
    {
        static Authorizer authorizer;

        public static Authorizer AppAuthorizer
        {
            get
            {
                if (authorizer == null)
                {
                    authorizer = new Authorizer();
                }
                return authorizer;
            }
        }

        public int Id { get; set; } = -1;

        public string Login { get; set; } = "NoName";

        private Authorizer()
        { }
    }
}
