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

        public static int GetLast()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.ExecuteScalar<int>("SELECT MAX(StudentID) FROM Students");
            }
        }

        public static IEnumerable<Locker> GetLockers()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return connection.Query<Locker>("SELECT * FROM Lockers");
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

        public class Locker
        {
            public int LockerID { get; set; }
            public int Number { get; set; }
            public int Unit { get; set; }
            public int Floor { get; set; }
            public double ClosestClass { get; set; }
        }
    }
}