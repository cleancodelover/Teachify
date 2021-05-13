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
    public partial class SearchInstructorPage : ContentPage
    {
        public ObservableCollection<ProviderVM> Instructors;
        public bool First = true;
        private string _course;
        private string _city;
        private string _gender;
        public SearchInstructorPage(string course, string city, string gender)
        {
            InitializeComponent();
            Instructors = new ObservableCollection<ProviderVM>();
            _city = city;
            _course = course;
            _gender = gender;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AppService apiService = new AppService();
            var instructors = await apiService.SearchInstructors(_course, _gender, _city);
            if (First)
            {
                foreach (var instructor in instructors)
                {
                    Instructors.Add(instructor);
                }

                LvInstructors.ItemsSource = Instructors;
            }
            First = false;
        }

        private void LvInstructors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedInstructor = e.SelectedItem as ProviderVM;
            Navigation.PushAsync(new InstructorProfilePage(selectedInstructor.Id));
        }

        private void TbSearch_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FindInstructorPage());
        }
    }
}