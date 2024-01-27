(function () {
    'use strict';
    angular.module('app').service('extracurricularService', ['dataConstants', 'apiHttpService', extracurricularService]);

    function extracurricularService(dataConstants, apiHttpService) {
        var service = {
            getExtracurriculars: getExtracurriculars,
            getExtracurricular: getExtracurricular,
            saveExtracurricular: saveExtracurricular,
            updateExtracurricular: updateExtracurricular,
            deleteExtracurricular: deleteExtracurricular
        };

        return service;
        function getExtracurriculars(employeeId) {
            var url = dataConstants.EXTRACURRICULAR_URL + 'get-extracurriculars?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getExtracurricular(employeeId, extracurricularId) {
            var url = dataConstants.EXTRACURRICULAR_URL + 'get-extracurricular?employeeId=' + employeeId + '&extracurricularId=' + extracurricularId;
            return apiHttpService.GET(url);
        }

        function saveExtracurricular(employeeId,data) {
            var url = dataConstants.EXTRACURRICULAR_URL + 'save-extracurricular/' + employeeId;
            return apiHttpService.POST(url, data);
        }

        function updateExtracurricular(extracurricularId, data) {
            var url = dataConstants.EXTRACURRICULAR_URL + 'update-extracurricular/' + extracurricularId;
            return apiHttpService.PUT(url, data);
        }

        function deleteExtracurricular(id) {
            var url = dataConstants.EXTRACURRICULAR_URL + 'delete-extracurricular/' + id;
            return apiHttpService.DELETE(url);
        }

    }
})();