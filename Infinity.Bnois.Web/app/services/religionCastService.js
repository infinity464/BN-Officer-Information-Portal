(function () {
    'use strict';
    angular.module('app').service('religionCastService', ['dataConstants', 'apiHttpService', religionCastService]);

    function religionCastService(dataConstants, apiHttpService) {
        var service = {
            getReligionCasts: getReligionCasts,
            getReligionCast: getReligionCast,
            saveReligionCast: saveReligionCast,
            updateReligionCast: updateReligionCast,
            deleteReligionCast: deleteReligionCast,
            getReligionCastByReligion: getReligionCastByReligion
        };

        return service;
        function getReligionCasts(pageSize, pageNumber, searchText) {
            var url = dataConstants.RELIGION_CAST_URL + 'get-religion-casts?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getReligionCast(id) {
            var url = dataConstants.RELIGION_CAST_URL + 'get-religion-cast?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveReligionCast(data) {
            var url = dataConstants.RELIGION_CAST_URL + 'save-religion-cast';
            return apiHttpService.POST(url, data);
        }

        function updateReligionCast(id, data) {
            var url = dataConstants.RELIGION_CAST_URL + 'update-religion-cast/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteReligionCast(id) {
            var url = dataConstants.RELIGION_CAST_URL + 'delete-religion-cast/' + id;
            return apiHttpService.DELETE(url);
        }


        function getReligionCastByReligion(religionId) {
            var url = dataConstants.RELIGION_CAST_URL + 'get-religion-cast-by-religion?religionId=' + religionId;
            return apiHttpService.GET(url);
        }
    }
})();