using System.Collections.Generic;
using System.Linq;

namespace SupRealClient.Search
{
	/// <summary>
	/// Результаты поиска
	/// </summary>
	public class SearchResult
	{
		List<long> indexes = new List<long>();
		int current = -1;

		public void Add(long index)
		{
			current = 0;
			indexes.Add(index);
		}

		/// <summary>
		/// Перейти к первому найденному элементу
		/// </summary>
		public long Begin()
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
		public long Next()
		{
			if (indexes.Any())
			{
                if (current < indexes.Count - 1)
                {
                    current++;
                }
                else
                    current = 0;

				return indexes[current];
			}

			return -1;
		}

        public bool Any()
        {
            return indexes.Count > 0;
        }
	}
}
