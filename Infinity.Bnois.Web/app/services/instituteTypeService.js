(function () {
    'use strict';
    angular.module('app').service('instituteTypeService', ['dataConstants', 'apiHttpService', instituteTypeService]);

    function instituteTypeService(dataConstants, apiHttpService) {
        var service = {
            getInstituteTypes: getInstituteTypes,
            getInstituteType: getInstituteType,
            saveInstituteType: saveInstituteType,
            updateInstituteType: updateInstituteType,
            deleteInstituteType: deleteInstituteType
        };

        return service;
        function getInstituteTypes(pageSize, pageNumber, searchText) {
            var url = dataConstants.INSTITUTE_TYPE_URL + 'get-institute-types?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getInstituteType(id) {
            var url = dataConstants.INSTITUTE_TYPE_URL + 'get-institute-type?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveInstituteType(data) {
            var url = dataConstants.INSTITUTE_TYPE_URL + 'save-institute-type';
            return apiHttpService.POST(url, data);
        }

        function updateInstituteType(id, data) {
            var url = dataConstants.INSTITUTE_TYPE_URL + 'update-institute-type/' + id;
            return apiHttpService.PUT(url, data);                
        }                                                        
                                                                 
        function deleteInstituteType(id) {             
            var url = dataConstants.INSTITUTE_TYPE_URL + 'delete-institute-type/' + id;
            return apiHttpService.DELETE(url);
        }


    }
})();