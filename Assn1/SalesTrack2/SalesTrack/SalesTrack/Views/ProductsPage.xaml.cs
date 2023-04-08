using SalesTrack.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        public ProductsPage()
        {
            InitializeComponent();
            BindingContext = new ProductsViewModel();
        }
    }
}