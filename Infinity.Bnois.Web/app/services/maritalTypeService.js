(function () {
    'use strict';
    angular.module('app').service('maritalTypeService', ['dataConstants', 'apiHttpService', maritalTypeService]);

    function maritalTypeService(dataConstants, apiHttpService) {
        var service = {
            getMaritalTypes: getMaritalTypes,
            getMaritalType: getMaritalType,
            saveMaritalType: saveMaritalType,
            updateMaritalType: updateMaritalType,
            deleteMaritalType: deleteMaritalType
        };

        return service;
        function getMaritalTypes(pageSize, pageNumber, searchString) {
            var url = dataConstants.MARITAL_TYPE_URL + 'get-marital-types?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchString;
            return apiHttpService.GET(url);
        }

        function getMaritalType(id) {
            var url = dataConstants.MARITAL_TYPE_URL + 'get-marital-type?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveMaritalType(data) {
            var url = dataConstants.MARITAL_TYPE_URL + 'save-marital-type';
            return apiHttpService.POST(url, data);
        }

        function updateMaritalType(id, data) {
            var url = dataConstants.MARITAL_TYPE_URL + 'update-marital-type/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteMaritalType(id) {
            var url = dataConstants.MARITAL_TYPE_URL + 'delete-marital-type/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();