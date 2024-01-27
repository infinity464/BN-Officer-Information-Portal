using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer3.EntityFramework;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Infinity.Bnois.Api.Web.Data;
using Infinity.Bnois.Api.Web.Models;

namespace Infinity.Bnois.Api.Web.IdentityServer
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'InitialValues'
    public class InitialValues
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'InitialValues'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'InitialValues.ConfigureInitValues(EntityFrameworkServiceOptions)'
        public static void ConfigureInitValues(EntityFrameworkServiceOptions options)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'InitialValues.ConfigureInitValues(EntityFrameworkServiceOptions)'
        {
            ConfigureLanguage(options);
            ConfigureRoles(options);
            ConfigureUsers(options);
        }

        private static void ConfigureLanguage(EntityFrameworkServiceOptions options)
        {
            using (InfinityIdentityDbContext db = new InfinityIdentityDbContext())
            {
                if (!db.Languages.Any())
                {
                    List<Language> languages = new List<Language>
                    {
                        new Language {CultureCode = "en-US", DisplayName = "English" },
                        new Language {CultureCode="sv-SE", DisplayName = "Swedish" }
                    };

                    foreach (Language language in languages)
                    {
                        db.Languages.Add(language);
                    }
                    db.SaveChanges();
                }
            }
        }

        private static void ConfigureRoles(EntityFrameworkServiceOptions options)
        {
            using (InfinityIdentityDbContext context = new InfinityIdentityDbContext("Administration"))
            {
                IdentityUserRole identityUserRole = new IdentityUserRole();

                if (!context.Roles.Any())
                {
                    context.Roles.Add(new Role { Id = Guid.NewGuid().ToString(), Name =Roles.SuperAdmin });
                    context.Roles.Add(new Role { Id = Guid.NewGuid().ToString(), Name = Roles.Admin });
                    context.Roles.Add(new Role { Id = Guid.NewGuid().ToString(), Name = Roles.User });
                    context.SaveChanges();
                }
            }
        }

        private static async void ConfigureUsers(EntityFrameworkServiceOptions options)
        {
            using (InfinityIdentityDbContext context = new InfinityIdentityDbContext("Administration"))
            {
                if (!context.Users.Any())
                {
                    Role role = context.Roles.FirstOrDefault();
                    string id = Guid.NewGuid().ToString();
                    User sa = new User
                    {
                        Id = id,
                        UserName = "sa",
                        FirstName = "Itolk",
                        LastName = "SuperAdmin",
                        CultureCode = "en-US",
                        Email = "superadmin@itolk.net",
                        IsActive = true,
                        CreatedBy= id,
                       CreatedDate=DateTime.Now
                    };
                    
                    using (var store = new UserStore(context))
                    {
                        using (var userManager = new UserManager(store))
                        {
                            IdentityResult result = await userManager.CreateAsync(sa, "Admin123");

                            if (result.Succeeded)
                            {
                                var currentUser = userManager.FindByName(sa.UserName);

                                var roleresult = userManager.AddToRole(sa.Id, "SuperAdmin");
                            }
                        }
                    }
                }
            }
        }
    }
}