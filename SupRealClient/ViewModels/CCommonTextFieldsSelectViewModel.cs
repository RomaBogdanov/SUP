using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SupRealClient.Annotations;

namespace SupRealClient.ViewModels
{
	public class CCommonTextFieldsSelectViewModel : INotifyPropertyChanged
	{
		private bool? _isSurnameSelect = true;
		private bool? _isNameSelect = true;
		private bool? _isPatronymicSelect = true;
		private bool? _isBirthDateSelect = true;
		private bool? _isPortraitSelect = true;
		private bool? _isAllSelect = true;

		public bool? IsSurnameSelect
		{
			get { return _isSurnameSelect; }
			set {
				if (value==null)
				{
					return;
				}

					_isSurnameSelect = value;
					UpdateSelect();
					OnPropertyChanged(nameof(IsSurnameSelect));
			}
		}

		public bool? IsNameSelect
		{
			get { return _isNameSelect; }
			set
			{
				if (value == null)
				{
					return;
				}
				_isNameSelect = value;
					UpdateSelect();
					OnPropertyChanged(nameof(IsNameSelect));
			}
		}

		public bool? IsPatronymicSelect
		{
			get { return _isPatronymicSelect; }
			set
			{
				if (value == null)
				{
					return;
				}
				_isPatronymicSelect = value;
					UpdateSelect();
					OnPropertyChanged(nameof(IsPatronymicSelect));
			}
		}

		public bool? IsBirthDateSelect
		{
			get { return _isBirthDateSelect; }
			set
			{
				if (value == null)
				{
					return;
				}
				_isBirthDateSelect = value;
					UpdateSelect();
					OnPropertyChanged(nameof(IsBirthDateSelect));
			}
		}

		public bool? IsPortraitSelect
		{
			get { return _isPortraitSelect; }
			set
			{
				if (value == null)
				{
					return;
				}
				_isPortraitSelect = value;
					UpdateSelect();
					OnPropertyChanged(nameof(IsPortraitSelect));
			}
		}

		public bool? IsAllSelect
		{
			get { return _isAllSelect; }
			set
			{

				_isAllSelect = value.Value;
				UpdateSelect(true);
				OnPropertyChanged(nameof(IsAllSelect));
			}
		}

		private void UpdateSelect(bool isAllCheck = false)
		{
			if (isAllCheck)
			{
				IsSurnameSelect = IsAllSelect;
				IsNameSelect = IsAllSelect;
				IsBirthDateSelect = IsAllSelect;
				IsPatronymicSelect = IsAllSelect;
				IsPortraitSelect = IsAllSelect;
			}
		}
		

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
