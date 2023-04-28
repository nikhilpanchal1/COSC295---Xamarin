using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COSC2952023
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradePage : ContentPage
    {
        private Class _selectedClass;
        private SchoolDatabase _schoolDatabase;

        public GradePage(Class selectedClass, SchoolDatabase schoolDatabase)
        {
            InitializeComponent();
            _selectedClass = selectedClass;
            _schoolDatabase = schoolDatabase;

            Title = selectedClass.name;

            Grade currentGrade = _schoolDatabase.GetGrade(selectedClass.ID);
            if (currentGrade != null)
            {
                GradePicker.SelectedIndex = 4 - currentGrade.grade;
                GradePicker.IsEnabled = false;
                SaveGradeButton.IsVisible = false;
            }
        }

        private async void OnSaveGradeClicked(object sender, EventArgs e)
        {
            int gradeValue = 4 - GradePicker.SelectedIndex;
            Grade newGrade = new Grade { classID = _selectedClass.ID, grade = gradeValue };
            _schoolDatabase.SaveGrade(newGrade);

            await Navigation.PopAsync();
        }
    }
}