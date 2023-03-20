using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesTrack.Data;
using SalesTrack.ViewModels;
using SalesTrack.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomersPage : ContentPage
    {
        private readonly CustomersViewModel _viewModel;
        public CustomersPage()
        {
            InitializeComponent();

            _viewModel = new CustomersViewModel();
            BindingContext = _viewModel;
        }
        private async void AddNewCustomerClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new AddEditCustomerPage());
        }

        private async void OnDeleteCustomerSwipeItemInvoked(object sender, EventArgs e)
        {
            var menuItem = sender as SwipeItem;
            var customer = menuItem.CommandParameter as Customer;

            var confirmation = await DisplayAlert("Delete Customer?", $"Are you sure you want to delete {customer.FullName}?", "Yes", "No");

            if (confirmation)
            {
                _viewModel.DeleteCustomer(customer);
            }
        }

        private async void ViewCustomerInteractionsCommandHandler(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var customer = menuItem.CommandParameter as Customer;

            await Shell.Current.Navigation.PushAsync(new InteractionsPage(customer));
        }
    }
}