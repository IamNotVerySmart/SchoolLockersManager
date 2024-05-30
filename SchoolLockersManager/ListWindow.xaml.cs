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
using System.Xml.Linq;

namespace SchoolLockersManager
{
	/// <summary>
	/// Interaction logic for ListWindow.xaml
	/// </summary>
	public partial class ListWindow : Window
	{
		public ListWindow()
		{
			InitializeComponent();
		}

		private void Menu_Click(object sender, RoutedEventArgs e)
		{
			string name = string.Empty;
			if(sender is Label)
			{
				name = ((Label)sender).Name;
			}
			else
            {
				name = ((Button)sender).Name;
            }

            switch (name)
            {
                case "Main":
                case "AddStudent":
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                    break;
                case "View":
                    ViewWindow viewWindow = new ViewWindow();
                    viewWindow.Show();
                    this.Close();
                    break;
            }
        }
	}
}
