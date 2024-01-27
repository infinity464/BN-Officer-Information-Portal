(function () {
    'use strict';
    angular.module('app').service('quickLinkService', ['dataConstants', 'apiHttpService', quickLinkService]);

    function quickLinkService(dataConstants, apiHttpService) {
        var service = {
            getQuickLinks: getQuickLinks,
            getDashboardQuickLinks: getDashboardQuickLinks,
            getQuickLink: getQuickLink,
            saveQuickLink: saveQuickLink,
            updateQuickLink: updateQuickLink,
            deleteQuickLink: deleteQuickLink,
            fileUploadUrl: fileUploadUrl
        };

        return service;


        function fileUploadUrl() {
            var url = dataConstants.QUICK_LINK_URL + 'upload-quick-link-file';
            return url;
        }
        function getQuickLinks(pageSize, pageNumber, searchText) {
            var url = dataConstants.QUICK_LINK_URL + 'get-quick-links?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }
        function getDashboardQuickLinks() {
            var url = dataConstants.QUICK_LINK_URL + 'get-dashboard-quick-links';
            return apiHttpService.GET(url);
        }

        function getQuickLink(quickLinkId) {
            var url = dataConstants.QUICK_LINK_URL + 'get-quick-link?id=' + quickLinkId;
            return apiHttpService.GET(url);
        }

 

        function saveQuickLink(data) {
            var url = dataConstants.QUICK_LINK_URL + 'save-quick-link';
            return apiHttpService.POST(url, data);
        }

        function updateQuickLink(quickLinkId, data) {
            var url = dataConstants.QUICK_LINK_URL + 'update-quick-link/' + quickLinkId;
            return apiHttpService.PUT(url, data);
        }

        function deleteQuickLink(quickLinkId) {
            var url = dataConstants.QUICK_LINK_URL + 'delete-quick-link/' + quickLinkId;
            return apiHttpService.DELETE(url);
        }


    }
})();