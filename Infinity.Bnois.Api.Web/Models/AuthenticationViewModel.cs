using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.Api.Web.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ForgotPasswordViewModel'
    public class ForgotPasswordViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ForgotPasswordViewModel'
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ForgotPasswordViewModel.Email'
        public string Email { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ForgotPasswordViewModel.Email'
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordViewModel'
    public class ResetPasswordViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordViewModel'
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordViewModel.Email'
        public string Email { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordViewModel.Email'

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordViewModel.Password'
        public string Password { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordViewModel.Password'

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordViewModel.ConfirmPassword'
        public string ConfirmPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordViewModel.ConfirmPassword'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordViewModel.Code'
        public string Code { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordViewModel.Code'
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordSmsViewModel'
    public class ResetPasswordSmsViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordSmsViewModel'
    {
        [Required]
        [Display(Name = "Phone Number")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordSmsViewModel.PhoneNumber'
        public string PhoneNumber { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordSmsViewModel.PhoneNumber'

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordSmsViewModel.Password'
        public string Password { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordSmsViewModel.Password'

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordSmsViewModel.ConfirmPassword'
        public string ConfirmPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordSmsViewModel.ConfirmPassword'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordSmsViewModel.Code'
        public string Code { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ResetPasswordSmsViewModel.Code'
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'VerifyPhoneNumberViewModel'
    public class VerifyPhoneNumberViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'VerifyPhoneNumberViewModel'
    {
        [Required]
        [Display(Name = "Code")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'VerifyPhoneNumberViewModel.Code'
        public string Code { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'VerifyPhoneNumberViewModel.Code'

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'VerifyPhoneNumberViewModel.PhoneNumber'
        public string PhoneNumber { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'VerifyPhoneNumberViewModel.PhoneNumber'
    }
}