using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SalesTrack.Data;

namespace SalesTrack.ViewModels
{
    public class SettingsViewModel
    {
        private readonly DatabaseContext _databaseContext;

        public ICommand ResetAppCommand { get; }

        public SettingsViewModel()
        {
            _databaseContext = new DatabaseContext(DependencyService.Get<IFileHelper>().GetLocalFilePath("database.sqlite"));
            ResetAppCommand = new Command(async () => await ResetApp());
        }

        private async Task ResetApp()
        {
            await _databaseContext.ResetAppDataAsync();
        }
    }
}