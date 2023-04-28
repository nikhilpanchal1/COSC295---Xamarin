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
                if (value < 2023)
                {
                    throw new ArgumentException("Year must be 2023 or greater.");
                }
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
                if (value < 1)
                {
                    throw new ArgumentException("Credits must be greater than 0.");
                }
                _credits = value;
            }
        }
        public override string ToString()
        {
            return ID + " " + name + " " + year + " " + credits;
        }
    }
}