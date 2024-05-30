using System.IO;
using System.Data.SQLite;

namespace SchoolLockersManager
{
    class DatabaseInitializer
    {

        // Klasa ta odpowiedzialna jest za inicializacje lokalnej bazy danych. Do obsługi służy klasa "DataAccess"
        // w pliku o nazwie "DataAccess.cs"

        private static string dbFilePath = "lockersDB.sqlite";

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
                using (var connection = new SQLiteConnection($"Data Source = {dbFilePath};Version=3;"))
                {
                    connection.Open();
                    string createStudentsTable = @"
                    CREATE TABLE Students (
                        StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT,
                        Surname TEXT,
                        Class INTEGER,
                        Specialization TEXT,
                        AttendsSchool INTEGER
                    );";

                    string createLockersTable = @"
                    CREATE TABLE Lockers (
                        LockerID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Number INTEGER,
                        Unit INTEGER,
                        Floor INTEGER,
                        ClosestClass REAL
                    );";

                    string createAssignmentsTable = @"
                    CREATE TABLE Assignments (
                        AssignmentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        StudentID INTEGER,
                        LockerID INTEGER,
                        DateOfAssignment DATE,
                        FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                        FOREIGN KEY (LockerID) REFERENCES Lockers(LockerID)
                    );";

                    using (var command = new SQLiteCommand(createStudentsTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (var command = new SQLiteCommand(createLockersTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (var command = new SQLiteCommand(createAssignmentsTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}