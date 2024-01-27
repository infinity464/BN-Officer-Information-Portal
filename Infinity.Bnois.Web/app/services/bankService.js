(function () {
    'use strict';
    angular.module('app').service('bankService', ['dataConstants', 'apiHttpService', bankService]);

    function bankService(dataConstants, apiHttpService) {
        var service = {
            getBanks: getBanks,
            getBank: getBank,
            saveBank: saveBank,
            updateBank: updateBank,
            deleteBank: deleteBank
        };

        return service;
        function getBanks(pageSize, pageNumber,searchText) {
            var url = dataConstants.BANK_URL + 'get-banks?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getBank(bankId) {
            var url = dataConstants.BANK_URL + 'get-bank?bankId=' + bankId;
            return apiHttpService.GET(url);
        }

        function saveBank(data) {
            var url = dataConstants.BANK_URL + 'save-bank';
            return apiHttpService.POST(url, data);
        }

        function updateBank(bankId, data) {
            var url = dataConstants.BANK_URL + 'update-bank/' + bankId;
            return apiHttpService.PUT(url, data);
        }

        function deleteBank(bankId) {
            var url = dataConstants.BANK_URL + 'delete-bank/' + bankId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();