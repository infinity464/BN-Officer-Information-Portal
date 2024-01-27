(function () {
    'use strict';
    angular.module('app').service('accountHeadService', ['dataConstants', 'apiHttpService', accountHeadService]);

    function accountHeadService(dataConstants, apiHttpService) {
        var service = {
            getAccountHeads: getAccountHeads,
            getAccountHead: getAccountHead,
            saveAccountHead: saveAccountHead,
            updateAccountHead: updateAccountHead,
            deleteAccountHead: deleteAccountHead
        };

        return service;
        function getAccountHeads(pageSize, pageNumber, searchText) {
            var url = dataConstants.ACCOUNT_HEAD_URL + 'get-account-heads?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getAccountHead(accountHeadId) {
            var url = dataConstants.ACCOUNT_HEAD_URL + 'get-account-head?accountHeadId=' + accountHeadId;

            return apiHttpService.GET(url);
        }

        function saveAccountHead(data) {
            var url = dataConstants.ACCOUNT_HEAD_URL + 'save-account-head';
            return apiHttpService.POST(url, data);
        }

        function updateAccountHead(accountHeadId, data) {
            var url = dataConstants.ACCOUNT_HEAD_URL + 'update-account-head/' + accountHeadId;
            return apiHttpService.PUT(url, data);
        }

        function deleteAccountHead(accountHeadId) {
            var url = dataConstants.ACCOUNT_HEAD_URL + 'delete-account-head/' + accountHeadId;
            return apiHttpService.DELETE(url);
        }


    }
})();