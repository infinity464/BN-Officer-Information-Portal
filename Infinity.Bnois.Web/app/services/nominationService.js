(function () {
    'use strict';
    angular.module('app').service('nominationService', ['dataConstants', 'apiHttpService', nominationService]);

    function nominationService(dataConstants, apiHttpService) {
        var service = {
            getNominations: getNominations,
            getNomination: getNomination,
            getNominationType: getNominationType,
            getNominationSchedule: getNominationSchedule,
            getNominationByType: getNominationByType,
            saveNomination: saveNomination,
            updateNomination: updateNomination,
            deleteNomination: deleteNomination
        };

        return service;
        function getNominations(pageSize, pageNumber, searchText,type) {
            var url = dataConstants.NOMINATION_URL + 'get-nominations?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText+"&type="+type;
            return apiHttpService.GET(url);
        }

        function getNomination(type,nominationId) {
            var url = dataConstants.NOMINATION_URL + 'get-nomination?type='+type+'&id=' + nominationId;
            return apiHttpService.GET(url);
        }
        function getNominationType() {
            var url = dataConstants.NOMINATION_URL + 'get-nomination-type';
            return apiHttpService.GET(url);
        }
        function getNominationByType(type) {
            var url = dataConstants.NOMINATION_URL + 'get-nomination-by-type?type='+type;
            return apiHttpService.GET(url);
        }
        function getNominationSchedule(id,type) {
            var url = dataConstants.NOMINATION_URL + 'get-nomination-schedule?id='+id+'&type='+type;
            return apiHttpService.GET(url);
        }

        function saveNomination(data) {
            var url = dataConstants.NOMINATION_URL + 'save-nomination';
            return apiHttpService.POST(url, data);
        }

        function updateNomination(nominationId, data) {
            var url = dataConstants.NOMINATION_URL + 'update-nomination/' + nominationId;
            return apiHttpService.PUT(url, data);
        }

        function deleteNomination(nominationId) {
            var url = dataConstants.NOMINATION_URL + 'delete-nomination/' + nominationId;
            return apiHttpService.DELETE(url);
        }


    }
})();