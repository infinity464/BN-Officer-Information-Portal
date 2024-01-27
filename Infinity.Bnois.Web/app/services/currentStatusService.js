(function () {
    'use strict';
    angular.module('app').service('currentStatusService', ['dataConstants', 'identityDataConstants', 'apiHttpService', currentStatusService]);

    function currentStatusService(dataConstants, identityDataConstants, apiHttpService) {
        var service = {
            getGeneralInformation: getGeneralInformation,
            getCivilAcademicQualification: getCivilAcademicQualification,
            getSecurityClearance: getSecurityClearance,
            getCourseAttended: getCourseAttended,
            getExamTestResult: getExamTestResult,
            getPunishmentDiscipline: getPunishmentDiscipline,
            getCommendationAppreciation: getCommendationAppreciation,
            getAward: getAward,
            getMedal: getMedal,
            getCleanService: getCleanService,
            getPublication: getPublication,
            getChildren: getChildren,
            getSibling: getSibling,
            getNextOfKin: getNextOfKin,
            getHeir: getHeir,
            getOprGrading: getOprGrading,
            getOprIndex: getOprIndex,
            getForeignVisit: getForeignVisit,
            getParentInfo: getParentInfo,
            getSpouseInfo: getSpouseInfo,
            getTransferHistory: getTransferHistory,
            getCostGuardHistory: getCostGuardHistory,
            getPromotionHistory: getPromotionHistory,
            getAdditionalSeaServices: getAdditionalSeaServices,
            getSeaServices: getSeaServices,
            getInstructionalServices: getInstructionalServices,
            getSeaCommandServices: getSeaCommandServices,
            getInterOrganizationServices: getInterOrganizationServices,
            getIntelligenceServices: getIntelligenceServices,
            getCurrentStatus: getCurrentStatus,
            getNotifications: getNotifications,
            getRemark: getRemark,
            getPersuasion: getPersuasion,
            getCourseFuturePlan: getCourseFuturePlan,
            getTransferFuturePlan: getTransferFuturePlan,
            getTemporaryTransferHistory: getTemporaryTransferHistory,
            getLeaveInfo: getLeaveInfo,
            getISSB: getISSB,
            getForeignProjects: getForeignProjects,
            getMissions: getMissions,
            getHODServices: getHODServices,
            getDockyardServices: getDockyardServices,
            getZoneServices: getZoneServices,
            getEmployeeLeaveAndEmployeeInfo: getEmployeeLeaveAndEmployeeInfo,

            UpdateEmployeeEmailContact: UpdateEmployeeEmailContact,
            changePassword: changePassword,
            getCurrentStatus1: getCurrentStatus1,
            GetUserEmailContactLog: GetUserEmailContactLog,
            GetUserEmailContactLogData: GetUserEmailContactLogData

        };

        return service;
     
        function getGeneralInformation(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-general-information?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getCivilAcademicQualification(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-civil-academic-qualification?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getSecurityClearance(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-security-clearance?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getCourseAttended(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-course-attended?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getExamTestResult(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-exam-test-result?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getPunishmentDiscipline(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-punishment-discipline?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getCommendationAppreciation(pNo,type) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-commendation-appreciation?pNo=' + pNo+'&type='+type;
            return apiHttpService.GET(url);
        }
        function getAward(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-award?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getMedal(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-medal?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getPublication(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-publication?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getCleanService(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-clean-service?pNo=' + pNo;
            return apiHttpService.GET(url);
        }


        function getChildren(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-current-children?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getSibling(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-current-sibling?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getNextOfKin(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-current-next-of-kin?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getHeir(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-heir-info?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getOprGrading(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-opr-grading?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getOprIndex(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-opr-index?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getForeignVisit(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-foreign-visit?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getParentInfo(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-parent-info?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getSpouseInfo(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-spouse-info?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getTransferHistory(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-transfer-history?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getCostGuardHistory(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-costguard-history?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getTemporaryTransferHistory(transferId) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-temporary-transfer-history?transferId=' + transferId;
            return apiHttpService.GET(url);
        }

        function getPromotionHistory(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-promotion-history?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getAdditionalSeaServices(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-additional-sea-services?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getSeaServices(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-sea-services?pNo=' + pNo;
            return apiHttpService.GET(url);
        }


        function getZoneServices(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-zone-services?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getInstructionalServices(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-instructional-services?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getSeaCommandServices(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-sea-command-services?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getInterOrganizationServices(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-inter-organization-services?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getIntelligenceServices(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-intelligence-services?pNo=' + pNo;
            return apiHttpService.GET(url);
        }


        function getHODServices(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-hod-services?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getDockyardServices(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-dockyard-services?pNo=' + pNo;
            return apiHttpService.GET(url);
        }



        function getForeignProjects(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-foreign-projects?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getMissions(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-missions?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getRemark(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-remark?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getPersuasion(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-persuasion?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getCourseFuturePlan(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-course-future-plan?pNo=' + pNo;
            return apiHttpService.GET(url);
        }
        function getTransferFuturePlan(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-transfer-future-plan?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getNotifications(pNo) {
            var url = dataConstants.EMPLOYEELEAVE_URL + 'get-notifications?pNo=' + pNo;
            return apiHttpService.GET(url);
        }


        function getCurrentStatus(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-current-status?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function getCurrentStatus1(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-current-status1?pNo=' + pNo;
            return apiHttpService.GET(url);
        }


        function getISSB(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-issb?pNo=' + pNo;
            return apiHttpService.GET(url);
        }



        function getLeaveInfo(pNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-leave-info?pNo=' + pNo;
            return apiHttpService.GET(url);
        }

        function UpdateEmployeeEmailContact(PNo, EmailAddress, ContactNo) {
            var url = dataConstants.CURRENT_STATUS_URL + 'update-employee-email-mobile?PNo=' + PNo + '&EmailAddress=' + EmailAddress + '&ContactNo=' + ContactNo;
            return apiHttpService.PUT(url);
        }
        function changePassword(data) {
            var url = identityDataConstants.ACCOUNT_URL + 'reset-password';
            return apiHttpService.POST(url, data);
        }

        function getEmployeeLeaveAndEmployeeInfo(pId) {

            var url = dataConstants.CURRENT_STATUS_URL + 'get-employee-and-leaveInfo-from-currentstatus?pId=' + pId;
            return apiHttpService.GET(url);
        }

        function GetUserEmailContactLog() {

            var url = dataConstants.CURRENT_STATUS_URL + 'get-user-email-contact';
            return apiHttpService.GET(url);
        }

        function GetUserEmailContactLogData(data) {
            var url = dataConstants.CURRENT_STATUS_URL + 'get-user-email-contact-log';
            return apiHttpService.POST(url, data);
        }
    }
})();