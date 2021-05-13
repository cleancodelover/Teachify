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
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<ProviderVM> Instructors;
        public bool First = true;
        public HomePage()
        {
            InitializeComponent();
            Instructors = new ObservableCollection<ProviderVM>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AppService apiService = new AppService();
            var instructors = await apiService.GetInstructors();
            if (First)
            {
                foreach (var instructor in instructors)
                {
                    Instructors.Add(instructor);
                }
                LvInstructors.ItemsSource = Instructors;
                BusyIndicator.IsRunning = false;
            }
            First = false;
        }

        private void LvInstructors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedInstructor = e.SelectedItem as ProviderVM;
            if(selectedInstructor != null)
            {
                Navigation.PushAsync(new InstructorProfilePage(selectedInstructor.Id));
            }
            ((ListView)sender).SelectedItem = null;
        }

        private void TbSearch_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FindInstructorPage());
        }
    }
}