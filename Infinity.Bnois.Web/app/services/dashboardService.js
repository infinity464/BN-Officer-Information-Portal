(function () {
    'use strict';
    angular.module('app').service('dashboardService', ['dataConstants', 'apiHttpService', dashboardService]);

    function dashboardService(dataConstants, apiHttpService) {
        var service = {
            getDashboardOutSideNavy: getDashboardOutSideNavy,
            getDashboardBranch: getDashboardBranch,
            getDashboardAdminAuthority: getDashboardAdminAuthority,
            getDashboardInsideNavyOrganization: getDashboardInsideNavyOrganization,
            getDashboardBCGOrganization: getDashboardBCGOrganization,
            getDashboardLeave: getDashboardLeave,
            getDashboardExBDLeave: getDashboardExBDLeave,
            getDashboardStream: getDashboardStream,
            getDashboardCategory: getDashboardCategory,
            getDashboardGender: getDashboardGender,
            getDashboardOfficeAppointment: getDashboardOfficeAppointment,
            getDashboardUnderCourse: getDashboardUnderCourse,
            getDashboardUnderMission: getDashboardUnderMission,
            getOutsideNavyOfficer: getOutsideNavyOfficer,
            getAdminAuthorityOfficer: getAdminAuthorityOfficer,
            getLeaveOfficer: getLeaveOfficer,
            getExBDLeaveOfficer: getExBDLeaveOfficer,
            getBranchOfficer: getBranchOfficer,
            getStreamOfficer: getStreamOfficer,
            getCategoryOfficer: getCategoryOfficer,
            getGenderOfficer: getGenderOfficer
           
           
        };

        return service;
     
        function getDashboardOutSideNavy(officeId) {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-outside-navy?officeId=' + officeId;
            return apiHttpService.GET(url);
        }

        function getDashboardAdminAuthority() {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-admin-authority';
            return apiHttpService.GET(url);
        }
        function getDashboardInsideNavyOrganization() {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-inside-navy-organization';
            return apiHttpService.GET(url);
        }
         function getDashboardBCGOrganization() {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-bcg-organization';
            return apiHttpService.GET(url);
        }

       
        function getDashboardBranch() {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-branch';
            return apiHttpService.GET(url);
        }

        function getDashboardLeave() {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-leave';
            return apiHttpService.GET(url);
        }

       function getDashboardExBDLeave() {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-ex-bd-leave';
            return apiHttpService.GET(url);
        }

       
        function getDashboardStream(streamId) {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-stream?streamId='+streamId;
            return apiHttpService.GET(url);
        }


        function getDashboardCategory(categoryId) {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-category?categoryId=' + categoryId;
            return apiHttpService.GET(url);
        }

        function getDashboardGender(genderId) {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-gender?genderId=' + genderId;
            return apiHttpService.GET(url);
        }


        function getDashboardOfficeAppointment(officeType, rankId, displayId) {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-office-appointment?officeType=' + officeType + '&rankId=' + rankId + '&displayId=' + displayId;
            return apiHttpService.GET(url);
        }

        function getDashboardUnderMission(rankId) {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-under-mission?rankId=' + rankId;
            return apiHttpService.GET(url);
        }

        function getDashboardUnderCourse(rankId) {
            var url = dataConstants.DASHBOARD_URL + 'get-dashboard-under-course?rankId=' + rankId;
            return apiHttpService.GET(url);
        }



        function getOutsideNavyOfficer(officeId, rankLevel, parentId) {
            var url = dataConstants.DASHBOARD_URL + 'get-outside-navy-officer?officeId=' + officeId + '&rankLevel=' + rankLevel + '&parentId=' + parentId;
            return apiHttpService.GET(url);
        }

        function getAdminAuthorityOfficer(officeId, rankLevel,type) {
            var url = dataConstants.DASHBOARD_URL + 'get-admin-authority-officer?officeId=' + officeId+'&rankLevel='+rankLevel+'&type='+type;
            return apiHttpService.GET(url);
        }


        function getLeaveOfficer(leaveType, rankLevel) {
            var url = dataConstants.DASHBOARD_URL + 'get-leave-officer?leaveType=' + leaveType+'&rankLevel='+rankLevel;
            return apiHttpService.GET(url);
        }

        function getExBDLeaveOfficer( rankLevel) {
            var url = dataConstants.DASHBOARD_URL + 'get-ex-bd-leave-officer?rankLevel='+rankLevel;
            return apiHttpService.GET(url);
        }


        function getBranchOfficer(rankId, branch) {
            var url = dataConstants.DASHBOARD_URL + 'get-branch-officer?rankId=' + rankId + '&branch=' + branch;
            return apiHttpService.GET(url);
        }


        function getStreamOfficer(rankId, branch, streamId) {
            var url = dataConstants.DASHBOARD_URL + 'get-stream-officer?rankId=' + rankId + '&branch=' + branch + '&streamId=' + streamId;
            return apiHttpService.GET(url);
        }

        function getCategoryOfficer(rankId, branch, categoryId) {
            var url = dataConstants.DASHBOARD_URL + 'get-category-officer?rankId=' + rankId + '&branch=' + branch + '&categoryId=' + categoryId;
            return apiHttpService.GET(url);
        }

        function getGenderOfficer(rankId, branch, genderId) {
            var url = dataConstants.DASHBOARD_URL + 'get-gender-officer?rankId=' + rankId + '&branch=' + branch + '&genderId=' + genderId;
            return apiHttpService.GET(url);
        }



       

    }
})();