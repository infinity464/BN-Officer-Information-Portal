(function () {
    'use strict';
    angular.module('app').service('uploadFileService', ['dataConstants', 'apiHttpService', uploadFileService]);

    function uploadFileService(dataConstants, apiHttpService) {
        var service = {
            uplaodImageUrl: uplaodImageUrl
        };
        return service;
        function uplaodImageUrl() {
            var url = dataConstants.UPLOAD_FILE_URL + 'upload-image';
            return url;
        }

    }
})();