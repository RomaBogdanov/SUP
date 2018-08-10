using SupRealClient.Models;
using SupRealClient.Common.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace SupRealClient.ViewModels
{
	public class Search1ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private ISearch1Model model;
		private string text = "";
		private string field = "";
		private bool register = false;
		private bool equal = true;
		private bool startWith = false;
		private bool contains = false;
		private bool beginEnabled = false;
		private bool nextEnabled = false;
		public IDictionary<string, string> fields = new Dictionary<string, string>();

		public string Text
		{
			get { return text; }
			set
			{
				if (value != null)
				{
					text = value?.TrimStart();
					OnPropertyChanged("Text");
				}
			}
		}

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

		public bool Register
		{
			get { return register; }
			set
			{
				register = value;
				OnPropertyChanged("Register");
			}
		}

		public bool Equal
		{
			get { return equal; }
			set
			{
				equal = value;
				OnPropertyChanged("Equal");
			}
		}

		public bool StartWith
		{
			get { return startWith; }
			set
			{
				startWith = value;
				OnPropertyChanged("StartWith");
			}
		}

		public bool Contains
		{
			get { return contains; }
			set
			{
				contains = value;
				OnPropertyChanged("Contains");
			}
		}

		public bool BeginEnabled
		{
			get { return beginEnabled; }
			set
			{
				beginEnabled = value;
				OnPropertyChanged("BeginEnabled");
			}
		}

		public bool NextEnabled
		{
			get { return nextEnabled; }
			set
			{
				nextEnabled = value;
				OnPropertyChanged("NextEnabled");
			}
		}

		public IDictionary<string, string> Fields
		{
			get { return fields; }
			set
			{
				fields = value;
				OnPropertyChanged("Fields");
			}
		}

		public ICommand Find
		{ get; set; }

		public ICommand Begin
		{ get; set; }

		public ICommand Next
		{ get; set; }

		public ICommand Cancel
		{ get; set; }

		public Search1ViewModel()
		{
		}

		public void SetModel(ISearch1Model search1Model)
		{
			this.model = search1Model;
			this.Find = new RelayCommand(arg => {
                BeginEnabled = NextEnabled = this.model.Find(new SearchData
                {
                    Field = Field,
                    Text = Text,
                    Register = Register,
                    Equal = Equal,
                    StartWith = StartWith,
                    Contains = Contains
                });
            });
			this.Begin = new RelayCommand(arg => this.model.Begin());
			this.Next = new RelayCommand(arg => this.model.Next());
			this.Cancel = new RelayCommand(arg => this.model.Cancel());

			this.Fields = model.GetFields();
			if (this.Fields.Any())
			{
				Field = this.Fields.Keys.FirstOrDefault();
			}
		}

		protected virtual void OnPropertyChanged(string propertyName) =>
			this.PropertyChanged?.Invoke(this,
			new PropertyChangedEventArgs(propertyName));
	}
}
