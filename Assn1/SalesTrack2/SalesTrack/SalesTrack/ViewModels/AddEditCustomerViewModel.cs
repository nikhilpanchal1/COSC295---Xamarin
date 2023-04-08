using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SalesTrack.Data;
using SalesTrack.Views;

namespace SalesTrack.ViewModels
{
    public class AddEditCustomerViewModel
    {
        private readonly DatabaseContext _databaseContext;
        public ICommand GoToProductsSettingsCommand { get; }

        public ICommand SaveCustomerAsync { get; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public AddEditCustomerViewModel()
        {
            _databaseContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));

            SaveCustomerAsync = new Command(async () => await SaveCustomer());
            GoToProductsSettingsCommand = new Command(async () => await GoToProductsSettings());

        }

        public async Task<bool> SaveCustomer()
        {
            var newCustomer = new Customer
            {
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                Phone = Phone,
                Email = Email
            };

            _databaseContext.AddCustomer(newCustomer);
            await _databaseContext.SaveChangesAsync();

            return true;
        }
        private async Task GoToProductsSettings()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());
        }

    }
}
