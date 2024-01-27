(function () {
    'use strict';
    angular.module('app').service('oprEntryService', ['dataConstants', 'apiHttpService', oprEntryService]);

    function oprEntryService(dataConstants, apiHttpService) {
        var service = {
            getoprEntries: getoprEntries,
            getoprEntry: getoprEntry, 
            saveoprEntry: saveoprEntry,
            updateoprEntry: updateoprEntry,
            deleteoprEntry: deleteoprEntry,
            getOprFileUpload: getOprFileUpload,
            uplaodOprSectionUrl: uplaodOprSectionUrl,
            getOprFileDownloadUrl: getOprFileDownloadUrl
        };

        return service;
        function getoprEntries(pageSize, pageNumber,searchText) {
            var url = dataConstants.OPR_ENTRY_URL + 'get-opr-entries?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getoprEntry(oprEntryId) {
            var url = dataConstants.OPR_ENTRY_URL + 'get-opr-entry?id=' + oprEntryId;
            return apiHttpService.GET(url);
        }


        function saveoprEntry(data) {
            var url = dataConstants.OPR_ENTRY_URL + 'save-opr-entry';
            return apiHttpService.POST(url, data);
        }

        function updateoprEntry(oprEntryId, data) {
            var url = dataConstants.OPR_ENTRY_URL + 'update-opr-entry/' + oprEntryId;
            return apiHttpService.PUT(url, data);
        }

        function deleteoprEntry(oprEntryId) {
            var url = dataConstants.OPR_ENTRY_URL + 'delete-opr-entry/' + oprEntryId;
            return apiHttpService.DELETE(url);
        }
        function getOprFileUpload(oprEntryId) {
            var url = dataConstants.OPR_ENTRY_URL + 'get-opr-file-upload?id=' + oprEntryId;
            return apiHttpService.GET(url);
        }

        function uplaodOprSectionUrl(id, oprSection) {
            var url = dataConstants.OPR_ENTRY_URL + 'upload-opr-section?id=' + id + "&oprSection=" + oprSection;
            return url;
        }

        function getOprFileDownloadUrl(oprEntryId) {
            var url = dataConstants.OPR_ENTRY_URL + 'downlaod-opr-file?id=' + oprEntryId;
            return  url;
        }

    }
})();