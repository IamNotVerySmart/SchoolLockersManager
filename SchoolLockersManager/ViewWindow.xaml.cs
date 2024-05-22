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

namespace SchoolLockersManager
{
    /// <summary>
    /// Interaction logic for ViewWindow.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
        public ViewWindow()
        {
            InitializeComponent();
        }
        private void Menu_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
            }
        }
    }
}
