using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teachify.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teachify.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            AppService apiService = new AppService();
            var response = await apiService.ChangePassword(EntOldPassword.Text, EntNewPassword.Text, EntConfirmPassword.Text);
            if (!response)
            {
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
            else
            {
                await DisplayAlert("Password Changed", "Kindly login with your new password", "Alright");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}