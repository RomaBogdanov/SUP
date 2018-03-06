using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

/// <summary>
/// Пространство имён с информацией по контрактам между хостом и клиентами
/// в SUP.
/// </summary>
namespace SupContract
{
    /// <summary>
    /// Контракт для синхронизации логических таблиц сервера и клиента.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(ITableCallback))]
    public interface ITableService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        /// <summary>
        /// Процедура получения клиентом таблицы с данными.
        /// </summary>
        /// <param name="composite">Данные для идентификации таблицы.</param>
        /// <returns>Таблица с данными.</returns>
        [OperationContract]
        DataTable GetTable(CompositeType composite, string login);

        /// <summary>
        /// Процедура передачи серверу строки с новыми данными.
        /// </summary>
        /// <param name="composite">Данные для идентификации таблицы.</param>
        /// <param name="objs">строки с данными.</param>
        /// <returns></returns>
        [OperationContract]
        bool InsertRow(CompositeType composite, object[] objs, string login);

        /// <summary>
        /// Процедура передачи серверу строки с изменёнными данными.
        /// </summary>
        /// <param name="composite">Данныме для идентификации таблицы.</param>
        /// <param name="objs">строки с данными.</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateRow(CompositeType composite, int rowNumber, object[] objs, string login);

        /// <summary>
        /// Процедура удаления с сервера строки с изменёнными данными.
        /// </summary>
        /// <param name="composite">Данные для идентификации таблицы.</param>
        /// <param name="rowNumber">Номер строки.</param>
        /// <param name="objs">строки с данными.</param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteRow(CompositeType composite, object[] row, string login);

        /// <summary>
        /// Процедура получения изображения в виде набора байтов.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] GetImage(int id, string login);

        /// <summary>
        /// Процедура авторизации.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [OperationContract]
        bool Authorize(string login, string pass);

        /// <summary>
        /// Процедура выхода из профиля.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [OperationContract]
        bool ExitAuthorize(string login);
    }

    // Используйте контракт данных, как показано на следующем примере, чтобы добавить сложные типы к сервисным операциям.
    // В проект можно добавлять XSD-файлы. После построения проекта вы можете напрямую использовать в нем определенные типы данных с пространством имен "SupContract.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        TableName tableName = TableName.TestTable1;
        //DataRow row = null;

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }

        [DataMember]
        public TableName TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
    }
}
