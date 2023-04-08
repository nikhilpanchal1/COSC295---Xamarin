using Xamarin.Forms;

namespace SalesTrack.Data
{
    public class PurchaseToggle : Switch
    {
        public PurchaseToggle()
        {
            Toggled += OnToggled;
        }

        private void OnToggled(object sender, ToggledEventArgs e)
        {
            // Handle the toggling of the Purchased property
            // You may need to modify this to update the database or the ViewModel.
        }
    }
}