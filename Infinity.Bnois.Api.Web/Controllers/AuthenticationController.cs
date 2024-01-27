
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Infinity.Bnois.Api.Web.Data;
using Infinity.Bnois.Api.Web.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ExceptionHelper;
using Microsoft.AspNet.Identity;

namespace Infinity.Bnois.Api.Web.Controllers
{
    [RoutePrefix("core/authentication")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController'
    public class AuthenticationController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController'
    {
        private readonly UserManager userManager;
        private readonly UserStore userStore;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.AuthenticationController()'
        public AuthenticationController()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.AuthenticationController()'
        {
            userStore = new UserStore(new InfinityIdentityDbContext("Administration"));
            userManager = new UserManager(userStore);
        }
        [Route("forget-password")]
        [AllowAnonymous]
        [HttpGet]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ForgetPassword()'
        public ActionResult ForgetPassword()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ForgetPassword()'
        {
            return View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("forget-password")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ForgotPassword(ForgotPasswordViewModel)'
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ForgotPassword(ForgotPasswordViewModel)'
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("reset-password", "core/authentication", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await userManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("forgot-password-confirmation", "core/authentication");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [Route("forgot-password-confirmation")]
        [AllowAnonymous]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ForgotPasswordConfirmation()'
        public ActionResult ForgotPasswordConfirmation()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ForgotPasswordConfirmation()'
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        [Route("reset-password")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPassword(string)'
        public ActionResult ResetPassword(string code)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPassword(string)'
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("reset-password")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPassword(ResetPasswordViewModel)'
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPassword(ResetPasswordViewModel)'
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("reset-password-unable", "core/authentication");
            }
            var result = await userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("reset-password-confirmation", "core/authentication");
            }

            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        [Route("reset-password-confirmation")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPasswordConfirmation()'
        public ActionResult ResetPasswordConfirmation()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPasswordConfirmation()'
        {
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        [Route("reset-password-unable")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPasswordUnable()'
        public ActionResult ResetPasswordUnable()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPasswordUnable()'
        {
            return View();
        }

        [Route("reset-password-sms")]
        [AllowAnonymous]
        [HttpGet]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPasswordSms()'
        public ActionResult ResetPasswordSms()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPasswordSms()'
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("reset-password-sms")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPasswordSms(string)'
        public async Task<ActionResult> ResetPasswordSms(string phoneNumber)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPasswordSms(string)'
        {
            var user = userManager.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            if (user != null)
            {
                var token = await userManager.SmsResetTokenProvider.GenerateAsync("Reset Password", this.userManager, user);
                var message = "Your One Time Password (OTP) for BNOIP: " + token;
                await userManager.SendSmsAsync(user.Id, message);

                return RedirectToAction("reset-password-confirmation-sms", "core/authentication");
            }
            else
            {
                ViewBag.message = "User not found.";
            }
            return View();
        }

        [Route("reset-password-confirmation-sms")]
        [AllowAnonymous]
        [HttpGet]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPasswordConfirmationSms()'
        public ActionResult ResetPasswordConfirmationSms()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPasswordConfirmationSms()'
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("reset-password-confirmation-sms")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPasswordConfirmationSms(ResetPasswordSmsViewModel)'
        public async Task<ActionResult> ResetPasswordConfirmationSms(ResetPasswordSmsViewModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ResetPasswordConfirmationSms(ResetPasswordSmsViewModel)'
        {
            var user = userManager.Users.FirstOrDefault(x => x.PNo == model.PhoneNumber && x.PhoneNumberConfirmed);
            if(user!= null)
            {
                var valid = await userManager.SmsResetTokenProvider.ValidateAsync("Reset Password", model.Code, this.userManager, user);
                if (valid)
                {
                    IUserPasswordStore<User, string> passwordStore = userStore as IUserPasswordStore<User, string>;

                    var result = await userManager.UpdateTokenPassword(passwordStore, user, model.Password);
                    if (result.Succeeded)
                    {
                        var res = await userManager.UpdateAsync(user);

                        if (res.Succeeded)
                        {
                            return RedirectToAction("reset-password-confirmation", "core/authentication");
                        }
                        else
                        {
                            ViewBag.message = "Something went wrong";
                        }
                    }
                    else
                    {
                        ViewBag.message = "Something went wrong";
                    }
                }
                else
                {
                    ViewBag.message = "You have entered wrong code";
                }
            }
            else
            {
                ViewBag.message = "User not found";
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("verify-phone-number")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.VerifyPhoneNumber()'
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<ActionResult> VerifyPhoneNumber()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.VerifyPhoneNumber()'
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("verify-phone-number")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.VerifyPhoneNumber(VerifyPhoneNumberViewModel)'
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.VerifyPhoneNumber(VerifyPhoneNumberViewModel)'
        {
            var user = userManager.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber && !x.PhoneNumberConfirmed);

            var result = await userManager.ChangePhoneNumberAsync(user.Id, model.PhoneNumber, model.Code);

            // Send an email with this link
            string code = await userManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Action("verify-email-address", "core/authentication", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            await userManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            return RedirectToAction("Confirm-email", "core/authentication");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Confirm-email")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ConfirmEmail()'
        public ActionResult ConfirmEmail()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.ConfirmEmail()'
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("verify-email-address")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.VarifyEmailAddress(string, string)'
        public async Task<ActionResult> VarifyEmailAddress(string userId, string code)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.VarifyEmailAddress(string, string)'
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await userManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {

                return Redirect(IdentityConfig.ApplicantUri);

            }
            return View("Error");
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.Login()'
        public ActionResult Login()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AuthenticationController.Login()'
        {
            return Redirect(IdentityConfig.ApplicantUri);
        }
    }
}