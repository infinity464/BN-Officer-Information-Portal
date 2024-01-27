(function () {
    'use strict';
    angular.module('app').service('mainHeadService', ['dataConstants', 'apiHttpService', mainHeadService]);

    function mainHeadService(dataConstants, apiHttpService) {
        var service = {
            getMainHeads: getMainHeads,
            getMainHead: getMainHead,
            saveMainHead: saveMainHead,
            updateMainHead: updateMainHead,
            deleteMainHead: deleteMainHead
        };

        return service;
        function getMainHeads(accountHeadId, pageSize, pageNumber, searchText) {
            var url = dataConstants.MAIN_HEAD_URL + 'get-main-heads?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText + "&accountHeadId=" + accountHeadId;;
            return apiHttpService.GET(url);
        }

        function getMainHead(accountHeadId, mainHeadId) {
            var url = dataConstants.MAIN_HEAD_URL + 'get-main-head?accountHeadId=' + accountHeadId + "&mainHeadId=" + mainHeadId;
            return apiHttpService.GET(url);
        }

        function saveMainHead(data) {
            var url = dataConstants.MAIN_HEAD_URL + 'save-main-head';
            return apiHttpService.POST(url, data);
        }

        function updateMainHead(mainHeadId, data) {
            var url = dataConstants.MAIN_HEAD_URL + 'update-main-head/' + mainHeadId;
            return apiHttpService.PUT(url, data);
        }

        function deleteMainHead(mainHeadId) {
            var url = dataConstants.MAIN_HEAD_URL + 'delete-main-head/' + mainHeadId;
            return apiHttpService.DELETE(url);
        }


    }
})();