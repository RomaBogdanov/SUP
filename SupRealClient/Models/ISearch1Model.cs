using SupRealClient.Common.Interfaces;
using SupRealClient.Common.Data;
using System;
using System.Collections.Generic;

namespace SupRealClient.Models
{
	public interface ISearch1Model
	{
		event Action OnClose;
		void SetSearchHelper(ISearchHelper searchHelper);
		bool Find(SearchData searchData);
		void Begin();
		void Next();
		void Cancel();
		IDictionary<string, string> GetFields();
	}
}
