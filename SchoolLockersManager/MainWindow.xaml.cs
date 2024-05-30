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
        }

        //Funkcja która po wcześniejszemu zczytaniu informacji z textboxów, generuje oraz wyświetla użytkownikowi kod QR
        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            QRCodeGenerator qr = new QRCodeGenerator();
            string tmp = "Imie: " + TxtBoxName.Text + "\n"
                + "Nazwisko: " + TxtBoxSurname.Text + "\n"
                + "Rocznik: " + TxtBoxGrade.Text + "\n"
                + "Nr. szafki: " + TxtBoxLocker.Text + "\n"
                + "Blok szafek: " + TxtBoxUnit.Text + "\n"
                + "Pietro/blok szkoły (A/B)/najbliższa sala: " + TxtBoxLocation.Text;
            QRCodeData data = qr.CreateQrCode(tmp, QRCodeGenerator.ECCLevel.Q);
            XamlQRCode code = new XamlQRCode(data);
            DrawingImage tmp1 = code.GetGraphic(1);
            ImageQRCode.Source = tmp1;
        }

        // funkcja odpowiedzialna za drukowanie QR kodu
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
            MessageBox.Show("placeholder", "to też");
        }

        private void Menu_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string name = ((Label)sender).Name;
            switch (name)
            {
                case "Manage":
                    ListWindow listWindow = new ListWindow();
                    listWindow.Show();
                    this.Close();
                    break;
            }
        }
    }
}