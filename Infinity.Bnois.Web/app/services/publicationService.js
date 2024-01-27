(function () {
    'use strict';
    angular.module('app').service('publicationService', ['dataConstants', 'apiHttpService', publicationService]);

    function publicationService(dataConstants, apiHttpService) {
        var service = {
            getPublications: getPublications,
            getPublication: getPublication,
            savePublication: savePublication,
            updatePublication: updatePublication,
            deletePublication: deletePublication
        };

        return service;
        function getPublications(pageSize, pageNumber, searchText) {
            var url = dataConstants.PUBLICATION_URL + 'get-publications?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getPublication(id) {
            var url = dataConstants.PUBLICATION_URL + 'get-publication?id=' + id;
            return apiHttpService.GET(url);
        }

        function savePublication(data) {
            var url = dataConstants.PUBLICATION_URL + 'save-publication';
            return apiHttpService.POST(url, data);
        }

        function updatePublication(id, data) {
            var url = dataConstants.PUBLICATION_URL + 'update-publication/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deletePublication(id) {
            var url = dataConstants.PUBLICATION_URL + 'delete-publication/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();