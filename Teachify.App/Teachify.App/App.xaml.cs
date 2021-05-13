using System;
using Teachify.App.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teachify.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(Preferences.Get("access_token","")))
            {
                MainPage = new NavigationPage(new MasterPage());
            }else if(string.IsNullOrEmpty(Preferences.Get("useremail","")) && string.IsNullOrEmpty(Preferences.Get("password", "")))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
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
}
