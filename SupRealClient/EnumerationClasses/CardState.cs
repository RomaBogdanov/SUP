using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupRealClient.EnumerationClasses
{
	/// <summary>
	/// Состояние карты
	/// </summary>
	public enum CardState
	{
		/// <summary>
		/// Неизвестное состояние
		/// </summary>
		Unknown,
		/// <summary>
		/// Карта активна
		/// </summary>
		Active,
		/// <summary>
		/// Карта неактивна или невыдана
		/// </summary>
		Inactive,
		/// <summary>
		/// Карта выдана
		/// </summary>
		Issued,
		/// <summary>
		/// Карта утеряна
		/// </summary>
		Lost,
		/// <summary>
		/// Карта возвращена
		/// </summary>
		Returnded
	}
}
