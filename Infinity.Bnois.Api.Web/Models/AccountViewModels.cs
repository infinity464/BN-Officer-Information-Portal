
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.Api.Web
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ExternalLoginViewModel'
    public class ExternalLoginViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ExternalLoginViewModel'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ExternalLoginViewModel.Name'
        public string Name { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ExternalLoginViewModel.Name'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ExternalLoginViewModel.Url'
        public string Url { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ExternalLoginViewModel.Url'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ExternalLoginViewModel.State'
        public string State { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ExternalLoginViewModel.State'
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ManageInfoViewModel'
    public class ManageInfoViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ManageInfoViewModel'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ManageInfoViewModel.LocalLoginProvider'
        public string LocalLoginProvider { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ManageInfoViewModel.LocalLoginProvider'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ManageInfoViewModel.Email'
        public string Email { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ManageInfoViewModel.Email'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ManageInfoViewModel.Logins'
        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ManageInfoViewModel.Logins'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ManageInfoViewModel.ExternalLoginProviders'
        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ManageInfoViewModel.ExternalLoginProviders'
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserInfoViewModel'
    public class UserInfoViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserInfoViewModel'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserInfoViewModel.Email'
        public string Email { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserInfoViewModel.Email'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserInfoViewModel.HasRegistered'
        public bool HasRegistered { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserInfoViewModel.HasRegistered'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserInfoViewModel.LoginProvider'
        public string LoginProvider { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserInfoViewModel.LoginProvider'
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserLoginInfoViewModel'
    public class UserLoginInfoViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserLoginInfoViewModel'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserLoginInfoViewModel.LoginProvider'
        public string LoginProvider { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserLoginInfoViewModel.LoginProvider'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserLoginInfoViewModel.ProviderKey'
        public string ProviderKey { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserLoginInfoViewModel.ProviderKey'
    }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AddPhoneNumberViewModel'
    public class AddPhoneNumberViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AddPhoneNumberViewModel'
    {
        [Required]
        [Phone]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AddPhoneNumberViewModel.Number'
        public string Number { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AddPhoneNumberViewModel.Number'
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel'
    public class RegisterViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel'
    {

        [Required(ErrorMessage = "First Name is required")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel.FirstName'
        public string FirstName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel.FirstName'

        [Required(ErrorMessage = "Last Name is required")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel.LastName'
        public string LastName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel.LastName'

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel.Email'
        public string Email { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel.Email'
        [Required]
        [Phone]
        [Display(Name = "Phone")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel.PhoneNumber'
        public string PhoneNumber { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel.PhoneNumber'

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel.Password'
        public string Password { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel.Password'

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel.ConfirmPassword'
        public string ConfirmPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterViewModel.ConfirmPassword'
    }
}


