using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SupHost
{
    class Accounts
    {
        static Accounts accounts;
        Dictionary<string, int> listAccs = new Dictionary<string, int>();
        int maxTime = 60000;
        int elapsedTime = 30000;

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
            if (listAccs.ContainsKey(login))
            {
                return true;
            }
            return false;
        }

        public void AddAccount(string login, int id)
        {
            this.listAccs.Add(login, id);
        }

        public void RemoveAccount(string login)
        {
            this.listAccs.Remove(login);
        }

        public bool CheckAccount(string login)
        {
            if (listAccs.ContainsKey(login))
            {
                listAccs[login] = 0;
                return true;
            }
            return false;
        }

        private Accounts()
        {
            var timer = new Timer(elapsedTime);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (this.listAccs)
            {
                foreach (var item in this.listAccs)
                {
                    if (item.Value >= maxTime)
                    {
                        this.listAccs.Remove(item.Key);
                    }
                    else
                    {
                        this.listAccs[item.Key] += elapsedTime;
                    }
                }
            }
        }
    }
}
