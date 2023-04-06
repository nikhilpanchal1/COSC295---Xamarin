using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SalesTrack.Data;

using SalesTrack.Views;

namespace SalesTrack.ViewModels
{
    public class CustomersViewModel
    {
        private readonly DatabaseContext _databaseContext;

        public ICommand ViewCustomerInteractionsCommand { get; }

        public ObservableCollection<Customer> Customers { get; }
        
        
        public CustomersViewModel()
        {
            _databaseContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));
            Customers = new ObservableCollection<Customer>(_databaseContext.GetCustomers());
            ViewCustomerInteractionsCommand = new Command<Customer>(async (customer) => await NavigateToInteractionsPage(customer));
            LoadCustomers();
        }
        public async Task LoadCustomers()
        {
            Customers.Clear();
            var customersList = await _databaseContext.GetCustomersAsync();
            foreach (var customer in customersList)
            {
                Customers.Add(customer);
            }
        }
        public async Task NavigateToInteractionsPage(Customer customer)
        {
            if (customer != null)
            {
                await Shell.Current.Navigation.PushAsync(new InteractionsPage(customer));
            }
        }


        public void DeleteCustomer(Customer customer)
        {
            _databaseContext.DeleteCustomer(customer);
            Customers.Remove(customer);
        }
    }
}