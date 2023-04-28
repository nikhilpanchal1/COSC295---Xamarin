using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COSC2952023
{
    public class SchoolDatabase
    {
        readonly SQLiteConnection database;

        public SchoolDatabase(string dbPath)
        {
            database = new SQLiteConnection(dbPath);

            database.CreateTable<Class>();
            database.CreateTable<Grade>();

            if (database.Table<Class>().Count() == 0)
            {
                Class myclass = new Class { name = "COSC295", credits = 5, year = 2023 };
                database.Insert(myclass);
                Grade grade = new Grade { classID = 1, grade = 75 };
                database.Insert(grade);
            }
        }

        public List<Class> GetClasses()
        {
            return database.Table<Class>().ToList<Class>();
        }

        public List<Grade> GetGrades()
        {
            return database.Table<Grade>().ToList<Grade>();
        }

        public int SaveClass(Class myclass)
        {
            if (myclass.ID != 0)
            {
                return database.Update(myclass);
            }
            else
            {
                return database.Insert(myclass);
            }
        }

        ////
        // Add new methods here:
        ////
    }
}
