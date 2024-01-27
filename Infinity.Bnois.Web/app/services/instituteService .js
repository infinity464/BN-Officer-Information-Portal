(function () {
    'use strict';
    angular.module('app').service('instituteService', ['dataConstants', 'apiHttpService', instituteService]);

    function instituteService(dataConstants, apiHttpService) {
        var service = {
            getInstitutes: getInstitutes,
            getInstitute: getInstitute,
            saveInstitute: saveInstitute,
            updateInstitute: updateInstitute,
            deleteInstitute: deleteInstitute,
            getBoardsByBoardType: getBoardsByBoardType
        };

        return service;
        function getInstitutes(pageSize, pageNumber, searchText) {
            var url = dataConstants.INSTITUTE_URL + 'get-institutes?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;       
            return apiHttpService.GET(url);
        }

        function getInstitute(id) {
            var url = dataConstants.INSTITUTE_URL + 'get-institute?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveInstitute(data) {
            var url = dataConstants.INSTITUTE_URL + 'save-institute';
            return apiHttpService.POST(url, data);
        }

        function updateInstitute(id, data) {
            var url = dataConstants.INSTITUTE_URL + 'update-institute/' + id;
            return apiHttpService.PUT(url, data);                
        }                                                        
                                                                 
        function deleteInstitute(id) {             
            var url = dataConstants.INSTITUTE_URL + 'delete-institute/' + id;
            return apiHttpService.DELETE(url);
        }

        function getBoardsByBoardType(boardType) {
            var url = dataConstants.INSTITUTE_URL + 'get-board-by-board-type/' + boardType;
            return apiHttpService.GET(url);
        }
    }
})();