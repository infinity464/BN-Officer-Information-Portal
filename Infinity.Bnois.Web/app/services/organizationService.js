(function () {
    'use strict';
    angular.module('app').service('organizationService', ['dataConstants', 'apiHttpService', organizationService]);

    function organizationService(dataConstants, apiHttpService) {
        var service = {
            getOrganizations: getOrganizations,
            getOrganization: getOrganization,
            saveOrganization: saveOrganization,
            updateOrganization: updateOrganization,
            deleteOrganization: deleteOrganization
        };

        return service;
        function getOrganizations(pageSize, pageNumber, searchText) {
            var url = dataConstants.ORGANIZATION_URL + 'get-organizations?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getOrganization(organizationId) {
            var url = dataConstants.ORGANIZATION_URL + 'get-organization?organizationId=' + organizationId;
            return apiHttpService.GET(url);
        }

        function saveOrganization(data) {
            var url = dataConstants.ORGANIZATION_URL + 'save-organization';
            return apiHttpService.POST(url, data);
        }

        function updateOrganization(organizationId, data) {
            var url = dataConstants.ORGANIZATION_URL + 'update-organization/' + organizationId;
            return apiHttpService.PUT(url, data);
        }

        function deleteOrganization(organizationId) {
            var url = dataConstants.ORGANIZATION_URL + 'delete-organization/' + organizationId;
            return apiHttpService.DELETE(url);
        }


    }
})();