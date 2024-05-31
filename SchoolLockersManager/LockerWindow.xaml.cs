using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static SchoolLockersManager.DataAccess;

namespace SchoolLockersManager
{
    /// <summary>
    /// Interaction logic for LockerWindow.xaml
    /// </summary>
    public partial class LockerWindow : Window
    {
        public LockerWindow()
        {
            InitializeComponent();
        }

        private void AddLocker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrWhiteSpace(TxtBoxLockerNumber.Text) ||
                    string.IsNullOrWhiteSpace(TxtBoxLockerUnit.Text) ||
                    string.IsNullOrWhiteSpace(TxtBoxLockerFloor.Text) ||
                    string.IsNullOrWhiteSpace(TxtBoxLockerClosestClass.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Parse fields to appropriate data types
                if (!int.TryParse(TxtBoxLockerNumber.Text, out int number) ||
                    !int.TryParse(TxtBoxLockerUnit.Text, out int unit) ||
                    !int.TryParse(TxtBoxLockerFloor.Text, out int floor) ||
                    !double.TryParse(TxtBoxLockerClosestClass.Text, out double closestClass))
                {
                    MessageBox.Show("Invalid input format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Create a new locker object
                var newLocker = new Locker
                {
                    Number = number,
                    Unit = unit,
                    Floor = floor,
                    ClosestClass = closestClass
                };

                // Add the locker to the database
                DataAccess.AddLocker(newLocker);
                MessageBox.Show("Szafka została dodana!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
                case "Main":
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                    break;
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
