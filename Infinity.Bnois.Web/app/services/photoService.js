(function () {
    'use strict';
    angular.module('app').service('photoService', ['dataConstants', 'apiHttpService', photoService]);

    function photoService(dataConstants, apiHttpService) {
        var service = {
            getPhoto: getPhoto,
            //updatePhoto: updatePhoto,
            uplaodPhotoUrl: uplaodPhotoUrl
        };

        return service;

        function getPhoto(employeeId, type) {
            var url = dataConstants.PHOTO_URL + 'get-photo?employeeId=' + employeeId + '&type=' + type;
            return apiHttpService.GET(url);
        }

        //function updatePhoto(employeeId, data) {
        //    alert();
        //    var url = dataConstants.PHOTO_URL + 'update-photo/' + employeeId;
        //    return apiHttpService.POST(url, data);
        //}


        function uplaodPhotoUrl(employeeId, photoType) {
            var url = dataConstants.PHOTO_URL + 'upload-photo?employeeId=' + employeeId + "&photoType=" + photoType;
            return url;
        }

        //function uplaodPictureUrl() {
        //    var url = dataConstants.Photo_URL + 'upload-picture';
        //    return url;
        //}
    }
})();