using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Web.Models;
using Infinity.Bnois.Api.Web.Services;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Bnois.Api.Web.Controllers
{

    [RoutePrefix(IdentityRoutePrefix.Accounts)]
    [EnableCors("*", "*", "*")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AccountController'
    public class AccountController : BaseController
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AccountController'
    {
        private readonly IUserService userService;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AccountController.AccountController(IUserService)'
        public AccountController(IUserService userService)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AccountController.AccountController(IUserService)'
        {
            this.userService = userService;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("get-register")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AccountController.GetRegister()'
        public IHttpActionResult GetRegister()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AccountController.GetRegister()'
        {
            return Ok(new ResponseMessage<UserModel>
            {
                Result = new UserModel()
            });
        }
        [HttpPost]
        [AllowAnonymous]
        [ModelValidation]
        [Route("save-register")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AccountController.SaveRegister(UserModel)'
        public IHttpActionResult SaveRegister([FromBody] UserModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AccountController.SaveRegister(UserModel)'
        {
            UserModel user = new UserModel { UserName = model.Email, Password = model.Password, Email = model.Email ,FirstName=model.FirstName,LastName=model.LastName};
            userService.SaveApplicantUser(user);
            return Ok(new ResponseMessage<UserModel>
            {
                Result = model
            });
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("get-add-phone-number")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AccountController.AddPhoneNumber()'
        public IHttpActionResult AddPhoneNumber()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AccountController.AddPhoneNumber()'
        {
            return Ok(new ResponseMessage<AddPhoneNumberViewModel>
            {
                Result = new AddPhoneNumberViewModel(),
            });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("save-phone-number")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AccountController.AddPhoneNumber(AddPhoneNumberViewModel)'
        public IHttpActionResult AddPhoneNumber([FromBody] AddPhoneNumberViewModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AccountController.AddPhoneNumber(AddPhoneNumberViewModel)'
        {
            return Ok(new ResponseMessage<AddPhoneNumberViewModel>
            {
                Result = new AddPhoneNumberViewModel(),
            });
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AccountController.ResetPassword(ChangePasswordBindingModel)'
        public IHttpActionResult ResetPassword([FromBody] ChangePasswordBindingModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AccountController.ResetPassword(ChangePasswordBindingModel)'
        {
            bool result=  userService.ChangePassword(base.UserName,model.OldPassword, model.NewPassword);
            return Ok(new ResponseMessage<bool>
            {
                Result = result,
            });
        }

    }
}