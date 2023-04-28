using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COSC2952023
{
    public partial class App : Application
    {
        static SchoolDatabase database;
        public static SchoolDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new SchoolDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("grades.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
