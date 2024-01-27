(function () {
    'use strict';
    angular.module('app').service('empRejoinService', ['dataConstants', 'apiHttpService', empRejoinService]);

    function empRejoinService(dataConstants, apiHttpService) {
        var service = {
            getEmpRejoins: getEmpRejoins,
            getEmpRejoin: getEmpRejoin,
            saveEmpRejoin: saveEmpRejoin,
            updateEmpRejoin: updateEmpRejoin,
            deleteEmpRejoin: deleteEmpRejoin
        };

        return service;
        function getEmpRejoins(pageSize, pageNumber, searchText) {
            var url = dataConstants.EMPLOYEE_REJOIN_URL + 'get-employee-rejoins?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getEmpRejoin(id) {
            var url = dataConstants.EMPLOYEE_REJOIN_URL + 'get-employee-rejoin?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveEmpRejoin(data) {
            var url = dataConstants.EMPLOYEE_REJOIN_URL + 'save-employee-rejoin';
            return apiHttpService.POST(url, data);
        }

        function updateEmpRejoin(id, data) {
            var url = dataConstants.EMPLOYEE_REJOIN_URL + 'update-employee-rejoin/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteEmpRejoin(id) {
            var url = dataConstants.EMPLOYEE_REJOIN_URL + 'delete-employee-rejoin/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();