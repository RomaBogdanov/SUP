using System;

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
                    // TODO - создавать разные коннекторы
                    connector = new ClientConnector();
                    return connector;
                }
                return connector;
            }
        }

        public static IClientConnector ResetConnector(Uri uri)
        {
            ClientConnectorFactory.Uri = uri;
            // TODO - создавать разные коннекторы
            connector = new ClientConnector();
            return connector;
        }
    }
}
