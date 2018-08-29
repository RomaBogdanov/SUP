using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using SupRealClient.Common;
using SupRealClient.EnumerationClasses;

namespace SupRealClient.Models.Helpers
{
	public static class OrderElementsHelper
	{
		public const string OnlyZonesPassesString = "Назначены зоны доступа";
		public const string BothPassesString = "+ зоны доступа";
		public const string NoPassesString = "Доступ не назначен";

		/// <summary>
		/// Проверяет корректность и полноту данных элемента заявки
		/// </summary>
		/// <param name="orderElement"></param>
		/// <param name="errorMessage">Если данные некорректные, содержит в себе строку ошибки</param>
		/// <param name="isVirtueOrder"></param>
		/// <returns></returns>
		public static bool IsOrderElementDataCorrect(this OrderElement orderElement, out string errorMessage, bool isVirtueOrder = false)
        {
            if (orderElement.VisitorId == 0)
            {
                errorMessage = "Не выбран посетитель.";
                return false;
            }

            if (orderElement.OrganizationId == 0)
            {
                errorMessage = "Не выбрана организация.";
                return false;
            }

            //if (string.IsNullOrEmpty(orderElement.Position))
            //{
            //    errorMessage = "Не указана должность.";
            //    return false;
            //}

			//if (!isVirtueOrder && string.IsNullOrEmpty(orderElement.Catcher))
			//{
			//	errorMessage = "Не указано принимающее лицо.";
			//  return false;
			//}

			if (string.IsNullOrEmpty(orderElement.Passes) || orderElement.Passes == OrderElementsHelper.NoPassesString)
			{
				errorMessage = "Необходимо назначить доступ. Поле \"Проходы\".";
				return false;
			}

			if (orderElement.From > orderElement.To)
            {
                errorMessage = " \"Время от\" не может быть позже, чем \"Время до\"";
                return false;
            }

            //if (!CommonHelper.IsPositionCorrect(orderElement.Position))
            //{
            //    errorMessage = "Неверно введена должность.";
            //    return false;
            //}

            errorMessage = null;
            return true;
        }
	}
}
