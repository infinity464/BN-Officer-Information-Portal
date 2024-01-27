(function () {
    'use strict';
    angular.module('app').service('awardService', ['dataConstants', 'apiHttpService', awardService]);

    function awardService(dataConstants, apiHttpService) {
        var service = {
            getAwards: getAwards,
            getAward: getAward,
            saveAward: saveAward,
            updateAward: updateAward,
            deleteAward: deleteAward
        };

        return service;
        function getAwards(pageSize, pageNumber, searchText) {
            var url = dataConstants.AWARD_URL + 'get-awards?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getAward(id) {
            var url = dataConstants.AWARD_URL + 'get-award?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveAward(data) {
            var url = dataConstants.AWARD_URL + 'save-award';
            return apiHttpService.POST(url, data);
        }

        function updateAward(id, data) {
            var url = dataConstants.AWARD_URL + 'update-award/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteAward(id) {
            var url = dataConstants.AWARD_URL + 'delete-award/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();