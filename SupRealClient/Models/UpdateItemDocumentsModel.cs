using System;
using System.Data;
using System.Windows;
using SupClientConnectionLib;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common.Data;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
	/// <summary>
	/// Обновление документа - модель
	/// </summary>
	class UpdateItemDocumentsModel : IAddItem1Model
	{
		private Document document;

		public FieldData Data
		{
			get { return new FieldData {Field = document.DocName}; }
		}

		public event Action OnClose;

		public UpdateItemDocumentsModel(Document document)
		{
			this.document = document;
		}

		public void Ok(FieldData data)
		{

			DocumentsWrapper documents = DocumentsWrapper.CurrentTable();
			if (!string.IsNullOrWhiteSpace(data.Field))
			{
				DataRow row = documents.Table.Rows.Find(document.Id);
				row["f_doc_name"] = data.Field;
				row["f_deleted"] = "N";
				row["f_rec_date"] = DateTime.Now;
				row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
				row.EndEdit();

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