(function () {
    'use strict';
    angular.module('app').service('downloadfileService', ['dataConstants', 'apiHttpService', downloadfileService]);

    function downloadfileService(dataConstants, apiHttpService) {
        var service = {
            getDownloads: getDownloads,
            getDownloadById: getDownloadById,
            fileUploadUrl: fileUploadUrl,
            SaveDownload: SaveDownload,
            updateDownload: updateDownload,
            getDownloadsForView: getDownloadsForView,
            migrationUploadUrl: migrationUploadUrl,
            dataMigration: dataMigration,
            deleteDownload: deleteDownload
        };

        return service;
        function getDownloads(pageSize, pageNumber, searchText) {
            var url = dataConstants.DOWNLOAD_URL + 'get-downloads?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getDownloadsForView() {
            var url = dataConstants.DOWNLOAD_URL + 'get-downloads-for-view';
            return apiHttpService.GET(url);
        }


        //function saveFeedBack(data) {
        //    var url = dataConstants.DOWNLOAD_URL + 'save-feed-back';
        //    return apiHttpService.POST(url, data);
        //}
        function SaveDownload(data) {
            var url = dataConstants.DOWNLOAD_URL + 'save-download';
            return apiHttpService.POST(url, data);
        }

        function updateDownload(id, data) {
            var url = dataConstants.DOWNLOAD_URL + 'update-download/' + id;
            return apiHttpService.PUT(url, data);
        }
        function deleteDownload(id) {
            var url = dataConstants.DOWNLOAD_URL + 'delete-download/' + id;
            return apiHttpService.DELETE(url);
        }

        //function getDownloadById(Id) {
        //    var url = dataConstants.DOWNLOAD_URL + 'get-file-for-download?id=' + Id;
        //    return apiHttpService.GET(url);
        //}

        function getDownloadById(id) {
            var url = dataConstants.DOWNLOAD_URL + 'get-file-for-download-by-id?id=' + id;
            return apiHttpService.GET(url);
        }

        //function getAchievement(achievementId) {
        //    var url = dataConstants.ACHIEVEMENT_URL + 'get-achievement?id=' + achievementId;
        //    return apiHttpService.GET(url);
        //}


        function fileUploadUrl() {
            var url = dataConstants.DOWNLOAD_URL + 'upload-download-file';
            return url;
        }



        function migrationUploadUrl() {
            var url = dataConstants.DOWNLOAD_URL + 'migration-upload-download-file';
            return url;
        }
        //function getFeedbackForUser() {
        //    var url = dataConstants.DOWNLOAD_URL + 'get-feed-back-for-user';
        //    return apiHttpService.GET(url);
        //}

        function dataMigration(dataSource) {
            var url = dataConstants.DOWNLOAD_URL + 'data-migration-sql-file?dataSource=' + dataSource;
            return apiHttpService.GET(url);
        }
    }
})();