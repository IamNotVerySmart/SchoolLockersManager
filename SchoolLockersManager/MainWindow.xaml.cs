using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using QRCoder;
using QRCoder.Xaml;
using static SchoolLockersManager.DataAccess;

namespace SchoolLockersManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DatabaseInitializer.InitializeDatabase();
		}

		//Funkcja która po wcześniejszemu zczytaniu informacji z textboxów, generuje oraz wyświetla użytkownikowi kod QR
		private void Generate_Click(object sender, RoutedEventArgs e)
		{
			QRCodeGenerator qr = new QRCodeGenerator();
			string tmp = TxtBoxClass.Text + "\n"
				+ TxtBoxSpecialization.Text + "\n" 
				+ TxtBoxName.Text + "\n" 
				+ TxtBoxSurname.Text;
			QRCodeData data = qr.CreateQrCode(tmp,QRCodeGenerator.ECCLevel.Q);
			XamlQRCode code = new XamlQRCode(data);
			DrawingImage tmp1 = code.GetGraphic(1);
			ImageQRCode.Source = tmp1;
		}

		private void Print_Click(object sender, RoutedEventArgs e)
		{
			PrintDialog printDialog = new PrintDialog();

			// Wybór drukarki i ilosci kopi
			if (printDialog.ShowDialog() == true)
			{
				// Drukowanie kodu QR
				printDialog.PrintVisual(ImageQRCode, "Drukowanie kodu QR");
			}
		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				// Sprawdzanie czy pola są puste, jeśli tak następuję pokazanie błedu o tym informującym
				if (string.IsNullOrWhiteSpace(TxtBoxName.Text) ||
					string.IsNullOrWhiteSpace(TxtBoxSurname.Text) ||
					string.IsNullOrWhiteSpace(TxtBoxClass.Text) ||
					string.IsNullOrWhiteSpace(TxtBoxSpecialization.Text))
				{
					MessageBox.Show("Wszystkie pola muszą być uzupełnione.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
				// Sprawdzanie pola klasy czy jest INT, jeśli nie to informuję użytkownika o błędzie.
				if (!int.TryParse(TxtBoxClass.Text, out int studentClass))
				{
					MessageBox.Show("Pole klasa musi być liczbą.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}

				// Tworzenie nowego ucznia i przypisanie mu podanych parametrów
				var newStudent = new Student
				{
					Name = TxtBoxName.Text,
					Surname = TxtBoxSurname.Text,
					Class = studentClass,
					Specialization = TxtBoxSpecialization.Text,
					AttendsSchool = true
				};

				// Dodanie wcześniej stworzonego ucznia do bazy
				DataAccess.AddStudent(newStudent);
				MessageBox.Show("Uczeń został dodany!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Błąd: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		// Przełączanie pomiędzy oknamy do zarządzania, drukowaniem i dodawaniem uczniów oraz szafek.
		private void Menu_Click(object sender, RoutedEventArgs e)
		{
			string name = ((Label)sender).Name;
			switch (name)
			{
				case "List":
					ListWindow listWindow = new ListWindow();
					listWindow.Show();
					this.Close();
					break;
				case "Locker":
					LockerWindow lockerWindow = new LockerWindow();
					lockerWindow.Show();
					this.Close();
					break;
			}
		}
	}
}