
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
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Ers.ApplicationService;
using System.Net.Http;
using Infinity.Bnois.ExceptionHelper;
using Infinity.Bnois.Configuration;
using System.IO;
using System.Net.Http.Headers;
using System.Net;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.StatusChanges)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.STATUS_CHANGES)]

    public class StatusChangesController : PermissionController
    {
        private readonly IStatusChangeService statusChangeService;
        private readonly IMedicalCategoryService medicalCategoryService;
        private readonly IEyeVisionService eyeVisionService;
        private readonly ICommissionTypeService commissionTypeService;
        private readonly IBranchService branchService;



        public StatusChangesController(IStatusChangeService statusChangeService, IMedicalCategoryService medicalCategoryService,
            IEyeVisionService eyeVisionService, ICommissionTypeService commissionTypeService,
            IBranchService branchService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.statusChangeService = statusChangeService;
            this.medicalCategoryService = medicalCategoryService;
            this.eyeVisionService = eyeVisionService;
            this.commissionTypeService = commissionTypeService;
            this.branchService = branchService;

        }

        [HttpGet]
        [Route("get-status-changes")]
        public IHttpActionResult GetStatusChanges(int ps, int pn, string qs)
        {
            int total = 0;
            List<StatusChangeModel> models = statusChangeService.GetStatusChanges(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.STATUS_CHANGES);
            return Ok(new ResponseMessage<List<StatusChangeModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-status-change")]
        public async Task<IHttpActionResult> GetStatusChange(int id)
        {
            StatusChangeViewModel vm = new StatusChangeViewModel();
            vm.StatusChange = await statusChangeService.GetStatusChange(id);

            if (vm.StatusChange != null)
            {
                if (vm.StatusChange.StatusType == 1)
                {
                    vm.SelectModels = await medicalCategoryService.GetMedicalCategorySelectModels();
                }
                else if (vm.StatusChange.StatusType == 2)
                {
                    vm.SelectModels = await eyeVisionService.GetEyeVisionSelectModels();

                }
                else if (vm.StatusChange.StatusType == 3)
                {
                    vm.SelectModels = await commissionTypeService.GetCommissionTypeSelectModels();

                }
                else if (vm.StatusChange.StatusType == 4)
                {
                    vm.SelectModels = await branchService.GetBranchSelectModels();

                }
                else
                {
                    vm.SelectModels = null;
                }
            }

           
            return Ok(new ResponseMessage<StatusChangeViewModel>
            {
                Result = vm
            });
        }

        [HttpGet]
        [Route("get-status-change-select-models")]
        public async Task<IHttpActionResult> GetStatusChangeSelectModel(int type,int employeeId)
        {
            StatusChangeViewModel vm = new StatusChangeViewModel();

           
                if (type == 1)
                {
                    vm.SelectModels = await medicalCategoryService.GetMedicalCategorySelectModels();
                    vm.CurrentStatusId = await statusChangeService.GetEmployeeMedicalCategory(employeeId);

                }
                else if (type == 2)
                {
                    vm.SelectModels = await eyeVisionService.GetEyeVisionSelectModels();
                    vm.CurrentStatusId = await statusChangeService.GetEmployeeEyeVision(employeeId);

            }
            else if (type == 3)
                {
                    vm.SelectModels = await commissionTypeService.GetCommissionTypeSelectModels();
                    vm.CurrentStatusId = await statusChangeService.GetEmployeeCommissionType(employeeId);

            }
            else if (type == 4)
                {
                    vm.SelectModels = await branchService.GetBranchSelectModels();
                    vm.CurrentStatusId = await statusChangeService.GetEmployeeBranch(employeeId);

            }
            else
                {
                    vm.SelectModels = null;
                    vm.CurrentStatusId = 0;

            }



            return Ok(new ResponseMessage<StatusChangeViewModel>
            {
                Result = vm
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-status-change")]
        public async Task<IHttpActionResult> SaveStatusChange([FromBody] StatusChangeModel model)
        {
            return Ok(new ResponseMessage<StatusChangeModel>
            {
                Result = await statusChangeService.SaveStatusChange(0, model)
            });
        }

        [HttpPut]
        [Route("update-status-change/{id}")]
        public async Task<IHttpActionResult> UpdateStatusChange(int id, [FromBody] StatusChangeModel model)
        {
            return Ok(new ResponseMessage<StatusChangeModel>
            {
                Result = await statusChangeService.SaveStatusChange(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-status-change/{id}")]
        public async Task<IHttpActionResult> DeleteStatusChange(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await statusChangeService.DeleteStatusChange(id)
            });
        }

    }
}