(function () {
    'use strict';
    angular.module('app').service('punishmentAccidentService', ['dataConstants', 'apiHttpService', punishmentAccidentService]);

    function punishmentAccidentService(dataConstants, apiHttpService) {
        var service = {
            getPunishmentAccidents: getPunishmentAccidents,
            getPunishmentAccident: getPunishmentAccident,
            getPunishmentSubCategoriesByCategory: getPunishmentSubCategoriesByCategory,
            savePunishmentAccident: savePunishmentAccident,
            updatePunishmentAccident: updatePunishmentAccident,
            deletePunishmentAccident: deletePunishmentAccident,
            fileUploadUrl: fileUploadUrl
        };

        return service;

        function fileUploadUrl() {
            var url = dataConstants.ACHIEVEMENT_URL + 'upload-achievement-file';
            return url;
        }
        function getPunishmentAccidents(pageSize, pageNumber, searchText) {
            var url = dataConstants.PUNISHMENT_ACCIDENT_URL + 'get-punishment-accidents?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getPunishmentAccident(punishmentAccidentId) {
            var url = dataConstants.PUNISHMENT_ACCIDENT_URL + 'get-punishment-accident?id=' + punishmentAccidentId;
            return apiHttpService.GET(url);
        }

        function getPunishmentSubCategoriesByCategory(categoryId) {
            var url = dataConstants.PUNISHMENT_ACCIDENT_URL + 'get-punishment-sub-categories-by-category?id=' + categoryId;
            return apiHttpService.GET(url);
        }

        function savePunishmentAccident(data) {
            var url = dataConstants.PUNISHMENT_ACCIDENT_URL + 'save-punishment-accident';
            return apiHttpService.POST(url, data);
        }

        function updatePunishmentAccident(punishmentAccidentId, data) {
            var url = dataConstants.PUNISHMENT_ACCIDENT_URL + 'update-punishment-accident/' + punishmentAccidentId;
            return apiHttpService.PUT(url, data);
        }

        function deletePunishmentAccident(punishmentAccidentId) {
            var url = dataConstants.PUNISHMENT_ACCIDENT_URL + 'delete-punishment-accident/' + punishmentAccidentId;
            return apiHttpService.DELETE(url);
        }


    }
})();