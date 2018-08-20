using System.ComponentModel;
using System.Runtime.CompilerServices;
using SupRealClient.Annotations;

namespace SupRealClient.ViewModels
{
	public class CAndoverExportViewModel : INotifyPropertyChanged
	{
		private bool? _isAccessPointsSelect = true;
		private bool? _isAreasSelect = true;
		private bool? _isCardsSelect = true;
		private bool? _isSchedulesSelect = true;
		private bool? _isAllSelect = true;

		public bool? IsAccessPointsSelect
		{
			get { return _isAccessPointsSelect; }
			set
			{
				if (value == null)
				{
					return;
				}

				_isAccessPointsSelect = value;
				UpdateSelect_AllFieldsCheckBox();
				OnPropertyChanged(nameof(IsAccessPointsSelect));
			}
		}

		public bool? IsAreasSelect
		{
			get { return _isAreasSelect; }
			set
			{
				if (value == null)
				{
					return;
				}
				_isAreasSelect = value;
				UpdateSelect_AllFieldsCheckBox();
				OnPropertyChanged(nameof(IsAreasSelect));
			}
		}

		public bool? IsCardsSelect
		{
			get { return _isCardsSelect; }
			set
			{
				if (value == null)
				{
					return;
				}
				_isCardsSelect = value;
				UpdateSelect_AllFieldsCheckBox();
				OnPropertyChanged(nameof(IsCardsSelect));
			}
		}

		public bool? IsSchedulesSelect
		{
			get { return _isSchedulesSelect; }
			set
			{
				if (value == null)
				{
					return;
				}
				_isSchedulesSelect = value;
				UpdateSelect_AllFieldsCheckBox();
				OnPropertyChanged(nameof(IsSchedulesSelect));
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
			_isAccessPointsSelect = IsAllSelect;
			_isAreasSelect = IsAllSelect;
			_isCardsSelect = IsAllSelect;
			_isSchedulesSelect = IsAllSelect;


			OnPropertyChanged(nameof(IsAccessPointsSelect));
			OnPropertyChanged(nameof(IsAreasSelect));
			OnPropertyChanged(nameof(IsCardsSelect));
			OnPropertyChanged(nameof(IsSchedulesSelect));
		}

		private void UpdateSelect_AllFieldsCheckBox()
		{
			_isAllSelect = IsAccessPointsSelect.Value && IsAreasSelect.Value
							  && IsCardsSelect.Value && IsSchedulesSelect.Value;

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

