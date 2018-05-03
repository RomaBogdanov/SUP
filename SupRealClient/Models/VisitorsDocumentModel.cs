using SupRealClient.EnumerationClasses;
using System;

namespace SupRealClient.Models
{
    public class VisitorsDocumentModel
    {
        private VisitorsDocument visitorsDocument;

        public VisitorsDocument Data
        {
            get
            {
                return new VisitorsDocument
                {
                    Id = visitorsDocument.Id,
                    Name = visitorsDocument.Name,
                    Images = visitorsDocument.Images,
                };
            }
        }

        public event Action<object> OnClose;

        public VisitorsDocumentModel(VisitorsDocument visitorsDocument)
        {
            this.visitorsDocument = visitorsDocument ?? new VisitorsDocument();
        }

        public void Cancel()
        {
            OnClose?.Invoke(null);
        }

        public void Ok(VisitorsDocument data)
        {
            if (string.IsNullOrEmpty(data.Name.Trim()))
            {
                Cancel();
            }
            else
            {
                OnClose?.Invoke(data);
            }
        }
    }
}
