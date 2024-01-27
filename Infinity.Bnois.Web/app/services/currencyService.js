(function () {
    'use strict';
    angular.module('app').service('currencyService', ['dataConstants', 'apiHttpService', currencyService]);

    function currencyService(dataConstants, apiHttpService) {
        var service = {
            getCurrencies: getCurrencies,
            getCurrency: getCurrency,
            saveCurrency: saveCurrency,
            updateCurrency: updateCurrency,
            deleteCurrency: deleteCurrency
        };

        return service;
        function getCurrencies(pageSize, pageNumber,searchText) {
            var url = dataConstants.CURRENCY_URL + 'get-currencies?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getCurrency(currencyId) {
            var url = dataConstants.CURRENCY_URL + 'get-currency?currencyId=' + currencyId;
            return apiHttpService.GET(url);
        }

        function saveCurrency(data) {
            var url = dataConstants.CURRENCY_URL + 'save-currency';
            return apiHttpService.POST(url, data);
        }

        function updateCurrency(currencyId, data) {
            var url = dataConstants.CURRENCY_URL + 'update-currency/' + currencyId;
            return apiHttpService.PUT(url, data);
        }

        function deleteCurrency(CurrencyId) {
            var url = dataConstants.CURRENCY_URL + 'delete-Currency/' + CurrencyId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();