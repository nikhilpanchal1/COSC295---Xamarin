using SalesTrack.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SalesTrack.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}