(function () {
    'use strict';
    angular.module('app').service('memberRoleService', ['dataConstants', 'apiHttpService', memberRoleService]);

    function memberRoleService(dataConstants, apiHttpService) {
        var service = {
            getMemberRoles: getMemberRoles,
            getMemberRole: getMemberRole,
            saveMemberRole: saveMemberRole,
            updateMemberRole: updateMemberRole,
            deleteMemberRole: deleteMemberRole
        };

        return service;
        function getMemberRoles(pageSize, pageNumber, searchText) {
            var url = dataConstants.MEMBER_Role_URL + 'get-member-roles?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getMemberRole(id) {
            var url = dataConstants.MEMBER_Role_URL + 'get-member-role?id=' + id;

            return apiHttpService.GET(url);
        }

        function saveMemberRole(data) {
            var url = dataConstants.MEMBER_Role_URL + 'save-member-role';
            return apiHttpService.POST(url, data);
        }

        function updateMemberRole(id, data) {
            var url = dataConstants.MEMBER_Role_URL + 'update-member-role/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteMemberRole(id) {
            var url = dataConstants.MEMBER_Role_URL + 'delete-member-role/' + id;
            return apiHttpService.DELETE(url);
        }


    }
})();