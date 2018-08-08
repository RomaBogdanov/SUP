using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SupRealClient.ViewModels;

namespace SupRealClient.Views
{
	/// <summary>
	/// Interaction logic for BidsView.xaml
	/// </summary>
	public partial class BidsView
	{
		/// <summary>
		/// Последовательность элементов при проходе с помощью клавишы Enter
		/// </summary>
		private readonly List<UIElement> _enterUiElementsSequenceSingleOrder;
		private readonly List<UIElement> _enterUiElementsSequenceTempOrder;
		private readonly List<UIElement> _enterUiElementsSequenceVirtOrder;
		private UIElement _previousEnterUiElement = null;

		public BidsView()
		{
			InitializeComponent();
			comboMenu.SelectionChanged += ComboBox_SelectionChanged; // Подписываемся на событие выбора вкладки.

			_enterUiElementsSequenceSingleOrder = new List<UIElement>
			{
				dpSingleOrderDate,
				btnSingleOrderAdd,
				btnSingleOrderSigner,
				cbSingleOrderActive,
				tbSingleOrderNote,
				btnOk
			};

			_enterUiElementsSequenceTempOrder = new List<UIElement>
			{
				btnTempOrderAdd,
				dpTempOrderDateFrom,
				dpTempOrderDateTo,
				btnTempOrderSigner,
				btnTempOrderAgreeer,
				cbTempOrderActive,
				tbTempOrderNote,
				btnOk
			};

			_enterUiElementsSequenceVirtOrder = new List<UIElement>
			{
				dpVirtOrderFrom,
				dpVirtOrderTo,
				tbVirtOrderReason,
				btnVirtOrderVisitor,
				btnVirtOrderOrg,
				tbVirtOrderPosition,
				btnVirtOrderZones,
				cbVirtOrderActive,
				tbVirtOrderNote,
				btnOk
			};

			btnOk.Click += EndEditingButtonClick;
			btnCancel.Click += EndEditingButtonClick;
		}

        public void SetToVirtue()
        {
            comboMenu.SelectedIndex = 2;
            (this.DataContext as BidsViewModel).SetToVirtue();
        }

        private List<UIElement> EnterUiElementsSequence
		{
			get
			{
				switch (comboMenu.SelectedIndex)
				{
					case 0:
						return _enterUiElementsSequenceSingleOrder;
					case 1:
						return _enterUiElementsSequenceTempOrder;
					case 2:
						return _enterUiElementsSequenceVirtOrder;
					default:
						return _enterUiElementsSequenceSingleOrder;
				}
			}
		}

		private void MetroWindow_Initialized(object sender, EventArgs e)
		{
			this.DataContext = new ViewModels.BidsViewModel() {BidsModel = new Models.BidsModel()}; // Контекст данных.
			this.Height = (this.DataContext as ViewModels.BidsViewModel).WinSet.Height;
			this.Width = (this.DataContext as ViewModels.BidsViewModel).WinSet.Width;
			this.Left = (this.DataContext as ViewModels.BidsViewModel).WinSet.Left;
			this.Top = (this.DataContext as ViewModels.BidsViewModel).WinSet.Top;
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			mainTabControl.SelectedIndex = comboMenu.SelectedIndex;
		}

		/// <summary>
		/// Реакция на нажатие Enter
		/// Для кнопок первое нажатие активирует кнопку, а второе - переводит фокус на следующий элемент
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UiElement_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Enter || btnOk.IsEnabled == false)
			{
				return;
			}

			bool isSenderButton = sender is Button;

			if (_previousEnterUiElement != null && (!isSenderButton || _previousEnterUiElement.Equals(sender)))
			{
				if (!isSenderButton)
				{
					_previousEnterUiElement = (UIElement) sender;
				}

				UIElement newFocus = _previousEnterUiElement;
				while (true)
				{
					newFocus = MoveFocusToNext(newFocus);

					if (newFocus != null && !newFocus.IsEnabled)
					{
						continue;
					}

					break;
				}

				e.Handled = true;
			}
			else
			{
				_previousEnterUiElement = (UIElement) sender;
			}
		}

		private UIElement MoveFocusToNext(UIElement newFocus)
		{
			int index = EnterUiElementsSequence.FindIndex(x => x.Equals(newFocus));
			index = (index + 1) % EnterUiElementsSequence.Count;
			newFocus = EnterUiElementsSequence[index];
			newFocus?.Focus();
			return newFocus;
		}

		private void EndEditingButtonClick(object sender, RoutedEventArgs e)
		{
			_previousEnterUiElement = null;
		}

		/// <summary>
		/// Обработка нажатия клавиш в окне
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				if (btnCancel.IsEnabled) 
				{
					btnCancel.Command.Execute(null);
				}
				else
				{
					Close();
				}
				e.Handled = true;
			} else if (e.Key == Key.S && Keyboard.IsKeyDown(Key.LeftCtrl) && btnOk.IsEnabled)
			{
				btnOk.Command.Execute(null);
			} else if (e.Key == Key.Left && Keyboard.IsKeyDown(Key.LeftCtrl) || e.Key == Key.Home)
			{
				btnToFirst.Command.Execute(null);
				e.Handled = true;
			} else if (e.Key == Key.Right && Keyboard.IsKeyDown(Key.LeftCtrl) || e.Key == Key.End)
			{
				btnToLast.Command.Execute(null);
				e.Handled = true;
			}
			else if (e.Key == Key.Up && Keyboard.IsKeyDown(Key.LeftCtrl))
			{
				btnPrevious.Command.Execute(null);
				e.Handled = true;
			}
			else if (e.Key == Key.Down && Keyboard.IsKeyDown(Key.LeftCtrl))
			{
				btnNext.Command.Execute(null);
				e.Handled = true;
			} else if (e.Key == Key.F2 || e.Key == Key.D && Keyboard.IsKeyDown(Key.LeftCtrl))
			{
				btnEditOrder.Command.Execute(null);
				e.Handled = true;
			} else if (e.Key == Key.F5)
			{
				btnReload.Command.Execute(null);
				e.Handled = true;
			}
			else if (e.Key == Key.F && Keyboard.IsKeyDown(Key.LeftCtrl))
			{
				btnFind.Command.Execute(null);
				e.Handled = true;
			}
			else if (e.Key == Key.Insert)
			{
				btnAddOrder.Command.Execute(null);
				e.Handled = true;
			} else if (e.Key == Key.Enter && !EnterUiElementsSequence.Any(x => x.Equals(_previousEnterUiElement)))
			{
				switch (comboMenu.SelectedIndex)
				{
					case 0:
						dpSingleOrderDate.Focus();
						break;
					case 1:
						btnTempOrderAdd.Focus();
						break;
					case 2:
						dpVirtOrderFrom.Focus();
						break;
				}
			}
		}
	}
}