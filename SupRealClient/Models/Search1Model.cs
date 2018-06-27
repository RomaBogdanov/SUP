using SupRealClient.Common.Interfaces;
using SupRealClient.Common.Data;
using SupRealClient.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;

namespace SupRealClient.Models
{
	class Search1Model : ISearch1Model
	{
		public event Action OnClose;
        ISearchHelper searchHelper;
		SearchResult result = new SearchResult();
        
		public void SetSearchHelper(ISearchHelper _searchHelper)
		{
			searchHelper = _searchHelper;            
        }

		public IDictionary<string, string> GetFields()
		{
			return searchHelper.GetFields();
		}

		public bool Find(SearchData searchData)
		{
			bool findResult = false;
			if (searchData.Field != "" && searchData.Text != "")
			{
				result = new SearchResult();
				var context = new SearchContext();
				context.SetStrategy(CompareStrategyFactory.Create(searchData.Register,
                    searchData.Equal, searchData.StartWith, searchData.Contains));

                object set = searchHelper.GetType().GetProperty(@"Set")?.GetValue(searchHelper, null);
                if (set != null)
                {
                    IEnumerable enumerable = set as IEnumerable;
                    if (enumerable != null)
                    {
                        foreach (object element in enumerable)
                        {                            
                            object obj = element.GetType().GetProperty(searchData.Field)?.GetValue(element, null);
                            if (obj != null && context.Execute(obj, searchData.Text))
                            {
                                object id = element.GetType().GetProperty(@"Id")?.GetValue(element, null);
                                if (id is int)
                                {
                                    findResult = true;
                                    result.Add((int)id);
                                }                                    
                            }
                        }
                    }
                }

    //            var table = searchHelper.Rows;
    //            for (int i = 0; i < table.Length; i++)
				//{
    //                if (context.Execute(table[i].Field<object>(searchData.Field),
    //                    searchData.Text))
				//	{
				//		findResult = true;
				//		result.Add(searchHelper.GetId(i));
				//	}
    //            }                

                Begin();
			}

			return findResult;
		}

		public void Begin()
		{
			searchHelper.SetAt(result.Begin());
		}

		public void Next()
		{
			searchHelper.SetAt(result.Next());
		}

		public void Cancel()
		{
			OnClose?.Invoke();
		}
	}
}
