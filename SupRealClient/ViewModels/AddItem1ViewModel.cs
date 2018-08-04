using SupRealClient.Models;
using SupRealClient.Common.Data;
using System.ComponentModel;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
    /// <summary>
    /// ViewModel для добавления\редактирования документов, гражданств.
    /// </summary>
    public class AddItem1ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие изменения свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private IAddItem1Model model;
        private string field = "";

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public string Caption { get; private set; }
        public string InputHeader { get; private set; }

        public string Field
        {
            get { return field; }
            set
            {
                if (value != null)
                {
                    field = value;
                    OnPropertyChanged("Field");
                }
            }
        }

        public ICommand Ok
        { get; set; }

        public ICommand Cancel
        { get; set; }

        public AddItem1ViewModel() { }

        /// <summary>
        /// Задать заголовок окна.
        /// </summary>
        /// <param name="addItem1Model"></param>
        private void SetTitle(IAddItem1Model addItem1Model)
        {
            this.Caption = addItem1Model is AddItemDocumentsModel ? "Добавление документа" :
                addItem1Model is UpdateItemDocumentsModel ? "Редактирование документа" :
                addItem1Model is AddItemNationsModel ? "Добавление" :
                addItem1Model is UpdateItemNationsModel ? "Редактирование" :
                string.Empty;
        }

        public void SetModel(IAddItem1Model addItem1Model)
        {
            this.model = addItem1Model;
            SetTitle(this.model); // Заголовок окна.
            
            this.InputHeader = addItem1Model is AddItemDocumentsModel ? "Введите документ:" :
                               addItem1Model is AddItemNationsModel ? "Введите страну:" :
                               addItem1Model is UpdateItemNationsModel ? "Отредактировать страну:" :
                               "Введите новое имя:";
            this.Field = model.Data.Field;
            this.Ok = new RelayCommand(arg => this.model.Ok(new FieldData { Field = Field }));
            this.Cancel = new RelayCommand(arg => this.model.Cancel());
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
    }
}
