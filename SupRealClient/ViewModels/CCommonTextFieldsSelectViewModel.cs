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
				UpdateSelect_AllFieldsCheckBox();
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
				UpdateSelect_AllFieldsCheckBox();
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
				UpdateSelect_AllFieldsCheckBox();
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
				UpdateSelect_AllFieldsCheckBox();
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
				UpdateSelect_AllFieldsCheckBox();
					OnPropertyChanged(nameof(IsPortraitSelect));
			}
		}

		public bool? IsAllSelect
		{
			get { return _isAllSelect; }
			set
			{
				if (value == null)
				{
					return;
				}
				_isAllSelect = value.Value;
				UpdateSelect_Fields();
				OnPropertyChanged(nameof(IsAllSelect));
			}
		}

		private void UpdateSelect_Fields()
		{
			_isSurnameSelect = IsAllSelect;
			_isNameSelect = IsAllSelect;
			_isBirthDateSelect = IsAllSelect;
			_isPatronymicSelect = IsAllSelect;
			_isPortraitSelect = IsAllSelect;


			OnPropertyChanged(nameof(IsSurnameSelect));
			OnPropertyChanged(nameof(IsNameSelect));
			OnPropertyChanged(nameof(IsBirthDateSelect));
			OnPropertyChanged(nameof(IsPatronymicSelect));
			OnPropertyChanged(nameof(IsPortraitSelect));
		}

		private void UpdateSelect_AllFieldsCheckBox()
		{
			_isAllSelect = IsNameSelect.Value && IsSurnameSelect.Value
			                                  && IsPatronymicSelect.Value && IsBirthDateSelect.Value && IsPortraitSelect.Value;

			OnPropertyChanged(nameof(IsAllSelect));
		}


		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
