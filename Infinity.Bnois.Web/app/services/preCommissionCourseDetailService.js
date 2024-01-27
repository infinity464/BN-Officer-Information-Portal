(function () {
    'use strict';
    angular.module('app').service('preCommissionCourseDetailService', ['dataConstants', 'apiHttpService', preCommissionCourseDetailService]);

    function preCommissionCourseDetailService(dataConstants, apiHttpService) {
        var service = {
            getPreCommissionCourseDetails: getPreCommissionCourseDetails,
            getPreCommissionCourseDetail: getPreCommissionCourseDetail,
            savePreCommissionCourseDetail: savePreCommissionCourseDetail,
            updatePreCommissionCourseDetail: updatePreCommissionCourseDetail,

        };

        return service;
        function getPreCommissionCourseDetails(preCommissionCourseId) {
            var url = dataConstants.PRE_COMMISSION_COURSE_DETAIL + 'get-pre-commission-course-details?preCommissionCourseId=' + preCommissionCourseId;
            return apiHttpService.GET(url);
        }

        function getPreCommissionCourseDetail(preCommissionCourseId, preCommissionCourseDetailId) {
            var url = dataConstants.PRE_COMMISSION_COURSE_DETAIL + 'get-pre-commission-course-detail?preCommissionCourseId=' + preCommissionCourseId + '&preCommissionCourseDetailId=' + preCommissionCourseDetailId;
            return apiHttpService.GET(url);
        }

        function savePreCommissionCourseDetail(data) {
            var url = dataConstants.PRE_COMMISSION_COURSE_DETAIL + 'save-pre-commission-course-detail';
            return apiHttpService.POST(url, data);
        }

        function updatePreCommissionCourseDetail(preCommissionCourseDetailId, data) {
            var url = dataConstants.PRE_COMMISSION_COURSE_DETAIL + 'update-pre-commission-course-detail/' + preCommissionCourseDetailId;
            return apiHttpService.PUT(url, data);
        }
    }
})();