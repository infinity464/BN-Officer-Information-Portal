using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.Data.Models;
using Infinity.Ers.ApplicationService;

namespace Infinity.Bnois.Api.Controllers
{
	[RoutePrefix(BnoisRoutePrefix.EmployeeLeave)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.LEAVE_ENTRIES)]

    public class EmployeeLeaveController : PermissionController
    {
		private readonly IEmployeeLeaveService employeeLeaveService;
		private readonly IEmployeeService employeeService;
		private readonly ILeaveTypeService leaveTypeService;
		private readonly ICountryService countryService;
		private readonly ILeavePurposeService leavePurposeService;
		private readonly IOfficeAppointmentService officeAppointmentService;
        private readonly ITransferService transferService;
        private readonly ICurrentStatusService currentStatusService;

        public EmployeeLeaveController(ILeavePurposeService leavePurposeService, IEmployeeLeaveService employeeLeaveService, 
		    ILeaveTypeService leaveTypeService, IEmployeeService employeeService,
		    ICountryService countryService, IRoleFeatureService roleFeatureService, IOfficeAppointmentService officeAppointmentService, ITransferService transferService, ICurrentStatusService currentStatusService) : base(roleFeatureService)
        {
			this.employeeLeaveService = employeeLeaveService;
			this.employeeService = employeeService;
			this.leaveTypeService = leaveTypeService;
			this.countryService = countryService;
			this.leavePurposeService = leavePurposeService;
            this.officeAppointmentService = officeAppointmentService;
            this.transferService = transferService;
            this.currentStatusService = currentStatusService;

        }


		[HttpGet]
		[Route("get-employeeLeaves")]
		public async Task<IHttpActionResult> GetEmployeeLeaves(int ps, int pn, string qs, int? leaveType)
		{
			
			int total = 0;
			EmployeeLeaveViewModel vm = new EmployeeLeaveViewModel();
			vm.EmployeeLeaves = employeeLeaveService.GetEmployeeLeaves(ps, pn, qs, leaveType, out total);
			vm.LeaveTypes = await leaveTypeService.GetLeaveTypeSelectModel();
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.LEAVE_ENTRIES);

            return Ok(new ResponseMessage<EmployeeLeaveViewModel>()
			{
				Result = vm,
				Total = total, Permission=permission
			});
		}

		[HttpGet]
		[Route("get-employeeLeave")]	
		public async Task<IHttpActionResult> GetEmployeeLeave(int id)
		{
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.LEAVE_ENTRIES);
            EmployeeLeaveViewModel vm = new EmployeeLeaveViewModel();
            vm.EmployeeLeave = await employeeLeaveService.GetEmployeeLeave(id);
            vm.LeaveTypes = await leaveTypeService.GetLeaveTypeSelectModel();
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
			vm.LeavePurposeList = await leavePurposeService.GetLeavePurposeSelectModel();
            vm.CommandingOfficerList = officeAppointmentService.GetCommandingOfficerListByOfficeId();


            if (id > 0) vm.LeaveDetails = await employeeLeaveService.GetEmployeeLeaveDetailsByPNo(vm.EmployeeLeave.Employee.PNo);

            vm.File = new FileModel { FileName=vm.EmployeeLeave.FileName,FilePath=Documents.RemoteFileUrl+ vm.EmployeeLeave.FileName};
			return Ok(new ResponseMessage<EmployeeLeaveViewModel>
            {
                Result = vm,
                Permission = permission
            });
        }

        [HttpGet]
        [Route("get-last-officer-transfer")]
        public async Task<IHttpActionResult> GetLastOfficerTransfer(int employeeId)
        {
            TransferViewModel vm = new TransferViewModel();
            vwTransfer lastTransfer = transferService.GetLastTransfer(employeeId);
            if (lastTransfer.EmployeeStatus == 1 || lastTransfer.EmployeeStatus == 6)
            {
                vm.PreBornOffice = lastTransfer.BornOffice;
                vm.PreAttachOffice = lastTransfer.CurrentAttach;
                vm.PreAppointment = lastTransfer.Appointment;
            }


            return Ok(new ResponseMessage<TransferViewModel>
            {
                Result = vm
            });
        }


        [HttpPost]
		[ModelValidation]
		[Route("save-employeeLeave")]
		
		public async Task<IHttpActionResult> SaveEmployeeLeave([FromBody] EmployeeLeaveModel model)
		{
            
            return Ok(new ResponseMessage<EmployeeLeaveModel>
            {
                Result = await employeeLeaveService.SaveEmployeeLeave(0, model)
            });
        }

		[HttpPut]
		[Route("update-employeeLeave/{id}")]
		
		public async Task<IHttpActionResult> UpdateEmployeeLeave(int id, [FromBody] EmployeeLeaveModel model)
		{
            return Ok(new ResponseMessage<EmployeeLeaveModel>
            {
                Result = await employeeLeaveService.SaveEmployeeLeave(id, model)
            });
		}

		[HttpDelete]
		[Route("delete-employeeLeave/{id}")]
		public async Task<IHttpActionResult> DeleteEmployeeLeave(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await employeeLeaveService.DeleteEmployeeLeave(id)
			});
		}
        [HttpGet]
        [Route("employee-leave-approve")]
        public async Task<IHttpActionResult> employeeLeaveApprove(int leaveId, int leaveDetailId)
        {

            return Ok(new ResponseMessage<string>()
            {
                Result = await employeeLeaveService.employeeLeaveApprove(leaveId, leaveDetailId)
            });
        }
        [HttpGet]
        [Route("get-employee-leaveduration")]
        public async Task<IHttpActionResult> GetEmployeeLeaveDuration(int employeeId, int leaveType)
        {
           
            EmployeeLeaveViewModel vm =new EmployeeLeaveViewModel();
	        vm.LeaveBalances = await employeeLeaveService.GetEmployeeLeaveBalance(employeeId, leaveType);
			EmployeeLeaveModel employeeLeave = new EmployeeLeaveModel();
	        vm.EmployeeLeave = employeeLeave;
            vm.EmployeeLeave.LeaveDueCount = await employeeLeaveService.GetEmployeeLeaveDue(employeeId, leaveType);

            return Ok(new ResponseMessage<EmployeeLeaveViewModel>()
            {
	            Result = vm
			});
        }
        [HttpGet]
        [Route("get-employee-Defaultleaveduration")]
        public async Task<IHttpActionResult> GetDefaultEmployeeLeaveDuration(int employeeId, int leaveType,int fromDate)
        {
           

            EmployeeLeaveViewModel vm = new EmployeeLeaveViewModel();
            vm.LeaveBalances = await employeeLeaveService.GetDefaultEmployeeLeaveBalance(employeeId, leaveType, fromDate);
        

            return Ok(new ResponseMessage<EmployeeLeaveViewModel>()
            {
                Result = vm
            });
        }

        [HttpGet]
		[Route("get-employee-and-leaveInfo")]
		public async Task<IHttpActionResult> GetEmployeeAndLeaveInfo(string pId)
		{
			EmployeeLeaveViewModel vm = new EmployeeLeaveViewModel();
		    vm.LeaveDetails = await employeeLeaveService.GetEmployeeLeaveDetailsByPNo(pId);
			vm.Employee =await employeeService.GetEmployeeByPO();
           


            return Ok(new ResponseMessage<EmployeeLeaveViewModel>() { Result = vm });
		}

        [HttpPost]
        [Route("upload-leave-application-file")]
        public async Task<IHttpActionResult> UploadLeaveApplicationFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);
            }
            string fileSaveLocation = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;
            fileSaveLocation = HttpContext.Current.Server.MapPath("~/Documents/File");

            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            await Request.Content.ReadAsMultipartAsync(provider);

            fileName = System.IO.Path.GetFileName(provider.FileData[0].LocalFileName);
            filePath = string.Format("{0}://{1}/{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/File", fileName);

            FileModel file = new FileModel
            {
                FileName = fileName,
                FilePath = filePath
            };
            return Ok(new ResponseMessage<FileModel>
            {
                Result = file
            });
        }


        //[HttpGet]
        //[Route("get-employee-leaves-for-authorized-user")]
        //public async Task<IHttpActionResult> GetEmployeeLeavesForAuthorizedUser(int ps, int pn, string qs, int? leaveType)
        //{

        //    int total = 0;
        //    EmployeeLeaveViewModel vm = new EmployeeLeaveViewModel();
        //    vm.EmployeeLeaves = employeeLeaveService.GetEmployeeLeavesForAuthorizedUser(ps, pn, qs, leaveType);
        //    vm.LeaveTypes = await leaveTypeService.GetLeaveTypeSelectModel();
        //    RoleFeature permission = base.GetFeature(MASTER_SETUP.LEAVE_ENTRIES);

        //    return Ok(new ResponseMessage<EmployeeLeaveViewModel>()
        //    {
        //        Result = vm,
        //        Total = total,
        //        Permission = permission
        //    });
        //}

        [HttpGet]
        [Route("get-employee-approved-leaves-for-authorized-user")]
        public async Task<IHttpActionResult> GetEmployeeApprovedLeavesForAuthorizedUser(int ps, int pn, string qs,int leaveType)
        {
            int total = 0;
            List<object> approvedLeaves = employeeLeaveService.GetEmployeeApprovedLeavesForAuthorizedUser(ps, pn, qs,leaveType);
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = approvedLeaves,
                Total = total,
            });
        }

        [HttpGet]
        [Route("get-employee-leaves-for-authorized-user")]
        public async Task<IHttpActionResult> GetEmployeeLeavesForAuthorizedUser(int ps, int pn, string qs, int leaveType)
        {
            EmployeeLeaveViewModel vm = new EmployeeLeaveViewModel();
            vm.LeaveTypes = await leaveTypeService.GetLeaveTypeSelectModel();
            vm.approvedLeaves = employeeLeaveService.GetEmployeeLeavesForAuthorizedUser(ps, pn, qs, leaveType);
            return Ok(new ResponseMessage<EmployeeLeaveViewModel>
            {
                Result = vm
            });


            //List<object> approvedLeaves = employeeLeaveService.GetEmployeeLeavesForAuthorizedUser(ps, pn, qs, leaveType);
            //return Ok(new ResponseMessage<List<object>>()
            //{
            //    Result = approvedLeaves,
            //    Total = total,
            //});
        }

        [HttpGet]
        [Route("get-employee-leaveInfo-details")]
        public async Task<IHttpActionResult> GetEmployeeLeaveStatusData(int empLeaveId)
        {
            List<object> approvedLeaves = employeeLeaveService.GetEmployeeLeaveStatusData(empLeaveId);
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = approvedLeaves
            });
        }
        [HttpGet]
        [Route("leaves-tracking-by-leaveid")]
        public async Task<IHttpActionResult> LeaveTrackingByLeaveId(int leaveId)
        {
            List<object> leaveTranckingList =await employeeLeaveService.leaveTrackingByLeaveId(leaveId);
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = leaveTranckingList
            });
        }

        [HttpGet]
        [Route("get-employee-leaves-for-ns")]
        public async Task<IHttpActionResult> GetLeavesForNs(string pno,int leaveType)
        {
            int total = 0;
            EmployeeLeaveViewModel vm = new EmployeeLeaveViewModel();
            vm.LeaveTypes = await leaveTypeService.GetLeaveTypeSelectModel();
            vm.employeeLeaveForNS = employeeLeaveService.GetLeavesForNs(pno, leaveType);
            total = vm.employeeLeaveForNS.Count();
            return Ok(new ResponseMessage<EmployeeLeaveViewModel>
            {
                Result = vm,
                Total=total
            });
        }

        [HttpGet]
        [Route("get-employee-leaveInfo-details-by-employee-leaveid")]
        public async Task<IHttpActionResult> GetEmployeeLeaveStatusDatabyIndividualStatusId(int empLeaveId)
        {
            List<object> leaveDetails = employeeLeaveService.GetEmployeeLeaveStatusDatabyIndividualStatusId(empLeaveId);
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = leaveDetails
            });
        }

        [HttpGet]
        [Route("approve-employee-leave-for-ns")]
        public async Task<IHttpActionResult> ApproveEmployeeLeaveByNs(int empLeaveId, string remark)
        {
            string msg = employeeLeaveService.ApproveEmployeeLeaveByNs(empLeaveId,remark);
            return Ok(new ResponseMessage<string>()
            {
                Result = msg
            });
        }

        [HttpGet]
        [Route("cancel-employee-leave-for-ns")]
        public async Task<IHttpActionResult> CancelEmployeeLeaveByNs(int empLeaveId, string remark)
        {
            string msg = employeeLeaveService.CancelEmployeeLeaveByNs(empLeaveId, remark);
            return Ok(new ResponseMessage<string>()
            {
                Result = msg
            });
        }

        [HttpGet]
        [Route("cancel-employee-leave")]
        public async Task<IHttpActionResult> CancelEmployeeLeave(int empLeaveId, string remark)
        {
            string msg = employeeLeaveService.CancelEmployeeLeave(empLeaveId, remark);
            return Ok(new ResponseMessage<string>()
            {
                Result = msg
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
        [Route("get-notifications")]
        public IHttpActionResult GetNotifications(string pNo)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = currentStatusService.GetNotifications(base.UserId, pNo)
            });
        }
        [HttpGet]
        [Route("get-employee-leave-notifications")]
        public IHttpActionResult GetEmployeeLeaveNotifications()
        {
            return Ok(new ResponseMessage<string>()
            {
                Result = employeeLeaveService.GetEmployeeLeaveNotifications()
            });
        }
        [HttpGet]
        [Route("get-employee-inforamtions")]
        public IHttpActionResult GetCurrentEmployeeInformation()
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = employeeLeaveService.GetCurrentEmployeeInformation()
            });
        }
    }
}
