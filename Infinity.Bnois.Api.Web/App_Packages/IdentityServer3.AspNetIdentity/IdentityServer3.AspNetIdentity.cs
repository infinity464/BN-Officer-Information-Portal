/*
 * Copyright 2015 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core;
using IdentityModel;
using IdentityServer3.Core.Services.Default;

namespace IdentityServer3.AspNetIdentity
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>'
    public class AspNetIdentityUserService<TUser, TKey> : UserServiceBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>'
        where TUser : class, Microsoft.AspNet.Identity.IUser<TKey>, new()
        where TKey : IEquatable<TKey>
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.DisplayNameClaimType'
        public string DisplayNameClaimType { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.DisplayNameClaimType'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.EnableSecurityStamp'
        public bool EnableSecurityStamp { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.EnableSecurityStamp'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.userManager'
        protected readonly Microsoft.AspNet.Identity.UserManager<TUser, TKey> userManager;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.userManager'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.ConvertSubjectToKey'
        protected readonly Func<string, TKey> ConvertSubjectToKey;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.ConvertSubjectToKey'
        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.AspNetIdentityUserService(UserManager<TUser, TKey>, Func<string, TKey>)'
        public AspNetIdentityUserService(Microsoft.AspNet.Identity.UserManager<TUser, TKey> userManager, Func<string, TKey> parseSubject = null)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.AspNetIdentityUserService(UserManager<TUser, TKey>, Func<string, TKey>)'
        {
            if (userManager == null) throw new ArgumentNullException("userManager");
            
            this.userManager = userManager;

            if (parseSubject != null)
            {
                ConvertSubjectToKey = parseSubject;
            }
            else
            {
                var keyType = typeof (TKey);
                if (keyType == typeof (string)) ConvertSubjectToKey = subject => (TKey) ParseString(subject);
                else if (keyType == typeof (int)) ConvertSubjectToKey = subject => (TKey) ParseInt(subject);
                else if (keyType == typeof (uint)) ConvertSubjectToKey = subject => (TKey) ParseUInt32(subject);
                else if (keyType == typeof (long)) ConvertSubjectToKey = subject => (TKey) ParseLong(subject);
                else if (keyType == typeof (Guid)) ConvertSubjectToKey = subject => (TKey) ParseGuid(subject);
                else
                {
                    throw new InvalidOperationException("Key type not supported");
                }
            }

            EnableSecurityStamp = true;
        }

        private object ParseString(string sub)
        {
            return sub;
        }

        private object ParseInt(string sub)
        {
            int key;
            if (!Int32.TryParse(sub, out key)) return 0;
            return key;
        }

        private object ParseUInt32(string sub)
        {
            uint key;
            if (!UInt32.TryParse(sub, out key)) return 0;
            return key;
        }

        private object ParseLong(string sub)
        {
            long key;
            if (!Int64.TryParse(sub, out key)) return 0;
            return key;
        }

        private object ParseGuid(string sub)
        {
            Guid key;
            if (!Guid.TryParse(sub, out key)) return Guid.Empty;
            return key;
        }
        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.GetProfileDataAsync(ProfileDataRequestContext)'
        public override async Task GetProfileDataAsync(ProfileDataRequestContext ctx)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.GetProfileDataAsync(ProfileDataRequestContext)'
        {
            var subject = ctx.Subject;
            var requestedClaimTypes = ctx.RequestedClaimTypes;

            if (subject == null) throw new ArgumentNullException("subject");

            TKey key = ConvertSubjectToKey(subject.GetSubjectId());
            var acct = await userManager.FindByIdAsync(key);
            if (acct == null)
            {
                throw new ArgumentException("Invalid subject identifier");
            }

            var claims = await GetClaimsFromAccount(acct);
            if (requestedClaimTypes != null && requestedClaimTypes.Any())
            {
                claims = claims.Where(x => requestedClaimTypes.Contains(x.Type));
            }
            
            ctx.IssuedClaims = claims;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.GetClaimsFromAccount(TUser)'
        protected virtual async Task<IEnumerable<Claim>> GetClaimsFromAccount(TUser user)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.GetClaimsFromAccount(TUser)'
        {
            var claims = new List<Claim>{
                new Claim(Constants.ClaimTypes.Subject, user.Id.ToString()),
                new Claim(Constants.ClaimTypes.PreferredUserName, user.UserName),
            };

            if (userManager.SupportsUserEmail)
            {
                var email = await userManager.GetEmailAsync(user.Id);
                if (!String.IsNullOrWhiteSpace(email))
                {
                    claims.Add(new Claim(Constants.ClaimTypes.Email, email));
                    var verified = await userManager.IsEmailConfirmedAsync(user.Id);
                    claims.Add(new Claim(Constants.ClaimTypes.EmailVerified, verified ? "true" : "false"));
                }
            }

            if (userManager.SupportsUserPhoneNumber)
            {
                var phone = await userManager.GetPhoneNumberAsync(user.Id);
                if (!String.IsNullOrWhiteSpace(phone))
                {
                    claims.Add(new Claim(Constants.ClaimTypes.PhoneNumber, phone));
                    var verified = await userManager.IsPhoneNumberConfirmedAsync(user.Id);
                    claims.Add(new Claim(Constants.ClaimTypes.PhoneNumberVerified, verified ? "true" : "false"));
                }
            }

            if (userManager.SupportsUserClaim)
            {
                claims.AddRange(await userManager.GetClaimsAsync(user.Id));
            }

            if (userManager.SupportsUserRole)
            {
                var roleClaims =
                    from role in await userManager.GetRolesAsync(user.Id)
                    select new Claim(Constants.ClaimTypes.Role, role);
                claims.AddRange(roleClaims);
            }

            return claims;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.GetDisplayNameForAccountAsync(TKey)'
        protected virtual async Task<string> GetDisplayNameForAccountAsync(TKey userID)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.GetDisplayNameForAccountAsync(TKey)'
        {
            var user = await userManager.FindByIdAsync(userID);
            var claims = await GetClaimsFromAccount(user);

            Claim nameClaim = null;
            if (DisplayNameClaimType != null)
            {
                nameClaim = claims.FirstOrDefault(x => x.Type == DisplayNameClaimType);
            }
            if (nameClaim == null) nameClaim = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Name);
            if (nameClaim == null) nameClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            if (nameClaim != null) return nameClaim.Value;
            
            return user.UserName;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.FindUserAsync(string)'
        protected async virtual Task<TUser> FindUserAsync(string username)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.FindUserAsync(string)'
        {
            return await userManager.FindByNameAsync(username);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.PostAuthenticateLocalAsync(TUser, SignInMessage)'
        protected virtual Task<AuthenticateResult> PostAuthenticateLocalAsync(TUser user, SignInMessage message)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.PostAuthenticateLocalAsync(TUser, SignInMessage)'
        {
            return Task.FromResult<AuthenticateResult>(null);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.AuthenticateLocalAsync(LocalAuthenticationContext)'
        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext ctx)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.AuthenticateLocalAsync(LocalAuthenticationContext)'
        {
            var username = ctx.UserName;
            var password = ctx.Password;
            var message = ctx.SignInMessage;

            ctx.AuthenticateResult = null;

            if (userManager.SupportsUserPassword)
            {
                var user = await FindUserAsync(username);
                if (user != null)
                {
                    if (userManager.SupportsUserLockout &&
                        await userManager.IsLockedOutAsync(user.Id))
                    {
                        return;
                    }

                    if (await userManager.CheckPasswordAsync(user, password))
                    {
                        if (userManager.SupportsUserLockout)
                        {
                            await userManager.ResetAccessFailedCountAsync(user.Id);
                        }

                        var result = await PostAuthenticateLocalAsync(user, message);
                        if (result == null)
                        {
                            var claims = await GetClaimsForAuthenticateResult(user);
                            result = new AuthenticateResult(user.Id.ToString(), await GetDisplayNameForAccountAsync(user.Id), claims);
                        }
                        
                        ctx.AuthenticateResult = result;
                    }
                    else if (userManager.SupportsUserLockout)
                    {
                        await userManager.AccessFailedAsync(user.Id);
                    }
                }
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.GetClaimsForAuthenticateResult(TUser)'
        protected virtual async Task<IEnumerable<Claim>> GetClaimsForAuthenticateResult(TUser user)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.GetClaimsForAuthenticateResult(TUser)'
        {
            List<Claim> claims = new List<Claim>();
            if (EnableSecurityStamp && userManager.SupportsUserSecurityStamp)
            {
                var stamp = await userManager.GetSecurityStampAsync(user.Id);
                if (!String.IsNullOrWhiteSpace(stamp))
                {
                    claims.Add(new Claim("security_stamp", stamp));
                }
            }
            return claims;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.AuthenticateExternalAsync(ExternalAuthenticationContext)'
        public override async Task AuthenticateExternalAsync(ExternalAuthenticationContext ctx)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.AuthenticateExternalAsync(ExternalAuthenticationContext)'
        {
            var externalUser = ctx.ExternalIdentity;
            var message = ctx.SignInMessage;

            if (externalUser == null)
            {
                throw new ArgumentNullException("externalUser");
            }

            var user = await userManager.FindAsync(new Microsoft.AspNet.Identity.UserLoginInfo(externalUser.Provider, externalUser.ProviderId));
            if (user == null)
            {
                ctx.AuthenticateResult = await ProcessNewExternalAccountAsync(externalUser.Provider, externalUser.ProviderId, externalUser.Claims);
            }
            else
            {
                ctx.AuthenticateResult = await ProcessExistingExternalAccountAsync(user.Id, externalUser.Provider, externalUser.ProviderId, externalUser.Claims);
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.ProcessNewExternalAccountAsync(string, string, IEnumerable<Claim>)'
        protected virtual async Task<AuthenticateResult> ProcessNewExternalAccountAsync(string provider, string providerId, IEnumerable<Claim> claims)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.ProcessNewExternalAccountAsync(string, string, IEnumerable<Claim>)'
        {
            var user = await TryGetExistingUserFromExternalProviderClaimsAsync(provider, claims);
            if (user == null)
            {
                user = await InstantiateNewUserFromExternalProviderAsync(provider, providerId, claims);
                if (user == null)
                    throw new InvalidOperationException("CreateNewAccountFromExternalProvider returned null");

                var createResult = await userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    return new AuthenticateResult(createResult.Errors.First());
                }
            }

            var externalLogin = new Microsoft.AspNet.Identity.UserLoginInfo(provider, providerId);
            var addExternalResult = await userManager.AddLoginAsync(user.Id, externalLogin);
            if (!addExternalResult.Succeeded)
            {
                return new AuthenticateResult(addExternalResult.Errors.First());
            }

            var result = await AccountCreatedFromExternalProviderAsync(user.Id, provider, providerId, claims);
            if (result != null) return result;

            return await SignInFromExternalProviderAsync(user.Id, provider);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.InstantiateNewUserFromExternalProviderAsync(string, string, IEnumerable<Claim>)'
        protected virtual Task<TUser> InstantiateNewUserFromExternalProviderAsync(string provider, string providerId, IEnumerable<Claim> claims)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.InstantiateNewUserFromExternalProviderAsync(string, string, IEnumerable<Claim>)'
        {
            var user = new TUser() { UserName = Guid.NewGuid().ToString("N") };
            return Task.FromResult(user);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.TryGetExistingUserFromExternalProviderClaimsAsync(string, IEnumerable<Claim>)'
        protected virtual Task<TUser> TryGetExistingUserFromExternalProviderClaimsAsync(string provider, IEnumerable<Claim> claims)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.TryGetExistingUserFromExternalProviderClaimsAsync(string, IEnumerable<Claim>)'
        {
            return Task.FromResult<TUser>(null);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.AccountCreatedFromExternalProviderAsync(TKey, string, string, IEnumerable<Claim>)'
        protected virtual async Task<AuthenticateResult> AccountCreatedFromExternalProviderAsync(TKey userID, string provider, string providerId, IEnumerable<Claim> claims)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.AccountCreatedFromExternalProviderAsync(TKey, string, string, IEnumerable<Claim>)'
        {
            claims = await SetAccountEmailAsync(userID, claims);
            claims = await SetAccountPhoneAsync(userID, claims);

            return await UpdateAccountFromExternalClaimsAsync(userID, provider, providerId, claims);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.SignInFromExternalProviderAsync(TKey, string)'
        protected virtual async Task<AuthenticateResult> SignInFromExternalProviderAsync(TKey userID, string provider)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.SignInFromExternalProviderAsync(TKey, string)'
        {
            var user = await userManager.FindByIdAsync(userID);
            var claims = await GetClaimsForAuthenticateResult(user);

            return new AuthenticateResult(
                userID.ToString(), 
                await GetDisplayNameForAccountAsync(userID),
                claims,
                authenticationMethod: Constants.AuthenticationMethods.External, 
                identityProvider: provider);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.UpdateAccountFromExternalClaimsAsync(TKey, string, string, IEnumerable<Claim>)'
        protected virtual async Task<AuthenticateResult> UpdateAccountFromExternalClaimsAsync(TKey userID, string provider, string providerId, IEnumerable<Claim> claims)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.UpdateAccountFromExternalClaimsAsync(TKey, string, string, IEnumerable<Claim>)'
        {
            var existingClaims = await userManager.GetClaimsAsync(userID);
            var intersection = existingClaims.Intersect(claims, new ClaimComparer());
            var newClaims = claims.Except(intersection, new ClaimComparer());

            foreach (var claim in newClaims)
            {
                var result = await userManager.AddClaimAsync(userID, claim);
                if (!result.Succeeded)
                {
                    return new AuthenticateResult(result.Errors.First());
                }
            }

            return null;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.ProcessExistingExternalAccountAsync(TKey, string, string, IEnumerable<Claim>)'
        protected virtual async Task<AuthenticateResult> ProcessExistingExternalAccountAsync(TKey userID, string provider, string providerId, IEnumerable<Claim> claims)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.ProcessExistingExternalAccountAsync(TKey, string, string, IEnumerable<Claim>)'
        {
            return await SignInFromExternalProviderAsync(userID, provider);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.SetAccountEmailAsync(TKey, IEnumerable<Claim>)'
        protected virtual async Task<IEnumerable<Claim>> SetAccountEmailAsync(TKey userID, IEnumerable<Claim> claims)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.SetAccountEmailAsync(TKey, IEnumerable<Claim>)'
        {
            var email = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Email);
            if (email != null)
            {
                var userEmail = await userManager.GetEmailAsync(userID);
                if (userEmail == null)
                {
                    // if this fails, then presumably the email is already associated with another account
                    // so ignore the error and let the claim pass thru
                    var result = await userManager.SetEmailAsync(userID, email.Value);
                    if (result.Succeeded)
                    {
                        var email_verified = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.EmailVerified);
                        if (email_verified != null && email_verified.Value == "true")
                        {
                            var token = await userManager.GenerateEmailConfirmationTokenAsync(userID);
                            await userManager.ConfirmEmailAsync(userID, token);
                        }

                        var emailClaims = new string[] { Constants.ClaimTypes.Email, Constants.ClaimTypes.EmailVerified };
                        return claims.Where(x => !emailClaims.Contains(x.Type));
                    }
                }
            }

            return claims;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.SetAccountPhoneAsync(TKey, IEnumerable<Claim>)'
        protected virtual async Task<IEnumerable<Claim>> SetAccountPhoneAsync(TKey userID, IEnumerable<Claim> claims)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.SetAccountPhoneAsync(TKey, IEnumerable<Claim>)'
        {
            var phone = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.PhoneNumber);
            if (phone != null)
            {
                var userPhone = await userManager.GetPhoneNumberAsync(userID);
                if (userPhone == null)
                {
                    // if this fails, then presumably the phone is already associated with another account
                    // so ignore the error and let the claim pass thru
                    var result = await userManager.SetPhoneNumberAsync(userID, phone.Value);
                    if (result.Succeeded)
                    {
                        var phone_verified = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.PhoneNumberVerified);
                        if (phone_verified != null && phone_verified.Value == "true")
                        {
                            var token = await userManager.GenerateChangePhoneNumberTokenAsync(userID, phone.Value);
                            await userManager.ChangePhoneNumberAsync(userID, phone.Value, token);
                        }

                        var phoneClaims = new string[] { Constants.ClaimTypes.PhoneNumber, Constants.ClaimTypes.PhoneNumberVerified };
                        return claims.Where(x => !phoneClaims.Contains(x.Type));
                    }
                }
            }
            
            return claims;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.IsActiveAsync(IsActiveContext)'
        public override async Task IsActiveAsync(IsActiveContext ctx)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AspNetIdentityUserService<TUser, TKey>.IsActiveAsync(IsActiveContext)'
        {
            var subject = ctx.Subject;

            if (subject == null) throw new ArgumentNullException("subject");

            var id = subject.GetSubjectId();
            TKey key = ConvertSubjectToKey(id);
            var acct = await userManager.FindByIdAsync(key);

            ctx.IsActive = false;

            if (acct != null)
            {
                if (EnableSecurityStamp && userManager.SupportsUserSecurityStamp)
                {
                    var security_stamp = subject.Claims.Where(x => x.Type == "security_stamp").Select(x => x.Value).SingleOrDefault();
                    if (security_stamp != null)
                    {
                        var db_security_stamp = await userManager.GetSecurityStampAsync(key);
                        if (db_security_stamp != security_stamp)
                        {
                            return;
                        }
                    }
                }

                ctx.IsActive = true;
            }
        }
    }
}
