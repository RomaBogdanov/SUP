using SupHost.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Timers;

namespace SupHost
{
    class Accounts
    {
        private readonly object syncObj = new object();
        static Accounts accounts;
        Dictionary<UserData, int> listAccs = new Dictionary<UserData, int>();
        int elapsedTime = 30000;
        int maxTime;

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
            lock (syncObj)
            {
                return listAccs.Keys.Any(u => u.Name == login);
            }
        }

        public void AddAccount(UserData userData)
        {
            lock (syncObj)
            {
                this.listAccs.Add(userData, 0);
            }
        }

        public void RemoveAccount(UserData userData)
        {
            lock (syncObj)
            {
                this.listAccs.Remove(userData);
            }
        }

        public bool CheckAccount(UserData userData)
        {
            lock (syncObj)
            {
                if (listAccs.ContainsKey(userData))
                {
                    listAccs[userData] = 0;
                    return true;
                }
                return false;
            }
        }

        private Accounts()
        {
            int res;
            maxTime = int.TryParse(
                ConfigurationManager.AppSettings["UserTimeout"], out res) ? res : 180000;
            var timer = new Timer(elapsedTime);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (syncObj)
            {
                var keysToRemove = new List<UserData>();
                var keysToChange = new List<UserData>();
                foreach (var item in this.listAccs)
                {
                    if (item.Value >= maxTime)
                    {
                        keysToRemove.Add(item.Key);
                    }
                    else
                    {
                        keysToChange.Add(item.Key);
                    }
                }
                foreach (var key in keysToRemove)
                {
                    this.listAccs.Remove(key);
                }
                foreach (var key in keysToChange)
                {
                    this.listAccs[key] += elapsedTime;
                }
            }
        }
    }
}
