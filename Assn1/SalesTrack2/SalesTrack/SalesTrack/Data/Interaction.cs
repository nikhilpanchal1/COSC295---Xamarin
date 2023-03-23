using System;
using SQLite;

namespace SalesTrack.Data
{
    public class Interaction
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public int ProductID { get; set; }
        public bool Purchased { get; set; }
    }
}