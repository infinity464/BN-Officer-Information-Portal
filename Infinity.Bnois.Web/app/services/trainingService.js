(function () {
	'use strict';
	angular.module('app').service('trainingService', ['dataConstants', 'apiHttpService', trainingService]);

	function trainingService(dataConstants, apiHttpService) {
		var service = {
			getTrainings: getTrainings,
			getTraining: getTraining,
			saveTraining: saveTraining,
			updateTraining: updateTraining,
            deleteTraining: deleteTraining,
            getLocationsByCountryId: getLocationsByCountryId,
            getTrainingsByTrainingType: getTrainingsByTrainingType
		};

        return service;

        function getTrainings(pageSize, pageNumber, searchText) {
            var url = dataConstants.TRAINING_URL + 'get-trainings?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

		function getTraining(trainingId) {
			var url = dataConstants.TRAINING_URL + 'get-training?trainingId=' + trainingId;
			return apiHttpService.GET(url);
		}

		function saveTraining(data) {
			var url = dataConstants.TRAINING_URL + 'save-training';
			return apiHttpService.POST(url, data);
		}

		function updateTraining(trainingId, data) {
			var url = dataConstants.TRAINING_URL + 'update-training/' + trainingId;
			return apiHttpService.PUT(url, data);
		}

		function deleteTraining(trainingId) {
			var url = dataConstants.TRAINING_URL + 'delete-training/' + trainingId;
			return apiHttpService.DELETE(url);
        }

        function getLocationsByCountryId(countryId) {
            var url = dataConstants.TRAINING_URL + 'get-location-by-countryId/' + countryId;
            return apiHttpService.GET(url);
        }
        function getTrainingsByTrainingType(trainingTypeId) {
            var url = dataConstants.TRAINING_URL + 'get-trainings-by-training-type?trainingTypeId=' + trainingTypeId;
            return apiHttpService.GET(url);
        }

	}
})();