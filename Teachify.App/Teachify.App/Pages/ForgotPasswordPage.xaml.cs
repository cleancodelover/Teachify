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
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void BtnSend_Clicked(object sender, EventArgs e)
        {
            AppService apiService = new AppService();
            var response = await apiService.PasswordRecovery(EntEmail.Text);
            if (!response)
            {
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
            else
            {
                await DisplayAlert("Hi", "A passwod has been sent to your email. Kindly  type new password", "Alright");
                await Navigation.PopToRootAsync();
            }
        }
    }
}