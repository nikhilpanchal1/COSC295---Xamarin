using System;
using SalesTrack.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SalesTrack.ViewModels;

namespace SalesTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InteractionsPage : ContentPage
    {
        private readonly InteractionsViewModel _viewModel;

        public InteractionsPage(Customer customer)
        {
            InitializeComponent();
            BindingContext = _viewModel = new InteractionsViewModel(customer);
        }

        private void AddNewInteractionClicked(object sender, EventArgs e)
        {
            AddInteractionTableView.IsVisible = true;
        }
    }
}