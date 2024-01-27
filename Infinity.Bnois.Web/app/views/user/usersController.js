(function () {

    'use strict';

    var controllerId = 'UsersController';

    angular.module('app').controller(controllerId, usersController);

    usersController.$inject = ['$state', 'userService', '$location', 'notificationService'];

    function usersController($state, userService, location, notificationService) {

        /* jshint validthis:true */
        var vm = this;

        vm.users = [];
        vm.userStatus = true;
        vm.addUser = addUser;
        vm.deleteUser = deleteUser;
        vm.updateUser = updateUser;
        vm.addUserRoles = addUserRoles;
        vm.onSearch = onSearch;
        vm.searchText = '';
        vm.searchRole = searchRole;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;
        vm.loginUserId = '';
        vm.pageChanged = pageChanged;
        vm.activateUser = activateUser;
        vm.resetUserPasswordByUserId = resetUserPasswordByUserId;


        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }

        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }

        init();
        function init() {
            userService.getUsers(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                    vm.users = data.result.users;
                vm.total = data.total; 
                    vm.loginUserId = data.result.loginUserId;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage);
                });
        }

        function addUser() {
           
            $state.go('user-create');
        }
      

        function addUserRoles(user) {
        
            $state.go('user-roles', { userId: user.id});
        }
        
        function updateUser(user) {
          var url = location.url("/user-modify/" + user.id);
            location.path(url.$$url).search('pn', vm.pageNumber).search('ps', vm.pageSize).search('q', vm.searchText);
        }

        function deleteUser(user) {
            userService.deleteUser(user.id).then(function (data) {
              $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
                notificationService.displaySuccess("successfully inactivated");
          },
            function (errorMessage) {
                notificationService.displayError(errorMessage);
            });
        }
        function activateUser(user) {
            userService.activateUser(user.id).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
                notificationService.displaySuccess("successfully activated");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage);
                });
        }
        function resetUserPasswordByUserId(user) {
            userService.resetUserPasswordByUserId(user.id).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
                notificationService.displaySuccess("Password reset successfully");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage);
                });
        }

        function pageChanged() {
            $state.go('users', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }
        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
        function searchRole() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }
})();