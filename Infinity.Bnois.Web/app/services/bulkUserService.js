(function () {
    'use strict';
    angular.module('app').service('bulkUserService', ['dataConstants', 'apiHttpService', bulkUserService]);

    function bulkUserService(dataConstants, apiHttpService) {
        var service = {
            UserList: UserList,
            AdminDashboard: AdminDashboard,
        };

        return service;

        
        function UserList(pageSize, pageNumber, searchText) {
            var url = dataConstants.BULK_USER_URL + 'user-list-for-user-create?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }
        function AdminDashboard() {
            var url = dataConstants.BULK_USER_URL + 'get-admin-dashboard';
            return apiHttpService.GET(url);
        }
    }
})();