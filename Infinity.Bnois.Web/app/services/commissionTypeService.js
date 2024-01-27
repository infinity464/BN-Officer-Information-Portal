(function () {
    'use strict';
    angular.module('app').service('commissionTypeService', ['dataConstants', 'apiHttpService', commissionTypeService]);

    function commissionTypeService(dataConstants, apiHttpService) {
        var service = {
            getCommissionTypes: getCommissionTypes,
            getCommissionType: getCommissionType,
            saveCommissionType: saveCommissionType,
            updateCommissionType: updateCommissionType,
            deleteCommissionType: deleteCommissionType
        };

        return service;
        function getCommissionTypes(pageSize, pageNumber, searchText) {
            var url = dataConstants.COMMISSION_TYPE_URL + 'get-commission-types?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getCommissionType(id) {
            var url = dataConstants.COMMISSION_TYPE_URL + 'get-commission-type?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveCommissionType(data) {
            var url = dataConstants.COMMISSION_TYPE_URL + 'save-commission-type';
            return apiHttpService.POST(url, data);
        }

        function updateCommissionType(id, data) {
            var url = dataConstants.COMMISSION_TYPE_URL + 'update-commission-type/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteCommissionType(id) {
            var url = dataConstants.COMMISSION_TYPE_URL + 'delete-commission-type/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();