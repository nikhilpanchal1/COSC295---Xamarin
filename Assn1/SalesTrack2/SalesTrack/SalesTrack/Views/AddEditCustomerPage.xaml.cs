using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SalesTrack.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditCustomerPage : ContentPage
    {
        private AddEditCustomerViewModel _viewModel;
        public ICommand GoToProductsSettingsCommand { get; }

        public AddEditCustomerPage()
        {
            InitializeComponent();
            _viewModel = new AddEditCustomerViewModel();
            BindingContext = _viewModel;
            GoToProductsSettingsCommand = new Command(async () => await GoToProductsSettings());

        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            // Execute the SaveCustomerAsync method to save the customer data to the database
            bool saveResult = await _viewModel.SaveCustomer();

            if (saveResult)
            {
                // Navigate back to the CustomersPage
                await Navigation.PopAsync();
            }
        }
        private async Task GoToProductsSettings()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());
        }

    }
}