using SupHost.Data;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace SupHost
{
    class Accounts
    {
        private readonly object syncObj = new object();
        static Accounts accounts;
        Dictionary<UserTimeoutData, int> listAccs = new Dictionary<UserTimeoutData, int>();
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
            lock (syncObj)
            {
                return listAccs.Keys.Any(u => u.Name == login);
            }
        }

        public void AddAccount(UserTimeoutData userData)
        {
            lock (syncObj)
            {
                this.listAccs.Add(userData, 0);
            }
        }

        public void RemoveAccount(UserTimeoutData userData)
        {
            lock (syncObj)
            {
                this.listAccs.Remove(userData);
            }
        }

        public bool CheckAccount(UserTimeoutData userData)
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
            var timer = new Timer(elapsedTime);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (syncObj)
            {
                var keysToRemove = new List<UserTimeoutData>();
                var keysToChange = new List<UserTimeoutData>();
                foreach (var item in this.listAccs)
                {
                    if (item.Key.Timeout >= 0 && item.Value >= item.Key.Timeout)
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
                    this.listAccs[key] += elapsedTime / 1000;
                }
            }
        }
    }
}
