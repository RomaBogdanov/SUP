using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class Accounts
    {
        static Accounts accounts;
        List<string> listAccs = new List<string>();

        public static Accounts GetAccounts()
        {
            if (accounts == null)
            {
                accounts = new Accounts();
            }
            return accounts;
        }

        public bool IsExist(string login)
        {
            if (listAccs.Contains(login))
            {
                return true;
            }
            return false;
        }

        public void AddAccount(string login)
        {
            this.listAccs.Add(login);
        }

        public void RemoveAccount(string login)
        {
            this.listAccs.Remove(login);
        }

        private Accounts()
        { }
    }
}
