using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SupRealClient.Annotations;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.Common.Interfaces;

namespace SupRealClient.ViewModels.AddUpdateViewModel
{

    /// <summary>
    /// Класс-претендент на то, чтобы стать основой для всех ViewModel
    /// форм добавления-редактирования данных.
    /// </summary>
    public class AddUpdateBaseViewModel : INotifyPropertyChanged
    {
        protected AddUpdateAbstrModel model;
        protected string okCaption;
        //protected object currentItem;
        protected string title;

        public IWindow Parent { get; set; }

        public AddUpdateAbstrModel Model
        {
            get { return model; }
            set
            {
                if (model != null)
                {
                    model.OnModelPropertyChanged -= OnPropertyChanged;
                }
                model = value;
                model.Parent = Parent;
                model.OnModelPropertyChanged += OnPropertyChanged;
                OnPropertyChanged();
            }
        }

        public object CurrentItem
        {
            get { return Model.CurrentItem; }
            set
            {
                Model.CurrentItem = value;
                OnPropertyChanged();
            }
        }

        public string OkCaption
        {
            get { return okCaption; }
            set
            {
                okCaption = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }

        public AddUpdateBaseViewModel()
        {
            Ok = new RelayCommand(arg => OkCommand());
            Cancel = new RelayCommand(arg => CancelCommand());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName]
        string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OkCommand()
        {
            Model.Ok();
        }

        protected virtual void CancelCommand()
        {
            Model.Cancel();

        }
    }

    public class AddUpdateSpaceViewModel: AddUpdateBaseViewModel
    { }
}
