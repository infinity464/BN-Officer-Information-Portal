(function () {
    'use strict';
    angular.module('app').service('roleService', ['identityDataConstants', 'apiHttpService', roleService]);

    function roleService(identityDataConstants, apiHttpService) {

        var service = {
            getRoles: getRoles,
            getRole: getRole,
            saveRole: saveRole,
            updateRole: updateRole,
            deleteRole: deleteRole,
            getRoleFeatures: getRoleFeatures,
            updateRoleFeatures: updateRoleFeatures,
            assignFeature: assignFeature
        };

        return service;

        function getRoles(pageSize, pageNumber, searchText) {
          var url = identityDataConstants.ROLE_URL + 'get-roles?ps=' + pageSize + '&pn=' + pageNumber + '&q=' + searchText;
       
            return apiHttpService.GET(url);
        }

        function getRole(roleId) {
          var url = identityDataConstants.ROLE_URL + 'get-role?roleId=' + roleId;
            return apiHttpService.GET(url);
        }

        function saveRole(data) {
          var url = identityDataConstants.ROLE_URL + 'save-role';
            return apiHttpService.POST(url, data);
        }

        function updateRole(roleId, data) {
          var url = identityDataConstants.ROLE_URL + 'update-role/' + roleId;
            return apiHttpService.PUT(url, data);
        }

        function deleteRole(roleId) {
          var url = identityDataConstants.ROLE_URL + 'delete-role/' + roleId;
            return apiHttpService.DELETE(url);
        }
        function getRoleFeatures(roleId) {
            var url = identityDataConstants.ROLE_URL + 'get-role-features/' + roleId;
          
            return apiHttpService.GET(url);
        }
        function updateRoleFeatures(roleId, data) {
            var url = identityDataConstants.ROLE_URL + 'update-role-features/' + roleId;
            return apiHttpService.PUT(url, data);
        }
        function assignFeature(roleId, data) {
            var url = identityDataConstants.ROLE_URL + 'assign-role-feature/' + roleId;
            return apiHttpService.PUT(url, data);
        }
        
    }
})();