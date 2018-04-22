using SupContract;
using System;

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

        public string Machine { get { return Environment.MachineName; } }

        public OperationInfo GetInfo()
        {
            return new OperationInfo
            {
                Id = Id,
                User = Login,
                Machine = Machine
            };
        }

        private Authorizer()
        {
        }
    }
}
