using SupRealClient.EnumerationClasses;
using System;

namespace SupRealClient.Models
{
    public class VisitorsMainDocumentModel
    {
        private VisitorsMainDocument visitorsMainDocument;

        public VisitorsMainDocument Data
        {
            get
            {
                return new VisitorsMainDocument
                {
                    Id = visitorsMainDocument.Id,
                    TypeId = visitorsMainDocument.TypeId,
                    Seria = visitorsMainDocument.Seria,
                    Num = visitorsMainDocument.Num,
                    Date = visitorsMainDocument.Date,
                    Org = visitorsMainDocument.Org,
                    DateTo = visitorsMainDocument.DateTo,
                    Code = visitorsMainDocument.Code,
                    Comment = visitorsMainDocument.Comment,
                    Images = visitorsMainDocument.Images,
                };
            }
        }

        public event Action<object> OnClose;

        public VisitorsMainDocumentModel(VisitorsMainDocument visitorsMainDocument)
        {
            this.visitorsMainDocument = visitorsMainDocument ??
                new VisitorsMainDocument() { Id = -1 };
        }

        public void Cancel()
        {
            OnClose?.Invoke(null);
        }

        public void Ok(VisitorsMainDocument data)
        {
            OnClose?.Invoke(data);
        }
    }
}
