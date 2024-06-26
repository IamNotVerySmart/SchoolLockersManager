﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using static SchoolLockersManager.DataAccess;

namespace SchoolLockersManager
{
	/// <summary>
	/// Interaction logic for ListWindow.xaml
	/// </summary>
	public partial class ListWindow : Window
	{
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Locker> Lockers { get; set; }

        public ListWindow()
		{
			InitializeComponent();
            Students = new ObservableCollection<Student>();
            Lockers = new ObservableCollection<Locker>();
            StudentsDataGrid.ItemsSource = Students;
            LoadData();
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
                case "Locker":
                    LockerWindow lockerWindow = new LockerWindow();
                    lockerWindow.Show();
                    this.Close();
                    break;
            }
        }

        private void LoadData()
        {
            var students = DataAccess.GetStudents();
            Students.Clear();
            foreach (var student in students)
            {
                Students.Add(student);
            }

            var lockers = DataAccess.GetLockers();
            Lockers.Clear();
            foreach (var locker in lockers)
            {
                Lockers.Add(locker);
            }
        }
    }
}
