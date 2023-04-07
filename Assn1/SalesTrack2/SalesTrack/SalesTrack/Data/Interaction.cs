using System;
using System.Linq;
using SQLite;
using Xamarin.Forms;

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
    // Interaction.cs
    [Ignore]
    public string CustomerFullName
    {
        get
        {
            DatabaseContext dbContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));
            var customer = dbContext.GetCustomers().FirstOrDefault(c => c.ID == CustomerID);
            return customer?.FirstName + " " + customer?.LastName;
        }
    }
    // Interaction.cs
    [Ignore]
    public string InteractionDisplayText
    {
        get
        {
            DatabaseContext dbContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));
            var product = dbContext.GetProductById(ProductID);
            return $"{CustomerFullName}\n{Date:dddd, MMMM d, yyyy}\t{Comments}\n{product.Name}. Purchased: {Purchased}";
        }
    }
    // Interaction.cs
    [Ignore]
    public string ProductName
    {
        get
        {
            DatabaseContext dbContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));
            var product = dbContext.GetProductById(ProductID);
            return product?.Name;
        }
    }
    // Interaction.cs
    [Ignore]
    public string DateString
    {
        get
        {
            return Date.ToString("dddd, MMMM d, yyyy");
        }
    }

    [Ignore]
    public string ProductDisplay
    {
        get
        {
            DatabaseContext dbContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));
            var product = dbContext.GetProductById(ProductID);
            return $"{product.Name}. Purchased: {Purchased}";
        }
    }


    }
}