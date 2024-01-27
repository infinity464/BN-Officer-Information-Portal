(function () {
    'use strict';
    angular.module('app').service('medalAwardService', ['dataConstants', 'apiHttpService', medalAwardService]);

    function medalAwardService(dataConstants, apiHttpService) {
        var service = {
            getMedalAwards: getMedalAwards,
            getMedalAward: getMedalAward,
            getPublicationsByCategory: getPublicationsByCategory,
            saveMedalAward: saveMedalAward,
            updateMedalAward: updateMedalAward,
            deleteMedalAward: deleteMedalAward,
            fileUploadUrl: fileUploadUrl
        };

        return service;


        function fileUploadUrl() {
            var url = dataConstants.MEDAL_AWARD_URL + 'upload-medal-award-file';
            return url;
        }
        function getMedalAwards(pageSize, pageNumber, searchText) {
            var url = dataConstants.MEDAL_AWARD_URL + 'get-medal-awards?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getMedalAward(medalAwardId) {
            var url = dataConstants.MEDAL_AWARD_URL + 'get-medal-award?id=' + medalAwardId;
            return apiHttpService.GET(url);
        }

        function getPublicationsByCategory(categoryId) {
            var url = dataConstants.MEDAL_AWARD_URL + 'get-publications-by-categories?id=' + categoryId;
            return apiHttpService.GET(url);
        }


        function saveMedalAward(data) {
            var url = dataConstants.MEDAL_AWARD_URL + 'save-medal-award';
            return apiHttpService.POST(url, data);
        }

        function updateMedalAward(medalAwardId, data) {
            var url = dataConstants.MEDAL_AWARD_URL + 'update-medal-award/' + medalAwardId;
            return apiHttpService.PUT(url, data);
        }

        function deleteMedalAward(medalAwardId) {
            var url = dataConstants.MEDAL_AWARD_URL + 'delete-medal-award/' + medalAwardId;
            return apiHttpService.DELETE(url);
        }


    }
})();