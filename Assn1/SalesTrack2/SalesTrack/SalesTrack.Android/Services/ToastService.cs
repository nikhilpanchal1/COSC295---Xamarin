using Android.Widget;
using SalesTrack.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SalesTrack.Droid.Services.ToastService))]
namespace SalesTrack.Droid.Services
{
    public class ToastService : IToastService
    {
        public void Show(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}