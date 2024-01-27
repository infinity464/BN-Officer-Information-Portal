using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.Api.Web.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserViewModel'
    public class UserViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserViewModel'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserViewModel.User'
        public UserModel User { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserViewModel.User'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserViewModel.Users'
        public List<UserModel> Users { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserViewModel.Users'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserViewModel.UserRoles'
        public List<UserRoleModel> UserRoles { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserViewModel.UserRoles'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserViewModel.Languages'
        public List<SelectModel> Languages { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserViewModel.Languages'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserViewModel.LoginUserId'
        public string LoginUserId { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserViewModel.LoginUserId'
    }
}