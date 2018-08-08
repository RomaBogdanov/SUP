using System;
using System.Data;
using System.Windows;
using SupClientConnectionLib;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
	/// <summary>
	/// Добавление документа - модель
	/// </summary>
	public class AddItemDocumentsModel : IAddItem1Model
	{
		public event Action OnClose;

		public FieldData Data
		{
			get { return new FieldData(); }
		}

		public void Ok(FieldData data)
		{
			DocumentsWrapper documents = DocumentsWrapper.CurrentTable();
			if (!string.IsNullOrWhiteSpace(data.Field))
			{
				DataRow row = documents.Table.NewRow();
				row["f_doc_name"] = data.Field.TrimStart();
				row["f_deleted"] = "N";
				row["f_rec_date"] = DateTime.Now;
				row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
				documents.Table.Rows.Add(row);

				Cancel();
			}
			else
			{
				MessageBox.Show($"Введите корректное имя документа\nИмя документа не может быть пустым, не должно содержать только пробелы", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}

		public void Cancel()
		{
			OnClose?.Invoke();
		}
	}
}