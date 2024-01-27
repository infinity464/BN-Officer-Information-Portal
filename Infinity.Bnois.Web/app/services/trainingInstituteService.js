(function () {
    'use strict';
    angular.module('app').service('trainingInstituteService', ['dataConstants', 'apiHttpService', trainingInstituteService]);

    function trainingInstituteService(dataConstants, apiHttpService) {
        var service = {
            getTrainingInstitutes: getTrainingInstitutes,
            getTrainingInstitute: getTrainingInstitute,
            getTrainingInstituteSelectModel: getTrainingInstituteSelectModel,
            saveTrainingInstitute: saveTrainingInstitute,
            updateTrainingInstitute: updateTrainingInstitute,
            deleteTrainingInstitute: deleteTrainingInstitute
        };

        return service;
        function getTrainingInstitutes(pageSize, pageNumber, searchText) {
            var url = dataConstants.TRAINING_INSTITUTE_URL + 'get-training-institutes?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getTrainingInstitute(id) {
            var url = dataConstants.TRAINING_INSTITUTE_URL + 'get-training-institute?id=' + id;
            return apiHttpService.GET(url);
        }

        function getTrainingInstituteSelectModel(id) {
            var url = dataConstants.TRAINING_INSTITUTE_URL + 'get-training-institute-select-model?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveTrainingInstitute(data) {
            var url = dataConstants.TRAINING_INSTITUTE_URL + 'save-training-institute';
            return apiHttpService.POST(url, data);
        }

        function updateTrainingInstitute(id, data) {
            var url = dataConstants.TRAINING_INSTITUTE_URL + 'update-training-institute/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteTrainingInstitute(id) {
            var url = dataConstants.TRAINING_INSTITUTE_URL + 'delete-training-institute/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();