(function () {
    'use strict';
    angular.module('app').service('nominationDetailService', ['dataConstants', 'apiHttpService', nominationDetailService]);

    function nominationDetailService(dataConstants, apiHttpService) {
        var service = {
            getNominationDetails: getNominationDetails,
            getNominationDetail: getNominationDetail,
            getNominatedList: getNominatedList,
 
            saveNominationDetail: saveNominationDetail,
            updateNominationDetail: updateNominationDetail,
            deleteNominationDetail: deleteNominationDetail
        };

        return service;
        function getNominationDetails(nominationId) {
            var url = dataConstants.NOMINATION_DETAIL_URL + 'get-nomination-details?id=' + nominationId;
            return apiHttpService.GET(url);
        }

        function getNominationDetail(nominationDetailId) {
            var url = dataConstants.NOMINATION_DETAIL_URL + 'get-nomination-detail?id=' + nominationDetailId;
            return apiHttpService.GET(url);
        }

        function getNominatedList(nominationId) {
            var url = dataConstants.NOMINATION_DETAIL_URL + 'get-nominated-list?nominationId=' + nominationId;
            return apiHttpService.GET(url);
        }

        function saveNominationDetail(data, type) {
            var url = dataConstants.NOMINATION_DETAIL_URL + 'save-nomination-detail?type=' + type;
            return apiHttpService.POST(url, data);
        }

        function updateNominationDetail(nominationId, data) {
            var url = dataConstants.NOMINATION_DETAIL_URL + 'update-nomination-detail/' + nominationId;
            return apiHttpService.PUT(url, data);
        }


        function deleteNominationDetail(nominationDetailId) {
            var url = dataConstants.NOMINATION_DETAIL_URL + 'delete-nomination-detail/' + nominationDetailId;
            return apiHttpService.DELETE(url);
        }
    }
})();