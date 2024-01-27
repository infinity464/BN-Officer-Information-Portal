using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.Api.Web.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserModel'
    public class UserModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserModel'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserModel.Id'
        public string Id { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserModel.Id'

        [Required(ErrorMessage = "User Name is required")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserModel.UserName'
        public string UserName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserModel.UserName'

        [Required(ErrorMessage = "Password is required")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserModel.Password'
        public string Password { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserModel.Password'

        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserModel.ConfirmPassword'
        public string ConfirmPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserModel.ConfirmPassword'

        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserModel.FirstName'
        public string FirstName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserModel.FirstName'
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required(ErrorMessage = "Last Name is required")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserModel.LastName'
        public string LastName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserModel.LastName'


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserModel.CultureCode'
        public string CultureCode { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserModel.CultureCode'
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserModel.Email'
        public string Email { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserModel.Email'
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"(^([+]{1}[8]{2}|0088)?(01){1}[5-9]{1}\d{8})$", ErrorMessage = "Please enter valid phone number")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserModel.PhoneNumber'
        public string PhoneNumber { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserModel.PhoneNumber'
        [Required]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserModel.IsActive'
        public bool IsActive { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserModel.IsActive'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserModel.CompanyId'
        public string CompanyId { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserModel.CompanyId'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'User.PNo'
        public string PNo { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'User.PNo'


    }
}