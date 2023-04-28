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
        private SchoolDatabase _schoolDatabase;
        public MainPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath("SchoolDatabase.db3");

            _schoolDatabase = new SchoolDatabase(dbPath);

            var classes = _schoolDatabase.GetClasses();
            ClassListView.ItemsSource = classes;
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new NewClassPage());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
           
        }

        private async void OnRefreshing(object sender, EventArgs e)
        {
            LoadData();
            RefreshViewControl.IsRefreshing = false;
        }
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem == null)
                    return;

                Class selectedClass = e.SelectedItem as Class;
                await Navigation.PushAsync(new GradePage(selectedClass, _schoolDatabase));

                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            
        }
        
        private void OnDeleteGradesButtonClicked(object sender, EventArgs e)
        {
            _schoolDatabase.DeleteGrades();
            DisplayAlert("Grades Deleted", "All grades have been deleted.", "OK");
        }


    }
}