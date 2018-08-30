using SupRealClient.Views;
using System.Collections.ObjectModel;

namespace SupRealClient.EnumerationClasses
{
    public class VisitorsDocument : VisitorsDocumentBase
    {
        public string Name { get; set; }

	    public bool IsCanAddChanges { get; set; } = true;

	    public override string ToString()
        {
            return Name;
        }

		public static bool Detecting_CanAddChanges(ObservableCollection<VisitorsMainDocument> mainDocumentCollection, string nameDocument)
		{
			if (nameDocument == VisitsViewModel._nameDocument_PhotoImageType)
				return false;

			if (nameDocument == VisitsViewModel._nameDocument_SignatureImageType)
				return false;

			foreach (var item in mainDocumentCollection)
			{
				if (item.DocumentName == nameDocument)
					return false;
			}

			return true;
		}

	}
}
