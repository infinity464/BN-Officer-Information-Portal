(function () {
    'use strict';
    angular.module('app').service('examSubjectService', ['dataConstants', 'apiHttpService', examSubjectService]);

    function examSubjectService(dataConstants, apiHttpService) {
        var service = {
            getExamSubjects: getExamSubjects,
            getExamSubject: getExamSubject,
            saveExamSubject: saveExamSubject,
            updateExamSubject: updateExamSubject,
            deleteExamSubject: deleteExamSubject,
            getExaminationsByExamCategory: getExaminationsByExamCategory
        };

        return service;
        function getExamSubjects(pageSize, pageNumber, searchText) {
            var url = dataConstants.EXAM_SUBJECT_URL + 'get-exam-subjects?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getExamSubject(examSubjectId) {
            var url = dataConstants.EXAM_SUBJECT_URL + 'get-exam-subject?examSubjectId=' + examSubjectId;
            return apiHttpService.GET(url);
        }

        function saveExamSubject(data) {
            var url = dataConstants.EXAM_SUBJECT_URL + 'save-exam-subject';
            return apiHttpService.POST(url, data);
        }

        function updateExamSubject(examSubjectId, data) {
            var url = dataConstants.EXAM_SUBJECT_URL + 'update-exam-subject/' + examSubjectId;
            return apiHttpService.PUT(url, data);
        }

        function deleteExamSubject(examSubjectId) {
            var url = dataConstants.EXAM_SUBJECT_URL + 'delete-exam-subject/' + examSubjectId;
            return apiHttpService.DELETE(url);
        }
        function getExaminationsByExamCategory(examcategoryId) {
            var url = dataConstants.EXAM_SUBJECT_URL + 'get-examination-select-models/' + examcategoryId;
            return apiHttpService.GET(url);
        }

    }
})();