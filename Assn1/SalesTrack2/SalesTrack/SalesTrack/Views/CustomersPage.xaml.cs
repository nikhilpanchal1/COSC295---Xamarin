using System;
using SalesTrack.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SalesTrack.ViewModels;

namespace SalesTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomersPage : ContentPage
    {
        private CustomersViewModel _viewModel;

        public CustomersPage()
        {
            InitializeComponent();

            _viewModel = new CustomersViewModel();
            BindingContext = _viewModel;

            var listView = new ListView
            {
                ItemsSource = _viewModel.Customers,
                ItemTemplate = new DataTemplate(() =>
                {
                    var textCell = new TextCell();
                    textCell.SetBinding(TextCell.TextProperty, "FirstName");
                    textCell.SetBinding(TextCell.DetailProperty, "LastName");
                    textCell.ContextActions.Add(new MenuItem
                    {
                        Text = "Delete",
                        IsDestructive = true,
                        Command = new Command<Customer>((customer) => _viewModel.DeleteCustomer(customer)),
                        CommandParameter = textCell.BindingContext
                    });

                    textCell.Tapped += async (sender, args) =>
                    {
                        if (sender is TextCell cell && cell.BindingContext is Customer customer)
                        {
                            _viewModel.ViewCustomerInteractionsCommand.Execute(customer);
                        }
                    };

                    return textCell;
                })
            };

            var addNewCustomerButton = new Button
            {
                Text = "Add New Customer",
                Command = new Command(async () => await Navigation.PushAsync(new AddEditCustomerPage()))
            };

            Content = new StackLayout
            {
                Children = { listView, addNewCustomerButton }
            };
        }
    }
}
