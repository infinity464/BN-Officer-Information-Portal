using IdentityServer3.Core.Extensions;
using Infinity.Bnois.Api.Web.Data;
using Infinity.Bnois.Api.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Infinity.Bnois.Api.Web.Controllers
{
    [Route("core/2fa")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'TwoFactorController'
    public class TwoFactorController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'TwoFactorController'
    {
        private UserManager userMgr;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'TwoFactorController.TwoFactorController()'
        public TwoFactorController()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'TwoFactorController.TwoFactorController()'
        {
            userMgr = new UserManager(new UserStore(new InfinityIdentityDbContext("Administration")));
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'TwoFactorController.Index(SendCodeViewModel)'
        public async Task<ActionResult> Index(SendCodeViewModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'TwoFactorController.Index(SendCodeViewModel)'
        {
            var ctx = Request.GetOwinContext();
            var user = await ctx.Environment.GetIdentityServerPartialLoginAsync();
            if (user == null)
            {
                return View("Error");
            }

            //var token = await UserManager.GenerateTwoFactorTokenAsync(userId, model.SelectedProvider);
            //var identityResult = await UserManager.NotifyTwoFactorTokenAsync(userId, model.SelectedProvider, token);
            //if (!identityResult.Succeeded) return View("Error");

            //return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });

            return View("Index");
        }

        [HttpPost]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'TwoFactorController.Index(string)'
        public async Task<ActionResult> Index(string code)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'TwoFactorController.Index(string)'
        {
            var ctx = Request.GetOwinContext();

            var user = await ctx.Environment.GetIdentityServerPartialLoginAsync();
            if (user == null)
            {
                return View("Error");
            }

            var id = user.FindFirst("sub").Value;
            if (!(await this.userMgr.VerifyTwoFactorTokenAsync(id, "sms", code)))
            {
                ViewData["message"] = "Incorrect code";
                return View("Index");
            }

            var claims = user.Claims.Where(c => c.Type != "amr").ToList();
            claims.Add(new Claim("amr", "2fa"));
            await ctx.Environment.UpdatePartialLoginClaimsAsync(claims);

            var resumeUrl = await ctx.Environment.GetPartialLoginResumeUrlAsync();
            return Redirect(resumeUrl);
        }
    }
}