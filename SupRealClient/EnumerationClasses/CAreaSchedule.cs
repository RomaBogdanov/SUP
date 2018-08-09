using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using SupRealClient.Annotations;

namespace SupRealClient.EnumerationClasses
{
	public class CAreaSchedule:INotifyPropertyChanged
	{
		public string AreaName { get; set; }

		public string ScheduleName { get; set; }

		public int AreaIdHi { get; }
		public int AreaIdLo { get; }
		public int ScheduleId { get; set; }
		
		public bool IsDeletedFull { get; set; }
		public bool IsDeletedArea { get; set; }

		public CAreaSchedule(int areaIdHi, int areaIdLo, int scheduleId)
		{
			AreaIdHi = areaIdHi;
			AreaIdLo = areaIdLo;
			ScheduleId = scheduleId;
			SelectedItemIndex = 0;
			UpdateVisibleData();
		}
		public CAreaSchedule(string areaIdHi, string areaIdLo, int scheduleId)
		{
			try
			{
				AreaIdHi = int.Parse(areaIdHi);
				AreaIdLo = int.Parse(areaIdLo);
			}
			catch (Exception e)
			{
				MessageBox.Show(nameof(CAreaSchedule) + e.Message);
			}
			
			ScheduleId = scheduleId;
			SelectedItemIndex = 0;
			UpdateVisibleData();
		}

		private int _selectedItemIndex = -1;
		public int SelectedItemIndex
		{
			get { return _selectedItemIndex; }
			set
			{
				_selectedItemIndex = value;
				OnPropertyChanged(nameof(SelectedItemIndex));
				UpdateVisibleData();
			}
		}
		
		public object TestString
		{
			get { return SchedulesFromSameCAreaSchedules.ToList()[_selectedItemIndex]; }
		}

		public bool VisibleList
		{
			get { return SchedulesFromSameCAreaSchedules.Count() > 1; }
		}

		public bool InverceVisibleList
		{
			get { return !VisibleList; }
		}

		public IEnumerable<string> SchedulesFromSameCAreaSchedules { get; set; } = new List<string>();

		public override string ToString()
		{
			return $"{AreaIdHi}/{AreaIdLo}/{ScheduleId}";
		}

		public override bool Equals(object obj)
		{
			var item = obj as CAreaSchedule;
			return item.AreaIdHi==AreaIdHi && item.AreaIdLo==AreaIdLo && item.ScheduleId==ScheduleId;
		}

		public bool AreaEquals(object obj)
		{
			var item = obj as CAreaSchedule;
			return item.AreaIdHi == AreaIdHi && item.AreaIdLo == AreaIdLo;
		}


		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void UpdateVisibleData()
		{
			OnPropertyChanged(nameof(TestString));
			OnPropertyChanged(nameof(VisibleList));
			OnPropertyChanged(nameof(InverceVisibleList));
		}
	}
}
