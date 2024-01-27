(function () {
    'use strict';
    angular.module('app').service('achievementService', ['dataConstants', 'apiHttpService', achievementService]);

    function achievementService(dataConstants, apiHttpService) {
        var service = {
            getAchievements: getAchievements,
            getAchievement: getAchievement,
            saveAchievement: saveAchievement,
            updateAchievement: updateAchievement,
            deleteAchievement: deleteAchievement,
            fileUploadUrl: fileUploadUrl
        };

        return service;

        function fileUploadUrl() {
            var url = dataConstants.ACHIEVEMENT_URL + 'upload-achievement-file';
            return url;
        }
        function getAchievements(pageSize, pageNumber, searchText) {
            var url = dataConstants.ACHIEVEMENT_URL + 'get-achievements?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getAchievement(achievementId) {
            var url = dataConstants.ACHIEVEMENT_URL + 'get-achievement?id=' + achievementId;
            return apiHttpService.GET(url);
        }

        function saveAchievement(data) {
            var url = dataConstants.ACHIEVEMENT_URL + 'save-achievement';
            return apiHttpService.POST(url, data);
        }

        function updateAchievement(achievementId, data) {
            var url = dataConstants.ACHIEVEMENT_URL + 'update-achievement/' + achievementId;
            return apiHttpService.PUT(url, data);
        }

        function deleteAchievement(achievementId) {
            var url = dataConstants.ACHIEVEMENT_URL + 'delete-achievement/' + achievementId;
            return apiHttpService.DELETE(url);
        }


    }
})();