(function () {
    'use strict';
    angular.module('app').service('heirTypeService', ['dataConstants', 'apiHttpService', heirTypeService]);

    function heirTypeService(dataConstants, apiHttpService) {
        var service = {
            getHeirTypes: getHeirTypes,
            getHeirType: getHeirType,
            saveHeirType: saveHeirType,
            updateHeirType: updateHeirType,
            deleteHeirType: deleteHeirType
        };

        return service;
        function getHeirTypes(pageSize, pageNumber, searchText) {
            var url = dataConstants.HEIR_TYPE_URL + 'get-heir-types?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getHeirType(heirTypeId) {
            var url = dataConstants.HEIR_TYPE_URL + 'get-heir-type?id=' + heirTypeId;
            return apiHttpService.GET(url);
        }

        function saveHeirType(data) {
            var url = dataConstants.HEIR_TYPE_URL + 'save-heir-type';
            return apiHttpService.POST(url, data);
        }

        function updateHeirType(heirTypeId, data) {
            var url = dataConstants.HEIR_TYPE_URL + 'update-heir-type/' + heirTypeId;
            return apiHttpService.PUT(url, data);
        }

        function deleteHeirType(heirTypeId) {
            var url = dataConstants.HEIR_TYPE_URL + 'delete-heir-type/' + heirTypeId;
            return apiHttpService.DELETE(url);
        }


    }
})();