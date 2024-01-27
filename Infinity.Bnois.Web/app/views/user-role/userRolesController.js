(function () {

    'use strict';

    var controllerId = 'UserRolesController';

    angular.module('app').controller(controllerId, userRolesController);

    userRolesController.$inject = ['$stateParams','$state', 'userService', '$location', 'notificationService'];

    function userRolesController($stateParams, $state, userService, location, notificationService) {
        var vm = this;
        vm.title = 'User Role Assign';
        vm.userRoles =[];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.updateSelection = updateSelection;
        vm.userRoleForm = {};
        vm.user = {};

        if ($stateParams.userId !== undefined && $stateParams.userId !== '') {
            vm.userId = $stateParams.userId;
        }

        init();
        function init() {
           
          userService.getUserRoles(vm.userId).then(function (data) {
                      vm.userRoles = data.result.userRoles,
                  vm.user = data.result.user;
                      
              },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            userService.saveUserRoles(vm.userId, vm.userRoles).then(function (data) {
                    notificationService.displaySuccess("User added successfully");
                    Close();
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateSelection(position, entities) {
            angular.forEach(vm.userRoles, function (subscription, index) {
                if (position != index)
                    subscription.isAssigned = false;
            });
        }
        function close() {
            $state.go('users');

        }
    }
})();
