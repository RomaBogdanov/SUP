using System;
using SupRealClient.Models;
using SupRealClient.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using RegulaLib;
using Xceed.Wpf.Toolkit;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для VisitorsDocumentExtView.xaml
    /// </summary>
    public partial class VisitorsMainDocumentView
    {
        /// <summary>
        /// Двигатель фокуса
        /// </summary>
        private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);

        public VisitorsMainDocumentView(VisitorsMainDocumentModel model, bool editable, CPerson person)
        {
            model.OnClose += Handling_OnClose;
            DataContext = new VisitorsMainDocumentViewModel(editable);
            ((VisitorsMainDocumentViewModel)DataContext).SetModel(model, person);
	        ((VisitorsMainDocumentViewModel)DataContext)._MoveNextFocusingElement += MovingNextFocusingElement;
			((VisitorsMainDocumentViewModel)DataContext)._TestDatePickerEvent += TestingDatePickerEvent;
			InitializeComponent();

            AfterInitialize();

	        textBox_DocType.Focus();
	        this.PreviewKeyDown += Window_PreviewKeyDown_AndStop;
        }

		/// <summary>
		/// Конструктор - заглушка
		/// </summary>
		public VisitorsMainDocumentView()
        {
            InitializeComponent();
            DataContext = new VisitorsMainDocumentViewModel(false);
	        ((VisitorsMainDocumentViewModel)DataContext)._MoveNextFocusingElement += MovingNextFocusingElement;
	        ((VisitorsMainDocumentViewModel)DataContext)._TestDatePickerEvent += TestingDatePickerEvent;

	        textBox_DocType.Focus();
	        this.PreviewKeyDown += Window_PreviewKeyDown_AndStop;
		}

        private void MoveNextFocusControl(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
	            if (sender is DatePicker)
	            {
		            MaskedTextBox maskedTextBox = GetMaskedTextBox(datePicker_Date);
		            if (maskedTextBox.IsMaskFull && maskedTextBox.IsMaskCompleted)
		            {
			            switch ((sender as FrameworkElement).Name)
			            {
				            case nameof(datePicker_Date):
					            GoingTo_DatePicker(datePicker_DateTo);
					            break;
				            case nameof(datePicker_DateTo):
					            GoingTo_DatePicker(datePicker_BirthDate);
					            break;
				            case nameof(datePicker_BirthDate):
				            {
					            button_Ok.Focus();
				            }
					            break;
			            }
			            return;
		            }
		            return;
	            }

	            if ((sender as TextBox).Name == nameof(textBox_Code))
	            {
		            GoingTo_DatePicker(datePicker_Date);
		            return;
	            }


				((UIElement)sender).MoveFocus(_focusMover);
	            e.Handled = true;
			}
        }

	    private void MoveNext_DatePicker(object sender, KeyEventArgs e)
	    {
		    if (e.Key == Key.Enter)
			{
				
			    ((UIElement)sender).MoveFocus(_focusMover);
			    e.Handled = true;
		    }
	    }

	    private void KeyUp_LoadDocType(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				((VisitorsMainDocumentViewModel)DataContext).DocumentsListModel();
			}
	    }


		private void MovingNextFocusingElement(string e)
	    {
			if(e== "SetDocumentType")
				button_SetDocumentType.MoveFocus(_focusMover);
		}

	    private void TestingDatePickerEvent()
	    {
		    MaskedTextBox maskedTextBox = GetMaskedTextBox(datePicker_Date);
		    if (maskedTextBox.IsMaskFull && maskedTextBox.IsMaskCompleted)
			    ((VisitorsMainDocumentViewModel) DataContext).Date_Correct =
				    maskedTextBox.IsMaskFull && maskedTextBox.IsMaskCompleted;

		    maskedTextBox = GetMaskedTextBox(datePicker_DateTo);
		    if (maskedTextBox.IsMaskFull && maskedTextBox.IsMaskCompleted)
			    ((VisitorsMainDocumentViewModel)DataContext).DateTo_Correct =
				    maskedTextBox.IsMaskFull && maskedTextBox.IsMaskCompleted;

		    maskedTextBox = GetMaskedTextBox(datePicker_BirthDate);
		    if (maskedTextBox.IsMaskFull && maskedTextBox.IsMaskCompleted)
			    ((VisitorsMainDocumentViewModel)DataContext).BirthDate_Correct =
				    maskedTextBox.IsMaskFull && maskedTextBox.IsMaskCompleted;

		}

		private MaskedTextBox GetMaskedTextBox(DatePicker datePicker)
	    {
		    DatePickerTextBox datePickerTextBox = (DatePickerTextBox)datePicker?.Template?.FindName("PART_TextBox", datePicker);
		    return  (MaskedTextBox)datePickerTextBox?.Template?.FindName("PART_TextBox", datePickerTextBox);
		}

	    private void GoingTo_DatePicker(DatePicker datePicker)
		{
			MaskedTextBox maskedTextBox = GetMaskedTextBox(datePicker);
			maskedTextBox.Clear();
			maskedTextBox.CaretIndex = 0;
			maskedTextBox.Focus();
		}

	    private void datePicker_Date_GotFocus(object sender, RoutedEventArgs e)
		{
			int i = 0;
		}

	    private void Window_PreviewKeyDown_AndStop(object sender, KeyEventArgs e)
	    {
		    if (e.Key == Key.Escape)
		    {
			    e.Handled = false;
			    Close();
		    }
	    }
	}
}
