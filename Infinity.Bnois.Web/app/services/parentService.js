(function () {
    'use strict';
    angular.module('app').service('parentService', ['dataConstants', 'apiHttpService', parentService]);

    function parentService(dataConstants, apiHttpService) {
        var service = {
            getParent: getParent,
            updateParent: updateParent,
            imageUploadUrl: imageUploadUrl
        };

        return service;


        function imageUploadUrl(employeeId, relationType) {
            var url = dataConstants.PARENT_URL + 'upload-parent-image?employeeId=' + employeeId + '&relationType=' + relationType;
            return url;
        }
        function getParent(employeeId,relationType) {
            var url = dataConstants.PARENT_URL + 'get-employee-parents?employeeId=' + employeeId + '&relationType=' + relationType;
            return apiHttpService.GET(url);
        }
        
        function updateParent(employeeId, data) {
            var url = dataConstants.PARENT_URL + 'update-employee-parents/' + employeeId;
            return apiHttpService.PUT(url, data);
        }
    }
})();