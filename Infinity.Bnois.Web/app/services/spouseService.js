(function () {
    'use strict';
    angular.module('app').service('spouseService', ['dataConstants', 'apiHttpService', spouseService]);

    function spouseService(dataConstants, apiHttpService) {
        var service = {
            getSpouses: getSpouses,
            getSpouse: getSpouse,
            saveSpouse: saveSpouse,
            updateSpouse: updateSpouse,
            deleteSpouse: deleteSpouse,
            imageUploadUrl: imageUploadUrl,
            genFormUploadUrl: genFormUploadUrl
           
        };

        return service;


        function imageUploadUrl(employeeId,spouseId) {
            var url = dataConstants.SPOUSE_URL + 'spouse-image-upload?employeeId=' + employeeId + '&spouseId=' + spouseId;
            return url;
        }
        function genFormUploadUrl(employeeId, spouseId) {
            var url = dataConstants.SPOUSE_URL + 'spouse-gen-form-upload?employeeId=' + employeeId + '&spouseId=' + spouseId;
            return url;
        }
        

        function getSpouses(employeeId) {
            var url = dataConstants.SPOUSE_URL + 'get-spouses?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getSpouse(employeeId, spouseId) {
            var url = dataConstants.SPOUSE_URL + 'get-spouse?employeeId=' + employeeId + '&spouseId=' + spouseId;
            return apiHttpService.GET(url);
        }

        function saveSpouse(employeeId,data) {
            var url = dataConstants.SPOUSE_URL + 'save-spouse/' + employeeId;
            return apiHttpService.POST(url, data);
        }

        function updateSpouse(spouseId, data) {
            var url = dataConstants.SPOUSE_URL + 'update-spouse/' + spouseId;
            return apiHttpService.PUT(url, data);
        }
        function deleteSpouse(id) {
            var url = dataConstants.SPOUSE_URL + 'delete-spouse/' + id;
            return apiHttpService.DELETE(url);
        }
      
    }
})();