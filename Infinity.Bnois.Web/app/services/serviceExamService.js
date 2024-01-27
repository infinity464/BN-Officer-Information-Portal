(function () {
    'use strict';
    angular.module('app').service('serviceExamService', ['dataConstants', 'apiHttpService', serviceExamService]);

    function serviceExamService(dataConstants, apiHttpService) {
        var service = {
            getServiceExams: getServiceExams,
            getServiceExam: getServiceExam,
            getServiceExamByServiceExamCategory: getServiceExamByServiceExamCategory,
            saveServiceExam: saveServiceExam,
            updateServiceExam: updateServiceExam,
            deleteServiceExam: deleteServiceExam
        };

        return service;
        function getServiceExams(pageSize, pageNumber, searchText) {
            var url = dataConstants.SERVICE_EXAM_URL + 'get-service-exams?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getServiceExam(id) {
            var url = dataConstants.SERVICE_EXAM_URL + 'get-service-exam?id=' + id;
            return apiHttpService.GET(url);
        }

        function getServiceExamByServiceExamCategory(id) {
            var url = dataConstants.SERVICE_EXAM_URL + 'get-service-exam-by-service-exam-category?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveServiceExam(data) {
            var url = dataConstants.SERVICE_EXAM_URL + 'save-service-exam';
            return apiHttpService.POST(url, data);
        }

        function updateServiceExam(id, data) {
            var url = dataConstants.SERVICE_EXAM_URL + 'update-service-exam/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteServiceExam(id) {
            var url = dataConstants.SERVICE_EXAM_URL + 'delete-service-exam/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();