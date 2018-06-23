using AndoverLib;
using System;
using System.Configuration;
using System.ServiceModel;
using System.Xml;

namespace SupHost.Andover
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class AndoverConnector
    {
        private static AndoverConnector connector;
        public IAndoverService AndoverService { get; private set; }

        public static AndoverConnector CurrentConnector
        {
            get
            {
                if (connector == null)
                {
                    connector = new AndoverConnector();
                    return connector;
                }
                return connector;
            }
        }

        public AndoverConnector()
        {
            ResetConnection();
        }

        public void ResetConnection()
        {
            var binding = new WSDualHttpBinding()
            {
                MaxReceivedMessageSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxArrayLength = 2147483647,
                    MaxStringContentLength = 2147483647
                }
            };
            var myChannelFactory = new ChannelFactory<IAndoverService>(
                binding,
                new EndpointAddress(ConfigurationManager.AppSettings["AndoverHost"]));
            AndoverService = myChannelFactory.CreateChannel();
        }
    }
}
