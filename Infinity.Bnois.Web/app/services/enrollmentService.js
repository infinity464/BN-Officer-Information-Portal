(function () {
    'use strict';
    angular.module('app').service('enrollmentService', ['dataConstants', 'apiHttpService', enrollmentService]);

    function enrollmentService(dataConstants, apiHttpService) {
        var service = {
            getEnrollments: getEnrollments,
            getEnrollment: getEnrollment,
            saveEnrollment: saveEnrollment,
            updateEnrollment: updateEnrollment,
            deleteEnrollment: deleteEnrollment
        };

        return service;
        function getEnrollments(pageSize, pageNumber, searchText) {
            var url = dataConstants.ENROLLMENT_URL + 'get-enrollments?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEnrollment(enrollmentId) {
            var url = dataConstants.ENROLLMENT_URL + 'get-enrollment?enrollmentId=' + enrollmentId;
     
            return apiHttpService.GET(url);
        }

        function saveEnrollment(data) {
            var url = dataConstants.ENROLLMENT_URL + 'save-enrollment';
            return apiHttpService.POST(url, data);
        }

        function updateEnrollment(enrollmentId, data) {
            var url = dataConstants.ENROLLMENT_URL + 'update-enrollment/' + enrollmentId;
            return apiHttpService.PUT(url, data);
        }

        function deleteEnrollment(enrollmentId) {
            var url = dataConstants.ENROLLMENT_URL + 'delete-enrollment/' + enrollmentId;
            return apiHttpService.DELETE(url);
        }


    }
})();