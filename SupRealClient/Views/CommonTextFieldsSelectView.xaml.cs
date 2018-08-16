﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SupRealClient.Views
{
	/// <summary>
	/// Логика взаимодействия для CommonTextFieldsSelectView.xaml
	/// </summary>
	public partial class CommonTextFieldsSelectView
	{
		public EFields FieldsForSubstitution => (EFields)StackPanel.Children.Cast<CheckBox>().Select((item, index) => item.IsChecked.Value ? 1 << index : 0).Sum();
		public bool Result { get; private set; }

		public CommonTextFieldsSelectView()
		{
			InitializeComponent();
			KeyUp += CommonTextFieldsSelectView_KeyUp;
		}

		private void CommonTextFieldsSelectView_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Enter:
					ClickHandler(true);
					break;
				case Key.Escape:
					ClickHandler(false);
					break;
			}
		}

		private void OkClick(object sender, RoutedEventArgs e)
		{
			ClickHandler(true);
		}

		private void CancelClick(object sender, RoutedEventArgs e)
		{
			ClickHandler(false);
		}

		private void ClickHandler(bool value)
		{
			Result = value;
			Close();
		}

	}

	[Flags]
	public enum EFields
	{
		None = 0,
		Surname = 1,
		Name = 2,
		Patronymic = 4,
		BirthDate = 8,
		Portrait = 16
	}
}