using System;
using System.Configuration;

namespace SupClientConnectionLib
{
    public static class ClientConnectorFactory
    {
        private static IClientConnector connector;

        public static Uri Uri { get; private set; }

        public static IClientConnector CurrentConnector
        {
            get
            {
                if (connector == null)
                {
                    connector = CreateClientConnector();
                    return connector;
                }
                return connector;
            }
        }

        public static IClientConnector ResetConnector(Uri uri)
        {
            ClientConnectorFactory.Uri = uri;
            connector = CreateClientConnector();
            return connector;
        }

        public static IClientConnector CreateClientConnector()
        {
            string className = ConfigurationManager.AppSettings["IClientConnector"];
            if (className == "ClientConnector2")
            {
                return new ClientConnector2();
            }

            return new ClientConnector();
    }
    }
}
