using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Institutes)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.INSTITUES)]

    public class InstitutesController : PermissionController
    {
        private readonly IInstituteService instituteService;
        private readonly IBoardService boardService;
        public InstitutesController(IInstituteService instituteService, IBoardService boardService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.instituteService = instituteService;
            this.boardService = boardService;
        }

        [HttpGet]
        [Route("get-institutes")]
        public IHttpActionResult Institutes(int ps, int pn, string qs)
        {
            int total = 0;
            List<InstituteModel> models = instituteService.Institutes(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.INSTITUES);
            return Ok(new ResponseMessage<List<InstituteModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-institute")]
        public async Task<IHttpActionResult> GetInstitute(int id)
        {
            InstituteViewModel model = new InstituteViewModel();
            model.Institute = await instituteService.GetInstitute(id);
            model.BoardTypes = instituteService.getBoardTypeSelectModel();
            model.Boards = await boardService.GetBoardsSelectModelByBoardType(model.Institute.BoardType);
            return Ok(new ResponseMessage<InstituteViewModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-institute")]
        public async Task<IHttpActionResult> SaveInstitute([FromBody] InstituteModel model)
        {
            return Ok(new ResponseMessage<InstituteModel>
            {
                Result = await instituteService.SaveInstitute(0, model)
            });
        }


        [HttpPut]
        [Route("update-institute/{id}")]
        public async Task<IHttpActionResult> UpdateInstituteType(int id, [FromBody] InstituteModel model)
        {
            return Ok(new ResponseMessage<InstituteModel>
            {
                Result = await instituteService.SaveInstitute(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-institute/{id}")]
        public async Task<IHttpActionResult> DeleteInstituteType(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await instituteService.DeleteInstitute(id)
            });
        }

        [HttpGet]
        [Route("get-board-by-board-type/{boardType}")]
        public async Task<IHttpActionResult> GetBoardsByBoardType(int boardType)
        {
            InstituteViewModel model = new InstituteViewModel();
            model.Boards = await boardService.GetBoardsSelectModelByBoardType(boardType);
            return Ok(new ResponseMessage<InstituteViewModel>
            {
                Result = model
            });
        }
    }
}
