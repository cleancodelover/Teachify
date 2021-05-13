using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teachify.App.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teachify.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            AppService apiService = new AppService();
            BusyIndicator.IsRunning = true;
            var response = await apiService.GetToken(EntEmail.Text, EntPassword.Text);
            if (string.IsNullOrEmpty(response?.access_token))
            {
                BusyIndicator.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong", "Alright");
            }
            else
            {
                BusyIndicator.IsRunning = false;
                Preferences.Set("useremail", EntEmail.Text);
                Preferences.Set("password", EntPassword.Text);
                Preferences.Set("access_token", response.access_token);
                Application.Current.MainPage = new MasterPage();
            }
        }

        private void TapSignUp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        private void TapForgotPassword_Tapped(object sender, EventArgs e)
        {

        }
    }
}