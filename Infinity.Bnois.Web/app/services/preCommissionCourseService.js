(function () {
    'use strict';
    angular.module('app').service('preCommissionCourseService', ['dataConstants', 'apiHttpService', preCommissionCourseService]);

    function preCommissionCourseService(dataConstants, apiHttpService) {
        var service = {
            getPreCommissionCourses: getPreCommissionCourses,
            getPreCommissionCourse: getPreCommissionCourse,
            savePreCommissionCourse: savePreCommissionCourse,
            updatePreCommissionCourse: updatePreCommissionCourse,
            deletePreCommissionCourse: deletePreCommissionCourse,
            getPunishmentSubCategoriesByPunishmentCategory: getPunishmentSubCategoriesByPunishmentCategory
          
        };

        return service;
        function getPreCommissionCourses(employeeId) {
            var url = dataConstants.PRE_COMMISSION_COURSES + 'get-pre-commission-courses?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getPreCommissionCourse(employeeId, preCommissionCourseId) {
            var url = dataConstants.PRE_COMMISSION_COURSES + 'get-pre-commission-course?employeeId=' + employeeId + '&preCommissionCourseId=' + preCommissionCourseId;
            return apiHttpService.GET(url);
        }

        function savePreCommissionCourse(employeeId,data) {
            var url = dataConstants.PRE_COMMISSION_COURSES + 'save-pre-commission-course/' + employeeId;
            return apiHttpService.POST(url, data);
        }

        function updatePreCommissionCourse(preCommissionCourseId, data) {
            var url = dataConstants.PRE_COMMISSION_COURSES + 'update-pre-commission-course/' + preCommissionCourseId;
            return apiHttpService.PUT(url, data);
        }

        function getPunishmentSubCategoriesByPunishmentCategory(punishmentCategoryId) {
            var url = dataConstants.PRE_COMMISSION_COURSES + 'get-punishment-sub-categories/' + punishmentCategoryId;
            return apiHttpService.GET(url);
        }
      
        function deletePreCommissionCourse(id) {
            var url = dataConstants.PRE_COMMISSION_COURSES + 'delete-pre-commission-course/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();