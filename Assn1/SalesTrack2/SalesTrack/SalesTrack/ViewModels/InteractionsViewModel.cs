using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SalesTrack.Data;
using SalesTrack.Services;

namespace SalesTrack.ViewModels
{
    public class InteractionsViewModel
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Customer _customer;

        public ICommand DeleteInteractionCommand { get; }
        public ICommand SaveInteractionCommand { get; }
        public ObservableCollection<Interaction> Interactions { get; }
        public ObservableCollection<DatabaseContext.Product> Products { get; } // Corrected the type
        public ObservableCollection<DatabaseContext.Product> CustomerProducts { get; } // New property

        public DatabaseContext.Product SelectedProduct { get; set; } // Corrected the type
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public bool Purchased { get; set; }

        public InteractionsViewModel(Customer customer)
        {
           
            _databaseContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));
            _customer = customer;

            Interactions = new ObservableCollection<Interaction>(_databaseContext.GetInteractionsByCustomer(_customer.ID));
            Products = new ObservableCollection<DatabaseContext.Product>(_databaseContext.GetProducts().Where(p => p != null));
            CustomerProducts = new ObservableCollection<DatabaseContext.Product>(_databaseContext.GetProductsByCustomer(_customer.ID)); // Populate CustomerProducts

            DeleteInteractionCommand = new Command<Interaction>(DeleteInteraction);
            SaveInteractionCommand = new Command(async () => await SaveInteraction());

            SelectedProduct = Products.FirstOrDefault();
            Date = DateTime.Today;
            Purchased = false;
            Interactions.CollectionChanged += Interactions_CollectionChanged;
        }
        private void Interactions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (Interaction interaction in e.NewItems)
                {
                    interaction.PropertyChanged += Interaction_PropertyChanged;
                }
            }
        }
        private void Interaction_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Interaction.IsPurchased))
            {
                UpdateInteraction((Interaction)sender);
            }
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
            
            // Display a toast message after saving
            DependencyService.Get<IToastService>().Show("Interaction saved!");
            
            // Clear the input fields after saving
            SelectedProduct = Products.FirstOrDefault();
            Date = DateTime.Today;
            Comments = string.Empty;
            Purchased = false;
        }
        public void UpdateInteraction(Interaction interaction)
        {
            _databaseContext.UpdateInteraction(interaction);
            _databaseContext.SaveChangesAsync();
        }
        
        
    }
}
