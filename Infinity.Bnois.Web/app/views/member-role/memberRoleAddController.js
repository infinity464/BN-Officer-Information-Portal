

(function () {

    'use strict';

    var controllerId = 'memberRoleAddController';

    angular.module('app').controller(controllerId, memberRoleAddController);
    memberRoleAddController.$inject = ['$stateParams', 'memberRoleService', 'notificationService', '$state'];

    function memberRoleAddController($stateParams, memberRoleService, notificationService, $state) {
        var vm = this;
        vm.memberRoleId = 0;
        vm.title = 'ADD MODE';
        vm.memberRole = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.memberRoleForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.memberRoleId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            memberRoleService.getMemberRole(vm.memberRoleId).then(function (data) {
                vm.memberRole = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.memberRoleId !== 0) {
                updateMemberRole();
            } else {  
                insertMemberRole();
            }
        }

        function insertMemberRole() {
            memberRoleService.saveMemberRole(vm.memberRole).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateMemberRole() {
            memberRoleService.updateMemberRole(vm.memberRoleId, vm.memberRole).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('member-roles');
        }

    }
})();
