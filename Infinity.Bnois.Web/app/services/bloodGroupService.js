(function () {
    'use strict';
    angular.module('app').service('bloodGroupService', ['dataConstants', 'apiHttpService', bloodGroupService]);

    function bloodGroupService(dataConstants, apiHttpService) {
        var service = {
            getBloodGroups: getBloodGroups,
            getBloodGroup: getBloodGroup,
            saveBloodGroup: saveBloodGroup,
            updateBloodGroup: updateBloodGroup,
            deleteBloodGroup: deleteBloodGroup
        };

        return service;
        function getBloodGroups(pageSize, pageNumber, searchText) {
            var url = dataConstants.BLOOD_GROUP_URL + 'get-blood-groups?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getBloodGroup(bloodGroupId) {
            var url = dataConstants.BLOOD_GROUP_URL + 'get-blood-group?id=' + bloodGroupId;
            return apiHttpService.GET(url);
        }

        function saveBloodGroup(data) {
            var url = dataConstants.BLOOD_GROUP_URL + 'save-blood-group';
            return apiHttpService.POST(url, data);
        }

        function updateBloodGroup(bloodGroupId, data) {
            var url = dataConstants.BLOOD_GROUP_URL + 'update-blood-group/' + bloodGroupId;
            return apiHttpService.PUT(url, data);
        }

        function deleteBloodGroup(bloodGroupId) {
            var url = dataConstants.BLOOD_GROUP_URL + 'delete-blood-group/' + bloodGroupId;
            return apiHttpService.DELETE(url);
        }


    }
})();