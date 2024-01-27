(function () {
    'use strict';
    angular.module('app').service('relationService', ['dataConstants', 'apiHttpService', relationService]);

    function relationService(dataConstants, apiHttpService) {
        var service = {
            getRelations: getRelations,
            getRelation: getRelation,
            saveRelation: saveRelation,
            updateRelation: updateRelation,
            deleteRelation: deleteRelation
        };

        return service;
        function getRelations(pageSize, pageNumber, searchText) {
            var url = dataConstants.RELATION_URL + 'get-relations?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getRelation(relationId) {
            var url = dataConstants.RELATION_URL + 'get-relation?id=' + relationId;
            return apiHttpService.GET(url);
        }

        function saveRelation(data) {
            var url = dataConstants.RELATION_URL + 'save-relation';
            return apiHttpService.POST(url, data);
        }

        function updateRelation(relationId, data) {
            var url = dataConstants.RELATION_URL + 'update-relation/' + relationId;
            return apiHttpService.PUT(url, data);
        }

        function deleteRelation(relationId) {
            var url = dataConstants.RELATION_URL + 'delete-relation/' + relationId;
            return apiHttpService.DELETE(url);
        }


    }
})();