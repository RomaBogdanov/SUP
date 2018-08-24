using System;
using System.ComponentModel;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
    public class IssuedCardsMessageBoxViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event Action OnClose;

        public string Message { get; private set; }

        public ICommand Ok { get; set; }

        public ICommand Extradire { get; set; }

        public ICommand Cancel { get; set; }

        public int Result { get; private set; }

        public IssuedCardsMessageBoxViewModel(string name)
        {
            Message = "У посетителя " + name + " на руках уже есть пропуск.\r\n" +
                "Добавить новые проходы?";
           
            this.Ok = new RelayCommand(arg => { Result = 1; OnClose?.Invoke(); });
            this.Extradire = new RelayCommand(arg => { Result = 2; OnClose?.Invoke(); });
            this.Cancel = new RelayCommand(arg => { Result = 0; OnClose?.Invoke(); });
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
    }
}
