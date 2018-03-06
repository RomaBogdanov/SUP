using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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

        public bool ExistingLogin(string login, string pass)
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
                return false;
            }
            if (a.Count() > 1)
            {
                this.logger.Error($"Количество аккаунтов {login} больше одного!");
                return false;
            }
            if (a.ElementAt(0)["f_password"].ToString() == pass) return true;
            this.logger.Warn($@"Попытка зарегистрироваться под 
                    аккаунтом {login}. Ввод неправильного пароля.");
            return false;
        }
    }
}
