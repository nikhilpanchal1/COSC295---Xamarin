using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace COSC2952023
{
    public class Class
    {
        private int _ID;
        private string _name;
        private int _year;
        private int _credits;

        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public int year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
            }
        }
        public int credits
        {
            get
            {
                return _credits;
            }
            set
            {
                _credits = value;
            }
        }
        public override string ToString()
        {
            return ID + " " + name + " " + year + " " + credits;
        }
    }
}
