using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

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
            if (GetCustomers().Count == 0)
            {
                InsertDummyData();
            }
        }
        
        private void InsertDummyData()
        {
            var customers = new List<Customer>
            {
                new Customer { FirstName = "Marshall", LastName = "Matthers", Address = "Miane Street", Phone = "222-222-1246", Email = "marshall.mat@example.com" },
                new Customer { FirstName = "Jigger", LastName = "Jaggernaut", Address = "Southwest", Phone = "455-555-5678", Email = "jaggernaut@example.com" },
                new Customer { FirstName = "Slob", LastName = "Smol", Address = "TAXESS", Phone = "123-545-5454", Email = "TaxesSmol@example.com" }
            };

            foreach (var customer in customers)
            {
                AddCustomer(customer);
            }
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

        
    }
}