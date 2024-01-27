(function () {
    'use strict';
    angular.module('app').service('basicSearchService', ['dataConstants', 'apiHttpService', basicSearchService]);

    function basicSearchService(dataConstants, apiHttpService) {
        var service = {
            getBasicSearchSelectModels: getBasicSearchSelectModels,
            searchOfficers: searchOfficers,
            saveCheckedValue: saveCheckedValue,
            deleteCheckedColumn: deleteCheckedColumn
            
        };

        return service;
       
        function getBasicSearchSelectModels() {
            var url = dataConstants.BASIC_SEARCH_URL + 'get-basic-search-select-models';
            return apiHttpService.GET(url);
        }


        function searchOfficers(data) {
            var url = dataConstants.BASIC_SEARCH_URL + 'search-officers';
            return apiHttpService.POST(url, data);
        }



        function saveCheckedValue(checked, value) {
            var url = dataConstants.BASIC_SEARCH_URL + 'save-checked-column?check=' + checked + '&value=' + value;
            return apiHttpService.POST(url, {});
        }



        function deleteCheckedColumn() {
            var url = dataConstants.BASIC_SEARCH_URL + 'delete-checked-column';
            return apiHttpService.DELETE(url);
        }

       
    }
})();