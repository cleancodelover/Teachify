using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teachify.App.Services;
using Teachify.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teachify.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindInstructorPage : ContentPage
    {
        public ObservableCollection<CourseVM> Courses;
        public ObservableCollection<CityVM> Cities;
        AppService apiService = new AppService();
        public FindInstructorPage()
        {
            InitializeComponent();
            Courses = new ObservableCollection<CourseVM>();
            Cities = new ObservableCollection<CityVM>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadCourses();
            LoadCities();
        }

        public async void LoadCourses()
        {
            var courses = await apiService.GetCourses();
            foreach(var course in courses)
            {
                Courses.Add(course);
            }
            PickerCourse.ItemsSource = Courses;
        }

        public async void LoadCities()
        {
            var cities = await apiService.GetCities();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
            PickerCity.ItemsSource = Cities;
        }
        private void BtnSearchInstructor_Clicked(object sender, EventArgs e)
        {
            if(PickerCourse.SelectedIndex < 0 || PickerCity.SelectedIndex < 0 || PickerGender.SelectedIndex < 0)
            {
                DisplayAlert("Oops", "Please select all the optinos", "Cancel");
            }
            else
            {
                var course = PickerCourse.Items[PickerCourse.SelectedIndex].Trim();
                var city = PickerCity.Items[PickerCity.SelectedIndex].Trim();
                var gender = PickerGender.Items[PickerGender.SelectedIndex].Trim();

                Navigation.PushAsync(new SearchInstructorPage(course, city, gender));
            }
        }
    }
}