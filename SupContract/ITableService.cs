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
    [ServiceContract]
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
        DataTable GetTable(CompositeType composite);

        /// <summary>
        /// Процедура передачи серверу строк с новыми данными.
        /// </summary>
        /// <param name="composite">Данные для идентификации таблицы.</param>
        /// <param name="rows">строки с данными.</param>
        /// <returns></returns>
        [OperationContract]
        bool InsertRows(CompositeType composite, object[] objs);
    }

    // Используйте контракт данных, как показано на следующем примере, чтобы добавить сложные типы к сервисным операциям.
    // В проект можно добавлять XSD-файлы. После построения проекта вы можете напрямую использовать в нем определенные типы данных с пространством имен "SupContract.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        TableName tableName = TableName.TestTable1;
        DataRow row = null;

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
