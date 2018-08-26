using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SupRealClient.Annotations;
using SupRealClient.Models.OrganizationStructure.Interfaces;

namespace SupRealClient.Models.OrganizationStructure
{
    public class ModelBase : IModel
    {
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value; 
                if (string.IsNullOrEmpty(_description))
                {
                    _description = fullDescription;
                }                
                OnPropertyChanged();
            }
        }

        public string FullDescription
        {
            get { return fullDescription; }
            set
            {
                fullDescription = value;
                OnPropertyChanged();
            }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }
            }
        }
        private bool _isExpanded;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                    this.OnPropertyChanged("Background");
                }
            }
        }
        private bool _isSelected;

        public Brush Background
        {
            get { return IsSelected ? new SolidColorBrush(Color.FromRgb(0xCC, 0xDA, 0xFF)) : Brushes.Transparent; }
        }

        public int Id;

        public bool Save { get; set; }

        private string _description;
        private string fullDescription;
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnClose;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void EditItem()
        {
            //throw new NotImplementedException();
        }

        public void Cancel()
        {
            //throw new NotImplementedException();
        }
    }
}
