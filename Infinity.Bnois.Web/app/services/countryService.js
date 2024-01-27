(function () {
    'use strict';
    angular.module('app').service('countryService', ['dataConstants', 'apiHttpService', countryService]);

    function countryService(dataConstants, apiHttpService) {
        var service = {
            getCountries: getCountries,
            getCountry: getCountry,
            saveCountry: saveCountry,
            updateCountry: updateCountry,
            deleteCountry: deleteCountry
        };

        return service;
        function getCountries(pageSize, pageNumber, searchText) {
            var url = dataConstants.COUNTRY_URL + 'get-countries?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
        
            return apiHttpService.GET(url);
        }

        function getCountry(id) {
            var url = dataConstants.COUNTRY_URL + 'get-country?id=' + id;
       
            return apiHttpService.GET(url);
        }

        function saveCountry(data) {
            var url = dataConstants.COUNTRY_URL + 'save-country';
            return apiHttpService.POST(url, data);
        }

        function updateCountry(id, data) {
            var url = dataConstants.COUNTRY_URL + 'update-country/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteCountry(id) {
            var url = dataConstants.COUNTRY_URL + 'delete-country/' + id;
            return apiHttpService.DELETE(url);
        }


    }
})();