(function () {
    'use strict';
    angular.module('app').service('fiscalYearService', ['dataConstants', 'apiHttpService', fiscalYearService]);

    function fiscalYearService(dataConstants, apiHttpService) {
        var service = {
            getFiscalYears: getFiscalYears,
            getFiscalYear: getFiscalYear,
            saveFiscalYear: saveFiscalYear,
            updateFiscalYear: updateFiscalYear,
            deleteFiscalYear: deleteFiscalYear
        };

        return service;
        function getFiscalYears(pageSize, pageNumber,searchText) {
            var url = dataConstants.FISCAL_YEAR_URL + 'get-fiscal-years?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getFiscalYear(fiscalYearId) {
            var url = dataConstants.FISCAL_YEAR_URL + 'get-fiscal-year?fiscalYearId=' + fiscalYearId;
            return apiHttpService.GET(url);
        }

        function saveFiscalYear(data) {
            var url = dataConstants.FISCAL_YEAR_URL + 'save-fiscal-year';
            return apiHttpService.POST(url, data);
        }

        function updateFiscalYear(fiscalYearId, data) {
            var url = dataConstants.FISCAL_YEAR_URL + 'update-fiscal-year/' + fiscalYearId;
            return apiHttpService.PUT(url, data);
        }

        function deleteFiscalYear(fiscalYearId) {
            var url = dataConstants.FISCAL_YEAR_URL + 'delete-fiscal-year/' + fiscalYearId;
            return apiHttpService.DELETE(url);
        }
    }
})();