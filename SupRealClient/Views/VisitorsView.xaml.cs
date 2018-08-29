using System;
using SupRealClient.Common.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SupRealClient.Views
{
	/// <summary>
	/// Interaction logic for VisitorsView.xaml
	/// </summary>
	public partial class VisitorsView
	{
		private static VisitorsView visitorsView;
		private TraversalRequest _focusMover = new TraversalRequest(FocusNavigationDirection.Next);
		private int _numberFocusBranch = 0;
		private bool _eventsLoaded = false;


		public static VisitorsView Instance
		{
			get
			{
				if (visitorsView == null)
				{
					visitorsView = new VisitorsView();
				}

				return visitorsView;
			}
		}

		public VisitorsView(bool isNew = false)
		{
			InitializeComponent();

			Loaded += (s, e) =>
			{
				if (this.DataContext == null)
				{
					if (DataContext is VisitsViewModel visitsView)
					{
						visitsView.DocumentScanerRemoveSubscription();
					}

					this.DataContext = new VisitsViewModel(this);
				}

				if(!_eventsLoaded)
				{ 
					(DataContext as VisitsViewModel).MoveNextFocusingElement += MovingNextFocusingElement;
					(DataContext as VisitsViewModel).RedactModeEvent += VisitorsView_RedactModeEvent;
					this.Height = (this.DataContext as VisitsViewModel).WinSet.Height;
					this.Width = (this.DataContext as VisitsViewModel).WinSet.Width;
					this.Left = (this.DataContext as VisitsViewModel).WinSet.Left;
					this.Top = (this.DataContext as VisitsViewModel).WinSet.Top;					

					parentWindowChecking();

					_eventsLoaded = true;
					//(this.DataContext as VisitsViewModel).Model.OnClose += Handling_OnClose;
				}
			};
		}

		private void MovingNextFocusingElement(string e)
		{
			if (e == "OrganizationsList")
			{
				checkBoxNoForm.Focus();
			}
		}

		private void VisitorsView_RedactModeEvent(bool redactMode)
		{
			if (redactMode)
			{
				textBox_Family.Tag = 0;
				textBox_Telephones.Tag = 0;
				_numberFocusBranch = 0;
				textBox_Family.Focus();
				panel_TabItems.SelectedIndex = 0;
			}
		}

		public void NewVisitor()
		{
			((VisitsViewModel) this.DataContext).Model = new NewVisitsModel();
		}

		private void MetroWindow_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
		{
			parentWindowChecking();
		}

		private void MetroWindow_Activated(object sender, System.EventArgs e)
		{
			//parentWindowChecking();
		}

		void parentWindowChecking()
		{
			if (this.DataContext != null && this.ParentWindow is SupRealClient.Views.VisitorsListWindView)
			{
				if (this.Visibility == Visibility.Visible)
				{
					if ((this.ParentWindow as SupRealClient.Views.VisitorsListWindView).base4.modeEdit)
					{
						object currentItem = (this.ParentWindow as SupRealClient.Views.VisitorsListWindView).base4.baseTab.CurrentItem;
						if (currentItem is EnumerationClasses.Visitor && butEdit.IsEnabled)
						{
							int IdSel = (currentItem as EnumerationClasses.Visitor).Id;
							// -2, т.к Id начинается с 1, и далее вызываем команду некст, т.е. это еще +1
							(((VisitsViewModel) this.DataContext).Model as VisitsModel).selectedIndex = IdSel - 2;
							(this.DataContext as VisitsViewModel).NextCommand.Execute(null);
							(this.DataContext as VisitsViewModel).EditCommand.Execute(null);
						}

						(this.ParentWindow as SupRealClient.Views.VisitorsListWindView).base4.modeEdit = false;
					}
                    else if ((this.ParentWindow as SupRealClient.Views.VisitorsListWindView).base4.modeWatch)
                    {
                        object currentItem = (this.ParentWindow as SupRealClient.Views.VisitorsListWindView).base4.baseTab.CurrentItem;
                        if (currentItem is EnumerationClasses.Visitor)
                        {
                            int IdSel = (currentItem as EnumerationClasses.Visitor).Id;                           
                            (((VisitsViewModel)this.DataContext).Model as VisitsModel).selectedIndex = IdSel - 2;
                            (this.DataContext as VisitsViewModel).NextCommand.Execute(null);
                        }

                        (this.ParentWindow as SupRealClient.Views.VisitorsListWindView).base4.modeWatch = false;
                    }
                    else if (butNew.IsEnabled)
                    {
                        (this.DataContext as VisitsViewModel).NewCommand.Execute(null);
                    }
						
				}

				if (this.Visibility == Visibility.Hidden)
				{
					if (DataContext is VisitsViewModel visitsView)
					{
						visitsView.DocumentScanerRemoveSubscription();
					}

					this.DataContext = new VisitsViewModel(this);
				}

				
			}
		}

		/// <summary>
		/// На полях ФИО посетителя пробел воспринимается как Tab
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CheckIsSpaceAsTab(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Space) return;

			if (!(sender is TextBox textBoxElement)) return;

			bool spaceOnNameFields = textBoxElement.Equals(textBox_Family) ||
			                         textBoxElement.Equals(textBox_Name) ||
			                         textBoxElement.Equals(textbox_Patronymic); // Если на полях имени нажимается пробел, он воспринимается как таб

			if (spaceOnNameFields)
			{
				if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftShift)) // при комбинации Ctrl+Shift+Space пробел должен добавляться обычным образом
				{
					var selectionStart = textBoxElement.SelectionStart;
					textBoxElement.Text = textBoxElement.Text.Insert(selectionStart, " ");
					textBoxElement.SelectionStart = selectionStart + 1;
					
					return;
				}

				textBoxElement.MoveFocus(_focusMover);
			}

			e.Handled = true;
		}

		private void MoveNextFocusControl(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				((UIElement) sender).MoveFocus(_focusMover);
				e.Handled = true;

				Controlling_TabControl();
			}
			if (e.Key == Key.Tab)
			{
				switch ((sender as FrameworkElement).Name)
				{
					case "button_Cancel":
					case "textBox_Telephones":
					case "button_SelectNation":
					case "comboBox_Position":
					case "checkBox_Consent":
					case "datePicker_AgreeToDate":
					{

						Controlling_TabControl();
					}
						break;
					default: return;
				}
			}
		}

		private void MoveNextFocusControl_ToName(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				MoveNextFocusControl_ToName((sender as FrameworkElement).Name);
				Controlling_TabControl();
			}
			if (e.Key == Key.Tab)
			{
					switch ((sender as FrameworkElement).Name)
				{
					case "button_Cancel":
					case "textBox_Telephones":
					case "button_SelectNation":
					case "comboBox_Position":
					case "checkBox_Consent":
					case "datePicker_AgreeToDate":
					{

						Controlling_TabControl();
					}
						break;
					default: return;
				}
			}
		}

		private void MoveNextFocusControl_ToName(string nameControl)
		{
			switch (nameControl)
			{
				case "textbox_Patronymic":
				{
					button_LoadOrganization.Focus();
				}
					break;
				case "checkBoxNoForm":
				{
					checkBox_Consent.Focus();
				}
					break;

				case "checkBox_Consent":
				{
					button_Ok.Focus();
				}
					break;
				case "listBox_MainDocument":
				{
					(DataContext as VisitsViewModel)?.OpenMainDocument();
				}
					break;
				default: return;
			}

		}

		private void VisitorsView_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				if (!(DataContext is VisitsViewModel model)) return;
				if (!model.IsRedactMode)
				{
					Close();
				}
				else
				{
					model.Cancel();
				}
			} else if ((e.Key == Key.Home || e.Key == Key.Left && Keyboard.IsKeyDown(Key.LeftCtrl)) && butFirst.IsEnabled)
			{
				butFirst.Command.Execute(null);
				e.Handled = true;
			}
			else if ((e.Key == Key.End || e.Key == Key.Right && Keyboard.IsKeyDown(Key.LeftCtrl)) && butLast.IsEnabled)
			{
				butLast.Command.Execute(null);
				e.Handled = true;
			}
			else if (e.Key == Key.Up && Keyboard.IsKeyDown(Key.LeftCtrl) && butPrevious.IsEnabled)
			{
				butPrevious.Command.Execute(null);
				e.Handled = true;
			}
			else if (e.Key == Key.Down && Keyboard.IsKeyDown(Key.LeftCtrl) && butNew.IsEnabled)
			{
				butNext.Command.Execute(null);
				e.Handled = true;
			}
			else if (e.Key == Key.G && Keyboard.IsKeyDown(Key.LeftCtrl) && butGiveCard.IsEnabled)
			{
				butGiveCard.Command.Execute(null);
				e.Handled = true;
			}
			else if (e.Key == Key.F && Keyboard.IsKeyDown(Key.LeftCtrl) && butSearch.IsEnabled)
			{
				butSearch.Command.Execute(null);
				e.Handled = true;
			}
			else if ((e.Key == Key.F2 && butEdit.IsEnabled || e.Key == Key.D && Keyboard.IsKeyDown(Key.LeftCtrl)) && butEdit.IsEnabled)
			{
				butEdit.Command.Execute(null);
				e.Handled = true;
			}
			else if (e.Key == Key.F5 && butRefresh.IsEnabled)
			{
				butRefresh.Command.Execute(null);
				e.Handled = true;
			}
			else if (e.Key == Key.S && Keyboard.IsKeyDown(Key.LeftCtrl) && button_Ok.IsEnabled)
			{
				button_Ok.Command.Execute(null);
				e.Handled = true;
			}
			else if (e.Key == Key.T && Keyboard.IsKeyDown(Key.LeftCtrl) && butReturnCard.IsEnabled)
			{
				butReturnCard.Command.Execute(null);
				e.Handled = true;
			}
			else if (e.Key == Key.Insert && butNew.IsEnabled)
			{
				butNew.Command.Execute(null);
				e.Handled = true;
			}
		}

		private void GotFocus_FirstTabItemControls(object sender, RoutedEventArgs e)
		{
			Controlling_TabControl();


			if ((sender as FrameworkElement).Name == "listBox_MainDocument")
			{
				if (listBox_MainDocument.Items.Count > 0 && listBox_MainDocument.SelectedItem==null)
				{
					listBox_MainDocument.SelectedItem = listBox_MainDocument.Items[0];
				}
			}

			if ((sender as FrameworkElement).Name == "textBox_Telephones")
			{

				//if (((sender as FrameworkElement).Tag is int) && (int)(sender as FrameworkElement).Tag == 1)
				//{
				//	(sender as FrameworkElement).Tag = 0;
				//	textBox_Family.Focus();
				//}
				//else
				//{
				//	(sender as FrameworkElement).Tag = 1;
				//}

				if (_numberFocusBranch == 0)
				{
					textBox_Family.Focus();
				}
			}

			if ((sender as FrameworkElement).Name == "textBox_Family")
			{
				//if (((sender as FrameworkElement).Tag is int) && (int)(sender as FrameworkElement).Tag == 1)
				//{
				//	(sender as FrameworkElement).Tag = 0;
				//	button_Ok.Focus();
				//}
				//else
				//{
				//	(sender as FrameworkElement).Tag = 1;
				//}
				if (_numberFocusBranch != 0)
				{
					button_Ok.Focus();
				}
			}



			//if ((sender as FrameworkElement).Name == "button_Cancel")
			//{
			//	if ((button_Cancel.Tag is int) && (int)button_Cancel.Tag == 1)
			//	{
			//		button_Cancel.Tag = 0;
			//		textBox_Family.Focus();
			//	}
			//	else
			//	{
			//		button_Cancel.Tag = 1;
			//	}
			//}
		}

		private void Controlling_TabControl()
		{
			if (DataContext is VisitsViewModel)
				if ((DataContext as VisitsViewModel).IsRedactMode)
				{
					if (panel_TabItems.SelectedIndex != 0)
					{
						panel_TabItems.SelectedIndex = 0;
					}
				}
		}

		private void UIElement_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			Controlling_TabControl();
		}

		private void PrevMouseDown(object sender, MouseButtonEventArgs e)
		{
			if ((sender as FrameworkElement).Name == "textBox_Family")
			{
				textBox_Family.Tag = 0;
				textBox_Telephones.Tag = 0;
				_numberFocusBranch = 0;
			}
			if ((sender as FrameworkElement).Name == "textBox_Telephones")
			{
				textBox_Family.Tag = 0;
				textBox_Telephones.Tag = 1;
				_numberFocusBranch = 1;
			}

		}

		private void ControlLostFocus(object sender, RoutedEventArgs e)
		{
			if ((sender as FrameworkElement).Name == "checkBoxNoForm")
			{
				_numberFocusBranch = 0;
			}

			if ((sender as FrameworkElement).Name == "datePicker_AgreeToDate")
			{
				_numberFocusBranch = 1;
			}

			if ((sender as FrameworkElement).Name == "button_Cancel")
			{
				if (_numberFocusBranch != 0)
					_numberFocusBranch = 0;
				else
				{
					_numberFocusBranch = 1;
				}
			}
		}

		private void DataGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			HitTestResult result = VisualTreeHelper.HitTest(this, e.GetPosition(this));
			if (result.VisualHit.GetType() != typeof(DataGridRow))
			{
				AreasDataGrid.UnselectAllCells();
			}
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}
	}
}
