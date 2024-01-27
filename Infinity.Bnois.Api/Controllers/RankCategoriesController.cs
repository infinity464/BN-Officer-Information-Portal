using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix(BnoisRoutePrefix.RankCategories)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.RANKS_CATEGORIES)]
    public class RankCategoriesController: PermissionController
    {
        private readonly IRankCategoryService rankCategoryService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rankCategoryService"></param>
        public RankCategoriesController(IRankCategoryService rankCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.rankCategoryService = rankCategoryService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ps"></param>
        /// <param name="pn"></param>
        /// <param name="qs"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-rank-categories")]
        public IHttpActionResult GetRankCategories(int ps, int pn, string qs)
        {
            int total = 0;
            List<RankCategoryModel> rankCategories = rankCategoryService.GetRankCategories(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.RANKS_CATEGORIES);
            return Ok(new ResponseMessage<List<RankCategoryModel>>()
            {
                Result = rankCategories,
                Total = total, Permission=permission
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-rank-category")]
        public async Task<IHttpActionResult> GetRankCategory(int id)
        {
            RankCategoryModel model = await rankCategoryService.GetRankCategory(id);
            return Ok(new ResponseMessage<RankCategoryModel>
            {
                Result = model
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ModelValidation]
        [Route("save-rank-category")]
        public async Task<IHttpActionResult> SaveRankCategory([FromBody] RankCategoryModel model)
        {
            return Ok(new ResponseMessage<RankCategoryModel>
            {
                Result = await rankCategoryService.SaveRankCategory(0, model)
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-rank-category/{id}")]
        public async Task<IHttpActionResult> UpdateRankCategory(int id, [FromBody] RankCategoryModel model)
        {
            return Ok(new ResponseMessage<RankCategoryModel>
            {
                Result = await rankCategoryService.SaveRankCategory(id, model)

            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-rank-category/{id}")]
        public async Task<IHttpActionResult> DeleteRankCategory(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await rankCategoryService.DeleteRankCategoryAsync(id)
            });
        }
    }
}
