using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace SalesTrack.Data
{
    public class DatabaseContext
    {
        private SQLiteConnection _sqCon;

        public DatabaseContext(string dbPath)
        {
            _sqCon = new SQLiteConnection(dbPath);
            _sqCon.CreateTable<Customer>();
            _sqCon.CreateTable <Interaction>();
            _sqCon.CreateTable<Product>();
            
            if (GetProducts().Count == 0)
            {
                InsertDummyProducts();
            }
        }
        
        private void InsertDummyProducts()
        {
            var products = new List<Product>
            {
                new Product { Name = "MonkE Coffee", Description = "Coffee for everyone!", Price = 5000.00 },
                new Product { Name = "ePhone", Description = "iPhone for the ones who dont like iPhones", Price = 99999.00 },
                new Product { Name = "MatchStick", Description = "Exactly one and only one matchstick", Price = 25.00 }
            };

            foreach (var product in products)
            {
                AddProduct(product);
            }
        }

        public class Product
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }

            [Ignore]
            public int NumberOfInteractions
            {
                get
                {
                    DatabaseContext dbContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));
                    return dbContext.GetInteractionsByProduct(ID).Count;
                }
            }

            // Override the ToString() method to return the desired string representation
            public override string ToString()
            {
                return Name;
            }
        }

        public List<Product> GetProductsByCustomer(int customerId)
        {
            List<Interaction> interactions = GetInteractionsByCustomer(customerId);
            List<Product> products = new List<Product>();

            foreach (Interaction interaction in interactions)
            {
                Product product = GetProductById(interaction.ProductID);
                if (product != null)
                {
                    products.Add(product);
                }
            }

            return products;
        }

        public void AddCustomer(Customer customer)
        {
            _sqCon.Insert(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _sqCon.Update(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            _sqCon.Delete(customer);
        }

        public List<Customer> GetCustomers()
        {
            return _sqCon.Table<Customer>().ToList();
        }
        
        //Methods for interaction
        public void AddInteraction(Interaction interaction)
        {
            _sqCon.Insert(interaction);
        }

        public void UpdateInteraction(Interaction interaction)
        {
            _sqCon.Update(interaction);
        }

        public void DeleteInteraction(Interaction interaction)
        {
            _sqCon.Delete(interaction);
        }

        public List<Interaction> GetInteractionsByCustomer(int customerId)
        {
            return _sqCon.Table<Interaction>().Where(i => i.CustomerID == customerId).ToList();
        }

        public List<Interaction> GetInteractionsByProduct(int productId)
        {
            return _sqCon.Table<Interaction>().Where(i => i.ProductID == productId).ToList();
        }
        
        //For product
        public void AddProduct(Product product)
        {
            _sqCon.Insert(product);
        }

        public void UpdateProduct(Product product)
        {
            _sqCon.Update(product);
        }

        public void DeleteProduct(Product product)
        {
            _sqCon.Delete(product);
        }

        public List<Product> GetProducts()
        {
            return _sqCon.Table<Product>().ToList();
        }

        public Product GetProductById(int productId)
        {
            return _sqCon.Table<Product>().FirstOrDefault(p => p.ID == productId);
        }
        public async Task SaveChangesAsync()
        {
            await Task.Run(() => _sqCon.Commit());
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await Task.Run(() => GetCustomers());
        }
    }
}