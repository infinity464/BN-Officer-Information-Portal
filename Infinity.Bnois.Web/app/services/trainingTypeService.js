(function () {
    'use strict';
    angular.module('app').service('trainingTypeService', ['dataConstants', 'apiHttpService', trainingTypeService]);

    function trainingTypeService(dataConstants, apiHttpService) {
        var service = {
            getTrainingTypes: getTrainingTypes,
            getTrainingType: getTrainingType,
            saveTrainingType: saveTrainingType,
            updateTrainingType: updateTrainingType,
            deleteTrainingType: deleteTrainingType
        };

        return service;
        function getTrainingTypes(pageSize, pageNumber,searchText) {
            var url = dataConstants.TRAINING_TYPE_URL + 'get-training-types?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getTrainingType(trainingTypeId) {
            var url = dataConstants.TRAINING_TYPE_URL + 'get-training-type?trainingTypeId=' + trainingTypeId;
            return apiHttpService.GET(url);
        }

        function saveTrainingType(data) {
            var url = dataConstants.TRAINING_TYPE_URL + 'save-training-type';
            return apiHttpService.POST(url, data);
        }

        function updateTrainingType(trainingTypeId, data) {
            var url = dataConstants.TRAINING_TYPE_URL + 'update-training-type/' + trainingTypeId;
            return apiHttpService.PUT(url, data);
        }

        function deleteTrainingType(trainingTypeId) {
            var url = dataConstants.TRAINING_TYPE_URL + 'delete-training-type/' + trainingTypeId;
            return apiHttpService.DELETE(url);
        }


    }
})();