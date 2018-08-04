using AndoverLib;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Xml;

namespace AndoverExportTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DoWork();
                Console.WriteLine("SUCCESS");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        static void DoWork()
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
                new EndpointAddress("http://localhost:7001/AndoverHost"));
            IAndoverService wcfClient = myChannelFactory.CreateChannel();

            var persons = new List<Personnel>
            {
                new Personnel
                {
                    FirstName = "5Abcdefg",
                    LastName = "6Hijklmn",
                },
                new Personnel
                {
                    FirstName = "7Opqrstu",
                    LastName = "8Vwxyz",
                }
            };
            wcfClient.ExportPersons(persons);
        }
    }
}
