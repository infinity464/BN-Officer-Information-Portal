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
    [RoutePrefix(BnoisRoutePrefix.CurrentStatus)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.CURRENT_STATUS)]

    public class CurrentStatusController : BaseController
    {
        private readonly ICurrentStatusService currentStatusService;
        private readonly IEmployeeLeaveService employeeLeaveService;
        private readonly IEmployeeGeneralService employeeGeneralService;
        private readonly IEmployeeService employeeService;

        public CurrentStatusController(ICurrentStatusService currentStatusService, IEmployeeLeaveService employeeLeaveService,IEmployeeGeneralService employeeGeneralService, IEmployeeService employeeService)
        {
            this.currentStatusService = currentStatusService;
            this.employeeLeaveService = employeeLeaveService;
            this.employeeGeneralService = employeeGeneralService;
            this.employeeService = employeeService;
        }

        [HttpGet]
        [Route("get-general-information")]
        public IHttpActionResult GetGeneralInformation(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetGeneralInformation(pNo)
            });
        }

        [HttpGet]
        [Route("get-civil-academic-qualification")]
        public IHttpActionResult GetCivilAcademicQualification(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetCivilAcademicQualification(pNo)
            });
        }
        
        [HttpGet]
        [Route("get-security-clearance")]
        public IHttpActionResult GetSecurityClearance(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetSecurityClearance(pNo)
            });
        }


        [HttpGet]
        [Route("get-course-attended")]
        public IHttpActionResult GetCourseAttended(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetCourseAttended(pNo)
            });
        }

        [HttpGet]
        [Route("get-exam-test-result")]
        public IHttpActionResult GetExamTestResult(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetExamTestResult(pNo)
            });
        }

        [HttpGet]
        [Route("get-punishment-discipline")]
        public IHttpActionResult GetPunishmentDiscipline(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetPunishmentDiscipline(pNo)
            });
        }

        [HttpGet]
        [Route("get-commendation-appreciation")]
        public IHttpActionResult GetCommendationAppreciation(string pNo,int type)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetCommendationAppreciation(pNo,type)
            });
        }

        [HttpGet]
        [Route("get-award")]
        public IHttpActionResult GetAwardInfo(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetAward(pNo)
            });
        }

        [HttpGet]
        [Route("get-medal")]
        public IHttpActionResult GetMedalInfo(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetMedal(pNo)
            });
        }


        [HttpGet]
        [Route("get-publication")]
        public IHttpActionResult GetPublicationInfo(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetPublication(pNo)
            });
        }

        [HttpGet]
        [Route("get-clean-service")]
        public IHttpActionResult GetCleanService(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetCleanService(pNo)
            });
        }


        [HttpGet]
        [Route("get-current-children")]
        public IHttpActionResult GetChildren(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetChildren(pNo)
            });
        }

        [HttpGet]
        [Route("get-current-sibling")]
        public IHttpActionResult GetSiblingInfo(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetSibling(pNo)
            });
        }

        [HttpGet]
        [Route("get-current-next-of-kin")]
        public IHttpActionResult GetNextOfKin(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetNextOfKin(pNo)
            });
        }


	    [HttpGet]
	    [Route("get-heir-info")]
	    public IHttpActionResult GetHeirInfo(string pNo)
	    {
		    return Ok(new ResponseMessage<dynamic>()
		    {
			    Result = currentStatusService.GetHeirInfo(pNo)
		    });
	    }



        [HttpGet]
        [Route("get-opr-grading")]
        public IHttpActionResult GetOprGrading(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetOprGrading(pNo)
            });
        }

        [HttpGet]
        [Route("get-opr-index")]
        public IHttpActionResult GetOprIndex(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetOprIndex(pNo)
            });
        }

        [HttpGet]
        [Route("get-foreign-visit")]
        public IHttpActionResult GetForeignVisit(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetForeignVisit(pNo)
            });
        }

        [HttpGet]
        [Route("get-parent-info")]
        public IHttpActionResult GetParentInfo(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetParentInfo(pNo)
            });
        }

        [HttpGet]
        [Route("get-spouse-info")]
        public IHttpActionResult GetSpouseInfo(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetSpouseInfo(pNo)
            });
        }

        [HttpGet]
        [Route("get-transfer-history")]
        public IHttpActionResult GetTransferHistory(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetTransferHistory(pNo)
            });
        }
        [HttpGet]
        [Route("get-costguard-history")]
        public IHttpActionResult GetCostGuardHistory(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetCostGuardHistory(pNo)
            });
        }

        [HttpGet]
        [Route("get-temporary-transfer-history")]
        public IHttpActionResult GetTemporaryTransferHistory(int transferId)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetTemporaryTransferHistory(transferId)
            });
        }

        [HttpGet]
        [Route("get-promotion-history")]
        public IHttpActionResult GetPromotionHistory(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetPromotionHistory(pNo)
            });
        }



        [HttpGet]
        [Route("get-additional-sea-services")]
        public IHttpActionResult GetAdditionalSeaServices(string pNo)
        {
            CurrentStatusViewModel vm = new CurrentStatusViewModel();
            vm.Services = currentStatusService.GetAdditionalSeaServices(pNo);
            vm.GrandTotal = currentStatusService.GetAdditionalSeaServicesGrandTotal(pNo);

            return Ok(new ResponseMessage<dynamic>()
            {
                Result = vm
            });
        }

        [HttpGet]
        [Route("get-sea-services")]
        public IHttpActionResult GetSeaServices(string pNo)
        {
            CurrentStatusViewModel vm = new CurrentStatusViewModel();
            vm.Services = currentStatusService.GetSeaServices(pNo);
            vm.GrandTotal = currentStatusService.GetSeaServicesGrandTotal(pNo);

            return Ok(new ResponseMessage<dynamic>()
            {
                Result = vm
            });
        }


        [HttpGet]
        [Route("get-zone-services")]
        public IHttpActionResult GetZoneServices(string pNo)
        {
            CurrentStatusViewModel vm = new CurrentStatusViewModel();
            vm.Services = currentStatusService.GetZoneServices(pNo);
            vm.GrandTotal = currentStatusService.GetZoneServicesGrandTotal(pNo);

            return Ok(new ResponseMessage<dynamic>()
            {
                Result = vm
            });
        }

        [HttpGet]
	    [Route("get-instructional-services")]
	    public IHttpActionResult GetInstructionalServices(string pNo)
	    {
		    return Ok(new ResponseMessage<object>()
		    {
			    Result = currentStatusService.GetInstructionalServices(pNo)
		    });
	    }

	    [HttpGet]
	    [Route("get-sea-command-services")]
	    public IHttpActionResult GetSeaCommandServices(string pNo)
	    {
	        CurrentStatusViewModel vm =new CurrentStatusViewModel();
	        vm.Services = currentStatusService.GetSeaCommandServices(pNo);
	        vm.GrandTotal = currentStatusService.GetSeaCommandServicesGrandTotal(pNo);


            return Ok(new ResponseMessage<dynamic>()
		    {
			    Result =vm
		    });
	    }

	    [HttpGet]
	    [Route("get-inter-organization-services")]
	    public IHttpActionResult GetInterOrganizationServices(string pNo)
	    {
		    return Ok(new ResponseMessage<object>()
		    {
			    Result = currentStatusService.GetInterOrganizationServices(pNo)
		    });
	    }

	    [HttpGet]
	    [Route("get-intelligence-services")]
	    public IHttpActionResult GetIntelligenceServices(string pNo)
	    {
		    return Ok(new ResponseMessage<object>()
		    {
			    Result = currentStatusService.GetIntelligenceServices(pNo)
		    });
	    }



        [HttpGet]
        [Route("get-missions")]
        public IHttpActionResult GetMissions(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetMissions(pNo)
            });
        }



        [HttpGet]
        [Route("get-foreign-projects")]
        public IHttpActionResult GetForeignProjects(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetForeignProjects(pNo)
            });
        }

        [HttpGet]
        [Route("get-hod-services")]
        public IHttpActionResult GetHODServices(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetHODServices(pNo)
            });
        }




        [HttpGet]
        [Route("get-dockyard-services")]
        public IHttpActionResult GetDockyardServices(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetDockyardServices(pNo)
            });
        }


   

        [HttpGet]
        [Route("get-notifications")]
        public IHttpActionResult GetNotifications(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetNotifications(base.UserId,pNo)
            });
        }

    
        [HttpGet]
        [Route("get-remark")]
        public IHttpActionResult GetRemarkInfo(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetRemark(pNo)
            });
        }

        [HttpGet]
        [Route("get-persuasion")]
        public IHttpActionResult GetPersuasion(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetPersuasion(pNo)
            });
        }

        [HttpGet]
        [Route("get-course-future-plan")]
        public IHttpActionResult GetCourseFuturePlan(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetCourseFuturePlan(pNo)
            });
        }

        [HttpGet]
        [Route("get-transfer-future-plan")]
        public IHttpActionResult GetTransferFuturePlan(string pNo)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = currentStatusService.GetTransferFuturePlan(pNo)
            });
        }


        [HttpGet]
        [Route("get-current-status")]
        public IHttpActionResult GetCurrentStatus(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetCurrentStatus(pNo)
            });
        }
        [HttpGet]
        [Route("get-current-status1")]
        public IHttpActionResult GetCurrentStatus1(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetCurrentStatus1(pNo)
            });
        }

        [HttpGet]
        [Route("get-issb")]
        public IHttpActionResult GetISSB(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetISSB(pNo)
            });
        }



        [HttpGet]
        [Route("get-leave-info")]
        public async Task<IHttpActionResult> GetLeaveInfo(string pNo)
        {

            return Ok(new ResponseMessage<object>()
            {
                Result =await employeeLeaveService.GetLeaveSummary(pNo)
            });
        }

        [HttpPut]
        [Route("update-employee-email-mobile")]

        public IHttpActionResult UpdateEmployeeEmailContact(string PNo, string EmailAddress,string ContactNo)
        {
            bool saved = employeeGeneralService.UpdateEmployeeEmailContact(PNo, EmailAddress, ContactNo);
            return Ok(new ResponseMessage<bool>()
            {
                Result = saved
            });
        }

        [HttpGet]
        [Route("get-employee-and-leaveInfo-from-currentstatus")]
        public async Task<IHttpActionResult> GetEmployeeAndLeaveInfo(string pId)
        {
            EmployeeLeaveViewModel vm = new EmployeeLeaveViewModel();
            vm.LeaveDetails = await employeeLeaveService.GetEmployeeLeaveDetailsByPNo(pId);
            vm.Employee = await employeeService.GetEmployeeByPO();



            return Ok(new ResponseMessage<EmployeeLeaveViewModel>() { Result = vm });
        }

        [HttpGet]
        [Route("get-user-email-contact")]
        public IHttpActionResult GetUserEmailContactLog()
        {
            userLogModel userLogModel = new userLogModel();
            return Ok(new ResponseMessage<userLogModel>
            {
                Result = userLogModel
            });
        }

        [HttpPost]
        [Route("get-user-email-contact-log")]
        public async Task<IHttpActionResult> GetUserEmailContactLogData([FromBody] userLogModel model)
        {
            //userLogModel userLogModel = new userLogModel();
            return Ok(new ResponseMessage<List<object>>
            {
                Result = currentStatusService.GetUserLogData(model.FromDate,model.ToDate)
            });
        }
    }


}