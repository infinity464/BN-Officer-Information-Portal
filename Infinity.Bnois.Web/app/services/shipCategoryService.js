(function () {
    'use strict';
    angular.module('app').service('shipCategoryService', ['dataConstants', 'apiHttpService', shipCategoryService]);

    function shipCategoryService(dataConstants, apiHttpService) {
        var service = {
            getShipCategories: getShipCategories,
            getShipCategory: getShipCategory,
            saveShipCategory: saveShipCategory,
            updateShipCategory: updateShipCategory,
            deleteShipCategory: deleteShipCategory
        };

        return service;
        function getShipCategories(pageSize, pageNumber, searchText) {
            var url = dataConstants.SHIP_CATEGORY_URL + 'get-ship-categories?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getShipCategory(shipCategoryId) {
            var url = dataConstants.SHIP_CATEGORY_URL + 'get-ship-category?id=' + shipCategoryId;
            return apiHttpService.GET(url);
        }

        function saveShipCategory(data) {
            var url = dataConstants.SHIP_CATEGORY_URL + 'save-ship-category';
            return apiHttpService.POST(url, data);
        }

        function updateShipCategory(shipCategoryId, data) {
            var url = dataConstants.SHIP_CATEGORY_URL + 'update-ship-category/' + shipCategoryId;
            return apiHttpService.PUT(url, data);
        }

        function deleteShipCategory(shipCategoryId) {
            var url = dataConstants.SHIP_CATEGORY_URL + 'delete-ship-category/' + shipCategoryId;
            return apiHttpService.DELETE(url);
        }


    }
})();