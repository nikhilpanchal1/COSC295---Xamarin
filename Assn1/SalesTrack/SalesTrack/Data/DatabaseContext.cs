using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace SalesTrack.Data
{
    public class DatabaseContext
    {
        private SQLiteConnection _connection;

        public DatabaseContext(string dbPath)
        {
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Customer>();
            _connection.CreateTable <Interaction>();
            _connection.CreateTable<Product>();
        }

        public void AddCustomer(Customer customer)
        {
            _connection.Insert(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _connection.Update(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            _connection.Delete(customer);
        }

        public List<Customer> GetCustomers()
        {
            return _connection.Table<Customer>().ToList();
        }
        
        //Methods for interaction
        public void AddInteraction(Interaction interaction)
        {
            _connection.Insert(interaction);
        }

        public void UpdateInteraction(Interaction interaction)
        {
            _connection.Update(interaction);
        }

        public void DeleteInteraction(Interaction interaction)
        {
            _connection.Delete(interaction);
        }

        public List<Interaction> GetInteractionsByCustomer(int customerId)
        {
            return _connection.Table<Interaction>().Where(i => i.CustomerID == customerId).ToList();
        }

        public List<Interaction> GetInteractionsByProduct(int productId)
        {
            return _connection.Table<Interaction>().Where(i => i.ProductID == productId).ToList();
        }
        
        //For product
        public void AddProduct(Product product)
        {
            _connection.Insert(product);
        }

        public void UpdateProduct(Product product)
        {
            _connection.Update(product);
        }

        public void DeleteProduct(Product product)
        {
            _connection.Delete(product);
        }

        public List<Product> GetProducts()
        {
            return _connection.Table<Product>().ToList();
        }

        public Product GetProductById(int productId)
        {
            return _connection.Table<Product>().FirstOrDefault(p => p.ID == productId);
        }
        public async Task SaveChangesAsync()
        {
            await Task.Run(() => _connection.Commit());
        }

        
    }
}