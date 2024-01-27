using Infinity.Bnois.Configuration;
using Infinity.Bnois.ExceptionHelper;
using Infinity.Bnois.Api.Web.Data;
using Infinity.Bnois.Api.Web.Models;
using Microsoft.AspNet.Identity;

using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Infinity.Ers.IdentityServer.Services;
using System.Text;

namespace Infinity.Bnois.Api.Web.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserService'
    public class UserService : UserManager<User, string>, IUserService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService'
    {
        private readonly UserStore userStore;
        private readonly IRoleService roleService;
        private readonly SmsService smsService;
        private readonly Random _random = new Random();
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserService.UserService(UserStore, IRoleService)'
        public UserService(UserStore userStore, IRoleService roleService, SmsService smsService) : base(userStore)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.UserService(UserStore, IRoleService)'
        {
            this.userStore = userStore;
            this.roleService = roleService;
            this.smsService = smsService;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserService.GetUser(string)'
        public UserModel GetUser(string userId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.GetUser(string)'
        {
            if (!ConfigurationResolver.Get().LoggedInUser.UserRoles.Any(x => x == Roles.SuperAdmin))
            {

            }
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new UserModel();
            }
            User user = userStore.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new InfinityNotFoundException("User not found.");
            }
            return new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CultureCode = user.CultureCode,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                CompanyId = user.CompanyId,
                PNo=user.PNo
            };
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserService.GetUsers(int, int, out int)'
        public List<UserModel> GetUsers(int pageSize, int pageNumber, string q, out int total)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.GetUsers(int, int, out int)'
        {
            IQueryable<User> userQuery = userStore.Users.Where(x => x.PNo.Length > 0 && (x.PNo.Contains(q) || x.PhoneNumber.Contains(q) || String.IsNullOrEmpty(q))).AsQueryable();
            total = userQuery.Count();
            List<User> users = userQuery.OrderBy(x => x.UserName).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            List<UserModel> userModels = users.Select(x => new UserModel
            {
                Id = x.Id,
                UserName = x.UserName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                CultureCode = x.CultureCode,
                PhoneNumber = x.PhoneNumber,
                IsActive = x.IsActive,
                CompanyId = x.CompanyId,
                PNo = x.PNo,
            }).ToList();
            return userModels.OrderBy(x => x.UserName).ToList();
        }



#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserService.ChangePassword(string, string, string)'
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.ChangePassword(string, string, string)'
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new InfinityArgumentMissingException("User Name missing");
            }

            if (string.IsNullOrWhiteSpace(oldPassword))
            {
                throw new InfinityArgumentMissingException("Current Password missing");
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new InfinityArgumentMissingException("New Password missing");
            }

            var user = base.FindAsync(userName, oldPassword).Result;

            if (user == null)
            {
                throw new InfinityNotFoundException("Current Password is not correct.");
            }

            var result = base.ChangePasswordAsync(user.Id, oldPassword, newPassword).Result;

            if (!result.Succeeded)
            {
                throw new InfinityNotFoundException("Password is Not Updated");
            }
            return true;
        }
        public async Task<bool> ResetUserPasswordByUserId(string userId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.ChangePassword(string, string, string)'
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InfinityArgumentMissingException("User Name missing");
            }
            User user = userStore.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new InfinityNotFoundException("User does not found");
            }
            //string pass =  System.Web.Security.Membership.GeneratePassword(10, 2);
            Random rnd = new Random();
            string pass = Convert.ToString(rnd.Next(99999999, 999999999));
            user.PasswordHash = base.PasswordHasher.HashPassword(pass);
            bool success = base.UpdateAsync(user).Result.Succeeded;
            if(success == false)
            {
                throw new InfinityInternalServerException("Password is Not changed");
            }
            if(user.PhoneNumber != null)
            {
                string msg = @"AA Sir. Your BNOIP password has been reset. Website link: https://bnoip.navy.mil.bd User ID: P No, New Password: " + pass + ". Contact: 01769-702029 - Naval Sectt";

                IdentityMessage identityMessage = new IdentityMessage
                {
                    Destination = user.PhoneNumber,
                    Body = msg,
                };
                await smsService.SendAsync(identityMessage);
            }
            
            

            return true;
        }



