using SupClientConnectionLib;
using SupContract;
using SupRealClient.Annotations;
using SupRealClient.Common;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace SupRealClient.Andover
{
    public class AndoverTestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Card> set;
        private ObservableCollection<AccessPointEx> zones;
	private ObservableCollection<Schedule> _schedulesList;


	private CardsWrapper cardsWrapper = CardsWrapper.CurrentTable();
        private AccessPointsWrapper accessPointsWrapper = AccessPointsWrapper.CurrentTable();
	private SchedulesWrapper _schedulesWrapper = SchedulesWrapper.CurrentTable();



	private Card currentItem;
        private int selectedIndex;

        public AndoverTestViewModel()
        {
            Ok = new RelayCommand(obj => OkCom());

            cardsWrapper.OnChanged += Query;
            accessPointsWrapper.OnChanged += Query;
            Query();
            Begin();
        }

        public ICommand Ok { get; set; }

        public ObservableCollection<Card> Set
        {
            get { return set; }
            set
            {
                set = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AccessPointEx> Zones
        {
            get { return zones; }
            set
            {
                zones = value;
                OnPropertyChanged();
            }
        }
	    public ObservableCollection<Schedule> SchedulesList
	    {
		    get { return _schedulesList; }
		    set
		    {
			    _schedulesList = value;
			    OnPropertyChanged();
		    }
	    }

		public Card CurrentItem
        {
            get { return currentItem; }
            set
            {
                if (value != null)
                {
                    currentItem = value;
                    OnPropertyChanged();
                }
            }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged();
            }
        }

        public void Begin()
        {
            if (Set.Count > 0)
            {
                SelectedIndex = 0;
                CurrentItem = Set[SelectedIndex];
            }
            else
            {
                SelectedIndex = -1;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Query()
        {
            int oldIndex = SelectedIndex;

            DoQuery();

            if (oldIndex >= 0 && oldIndex < Set.Count - 1)
            {
                SelectedIndex = oldIndex;
                CurrentItem = Set[SelectedIndex];
            }
            else if (Set.Count > 0)
            {
                CurrentItem = Set[0];
            }
            OnPropertyChanged("CurrentItem");
        }

        protected void DoQuery()
        {
            Set = new ObservableCollection<Card>(
                from c in cardsWrapper.Table.AsEnumerable()
                where c.Field<int>("f_card_id") != 0 && !string.Equals(c.Field<string>("f_deleted").Trim().ToLower(), "y") &&
		CommonHelper.NotDeleted(c)
                select new Card
                {
                    Id = c.Field<int>("f_card_id"),
                    CurdNum = c.Field<int>("f_card_num"),
                    Name = c.Field<string>("f_card_name"),
                    Comment = c.Field<string>("f_comment"),
		
                });

            Zones = new ObservableCollection<AccessPointEx>(
                from accpnt in accessPointsWrapper.Table.AsEnumerable()
                where accpnt.Field<int>("f_access_point_id") != 0 && !string.Equals(accpnt.Field<string>("f_deleted").Trim().ToLower(), "y") &&
                CommonHelper.NotDeleted(accpnt)
                select new AccessPointEx
                {
                    Id = accpnt.Field<int>("f_access_point_id"),
                    Name = accpnt.Field<string>("f_access_point_name"),
                    Descript = accpnt.Field<string>("f_access_point_description"),
                    SpaceIn = accpnt.Field<string>("f_access_point_space_in"),
                    SpaceOut = accpnt.Field<string>("f_access_point_space_out"),
                    Path = accpnt.Field<string>("f_access_point_path"),
                });

	        SchedulesList = new ObservableCollection<Schedule>(
		        from sched in _schedulesWrapper.Table.AsEnumerable()
		        where sched.Field<int>("f_schedule_id") != 0 && !string.Equals(sched.Field<string>("f_deleted").Trim().ToLower(), "y") &&
		              CommonHelper.NotDeleted(sched)
		        select new Schedule
		        {
			        Id = sched.Field<int>("f_schedule_id"),
			        Name = sched.Field<string>("f_schedule_name"),
			        Path = sched.Field<string>("f_schedule_path")
			});
		}

        private void OkCom()
        {
            Card card = CurrentItem;
            if (card == null)
            {
                return;
            }

            List<AccessPointEx> accesPoints = Zones.Where(z => z.IsChecked).ToList();

            var data = new AndoverExportData
            {
                Card = card.Name,
                Doors = accesPoints.Select(p => p.Name).ToList(),
		Schedules = SchedulesList.Select(x=>x.Path).ToList()
            };

            ClientConnector clientConnector = ClientConnector.CurrentConnector;
            if (clientConnector.ExportToAndover(data))
            {
                MessageBox.Show("Пропуск был выгружен в Andover", "",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Выгрузка пропуска в Andover не удалась!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public class AccessPointEx : AccessPoint
        {
            public bool IsChecked { get; set; }
            public string Path { get; set; }
        }
    }
}
