using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace COSC2952023
{
    public class Grade
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int grade { get; set; }
        public int classID { get; set; }

        public override string ToString()
        {
            return ID + " " + classID + " " + grade;
        }
    }
}
