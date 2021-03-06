﻿using System;
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
        DataTable GetTable(CompositeType composite, OperationInfo info);

        /// <summary>
        /// Процедура передачи серверу строки с новыми данными.
        /// </summary>
        /// <param name="composite">Данные для идентификации таблицы.</param>
        /// <param name="objs">строки с данными.</param>
        /// <returns></returns>
        [OperationContract]
        bool InsertRow(CompositeType composite, object[] objs, OperationInfo info);

        /// <summary>
        /// Процедура передачи серверу строки с изменёнными данными.
        /// </summary>
        /// <param name="composite">Данныме для идентификации таблицы.</param>
        /// <param name="objs">строки с данными.</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateRow(CompositeType composite, int rowNumber, object[] objs, OperationInfo info);

        /// <summary>
        /// Процедура удаления с сервера строки с изменёнными данными.
        /// </summary>
        /// <param name="composite">Данные для идентификации таблицы.</param>
        /// <param name="rowNumber">Номер строки.</param>
        /// <param name="objs">строки с данными.</param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteRow(CompositeType composite, object[] row, OperationInfo info);

        /// <summary>
        /// Процедура получения изображения в виде набора байтов.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] GetImage(Guid alias, OperationInfo info);

        /// <summary>
        /// Процедура загрузки изображения в базу.
        /// </summary>
        /// <returns></returns>
        [OperationContract(IsOneWay = true)]
        void SetImages(Dictionary<Guid, byte[]> images, OperationInfo info);

        /// <summary>
        /// Процедура авторизации.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns>user Id</returns>
        [OperationContract]
        int Authorize(OperationInfo info, string pass);

        /// <summary>
        /// Проверка соединения с сервером.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [OperationContract]
        string Ping(OperationInfo info);

        /// <summary>
        /// Процедура выхода из профиля.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [OperationContract]
        bool ExitAuthorize(OperationInfo info);

        /// <summary>
        /// Загрузить данные из Andover
        /// </summary>
        /// <param name="info"></param>
        [OperationContract]
        bool ImportFromAndover(OperationInfo info);

        /// <summary>
        /// Выгрузить данные в Andover
        /// </summary>
        /// <param name="info"></param>
        [OperationContract]
        bool ExportToAndover(AndoverExportData data, OperationInfo info);
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

    [DataContract]
    public class OperationInfo
    {
        int id;
        string user;
        string machine;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string User
        {
            get { return user; }
            set { user = value; }
        }

        [DataMember]
        public string Machine
        {
            get { return machine; }
            set { machine = value; }
        }
    }

    [DataContract]
    public class AndoverExportData
    {
        string card;
        List<string> doors;
        List<string> schedules;

        [DataMember]
        public string Card
        {
            get { return card; }
            set { card = value; }
        }

        [DataMember]
        public List<string> Doors
        {
            get { return doors; }
            set { doors = value; }
        }

        [DataMember]
        public List<string> Schedules
        {
            get { return schedules; }
            set { schedules = value; }
        }
    }
}
