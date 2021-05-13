using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teachify.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class VerifyCodeViewModel
    {
        public string Provider { get; set; }
        
        public string Email { get; set; }

        public string Code { get; set; }
        public string ReturnUrl { get; set; }
        public string Verification { get; set; }

        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class VerifyEmailViewModel
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string Verification { get; set; }
    }

    public class ForgotViewModel
    {
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Verification { get; set; }

        public string AccountType { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string OldPassword { get; set; }

        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }
    }
}
