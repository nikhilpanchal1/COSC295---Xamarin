using System;
using SalesTrack.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditCustomerPage : ContentPage
    {
        private AddEditCustomerViewModel _viewModel;
        public AddEditCustomerPage()
        {
            InitializeComponent();
            _viewModel = new AddEditCustomerViewModel();
            BindingContext = _viewModel;
        }
        private async void SaveClicked(object sender, EventArgs e)
        {
            // Execute the SaveCommand to save the customer data to the database
            _viewModel.SaveCommand.Execute(null);

            // Navigate back to the previous page
            await Shell.Current.Navigation.PopAsync();
        }
    }
}



//// Get the binding context of the page, which should be an instance of AddEditCustomerViewModel
//var viewModel = (AddEditCustomerViewModel)BindingContext;