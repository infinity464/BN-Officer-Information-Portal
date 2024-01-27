(function () {
    'use strict';
    angular.module('app').service('flashNoticeService', ['dataConstants', 'apiHttpService', flashNoticeService]);

    function flashNoticeService(dataConstants, apiHttpService) {
        var service = {
            getFlashNotices: getFlashNotices,
            GetFlashNoticeById: GetFlashNoticeById,
            SaveFlashNotice: SaveFlashNotice,
            UpdateFlashNotice: UpdateFlashNotice,
            DeleteFlashNotice: DeleteFlashNotice,
            getFlashNoticeForMemberUser: getFlashNoticeForMemberUser
        };

        return service;
        function getFlashNotices() {
            var url = dataConstants.FLASH_NOTICE_URL + 'get-flash-notice';
            return apiHttpService.GET(url);
        }

        function GetFlashNoticeById(id) {
            var url = dataConstants.FLASH_NOTICE_URL + 'get-flash-notice-by-id?id=' + id;
            return apiHttpService.GET(url);
        }

        function SaveFlashNotice(data) {
            var url = dataConstants.FLASH_NOTICE_URL + 'save-flash-notice';
            return apiHttpService.POST(url, data);
        }

        function UpdateFlashNotice(id, data) {
            var url = dataConstants.FLASH_NOTICE_URL + 'update-flash-notice/' + id;
            return apiHttpService.PUT(url, data);
        }

        function DeleteFlashNotice(id) {
            var url = dataConstants.FLASH_NOTICE_URL + 'delete-flash-notice?id=' + id;
            return apiHttpService.DELETE(url);
        }

        function getFlashNoticeForMemberUser() {
            var url = dataConstants.FLASH_NOTICE_URL + 'get-flash-notice-memberuser';
            return apiHttpService.GET(url);
        }
    }
})();