using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teachify.App.Helpers;
using Teachify.App.Services;
using Teachify.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teachify.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BecomeInstructorPage : ContentPage
    {
        private MediaFile file;
        public BecomeInstructorPage()
        {
            InitializeComponent();
        }

        private async void TapCamera_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Custom,
                CustomPhotoSize = 30,
                CompressionQuality = 60,
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            ImgProfile.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void BtnApply_Clicked(object sender, EventArgs e)
        {
            byte[] imageArray = null;
            if(file != null)
            {
                imageArray = FileHelpers.ReadFully(file.GetStream());
                file.Dispose();
            }
            var instructor = new ProviderVM()
            {
                Firstname = EntName.Text,
                Language = EntLanguage.Text,
                Nationality = EntNationality.Text,
                Gender = PickerGender.Items[PickerGender.SelectedIndex],
                Phone = EntPhone.Text,
                Email = EntEmail.Text,
                Education = EntEducation.Text,
                Experience = PickerExperience.Items[PickerExperience.SelectedIndex],
                HourlyRate = PickerHourlyRate.Items[PickerHourlyRate.SelectedIndex],
                CourseDomain = PickerCourseDomain.Items[PickerCourseDomain.SelectedIndex],
                City = PickerCity.Items[PickerCity.SelectedIndex],
                OneLineTitle = EntOneLineTitle.Text,
                Description = EdtDescription.Text,
                ImageArray = imageArray
            };
            AppService apiService = new AppService();
            var response = await apiService.BecomeAnInstructor(instructor);
            if (!response)
            {
                await DisplayAlert("Oops", "Something wrong...", "Cancel");
            }
            else
            {
                await DisplayAlert("Congratulations", "You're now an instructor at teachify.", "Alright");
            }
        }
    }
}