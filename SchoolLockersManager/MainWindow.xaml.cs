using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using QRCoder;
using QRCoder.Xaml;

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
                + TxtBoxSurname.Text + "\n" 
                + TxtBoxLocker.Text + "\n"
                + TxtBoxUnit.Text + "\n"
                + TxtBoxFloor.Text;
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
            var newStudent = new DataAccess.Student
            {
                StudentID = DataAccess.GetLast() + 1,
                Name = TxtBoxName.Text,
                Surname = TxtBoxSurname.Text,
                Class = int.Parse(TxtBoxClass.Text),
                Specialization = TxtBoxSpecialization.Text,
                AttendsSchool = true
            };
            DataAccess.AddStudent(newStudent);
        }

        // Przełączanie pomiędzy oknamy do zarządzania, a drukowaniem i dodawaniem uczniów.
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            string name = ((Label)sender).Name;
            switch (name)
            {
                case "View":
                    ViewWindow viewWindow = new ViewWindow();
                    viewWindow.Show();
                    this.Close();
                    break;
                case "List":
                    ListWindow listWindow = new ListWindow();
                    listWindow.Show();
                    this.Close();
                    break;
            }
        }
    }
}