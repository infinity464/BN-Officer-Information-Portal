using IdentityServer3.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Validation;
using IdentityServer3.Core.ViewModels;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Infinity.Bnois.Api.Web.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService'
    public class CustomViewService : IViewService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService'
    {
        private IClientStore clientStore;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.CustomViewService(IClientStore)'
        public CustomViewService(IClientStore clientStore)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.CustomViewService(IClientStore)'
        {
            this.clientStore = clientStore;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.Login(LoginViewModel, SignInMessage)'
        public virtual async Task<System.IO.Stream> Login(LoginViewModel model, SignInMessage message)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.Login(LoginViewModel, SignInMessage)'
        {
            var client = await clientStore.FindClientByIdAsync(message.ClientId);
            var name = client != null ? client.ClientName : null;
            model.AllowRememberMe = true;
            return await Render(model, "login", name);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.Logout(LogoutViewModel, SignOutMessage)'
        public Task<Stream> Logout(LogoutViewModel model, SignOutMessage message)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.Logout(LogoutViewModel, SignOutMessage)'
        {
            return Render(model, "logout");
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.LoggedOut(LoggedOutViewModel, SignOutMessage)'
        public Task<Stream> LoggedOut(LoggedOutViewModel model, SignOutMessage message)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.LoggedOut(LoggedOutViewModel, SignOutMessage)'
        {
            return Render(model, "loggedOut");
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.Consent(ConsentViewModel, ValidatedAuthorizeRequest)'
        public Task<Stream> Consent(ConsentViewModel model, ValidatedAuthorizeRequest authorizeRequest)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.Consent(ConsentViewModel, ValidatedAuthorizeRequest)'
        {
            return Render(model, "consent");
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.ClientPermissions(ClientPermissionsViewModel)'
        public Task<Stream> ClientPermissions(ClientPermissionsViewModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.ClientPermissions(ClientPermissionsViewModel)'
        {
            return Render(model, "permissions");
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.Error(ErrorViewModel)'
        public virtual Task<System.IO.Stream> Error(ErrorViewModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.Error(ErrorViewModel)'
        {
            return Render(model, "error");
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.Render(CommonViewModel, string, string)'
        protected virtual Task<System.IO.Stream> Render(CommonViewModel model, string page, string clientName = null)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CustomViewService.Render(CommonViewModel, string, string)'
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });

            string html = LoadHtml(page);
            html = Replace(html, new
            {
                siteName = Microsoft.Security.Application.Encoder.HtmlEncode(model.SiteName),
                model = Microsoft.Security.Application.Encoder.HtmlEncode(json),
                clientName = clientName
            });

            return Task.FromResult(StringToStream(html));
        }

        private string LoadHtml(string name)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"scripts\app");
            file = Path.Combine(file, name + ".html");
            return File.ReadAllText(file);
        }

        private string Replace(string value, IDictionary<string, object> values)
        {
            foreach (var key in values.Keys)
            {
                var val = values[key];
                val = val ?? String.Empty;
                if (val != null)
                {
                    value = value.Replace("{" + key + "}", val.ToString());
                }
            }
            return value;
        }

        private string Replace(string value, object values)
        {
            return Replace(value, Map(values));
        }

        private IDictionary<string, object> Map(object values)
        {
            var dictionary = values as IDictionary<string, object>;

            if (dictionary == null)
            {
                dictionary = new Dictionary<string, object>();
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
                {
                    dictionary.Add(descriptor.Name, descriptor.GetValue(values));
                }
            }

            return dictionary;
        }

        private Stream StringToStream(string s)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(s);
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}