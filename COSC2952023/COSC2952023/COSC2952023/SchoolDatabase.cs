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
                Class myclass2 = new Class { name = "CDBM280", credits = 6, year = 2023 };
                database.Insert(myclass);
                database.Insert(myclass2);
                Grade grade = new Grade { classID = 1, grade = 75 };
                //Grade grade2 = new Grade { classID = 2, grade = 80 };
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
        //get grade from id
        public Grade GetGrade(int id)
        {
            return database.Table<Grade>().FirstOrDefault(g => g.classID == id);
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
        public List<Class> GetHighCreditClasses()
        {
            return database.Table<Class>().Where(c => c.credits >= 5).ToList<Class>();
        }
        
        //save grade
        // Save grade
        public int SaveGrade(Grade newGrade)
        {
            var existingGrade = database.Table<Grade>().FirstOrDefault(g => g.classID == newGrade.classID);

            if (existingGrade != null)
            {
                throw new InvalidOperationException("A grade for this class already exists.");
            }

            return database.Insert(newGrade);
        }
        
        public void DeleteGrades()
        {
            database.DeleteAll<Grade>();
        }

    }
}
