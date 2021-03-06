﻿using System;

namespace SupRealClient
{
    class SetupStorage
    {
        private static SetupStorage setupStorage;
        private bool userExit = true;

        public event Action<bool> ChangeUserExit;

        public static SetupStorage Current
        {
            get
            {
                if (setupStorage == null)
                {
                    setupStorage = new SetupStorage();
                }
                return setupStorage;
            }
        }

        public bool UserExit
        {
            get { return userExit; }
            set
            {
                userExit = value;
                ChangeUserExit?.Invoke(userExit);
            }
        }

        private SetupStorage()
        { }
    }
}
