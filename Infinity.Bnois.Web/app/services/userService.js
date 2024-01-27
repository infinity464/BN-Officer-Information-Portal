
(function () {
    'use strict';
    angular.module('app').service('userService', ['identityDataConstants', 'apiHttpService', userService]);

    function userService(identityDataConstants, apiHttpService) {
        var service = {
          getUsers: getUsers,
          getUser: getUser,
          saveUser: saveUser,
          updateUser: updateUser,
          deleteUser: deleteUser,
          getUserRoles: getUserRoles,
          saveUserRoles: saveUserRoles,
            changePassword: changePassword,
            activateUser: activateUser,
            resetUserPasswordByUserId: resetUserPasswordByUserId,
            saveBulkUser: saveBulkUser
    
        };
        return service;

        function getUsers(pageSize, pageNumber, searchText) {
            var url = identityDataConstants.USER_URL + 'get-users?ps=' + pageSize + '&pn=' + pageNumber + '&q=' + searchText;
         
          return apiHttpService.GET(url);
        }

        function getUser(userId) {
            var url = identityDataConstants.USER_URL + 'get-user?userId=' + userId;
            return apiHttpService.GET(url);
        }

        function saveUser(data) {
          var url = identityDataConstants.USER_URL + 'save-user';
            return apiHttpService.POST(url, data);
        }

        function saveBulkUser(data) {
            var url = identityDataConstants.USER_URL + 'save-bulk-user';
            return apiHttpService.POST(url, data);
        }

        function updateUser(userId, data) {
          var url = identityDataConstants.USER_URL + 'update-user/' + userId;
            return apiHttpService.PUT(url, data);
        }

        function deleteUser(userId) {
          var url = identityDataConstants.USER_URL + 'delete-user/' + userId;
            return apiHttpService.DELETE(url);
        }

        function activateUser(userId) {
            var url = identityDataConstants.USER_URL + 'activate-user/' + userId;
            return apiHttpService.DELETE(url);
        }

        function getUserRoles(userId) {
            var url = identityDataConstants.USER_URL + 'get-user-roles/' + userId;
          
            return apiHttpService.GET(url);
        }
        function saveUserRoles(userId, data) {
            var url = identityDataConstants.USER_URL + 'save-user-roles/' + userId;
            return apiHttpService.POST(url, data);
        }

        function changePassword(data) {
            var url = identityDataConstants.ACCOUNT_URL + 'reset-password';
            return apiHttpService.POST(url, data);
        }
        function resetUserPasswordByUserId(userId) {
            var url = identityDataConstants.USER_URL + 'reset-password-by-userid/' + userId;
            return apiHttpService.DELETE(url);
        }
    }
})();