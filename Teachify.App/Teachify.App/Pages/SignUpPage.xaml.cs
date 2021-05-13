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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            BusyIndicator.IsRunning = true;
            AppService apiService = new AppService();
            bool response = await apiService.RegisterUser(EntEmail.Text, EntPassword.Text, EntConfirmPassword.Text);
            if (!response)
            {
                BusyIndicator.IsRunning = false;
                await DisplayAlert("Oops", "Something went wrong", "Cancel");

            }
            else
            {
                BusyIndicator.IsRunning = false;
                await DisplayAlert("Hi", "Your Account has been created", "Alright");
                await Navigation.PopToRootAsync();
            }
        }
    }
}