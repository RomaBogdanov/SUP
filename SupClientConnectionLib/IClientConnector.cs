using SupContract;
using System;
using System.Collections.Generic;
using System.Data;

namespace SupClientConnectionLib
{
    public interface IClientConnector
    {
        event Action<string, object[]> OnInsert;
        event Action<string, int, object[]> OnUpdate;
        event Action<string, object[]> OnDelete;

        int Authorize(string login, string pass);

        bool Ping();

        bool ExitAuthorize();

        int GetDocumentTypeId(string documentType, int operatorId);

        bool ImportFromAndover(string tables);

        CExtraditionContract ExportToAndover(AndoverExportData data);

        DataTable GetTable(TableName tableName);

        bool InsertRow(object[] rowValues);

        bool UpdateRow(object[] rowValues, int numRow);

        bool DeleteRow(object[] objs);

        byte[] GetImage(Guid alias);

        bool SetImages(Dictionary<Guid, byte[]> images);
    }
}
