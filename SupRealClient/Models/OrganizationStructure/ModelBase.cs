using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
                OnPropertyChanged();
            }
        }

        public int Id;

        public bool Save { get; set; }

        private string _description;
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
