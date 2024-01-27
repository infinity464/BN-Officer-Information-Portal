(function () {
    'use strict';
    angular.module('app').service('enrollmentDetailService', ['dataConstants', 'apiHttpService', enrollmentDetailService]);

    function enrollmentDetailService(dataConstants, apiHttpService) {
        var service = {
            getEnrollmentDetails: getEnrollmentDetails,
            getEnrollmentDetail: getEnrollmentDetail,
            saveEnrollmentDetail: saveEnrollmentDetail,
            updateEnrollmentDetail: updateEnrollmentDetail,
            deleteEnrollmentDetail: deleteEnrollmentDetail,
            getFamilyLogs: getFamilyLogs,
            updateRoleFeatures: updateRoleFeatures
        };

        return service;
        function getEnrollmentDetails(enrollmentId, ps, pn, qs) {
            var url = dataConstants.ENROLLMENT_DETAIL_URL + 'get-enrollment-details?enrollmentId=' + enrollmentId + "&ps=" + ps + "&pn=" + pn+"&qs="+qs;
            return apiHttpService.GET(url);
        }

        function getEnrollmentDetail(enrollmentIdDetailId, enrollmentId) {
            var url = dataConstants.ENROLLMENT_DETAIL_URL + 'get-enrollment-detail?enrollmentDetailId=' + enrollmentIdDetailId + "&enrollmentId=" + enrollmentId;
            return apiHttpService.GET(url);
        }

        function saveEnrollmentDetail(data) {
            var url = dataConstants.ENROLLMENT_DETAIL_URL + 'save-enrollment-detail';
            return apiHttpService.POST(url, data);
        }

        function updateEnrollmentDetail(enrollmentDetailId, data) {
            var url = dataConstants.ENROLLMENT_DETAIL_URL + 'update-enrollment-detail/' + enrollmentDetailId;
            return apiHttpService.PUT(url, data);
        }

        function deleteEnrollmentDetail(enrollmentDetailId) {
            var url = dataConstants.ENROLLMENT_DETAIL_URL + 'delete-enrollment-detail/' + enrollmentDetailId;
            return apiHttpService.DELETE(url);
        }
        function getFamilyLogs(enrollmentDetailId) {
            var url = dataConstants.ENROLLMENT_DETAIL_URL + 'get-family-logs?enrollmentDetailId=' + enrollmentDetailId;
            return apiHttpService.GET(url);
        }
        function updateRoleFeatures(enrollmentDetailId,data) {
            var url = dataConstants.ENROLLMENT_DETAIL_URL + 'update-family-logs/' + enrollmentDetailId;
            return apiHttpService.PUT(url, data);
        }
    }
})();