(function () {
    'use strict';
    angular.module('app').service('trainingResultService', ['dataConstants', 'apiHttpService', trainingResultService]);

    function trainingResultService(dataConstants, apiHttpService) {
        var service = {
            getTrainingResults: getTrainingResults,
            getTrainingResult: getTrainingResult,
            getTrainingResultByEmployee: getTrainingResultByEmployee,
            saveTrainingResult: saveTrainingResult,
            updateTrainingResult: updateTrainingResult,
            deleteTrainingResult: deleteTrainingResult,
            getTrainingResultUploadUrl: getTrainingResultUploadUrl,
            trainingResultDownload: trainingResultDownload
        };

        return service;

        function getTrainingResultUploadUrl(trainingResultId, type) {
            var url = dataConstants.TRAINING_RESULT_URL + 'training-result-upload?id=' + trainingResultId + '&type=' + type;
            return url;
        }

        function trainingResultDownload(trainingResultId) {
            var url = dataConstants.TRAINING_RESULT_URL + 'downlaod-training-result?trainingResultId=' + trainingResultId;
            return url;
        }


        function getTrainingResults(pageSize, pageNumber, searchText) {
            var url = dataConstants.TRAINING_RESULT_URL + 'get-training-results?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getTrainingResult(trainingResultId) {
            var url = dataConstants.TRAINING_RESULT_URL + 'get-training-result?id=' + trainingResultId;
            return apiHttpService.GET(url);
        }

        function getTrainingResultByEmployee(employeeId) {
            var url = dataConstants.TRAINING_RESULT_URL + 'get-training-result-by-employee?id=' + employeeId;
            return apiHttpService.GET(url);
        }

        function saveTrainingResult(data) {
            var url = dataConstants.TRAINING_RESULT_URL + 'save-training-result';
            return apiHttpService.POST(url, data);
        }

        function updateTrainingResult(trainingResultId, data) {
            var url = dataConstants.TRAINING_RESULT_URL + 'update-training-result/' + trainingResultId;
            return apiHttpService.PUT(url, data);
        }

        function deleteTrainingResult(trainingResultId) {
            var url = dataConstants.TRAINING_RESULT_URL + 'delete-training-result/' + trainingResultId;
            return apiHttpService.DELETE(url);
        }

    }
})();