using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using SalesTrack.Data;
using SalesTrack.Models;
using SalesTrack.Views;

namespace SalesTrack.ViewModels
{
    public class CustomersViewModel : BaseViewModel
    {
        private readonly DatabaseContext _databaseContext;

        public ICommand ViewCustomerInteractionsCommand { get; }

        public ObservableCollection<Customer> Customers { get; }
        
        
        public CustomersViewModel()
        {
            _databaseContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));
            Customers = new ObservableCollection<Customer>(_databaseContext.GetCustomers());

            ViewCustomerInteractionsCommand = new Command<Customer>(async (customer) =>
            {
                await Shell.Current.Navigation.PushAsync(new InteractionsPage(customer));
            });
        }

        public void DeleteCustomer(Customer customer)
        {
            _databaseContext.DeleteCustomer(customer);
            Customers.Remove(customer);
        }
    }
}