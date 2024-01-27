(function () {
    'use strict';
    angular.module('app').service('colorService', ['dataConstants', 'apiHttpService', colorService]);

    function colorService(dataConstants, apiHttpService) {
        var service = {
            getColors: getColors,
            getColor: getColor,
            saveColor: saveColor,
            updateColor: updateColor,
            deleteColor: deleteColor
        };

        return service;
        function getColors(pageSize, pageNumber, searchText) {
            var url = dataConstants.COLOR_URL + 'get-colors?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getColor(id) {
            var url = dataConstants.COLOR_URL + 'get-color?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveColor(data) {
            var url = dataConstants.COLOR_URL + 'save-color';
            return apiHttpService.POST(url, data);
        }

        function updateColor(id, data) {
            var url = dataConstants.COLOR_URL + 'update-color/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteColor(id) {
            var url = dataConstants.COLOR_URL + 'delete-color/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();