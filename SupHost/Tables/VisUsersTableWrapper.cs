using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupHost.Data;

namespace SupHost
{
    /// <summary>
    /// Это особенная таблица. Она не должна передаваться пользователям,
    /// потому что содержит конфиденциальную информацию. Может использоваться
    /// только внутри сервера приложения.
    /// </summary>
    class VisUsersTableWrapper : AbstractTableWrapper
    {
        static VisUsersTableWrapper visUsersTableWrapper;

        public static VisUsersTableWrapper GetVisUsersTableWrapper()
        {
            if (visUsersTableWrapper == null)
            {
                visUsersTableWrapper = new VisUsersTableWrapper();
            }
            return visUsersTableWrapper;
        }

        private VisUsersTableWrapper()
        {
            this.getTableBehavior = new VisUsersTableBehavior();
        }

        public int ExistingLogin(string login, string pass)
        {
            // Делаем постоянное обновление данных из базы данных на случай,
            // если туда внесли новые.
            this.GetTable();
            var a = from i in this.table.AsEnumerable()
                    where i["f_user"].ToString() == login
                    select i;
            if (a.Count() == 0)
            {
                this.logger.Warn($@"Попытка зарегистрироваться под 
                    несуществующим аккаунтом {login}");
                return -1;
            }
            if (a.Count() > 1)
            {
                this.logger.Error($"Количество аккаунтов {login} больше одного!");
                return -1;
            }
            if (a.ElementAt(0)["f_pass"].ToString() == pass)
                return (int)a.ElementAt(0)["f_user_id"];
            this.logger.Warn($@"Попытка зарегистрироваться под 
                    аккаунтом {login}. Ввод неправильного пароля.");
            return -1;
        }

        public UserData GetUserTimeoutData(int id)
        {
            this.GetTable();
            var a = from i in this.table.AsEnumerable()
                    where (int)i["f_user_id"] == id
                    select i;

            var user = a.FirstOrDefault();

            return user == null ? null :
                new UserData
                {
                    Id = (int)user["f_user_id"],
                    Name = (string)user["f_user"]
                };
        }
    }
}
