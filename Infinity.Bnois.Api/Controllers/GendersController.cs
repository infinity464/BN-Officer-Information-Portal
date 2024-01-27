using Infinity.Bnois;
using Infinity.Bnois.Api;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Ers.ApplicationService;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
   
    [RoutePrefix(BnoisRoutePrefix.Genders)]
    [EnableCors("*", "*", "*")]

    public class GendersController : PermissionController
    {
        private readonly IGenderService genderService;
        /// <summary>
        /// GendersController constructor with IGenderService
        /// </summary>
        /// <param name="genderService">Instance of IGenderService</param>
        public GendersController(IGenderService genderService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.genderService = genderService;
        }

        /// <summary>
        /// Get all Gender
        /// </summary>
        /// <remarks>
        /// response data:
        /// {
        ///  isError:true/false,
        ///  message:"error message",
        ///  result: [{gender}],
        ///  total: total number of Gender
        /// }
        /// </remarks>
        /// <param name="ps">Represents number of page size</param>
        /// <param name="pn">Represents number of page number</param>
        /// <param name="qs">Represents searching key</param>
        /// <returns>Return list of Gender object</returns>
        [HttpGet]
        [Route("get-genders")]
        
        public IHttpActionResult GetGenders(int ps, int pn,string qs)
        {
            int total = 0;
            List<GenderModel> models = genderService.GetGenders(ps, pn,qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<GenderModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        /// <summary>
        /// Get Gender by Id 
        /// </summary>
        /// <param name="genderId">genderId</param>
        /// <returns>Return a single instance of Gender</returns>
        [HttpGet]
        [Route("get-gender")]
        
        public async Task<IHttpActionResult> GetGender(int genderId)
        {
            GenderModel model = await genderService.GetGender(genderId);
            return Ok(new ResponseMessage<GenderModel>()
            {
                Result = model
            });
        }

        /// <summary>
        /// Save Gender
        /// </summary>
        /// <remarks>
        /// response data:
        /// {
        ///  isError:true/false,
        ///  message:"error message",
        ///  result: {gender} 
        /// }
        /// </remarks>
        /// <param name="model">Gender</param>
        /// <returns>
        /// a single of Gender object
        /// </returns>
        [HttpPost]
        [ModelValidation]
        [Route("save-gender")]
        
        public async Task<IHttpActionResult> SaveGender([FromBody] GenderModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<GenderModel>()
            {
                Result = await genderService.SaveGender(0, model)
            });
        }

        /// <summary>
        /// Update Gender
        /// </summary>
        /// <remarks>
        /// response data:
        /// {
        ///  isError:true/false,
        ///  message:"error message",
        ///  result: {gender} 
        /// }
        /// </remarks>
        /// <param name="genderId">genderId</param>
        /// <param name="model">Gender</param>
        /// <returns>A newly created Gender</returns>
        /// <response code="201">Returns the updated Gender</response>
        /// <response code="400">If the Gender is null</response> 
        [HttpPut]
        [ModelValidation]
        [Route("update-gender/{id}")]
        
        public async Task<IHttpActionResult> UpdateGender(int genderId, [FromBody] GenderModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<GenderModel>()
            {
                Result = await genderService.SaveGender(genderId, model)
            });
        }

        /// <summary>
        /// Delete a Gender
        /// </summary>
        /// <param name="genderId">genderId</param>
        /// <returns>
        /// {
        ///  isError:true/false,
        ///  message:"error message",
        ///  result: true/false
        /// }
        /// </returns>
        [HttpDelete]
        [Route("delete-gender/{id}")]
        
        public async Task<IHttpActionResult> DeleteGender(int genderId)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = await genderService.DeleteGender(genderId)
            });
        }

        [HttpGet]
        [Route("get-gender-select-models")]
        public async Task<IHttpActionResult> GetCategorySelectModels()
        {
            List<SelectModel> selectModels = await genderService.GetGenderSelectModels();
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = selectModels
            });
        }
    }
}
