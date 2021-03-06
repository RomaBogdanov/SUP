﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

using System.Security.Principal;
using System.Diagnostics;
using System.Reflection;

using SupContract;
using SupHost.Connectors;
using SupHost.Andover;

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
            WindowsPrincipal p = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            
            bool hasAdmRights = p.IsInRole(WindowsBuiltInRole.Administrator);
            if (!hasAdmRights)
            {
                ProcessStartInfo procInfo = new ProcessStartInfo();
                procInfo.Verb = "runas";
                procInfo.FileName = Assembly.GetExecutingAssembly().Location;
                Process.Start(procInfo);
                Process.GetCurrentProcess().Close();
                return;
            }

            Connector con = VisConnector.CurrentConnector;
            Logger logger = Logger.CurrentLogger;
            //Uri baseAddress = new Uri("http://localhost:7000/HostSUP/");
            //ServiceHost host = new ServiceHost(typeof(TableService1), baseAddress);
            ServiceHost host = new ServiceHost(typeof(TableService1));
            
            try
            {
                /*host.AddServiceEndpoint(typeof(ITableService), 
                    new WSHttpBinding(), "TableService");
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                host.Description.Behaviors.Add(smb);
                host.Open();*/
                //ServiceHost host = new ServiceHost(typeof(TableService1));
                host.Open();
                logger.Info(host.BaseAddresses[0].AbsoluteUri);
                while (true)
                {
                    Console.WriteLine("1. Нажмите <ENTER> для закрытия сервера.");
                    Console.WriteLine("2. Нажмите v для проверки соединения с " +
                        "базой данных Visitors");
                    Console.WriteLine("3. Нажмите a для проверки соединения с " +
                        "AndoverAgent");
                    string mes = Console.ReadLine();
                    if (mes == "")
                    {
                        break;
                    }
                    if (mes == "v")
                    {
                        if (con.ConnectionAttempt())
                        {
                            logger.Info("Соединение с БД нормальное");
                        }
                        else
                        {
                            logger.Warn("Соединение с БД настроено неправильно");
                        }
                        continue;
                    }
                    if (mes == "a")
                    {
                        var andoverManager = new AndoverManager(null);
                        if (andoverManager.Ping())
                        {
                            logger.Info("Соединение с AndoverAgent нормальное");
                        }
                        else
                        {
                            logger.Warn("Соединение с AndoverAgent настроено неправильно");
                        }
                        continue;
                    }
                }
                host.Close();
            }
            catch (Exception err)
            {
                logger.Error(err.Message);
                host.Abort();
                Console.ReadLine();
            }
        }
    }
}
