(function () {
    'use strict';
    angular.module('app').service('extracurricularTypeService', ['dataConstants', 'apiHttpService', extracurricularTypeService]);

    function extracurricularTypeService(dataConstants, apiHttpService) {
        var service = {
            getExtracurricularTypes: getExtracurricularTypes,
            getExtracurricularType: getExtracurricularType,
            saveExtracurricularType: saveExtracurricularType,
            updateExtracurricularType: updateExtracurricularType,
            deleteExtracurricularType: deleteExtracurricularType
        };

        return service;
        function getExtracurricularTypes(pageSize, pageNumber, searchString) {
            var url = dataConstants.EXTRACURRICULAR_TYPE_URL + 'get-extracurricular-types?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchString;
            return apiHttpService.GET(url);
        }

        function getExtracurricularType(id) {
            var url = dataConstants.EXTRACURRICULAR_TYPE_URL + 'get-extracurricular-type?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveExtracurricularType(data) {
            var url = dataConstants.EXTRACURRICULAR_TYPE_URL + 'save-extracurricular-type';
            return apiHttpService.POST(url, data);
        }

        function updateExtracurricularType(id, data) {
            var url = dataConstants.EXTRACURRICULAR_TYPE_URL + 'update-extracurricular-type/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteExtracurricularType(id) {
            var url = dataConstants.EXTRACURRICULAR_TYPE_URL + 'delete-extracurricular-type/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();