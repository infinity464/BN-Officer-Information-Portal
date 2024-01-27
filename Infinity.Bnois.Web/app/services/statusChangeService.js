(function () {
    'use strict';
    angular.module('app').service('statusChangeService', ['dataConstants', 'apiHttpService', statusChangeService]);

    function statusChangeService(dataConstants, apiHttpService) {
        var service = {
            getStatusChanges: getStatusChanges,
            getStatusChange: getStatusChange,
            getStatusChangeSelectModels: getStatusChangeSelectModels,
            saveStatusChange: saveStatusChange,
            updateStatusChange: updateStatusChange,
            deleteStatusChange: deleteStatusChange
            
        };

        return service;

  

        function getStatusChanges(pageSize, pageNumber, searchText) {
            var url = dataConstants.STATUS_CHANGE_URL + 'get-status-changes?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getStatusChange(statusChangeId) {
            var url = dataConstants.STATUS_CHANGE_URL + 'get-status-change?id=' + statusChangeId;
            return apiHttpService.GET(url);
        }

        function getStatusChangeSelectModels(type,employeeId) {
            var url = dataConstants.STATUS_CHANGE_URL + 'get-status-change-select-models?type=' + type +'&employeeId='+employeeId;
            return apiHttpService.GET(url);
        }


        function saveStatusChange(data) {
            var url = dataConstants.STATUS_CHANGE_URL + 'save-status-change';
            return apiHttpService.POST(url, data);
        }



        function updateStatusChange(statusChangeId, data) {
            var url = dataConstants.STATUS_CHANGE_URL + 'update-status-change/' + statusChangeId;
            return apiHttpService.PUT(url, data);
        }


        function deleteStatusChange(statusChangeId) {
            var url = dataConstants.STATUS_CHANGE_URL + 'delete-status-change/' + statusChangeId;
            return apiHttpService.DELETE(url);
        }

    }
})();