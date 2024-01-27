(function () {
    'use strict';
    angular.module('app').service('spouseForeignVisitService', ['dataConstants', 'apiHttpService', spouseForeignVisitService]);

    function spouseForeignVisitService(dataConstants, apiHttpService) {
        var service = {
            getSpouseForeignVisits: getSpouseForeignVisits,
            getSpouseForeignVisit: getSpouseForeignVisit,
            saveSpouseForeignVisit: saveSpouseForeignVisit,
            updateSpouseForeignVisit: updateSpouseForeignVisit,
          
        };

        return service;
        function getSpouseForeignVisits(spouseId) {
            var url = dataConstants.SPOUSE_FOREIGN_VISIT_URL + 'get-spouse-foreign-visits?spouseId=' + spouseId;
            return apiHttpService.GET(url);
        }

        function getSpouseForeignVisit(spouseId, spouseForeignVisitId) {
            var url = dataConstants.SPOUSE_FOREIGN_VISIT_URL + 'get-spouse-foreign-visit?spouseId=' + spouseId + '&spouseForeignVisitId=' + spouseForeignVisitId;
            return apiHttpService.GET(url);
        }

        function saveSpouseForeignVisit(data) {
            var url = dataConstants.SPOUSE_FOREIGN_VISIT_URL + 'save-spouse-foreign-visit';
            return apiHttpService.POST(url, data);
        }

        function updateSpouseForeignVisit(spouseForeignVisitId, data) {
            var url = dataConstants.SPOUSE_FOREIGN_VISIT_URL + 'update-spouse-foreign-visit/' + spouseForeignVisitId;
            return apiHttpService.PUT(url, data);
        }
    }
})();