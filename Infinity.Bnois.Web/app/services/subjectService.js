(function () {
	'use strict';
	angular.module('app').service('subjectService', ['dataConstants', 'apiHttpService', subjectService]);

	function subjectService(dataConstants, apiHttpService) {
		var service = {
			getSubjects: getSubjects,
			getSubject: getSubject,
			saveSubject: saveSubject,
			updateSubject: updateSubject,
			deleteSubject: deleteSubject
		};

		return service;
		function getSubjects(pageSize, pageNumber, searchText) {
			var url = dataConstants.SUBJECT_URL + 'get-subjects?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
			return apiHttpService.GET(url);
		}

		function getSubject(subjectId) {
			var url = dataConstants.SUBJECT_URL + 'get-subject?id=' + subjectId;

			return apiHttpService.GET(url);
		}

		function saveSubject(data) {
			var url = dataConstants.SUBJECT_URL + 'save-subject';
			return apiHttpService.POST(url, data);
		}

		function updateSubject(subjectId, data) {
			var url = dataConstants.SUBJECT_URL + 'update-subject/' + subjectId;
			return apiHttpService.PUT(url, data);
		}

		function deleteSubject(subjectId) {
			var url = dataConstants.SUBJECT_URL + 'delete-subject/' + subjectId;
			return apiHttpService.DELETE(url);
		}


	}
})();