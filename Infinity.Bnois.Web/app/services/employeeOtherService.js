(function () {
    'use strict';
    angular.module('app').service('employeeOtherService', ['dataConstants', 'apiHttpService', employeeOtherService]);

    function employeeOtherService(dataConstants, apiHttpService) {
        var service = {
            getEmployeeOthers: getEmployeeOthers,
            getEmployeeOther: getEmployeeOther,
            updateEmployeeOther: updateEmployeeOther,
            imageUploadUrl: imageUploadUrl 
        };

        return service;

        

        function imageUploadUrl(employeeId, imageType) {
            var url = dataConstants.EMPLOYEEG_OTHER_URL + 'upload-employee-other-image?employeeId=' + employeeId + '&imageType=' + imageType;
            return url;
        }

        function getEmployeeOthers(employeeId) {
            var url = dataConstants.EMPLOYEEG_OTHER_URL + 'get-employee-others?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }
        function getEmployeeOther(employeeId) {
            var url = dataConstants.EMPLOYEEG_OTHER_URL + 'get-employee-other?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function updateEmployeeOther(employeeId, data) {
            var url = dataConstants.EMPLOYEEG_OTHER_URL + 'update-employee-other/' + employeeId;
            return apiHttpService.PUT(url, data);
        }
    }
})();