#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserService.SaveUser(string, UserModel)'
        public bool SaveUser(string userId, UserModel userModel)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.SaveUser(string, UserModel)'
        {
            if (userModel == null)
            {
                throw new InfinityArgumentMissingException("User data missing");
            }

            User user = new User
            {
                UserName = userModel.UserName,

            };

            if (!string.IsNullOrWhiteSpace(userId))
            {
                user = userStore.Users.FirstOrDefault(x => x.Id == userId);
            }
            else
            {
                user.IsActive = true;
                user.CreatedBy =ConfigurationResolver.Get().LoggedInUser.UserId;
                user.CreatedDate = DateTime.Now;
            }
            if (string.IsNullOrWhiteSpace(userId))
            {
                var userNameExist = base.FindByNameAsync(userModel.UserName).Result;
                if (userNameExist != null)
                {
                    throw new InfinityNotFoundException("User Name Already Exist. Please Change User Name");
                }
            }
            user.Email = userModel.Email;
            user.CultureCode = userModel.CultureCode;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.PhoneNumber = userModel.PhoneNumber;
            user.PNo = userModel.PNo;

            if (!string.IsNullOrWhiteSpace(userId))
            {
                return base.UpdateAsync(user).Result.Succeeded;
            }
            userModel.Id = user.Id;
            return base.CreateAsync(user, userModel.Password).Result.Succeeded;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool SaveBulkUser(UserModel userModel)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            if (userModel.PNo == null)
            {
                throw new InfinityArgumentMissingException("User data missing");
            }
            User user = new User();
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.IsActive = true;
            user.CultureCode = "en-US";
            user.CompanyId = null;
            user.Email = userModel.Email == "" || userModel.Email == "-" || userModel.Email == null ? "abc@navy.mil.bd" : userModel.Email;
            user.EmailConfirmed = false;

            Random rnd = new Random();
            int pass = rnd.Next(99999999, 999999999);

            //string pass = System.Web.Security.Membership.GeneratePassword(10, 2);
            user.PasswordHash = base.PasswordHasher.HashPassword(pass.ToString());
            user.PhoneNumber = userModel.PhoneNumber;
            user.PhoneNumberConfirmed = true;
            user.TwoFactorEnabled = true;
            user.LockoutEnabled = false;
            user.LockoutEndDateUtc = null;
            user.AccessFailedCount = 0;
            user.UserName = userModel.UserName.ToLower();
            user.CreatedBy = ConfigurationResolver.Get().LoggedInUser.UserId;
            user.CreatedDate = DateTime.Now;
            user.PNo = userModel.PNo;
            user.InActiveBy = null;
            user.InActiveDate = null;


            bool result = base.CreateAsync(user, pass.ToString()).Result.Succeeded;
            //8ba5c634 - 71bd - 4d9c - 9bcd - 388d2c2dbc4a    12  MemberUser
            bool roleAssignResult = base.AddToRolesAsync(user.Id, "MemberUser").Result.Succeeded;
            //string msg = @"AA Sir. BNOIP website link: bnoip.navy.mil.bd User ID: P No, Password: " + pass + ". Pl check registered mob no for OTP. Contact: 01769-702029 - Naval Sectt";
            string msg = @"AA Sir. BNOIP website link: https://bnoip.navy.mil.bd User ID: P No, Password: " + pass + ". Pl check registered mob no for OTP. Contact: 01769-702029 - Naval Sectt";
            if (user.PhoneNumber != null)
            {
                IdentityMessage identityMessage = new IdentityMessage
                {
                    Destination = user.PhoneNumber,
                    Body = msg,
                };
                smsService.SendAsync(identityMessage);
            }
            return result;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserService.DeleteUser(string)'
        public bool DeleteUser(string userId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.DeleteUser(string)'
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InfinityArgumentMissingException("Invalide Requiest !!");
            }
            User user = userStore.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                throw new InfinityNotFoundException("User not found.");
            }
            user.IsActive = false;
            user.InActiveBy = ConfigurationResolver.Get().LoggedInUser.UserId;
            user.InActiveDate = DateTime.Now;
            return base.UpdateAsync(user).Result.Succeeded;
        }

        public bool ActivateUser(string userId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.DeleteUser(string)'
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InfinityArgumentMissingException("Invalide Requiest !!");
            }
            User user = userStore.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                throw new InfinityNotFoundException("User not found.");
            }
            user.IsActive = true;
            user.InActiveBy = ConfigurationResolver.Get().LoggedInUser.UserId;
            user.InActiveDate = DateTime.Now;
            return base.UpdateAsync(user).Result.Succeeded;
        }
        

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserService.DeleteUserRoles(string)'
        public bool DeleteUserRoles(string userId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.DeleteUserRoles(string)'
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InfinityArgumentMissingException("Invalid user !");
            }

            string[] userRoles = base.GetRolesAsync(userId).Result.ToArray();
            if (userRoles.Any())
            {
                return base.RemoveFromRolesAsync(userId, userRoles).Result.Succeeded;
            }

            return true;
        }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserService.SaveUserRoles(string, UserRoleModel[])'
        public bool SaveUserRoles(string userId, UserRoleModel[] roles)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.SaveUserRoles(string, UserRoleModel[])'
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InfinityArgumentMissingException("Invalid user !");
            }

            string[] roleNames = roles.Where(x => x.IsAssigned).Select(y => y.RoleName).ToArray();
            if (DeleteUserRoles(userId))
            {
                return base.AddToRolesAsync(userId, roleNames).Result.Succeeded;
            }
            return false;
        }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserService.GetUserRoles(string)'
        public List<UserRoleModel> GetUserRoles(string userId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.GetUserRoles(string)'
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new InfinityArgumentMissingException("Invalid user !");
            }
            List<Role> roles = roleService.GetRoles().ToList();
            List<string> userRoles = base.GetRolesAsync(userId).Result.ToList();
            List<UserRoleModel> usrRoles= roles.Select(x => new UserRoleModel { RoleId = x.Id, RoleName = x.Name, IsAssigned = userRoles.Any(y => x.Name == y) }).ToList();
            return usrRoles;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserService.SaveApplicantUser(UserModel)'
        public bool SaveApplicantUser(UserModel userModel)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.SaveApplicantUser(UserModel)'
        {
            if (userModel == null)
            {
                throw new InfinityArgumentMissingException("User data missing");
            }
            User user = new User();
            user.UserName = userModel.UserName;
            var userNameExist = base.FindByNameAsync(userModel.UserName).Result;
            if (userNameExist != null)
            {
                throw new InfinityNotFoundException("User Name Already Exist. Please Change User Name");
            }
            user.Email = userModel.Email;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.CultureCode = "en-US";
            user.IsActive = true;
            user.TwoFactorEnabled = false;
            bool status= base.CreateAsync(user, userModel.Password).Result.Succeeded;
            userModel.Id = user.Id;
            status=  base.AddToRolesAsync(user.Id,  new string[]{ Roles.User }.ToArray()).Result.Succeeded;
            return status;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserService.ResetPassword(string, string)'
        public async Task<bool> ResetPassword(string user, string newPassword)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserService.ResetPassword(string, string)'
        {
            var code = await base.GeneratePasswordResetTokenAsync(user);
            IdentityResult result= await base.ResetPasswordAsync(user, code, newPassword);
            if (!result.Succeeded)
            {
                throw new InfinityNotFoundException("Password is Not Updated");
            }
            return result.Succeeded;
        }
    }
}