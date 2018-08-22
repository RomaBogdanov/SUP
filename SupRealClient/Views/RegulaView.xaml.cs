using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RegulaLib;
using SupRealClient.Common;

namespace SupRealClient.Views
{
	/// <summary>
	/// Interaction logic for VisitorsView.xaml
	/// </summary>
	public partial class RegulaView
	{
		private static readonly Brush RedBrush = new SolidColorBrush(Color.FromRgb(239, 48, 48));
		private static readonly Brush GreenBrush = new SolidColorBrush(Color.FromRgb(10, 239, 48));

		private static readonly string CorrectString = "Действительно";
		private static readonly string WrongString = "Не действительно";
		
		
		public bool Result { get; private set; }

		public RegulaView(CPerson person)
		{
			InitializeComponent();
			ConfirmButton.Click += ConfirmButton_OnClick;
			CancelButton.Click += CancelButton_OnClick;
			SecondPageButton.Click += SecondPageButton_Click;
			ViewPersonInfo(person);
		}

		private void SecondPageButton_Click(object sender, RoutedEventArgs e)
		{
			ClickHandler(false);
		}

		private void ViewPersonInfo(CPerson person)
		{
			try
			{
				DocumentPageImage.Source = GetImage(person.PagesScanHash[person.DocumentNumber?.Value + 1]);
			}
			catch (Exception)
			{
				if (person.PagesScanHash.Any())
				{
					DocumentPageImage.Source = GetImage(person.PagesScanHash.First().Value);
				}
			}
			
			SurnameTextBlock.Text = person.Surname?.Value;
			NameTextBlock.Text = person.Name?.Value;
			PatronymicTextBlock.Text = person.Patronymic?.Value;
			BirthDateTextBlock.Text = person.DateOfBirth?.Value;

			DocumentTypeTextBlock.Text = CommonHelper.GetDocumentTypeInRussian(person.DocumentType?.Value);
			DocumentSeriaTextBlock.Text = person.DocumentSeria?.Value;
			DocumentNumberTextBlock.Text = person.DocumentNumber?.Value;
			DocumentDeliveryPlaceTextBlock.Text = person.DocumentDeliveryPlace?.Value;

			CorrectionIndicatorEllipse.Fill = person.IsDataCorrect ? GreenBrush : RedBrush;
			CorrectionIndicatorText.Text = person.IsDataCorrect ? CorrectString : WrongString;

			PortraitImage.Source = GetImage(person.Portrait);

		}

		private static BitmapImage GetImage(byte[] image)
		{
			if (image==null)
			{
				return null;
			}

			var bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.StreamSource = new MemoryStream(image);
			bitmapImage.EndInit();
			return bitmapImage;
		}


		private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
		{
			ClickHandler(true);
		}

		private void CancelButton_OnClick(object sender, RoutedEventArgs e)
		{
			ClickHandler(false);
		}

		private void ClickHandler(bool isConfirm)
		{
			Result = isConfirm;
			Close();
		}

		~RegulaView()
		{
			ConfirmButton.Click -= ConfirmButton_OnClick;
			CancelButton.Click -= CancelButton_OnClick;
		}
		
		protected override void OnKeyDown(KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Escape:
					ClickHandler(false);
					break;
			}
		}
	}
}
