using System;
using System.Configuration;
using System.Data.SqlClient;
using System.ServiceModel;

namespace AndoverAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceHost = new ServiceHost(typeof(AndoverService));

            try
            {
                serviceHost.Open();
                Console.WriteLine("Агент запущен.\n\n");
                while (true)
                {
                    Console.WriteLine("1. Нажмите <ENTER> для закрытия агента.");
                    Console.WriteLine("2. Нажмите v для проверки соединения с " +
                        "базой данных Continuum");
                    string mes = Console.ReadLine();
                    if (mes == "")
                    {
                        break;
                    }
                    if (mes == "v")
                    {
                        try
                        {
                            using (var connection = new SqlConnection(
                                ConfigurationManager.
                                ConnectionStrings["Continuum"].ConnectionString))
                            {
                                connection.Open();
                                connection.Close();
                                Console.WriteLine("Соединение с БД нормальное");
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Соединение с БД настроено неправильно");
				Console.WriteLine(ConfigurationManager.
					ConnectionStrings["Continuum"].ConnectionString);
                        }
                        continue;
                    }
                }
                serviceHost.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                serviceHost.Abort();
                Console.ReadLine();
            }
        }
    }
}
