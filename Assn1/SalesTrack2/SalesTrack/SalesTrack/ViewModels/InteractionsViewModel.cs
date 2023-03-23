using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SalesTrack.Data;


namespace SalesTrack.ViewModels
{
    public class InteractionsViewModel
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Customer _customer;

        public ICommand DeleteInteractionCommand { get; }
        public ICommand SaveInteractionCommand { get; }

        public ObservableCollection<Interaction> Interactions { get; }
        public ObservableCollection<Product> Products { get; }

        public Product SelectedProduct { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public bool Purchased { get; set; }

        public InteractionsViewModel(Customer customer)
        {
            _databaseContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));
            _customer = customer;

            Interactions = new ObservableCollection<Interaction>(_databaseContext.GetInteractionsByCustomer(_customer.ID));
            Products = new ObservableCollection<Product>(_databaseContext.GetProducts());

            DeleteInteractionCommand = new Command<Interaction>(DeleteInteraction);
            SaveInteractionCommand = new Command(async () => await SaveInteraction());
            
            SelectedProduct = Products.FirstOrDefault();
            Date = DateTime.Today;
            Purchased = false;
        }

        public void DeleteInteraction(Interaction interaction)
        {
            _databaseContext.DeleteInteraction(interaction);
            Interactions.Remove(interaction);
        }

        private async Task SaveInteraction()
        {
            var interaction = new Interaction
            {
                CustomerID = _customer.ID,
                ProductID = SelectedProduct.ID,
                Date = Date,
                Comments = Comments,
                Purchased = Purchased
            };

            _databaseContext.AddInteraction(interaction);

            Interactions.Add(interaction);

            await _databaseContext.SaveChangesAsync();
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
