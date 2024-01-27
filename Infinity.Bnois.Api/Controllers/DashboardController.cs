using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Dashboard)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.DASHBOARD)]

    public class DashboardController : BaseController
    {
        private readonly IDashboardService dashboardService;
       

        public DashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }
        

        [HttpGet]
        [Route("get-dashboard-outside-navy")]
        public IHttpActionResult GetDashboardOutSideNavy(int officeId)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardOutSideNavy(officeId)
            });
        }
        

        [HttpGet]
        [Route("get-dashboard-branch")]
        public IHttpActionResult GetDashboardBranch()
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardBranch()
            });
        }

        [HttpGet]
        [Route("get-dashboard-admin-authority")]
        public IHttpActionResult GetDashboardAdminAuthority()
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardAdminAuthority()
            });
        }

        [HttpGet]
        [Route("get-dashboard-under-mission")]
        public IHttpActionResult GetDashboardUnderMission(int rankId)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardUnderMission( rankId)
            });
        }

        [HttpGet]
        [Route("get-dashboard-under-course")]
        public IHttpActionResult GetDashboardUnderCourse(int rankId)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardUnderCourse(rankId)
            });
        }


        [HttpGet]
        [Route("get-dashboard-inside-navy-organization")]
        public IHttpActionResult GetDashboardInsideNavyOrganization()
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardInsideNavyOrganization()
            });
        }


        [HttpGet]
        [Route("get-dashboard-bcg-organization")]
        public IHttpActionResult GetDashboardBCGOrganization()
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardBCGOrganization()
            });
        }


        [HttpGet]
        [Route("get-dashboard-leave")]
        public IHttpActionResult GetDashboardleave()
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardLeave()
            });
        }

        [HttpGet]
        [Route("get-dashboard-ex-bd-leave")]
        public IHttpActionResult GetDashboardExBDLeave()
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardExBDLeave()
            });
        }


        [HttpGet]
        [Route("get-dashboard-stream")]
        public IHttpActionResult GetDashboardStream(int streamId)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardStream(streamId)
            });
        }

        [HttpGet]
        [Route("get-dashboard-category")]
        public IHttpActionResult GetDashboardCategory(int categoryId)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardCategory(categoryId)
            });
        }
        [HttpGet]
        [Route("get-dashboard-gender")]
        public IHttpActionResult GetDashboardGender(int genderId)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardGender(genderId)
            });
        }


        [HttpGet]
        [Route("get-dashboard-office-appointment")]
        public IHttpActionResult GetDashboardOfficeAppointment(int officeType, int rankId, int displayId)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetDashboardOfficeAppointment(officeType,rankId,displayId)
            });
        }



        [HttpGet]
        [Route("get-outside-navy-officer")]
        public IHttpActionResult GetOutsideNavyOfficer(int officeId,int rankLevel, int parentId)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetOutsideNavyOfficer(officeId,rankLevel, parentId)
            });
        }


        [HttpGet]
        [Route("get-admin-authority-officer")]
        public IHttpActionResult GetAdminAuthorityOfficer(int officeId,int rankLevel,int type)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetAdminAuthorityOfficer(officeId,rankLevel,type)
            });
        }
        
       
        [HttpGet]
        [Route("get-leave-officer")]
        public IHttpActionResult GetLeaveOfficer(int leaveType,int rankLevel)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetLeaveOfficer(leaveType, rankLevel)
            });
        }

        [HttpGet]
        [Route("get-ex-bd-leave-officer")]
        public IHttpActionResult GetExBDLeaveOfficer( int rankLevel)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetExBDLeaveOfficer(rankLevel)
            });
        }


        [HttpGet]
        [Route("get-branch-officer")]
        public IHttpActionResult GetBranchOfficer(int rankId,string branch)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetBranchOfficer(rankId, branch)
            });
        }

        [HttpGet]
        [Route("get-category-officer")]
        public IHttpActionResult GetCategoryOfficer(int rankId,string branch, int categoryId)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetCategoryOfficer(rankId, branch, categoryId)
            });
        }

        [HttpGet]
        [Route("get-gender-officer")]
        public IHttpActionResult GetGenderOfficer(int rankId, string branch, int genderId)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetGenderOfficer(rankId, branch, genderId)
            });
        }

        [HttpGet]
        [Route("get-stream-officer")]
        public IHttpActionResult GetStreamOfficer(int rankId, string branch, int streamId)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = dashboardService.GetStreamOfficer(rankId, branch, streamId)
            });
        }



    }
}