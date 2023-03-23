using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesTrack.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditCustomerPage : ContentPage
    {
        public AddEditCustomerPage()
        {
            InitializeComponent();
        }
        private async void SaveClicked(object sender, EventArgs e)
        {
            // Get the binding context of the page, which should be an instance of AddEditCustomerViewModel
            var viewModel = (AddEditCustomerViewModel)BindingContext;

            // Execute the SaveCommand to save the customer data to the database
            viewModel.SaveCommand.Execute(null);

            // Navigate back to the previous page
            await Shell.Current.Navigation.PopAsync();
        }



    }
}