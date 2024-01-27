using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Infinity.Bnois.Api.Web
{
    // Models used as parameters to AccountController actions.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AddExternalLoginBindingModel'
    public class AddExternalLoginBindingModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AddExternalLoginBindingModel'
    {
        [Required]
        [Display(Name = "External access token")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AddExternalLoginBindingModel.ExternalAccessToken'
        public string ExternalAccessToken { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AddExternalLoginBindingModel.ExternalAccessToken'
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ChangePasswordBindingModel'
    public class ChangePasswordBindingModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ChangePasswordBindingModel'
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ChangePasswordBindingModel.OldPassword'
        public string OldPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ChangePasswordBindingModel.OldPassword'

        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ChangePasswordBindingModel.NewPassword'
        public string NewPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ChangePasswordBindingModel.NewPassword'

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ChangePasswordBindingModel.ConfirmPassword'
        public string ConfirmPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ChangePasswordBindingModel.ConfirmPassword'
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterBindingModel'
    public class RegisterBindingModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterBindingModel'
    {
        [Required]
        [Display(Name = "Email")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterBindingModel.Email'
        public string Email { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterBindingModel.Email'

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterBindingModel.Password'
        public string Password { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterBindingModel.Password'

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterBindingModel.ConfirmPassword'
        public string ConfirmPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterBindingModel.ConfirmPassword'
        [Required]
        [Display(Name = "Name")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterBindingModel.Name'
        public string Name { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterBindingModel.Name'
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterExternalBindingModel'
    public class RegisterExternalBindingModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterExternalBindingModel'
    {
        [Required]
        [Display(Name = "Email")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RegisterExternalBindingModel.Email'
        public string Email { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RegisterExternalBindingModel.Email'
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RemoveLoginBindingModel'
    public class RemoveLoginBindingModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RemoveLoginBindingModel'
    {
        [Required]
        [Display(Name = "Login provider")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RemoveLoginBindingModel.LoginProvider'
        public string LoginProvider { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RemoveLoginBindingModel.LoginProvider'

        [Required]
        [Display(Name = "Provider key")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RemoveLoginBindingModel.ProviderKey'
        public string ProviderKey { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RemoveLoginBindingModel.ProviderKey'
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SetPasswordBindingModel'
    public class SetPasswordBindingModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SetPasswordBindingModel'
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SetPasswordBindingModel.NewPassword'
        public string NewPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SetPasswordBindingModel.NewPassword'

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SetPasswordBindingModel.ConfirmPassword'
        public string ConfirmPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SetPasswordBindingModel.ConfirmPassword'
    }
}
