(function () {

    'use strict';

    var controllerId = 'RoleAddController';

    angular.module('app').controller(controllerId, roleAddController);

    roleAddController.$inject = ['$stateParams', 'roleService', '$state', 'notificationService'];

    function roleAddController($stateParams, roleService, $state, notificationService) {

        var vm = this;
    
        vm.title = 'ADD MODE';
        vm.role = {};
        vm.saveButtonText = 'Save';
        vm.save = Save;
        vm.close = Close;
        vm.roleForm = {};
        vm.roleId = '';

        if ($stateParams.roleId !== undefined && $stateParams.roleId !== null) {
            vm.roleId = $stateParams.roleId;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        init();

        function init() {
            roleService.getRole(vm.roleId).then(function (data) {
                vm.role = data.result;
            },
                function (errorMessage) {
                });
        };

        function Save() {
            if (vm.roleId !== null && vm.roleId.length>0) {
                UpdateRole();
            } else {
                InsertRole();
            }
        }

        function InsertRole() {
            roleService.saveRole(vm.role).then(function (data) {
                Close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function UpdateRole() {
            roleService.updateRole(vm.roleId, vm.role).then(function (data) {
                Close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function Close() {
            $state.go('roles');
           
        }
    }
})();
