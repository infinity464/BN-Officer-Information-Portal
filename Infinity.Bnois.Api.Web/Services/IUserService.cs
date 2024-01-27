
using Infinity.Bnois.Api.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Web.Services
{ 
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IUserService'
    public interface IUserService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IUserService'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IUserService.GetUser(string)'
        UserModel GetUser(string userId);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IUserService.GetUser(string)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IUserService.GetUsers(int, int, out int)'
        List<UserModel> GetUsers(int pageSize, int pageNumber, string q, out int total);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IUserService.GetUsers(int, int, out int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IUserService.SaveUser(string, UserModel)'
        bool SaveUser(string userId, UserModel userModel);

        bool SaveBulkUser(UserModel userModel);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IUserService.SaveUser(string, UserModel)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IUserService.DeleteUser(string)'
        bool DeleteUser(string userId);
        bool ActivateUser(string userId);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IUserService.DeleteUser(string)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IUserService.ChangePassword(string, string, string)'
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        Task<bool> ResetUserPasswordByUserId(string userId);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IUserService.ChangePassword(string, string, string)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IUserService.GetUserRoles(string)'
        List<UserRoleModel> GetUserRoles(string userId);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IUserService.GetUserRoles(string)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IUserService.SaveUserRoles(string, UserRoleModel[])'
        bool SaveUserRoles(string userId, UserRoleModel[] roles);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IUserService.SaveUserRoles(string, UserRoleModel[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IUserService.SaveApplicantUser(UserModel)'
        bool SaveApplicantUser(UserModel user);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IUserService.SaveApplicantUser(UserModel)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IUserService.ResetPassword(string, string)'
        Task<bool> ResetPassword(string userName, string newPassword);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IUserService.ResetPassword(string, string)'
    }
}