using SalesTrack.ViewModels;
using Xamarin.Forms;

namespace SalesTrack.Data
{
    public class PurchaseToggle : Switch
    {
        public static readonly BindableProperty InteractionProperty = BindableProperty.Create(nameof(Interaction), typeof(Interaction), typeof(PurchaseToggle), null, propertyChanged: OnInteractionChanged);

        public Interaction Interaction
        {
            get => (Interaction)GetValue(InteractionProperty);
            set => SetValue(InteractionProperty, value);
        }
        public PurchaseToggle()
        {
            SetBinding(IsToggledProperty, new Binding("IsPurchased", BindingMode.TwoWay));
        }
        private static void OnInteractionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Handle when the interaction changes (beyond the scope of this assn)
        }
        private void OnToggled(object sender, ToggledEventArgs e)
        {
            if (Interaction != null)
            {
                Interaction.Purchased = e.Value;
                // Call a method in the ViewModel to update the database
                var viewModel = BindingContext as InteractionsViewModel;
                viewModel?.UpdateInteraction(Interaction);
            }
        }

    }
}