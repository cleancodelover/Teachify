using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Teachify.Models;
using Xamarin.Essentials;

namespace Teachify.App.Services
{
    public class AppService
    {
        public async Task<bool> RegisterUser(string email, string password, string confirmPassword)
        {
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            if(password == confirmPassword && !string.IsNullOrEmpty(email))
            {
                var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(registerModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://teachifyapp.azurewebsites.net/api/account/register", content);
                return response.IsSuccessStatusCode;
            }
            return false;
        }

        public async Task<TokenResponse> GetToken(string email, string password)
        {
            var httpClient = new HttpClient();
            var content = new StringContent($"grant_type=password&username={email}&password={password}", Encoding.UTF8, "application/x-www-form-urlencoder");
            var response = await httpClient.PostAsync("https://teachifyapp.azurewebsites.net//Token", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TokenResponse>(jsonResult);
            return result;
        }

        public async Task<bool> PasswordRecovery(string email)
        {
            var httpClient = new HttpClient();
            var recoverPasswordModel = new PasswordRecoveryModel()
            {
                Email = email
            };
            var json = JsonConvert.SerializeObject(recoverPasswordModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://teachifyapp.azurewebsites.net/api/Users/PasswordRecovery", content);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var httpClient = new HttpClient();
            var changePasswordModel = new ResetPasswordViewModel()
            {
                OldPassword = oldPassword,
                Password=newPassword,
                ConfirmPassword=confirmPassword
            };

            var json = JsonConvert.SerializeObject(changePasswordModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", Preferences.Get("access_token", "")));
            var response = await httpClient.PostAsync("https://teachifyapp.azurewebsites.net/api/account/changepassword", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> BecomeAnInstructor(ProviderVM vm)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(vm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var token = Preferences.Get("access_token", "");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");

            var response = await httpClient.PostAsync("https://teachify.azurewebsites.net/api/providers/newprovider", content);
            return response.StatusCode == HttpStatusCode.Created;
        }

        public async Task<List<ProviderVM>> GetInstructors()
        {
            var httpClient = new HttpClient();
            var token = Preferences.Get("access_token", "");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await httpClient.GetStringAsync("https://teachifyapp.azurewebsites.net/api/providers/getallproviders");
            return JsonConvert.DeserializeObject<List<ProviderVM>>(response);
        }

        public async Task<ProviderVM> GetInstructor(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", ""));
            var response = await httpClient.GetStringAsync($"https://teachifyapp.azurewebsites.net/api/providers/getprovider/{id}");
            return JsonConvert.DeserializeObject<ProviderVM>(response);
        }

        public async Task<List<ProviderVM>> SearchInstructors(string subject, string gender, string city)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", ""));
            var response = await httpClient.GetStringAsync($"https://teachifyapp.azurewebsites.net/api/providers/getcityproviders?subject={subject}&gender={gender}&city={city}");
            return JsonConvert.DeserializeObject<List<ProviderVM>>(response);
        }

        public async Task<List<CityVM>> GetCities()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("https://teachifyapp.azurewebsites.net/api/settings/getcities");
            return JsonConvert.DeserializeObject<List<CityVM>>(response);
        }

        public async Task<List<CourseVM>> GetCourses()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("https://teachifyapp.azurewebsites.net/api/courses/getallcourses");
            return JsonConvert.DeserializeObject<List<CourseVM>>(response);
        }
    }
}
