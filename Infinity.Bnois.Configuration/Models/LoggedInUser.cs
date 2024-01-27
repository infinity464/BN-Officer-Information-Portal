using System;

namespace Infinity.Bnois.Configuration.Models
{
    public class LoggedInUser : ILoggedInUser
    {
        private string userId;
        private string[] userRoles;
        private string[] roleIds;
        private int[] userFeatureCodes;
        public LoggedInUser(string userId, string[] roleIds, string[] userRoles, int[] userFeatureCodes)
        {
            this.userId = userId;
            this.userRoles = userRoles;
            this.roleIds = roleIds;
            this.userFeatureCodes = userFeatureCodes;
        }
       
        public string UserId
        {
            get
            {
                return userId;
            }
        }


        public string[] UserRoles
        {
            get
            {
                return userRoles;
            }
        }

        public string[] RoleIds
        {
            get
            {
                return roleIds;
            }
        }

        public int[] UserFeatureCodes
        {
            get
            {
                return userFeatureCodes;
            }
        }


    }
}
