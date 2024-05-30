using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;

namespace SchoolLockersManager
{
	public class DataAccess
	{
		private static string dbFilePath = "lockersDB.sqlite";
		private static string connectionString = $"Data Source={dbFilePath};Version=3;";

		public static IEnumerable<Student> GetStudents()
		{
			using (var connection = new SQLiteConnection(connectionString))
			{
                return connection.Query<Student>("SELECT * FROM Students");
			}
		}

		public static void AddStudent(Student student)
		{
			using (var connection = new SQLiteConnection(connectionString))
			{
				connection.Execute("INSERT INTO Students (Name, Surname, Class, Specialization, AttendsSchool) VALUES (@Name, @Surname, @Class, @Specialization, @AttendsSchool)", student);
            }
		}
	}

	public class Student
	{
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Class { get; set; }
        public string Specialization { get; set; }
        public bool AttendsSchool { get; set; }
    }
}
