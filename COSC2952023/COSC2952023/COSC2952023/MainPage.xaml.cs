using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace COSC2952023
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath("SchoolDatabase.db3");

            var schoolDatabase = new SchoolDatabase(dbPath);
            var classes = schoolDatabase.GetClasses();
            ClassListView.ItemsSource = classes;
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewClassPage());
        }
    }
}