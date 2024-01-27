(function () {
    'use strict';
    angular.module('app').service('eyeVisionService', ['dataConstants', 'apiHttpService', eyeVisionService]);

    function eyeVisionService(dataConstants, apiHttpService) {
        var service = {
            getEyeVisions: getEyeVisions,
            getEyeVision: getEyeVision,
            saveEyeVision: saveEyeVision,
            updateEyeVision: updateEyeVision,
            deleteEyeVision: deleteEyeVision
        };

        return service;
        function getEyeVisions(pageSize, pageNumber, searchText) {
            var url = dataConstants.EYE_VISION_URL + 'get-eye-visions?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEyeVision(eyeVisionId) {
            var url = dataConstants.EYE_VISION_URL + 'get-eye-vision?EyeVisionId=' + eyeVisionId;
            return apiHttpService.GET(url);
        }

        function saveEyeVision(data) {
            var url = dataConstants.EYE_VISION_URL + 'save-eye-vision';
            return apiHttpService.POST(url, data);
        }

        function updateEyeVision(eyeVisionId, data) {
            var url = dataConstants.EYE_VISION_URL + 'update-eye-vision/' + eyeVisionId;
            return apiHttpService.PUT(url, data);
        }

        function deleteEyeVision(eyeVisionId) {
            var url = dataConstants.EYE_VISION_URL + 'delete-eye-vision/' + eyeVisionId;
            return apiHttpService.DELETE(url);
        }


    }
})();