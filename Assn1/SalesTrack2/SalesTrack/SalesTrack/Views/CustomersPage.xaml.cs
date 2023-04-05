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
                    var grid = new Grid();
                    var nameLabel = new Label();
                    var lastNameLabel = new Label { FontAttributes = FontAttributes.Italic };
                    nameLabel.SetBinding(Label.TextProperty, "FirstName");
                    lastNameLabel.SetBinding(Label.TextProperty, "LastName");

                    grid.Children.Add(nameLabel);
                    grid.Children.Add(lastNameLabel, 1, 0);

                    var deleteItem = new SwipeItem
                    {
                        Text = "Delete",
                        BackgroundColor = Color.Red,
                        Command = new Command<Customer>((customer) => _viewModel.DeleteCustomer(customer)),
                        CommandParameter = grid.BindingContext
                    };

                    var swipeItems = new SwipeItems { deleteItem };
                    swipeItems.Mode = SwipeMode.Execute;

                    var swipeView = new SwipeView
                    {
                        RightItems = swipeItems,
                        Content = grid
                    };
                    swipeView.SetBinding(SwipeView.BindingContextProperty, ".");
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                    var viewCell = new ViewCell { View = swipeView };
                    viewCell.Tapped += async (sender, args) =>
                    {
                        if (sender is ViewCell cell && cell.BindingContext is Customer customer)
                        {
                            _viewModel.ViewCustomerInteractionsCommand.Execute(customer);
                        }
                    };

                    return viewCell;
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
