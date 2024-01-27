(function () {
    'use strict';
    angular.module('app').service('resultGradeService', ['dataConstants', 'apiHttpService', resultGradeService]);

    function resultGradeService(dataConstants, apiHttpService) {
        var service = {
            getResultGrades: getResultGrades,
            getResultGrade: getResultGrade,
            saveResultGrade: saveResultGrade,
            updateResultGrade: updateResultGrade,
            deleteResultGrade: deleteResultGrade
        };

        return service;
        function getResultGrades(pageSize, pageNumber, searchText) {
            var url = dataConstants.RESULT_GRADE_URL + 'get-result-grades?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getResultGrade(id) {
            var url = dataConstants.RESULT_GRADE_URL + 'get-result-grade?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveResultGrade(data) {
            var url = dataConstants.RESULT_GRADE_URL + 'save-result-grade';
            return apiHttpService.POST(url, data);
        }

        function updateResultGrade(id, data) {
            var url = dataConstants.RESULT_GRADE_URL + 'update-result-grade/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteResultGrade(id) {
            var url = dataConstants.RESULT_GRADE_URL + 'delete-result-grade/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();