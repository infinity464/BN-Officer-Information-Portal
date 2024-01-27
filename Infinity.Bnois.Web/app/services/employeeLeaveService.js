(function () {
	'use strict';
	angular.module('app').service('employeeLeaveService', ['dataConstants', 'apiHttpService', employeeLeaveService]);

	function employeeLeaveService(dataConstants, apiHttpService) {
		var service = {
			getEmployeeLeaves: getEmployeeLeaves,
			getEmployeeLeave: getEmployeeLeave,
			saveEmployeeLeave: saveEmployeeLeave,
			updateEmployeeLeave: updateEmployeeLeave,
			deleteEmployeeLeave: deleteEmployeeLeave,		
			getEmployeeLeaveDurationInfo: getEmployeeLeaveDurationInfo,
			getEmployeeLeaveBreakDown: getEmployeeLeaveBreakDown,
            getEmployeeLeaveAndEmployeeInfo: getEmployeeLeaveAndEmployeeInfo,
            fileUploadUrl: fileUploadUrl,
            getPrivilegeLeaveDurationInfo: getPrivilegeLeaveDurationInfo,


            GetEmployeeApprovedLeavesForAuthorizedUser: GetEmployeeApprovedLeavesForAuthorizedUser,
            GetEmployeeLeavesForAuthorizedUser: GetEmployeeLeavesForAuthorizedUser,
            employeeLeaveApprove: employeeLeaveApprove,
            //GetEmployeeLeaveStatusData: GetEmployeeLeaveStatusData,
            GetEmployeeLeavesForNsUser: GetEmployeeLeavesForNsUser,
            GetEmployeeLeaveStatusDatabyIndividualStatusId: GetEmployeeLeaveStatusDatabyIndividualStatusId,
            GetEmployeeLeaveStatusData: GetEmployeeLeaveStatusData,
            ApproveEmployeeLeaveByNs: ApproveEmployeeLeaveByNs,
            leaveTrackingByLeaveId: leaveTrackingByLeaveId,
            CancelEmployeeLeaveByNs: CancelEmployeeLeaveByNs,
            CancelEmployeeLeave: CancelEmployeeLeave,
            GetEmployeeLeaveNotifications: GetEmployeeLeaveNotifications,
            GetCurrentEmployeeInformation: GetCurrentEmployeeInformation
		};

        return service;

        function fileUploadUrl() {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'upload-leave-application-file';
            return url;
        }

		function getEmployeeLeaves(pageSize, pageNumber, searchText,leaveTypeId) {
			var url = dataConstants.EMPLOYEELEAVE_URL + 'get-employeeLeaves?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText + "&leaveType=" + leaveTypeId ;
			return apiHttpService.GET(url);
        }


		function getEmployeeLeave(id) {
			var url = dataConstants.EMPLOYEELEAVE_URL + 'get-employeeLeave?id=' + id;
			return apiHttpService.GET(url);
        }

        function employeeLeaveApprove(leaveId, leaveDetailId) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'employee-leave-approve?leaveId=' + leaveId + "&leaveDetailId=" + leaveDetailId;
            return apiHttpService.GET(url);
        }

		function saveEmployeeLeave(data) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'save-employeeLeave';
			return apiHttpService.POST(url, data);
		}

		function updateEmployeeLeave(id, data) {
			var url = dataConstants.EMPLOYEELEAVE_URL + 'update-employeeLeave/' + id;
			return apiHttpService.PUT(url, data);
		}

		//function deleteEmployeeLeave(id) {
		//	var url = dataConstants.EMPLOYEELEAVE_URL + 'delete-employeeLeave/' + id;
		//	return apiHttpService.DELETE(url);			
		//}
        
        function getPrivilegeLeaveDurationInfo(employeeId, leaveType,fromDate) {

            var url = dataConstants.EMPLOYEELEAVE_URL + 'get-employee-Defaultleaveduration?employeeId=' + employeeId + "&leaveType=" + leaveType + "&fromDate=" + fromDate;
            return apiHttpService.GET(url);
        }
        function getEmployeeLeaveDurationInfo(employeeId, leaveType) {
         
            var url = dataConstants.EMPLOYEELEAVE_URL + 'get-employee-leaveduration?employeeId=' + employeeId + "&leaveType=" + leaveType ;
			return apiHttpService.GET(url);
		}
		function getEmployeeLeaveBreakDown(employeeId) {

			var url = dataConstants.EMPLOYEELEAVE_URL + 'get-leave-break-downs?employeeId=' + employeeId ;
			return apiHttpService.GET(url);
		}
		function getEmployeeLeaveAndEmployeeInfo(pId) {

			var url = dataConstants.EMPLOYEELEAVE_URL + 'get-employee-and-leaveInfo?pId=' + pId;
			return apiHttpService.GET(url);
		}

		function deleteEmployeeLeave(id) {
			var url = dataConstants.EMPLOYEELEAVE_URL + 'delete-employeeLeave/' + id;
			return apiHttpService.DELETE(url);
        }

        function GetEmployeeApprovedLeavesForAuthorizedUser(pageSize, pageNumber, searchText, leaveTypeId) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'get-employee-approved-leaves-for-authorized-user?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText + "&leaveType=" + leaveTypeId;
            return apiHttpService.GET(url);
        }
        function GetEmployeeLeavesForAuthorizedUser(pageSize, pageNumber, searchText, leaveTypeId) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'get-employee-leaves-for-authorized-user?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText + "&leaveType=" + leaveTypeId;
            return apiHttpService.GET(url);
        }
        function GetEmployeeLeavesForNsUser(pno, leaveType) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'get-employee-leaves-for-ns?pno=' + pno + "&leaveType=" + leaveType;
            return apiHttpService.GET(url);
        }
        function leaveTrackingByLeaveId(leaveId) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'leaves-tracking-by-leaveid?leaveId=' + leaveId;
            return apiHttpService.GET(url);
        }

        function GetEmployeeLeaveStatusDatabyIndividualStatusId(empLeaveId) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'get-employee-leaveInfo-details-by-employee-leaveid?empLeaveId=' + empLeaveId;
            return apiHttpService.GET(url);
        }
        function GetEmployeeLeaveStatusData(empLeaveId) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'get-employee-leaveInfo-details?empLeaveId=' + empLeaveId;
            return apiHttpService.GET(url);
        }
        function ApproveEmployeeLeaveByNs(empLeaveId, remark) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'approve-employee-leave-for-ns?empLeaveId=' + empLeaveId + "&remark=" + remark;
            return apiHttpService.GET(url);
        }

        function CancelEmployeeLeaveByNs(empLeaveId, remark) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'cancel-employee-leave-for-ns?empLeaveId=' + empLeaveId + "&remark=" + remark;
            return apiHttpService.GET(url);
        }

        function CancelEmployeeLeave(empLeaveId, remark) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'cancel-employee-leave?empLeaveId=' + empLeaveId + "&remark=" + remark;
            return apiHttpService.GET(url);
        }

        function GetEmployeeLeaveNotifications() {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'get-employee-leave-notifications';
            return apiHttpService.GET(url);
        }

        function GetCurrentEmployeeInformation() {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'get-employee-inforamtions';
            return apiHttpService.GET(url);
        }
	}
})();