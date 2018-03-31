using System.Collections.Generic;
using System.Linq;

namespace SupRealClient.Search
{
	/// <summary>
	/// Результаты поиска
	/// </summary>
	class SearchResult
	{
		List<int> indexes = new List<int>();
		int current = -1;

		public void Add(int index)
		{
			current = 0;
			indexes.Add(index);
		}

		/// <summary>
		/// Перейти к первому найденному элементу
		/// </summary>
		public int Begin()
		{
			if (indexes.Any())
			{
				current = 0;
				return indexes[current];
			}

			return -1;
		}

		/// <summary>
		/// Перейти к следующему найденному элементу
		/// </summary>
		public int Next()
		{
			if (indexes.Any())
			{
				if (current < indexes.Count - 1)
				{
					current++;
				}
				return indexes[current];
			}

			return -1;
		}
	}
}
