﻿using System.Text;
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
            string tmp = TxtBoxGrade.Text + "\n" 
                + TxtBoxName.Text + "\n" 
                + TxtBoxSurname.Text + "\n" 
                + TxtBoxLocker.Text + "\n"
                + TxtBoxUnit.Text;
            QRCodeData data = qr.CreateQrCode(tmp,QRCodeGenerator.ECCLevel.Q);
            XamlQRCode code = new XamlQRCode(data);
            DrawingImage tmp1 = code.GetGraphic(1);
            ImageQRCode.Source = tmp1;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("placeholder", "to nic nie robi(jeszcze(mam nadzieje))");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("placeholder", "to też");
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