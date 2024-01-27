	(function () {
		'use strict';
		angular.module('app').service('examinationService', ['dataConstants', 'apiHttpService', examinationService]);
		function examinationService(dataConstants, apiHttpService) {
			var service = {
				getExaminations: getExaminations,
				getExamination: getExamination,
				saveExamination: saveExamination,
				updateExamination: updateExamination,
				deleteExamination: deleteExamination
			};

			return service;
			function getExaminations(pageSize, pageNumber, searchString) {
				var url = dataConstants.EXAMINATION_URL + 'get-examinations?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchString;
				return apiHttpService.GET(url);
			}

			function getExamination(examinationId) {
				var url = dataConstants.EXAMINATION_URL + 'get-examination?examinationId=' + examinationId;
				return apiHttpService.GET(url);
			}

			function saveExamination(data) {
				var url = dataConstants.EXAMINATION_URL + 'save-examination';
				return apiHttpService.POST(url, data);
			}

			function updateExamination(examinationId, data) {
				var url = dataConstants.EXAMINATION_URL + 'update-examination/' + examinationId;
				return apiHttpService.PUT(url, data);
			}

			function deleteExamination(examinationId) {
				var url = dataConstants.EXAMINATION_URL + 'delete-examination/' + examinationId;
				return apiHttpService.DELETE(url);
			}

		}
	})();