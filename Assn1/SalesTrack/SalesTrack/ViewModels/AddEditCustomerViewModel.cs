using System.Windows.Input;
using Xamarin.Forms;
using SalesTrack.Data;
using SalesTrack.Models;
using Xamarin.Forms;

namespace SalesTrack.ViewModels
{
    public class AddEditCustomerViewModel : BaseViewModel
    {
        private readonly DatabaseContext _databaseContext;

        public ICommand SaveCommand { get; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public AddEditCustomerViewModel()
        {
            _databaseContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));

            SaveCommand = new Command(async () =>
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

                await Shell.Current.Navigation.PopAsync();
            });

        }
    }
}