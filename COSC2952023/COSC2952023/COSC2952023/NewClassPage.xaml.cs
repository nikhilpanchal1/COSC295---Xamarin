using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COSC2952023
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewClassPage : ContentPage
    {
        public NewClassPage()
        {
            InitializeComponent();
            CreditsLabel.Text = "1";
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            CreditsLabel.Text = Math.Round(e.NewValue).ToString();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var newClass = new Class
            {
                name = ClassNameEntry.Text,
                year = int.Parse(ClassYearEntry.Text),
                credits = int.Parse(CreditsLabel.Text)
            };

            var dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath("SchoolDatabase.db3");
            var schoolDatabase = new SchoolDatabase(dbPath);
            schoolDatabase.SaveClass(newClass);

            await Navigation.PopAsync();
        }
    }
}