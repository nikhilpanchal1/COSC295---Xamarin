using System.Collections.Generic;
using System.Collections.ObjectModel;
using SalesTrack.Data;
using Xamarin.Forms;

namespace SalesTrack.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private ObservableCollection<DatabaseContext.Product> _products;
        public ObservableCollection<DatabaseContext.Product> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }

        public ProductsViewModel()
        {
            Products = new ObservableCollection<DatabaseContext.Product>(GetAllProducts());
        }

        private List<DatabaseContext.Product> GetAllProducts()
        {
            DatabaseContext dbContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));
            return dbContext.GetProducts();
        }
    }
}