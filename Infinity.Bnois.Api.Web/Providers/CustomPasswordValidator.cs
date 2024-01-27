using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Infinity.Bnois.Api.Web.Providers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomPasswordValidator'
    public class CustomPasswordValidator : IIdentityValidator<string>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomPasswordValidator'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomPasswordValidator.RequiredLength'
        public int RequiredLength { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomPasswordValidator.RequiredLength'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomPasswordValidator.CustomPasswordValidator(int)'
        public CustomPasswordValidator(int length)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomPasswordValidator.CustomPasswordValidator(int)'
        {
            RequiredLength = length;

        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomPasswordValidator.ValidateAsync(string)'
        public Task<IdentityResult> ValidateAsync(string item)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomPasswordValidator.ValidateAsync(string)'
        {
            //if (!string.IsNullOrEmpty(item) || item.Length < RequiredLength)
            //{
            //    return Task.FromResult(IdentityResult.Failed(String.Format("Password should be of length {0}", RequiredLength)));
            //}

            //string pattern = @"^(?=.*[0-9])(?=.*[!@#$%^&*])[0-9a-zA-Z!@#$%^&*0-9]{10,}$";

            //if (!Regex.IsMatch(item, pattern))
            //{
            //    return Task.FromResult(IdentityResult.Failed("Password should have one numeral and one special character"));
            //}

            return Task.FromResult(IdentityResult.Success);
        }
    }

}