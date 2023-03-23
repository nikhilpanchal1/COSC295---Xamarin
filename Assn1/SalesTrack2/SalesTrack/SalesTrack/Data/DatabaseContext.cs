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