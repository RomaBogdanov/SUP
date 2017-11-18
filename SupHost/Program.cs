using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

using SupContract;

/// <summary>
/// Хост суп предназначен для:
/// 1. транзита информации между клиентами и БД;
/// 2. синхронизации данных от клиентов;
/// 3. Обсчёта данных (при необходимости).
/// </summary>
/// <remarks>
/// Принцип работы хоста:
/// <br>В классе Program создана сервисная WCF-служба, которая работает по 
/// контракту с клиентами.</br>
/// <br>Контракт на стороне сервера реализован через класс TableService1.</br>
/// <br>TableService1 работает с классами-наследниками класса 
/// AbstractTableWrapper, которые инкапсюлируют внутри таблицы DataTable.</br>
/// <br>Наследники AbstractTableWrapper реализуют взаимодействие с базами 
/// данных с помощью паттерна "Стратегия", подключая наследников интерфейса
/// ITableBehavior, которые реализуют алгоритмы взаимодействия с БД.</br>
/// <br>С БД Visitors организует алгоритм взаимодействия класс Connector, 
/// который реализует паттерн "Синглтон".</br>
/// </remarks>
namespace SupHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:7000/HostSUP/");
            ServiceHost host = new ServiceHost(typeof(TableService1), baseAddress);
            try
            {
                host.AddServiceEndpoint(typeof(ITableService), 
                    new WSHttpBinding(), "TableService");
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                host.Description.Behaviors.Add(smb);
                host.Open();
                Console.WriteLine("Нажмите <ENTER> для закрытия хоста.");
                Console.ReadLine();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                host.Abort();
            }
            Console.ReadLine();
        }
    }
}
