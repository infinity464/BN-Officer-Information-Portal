(function () {

    'use strict';

    var controllerId = 'roleFeaturesController';

    angular.module('app').controller(controllerId, roleFeaturesController);

    roleFeaturesController.$inject = ['$stateParams', 'roleService', '$state', 'notificationService'];

    function roleFeaturesController($stateParams, roleService, $state, notificationService) {
        var vm = this;
        vm.title = 'User Role Assign';
        vm.isReport = false;
        vm.roleFeatures = [];
        vm.featureTypes = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.roleFeatureForm = {};
        vm.role = {};
        vm.roleId = '';

        vm.featureTypeId = 1;
        vm.assignFeature = assignFeature;
        if ($stateParams.roleId !== undefined && $stateParams.roleId !== null) {
            vm.roleId = $stateParams.roleId;
        }

        init();
        function init() {
            roleService.getRoleFeatures(vm.roleId).then(function (data) {
              vm.roleFeatures = data.result.roleFeatures,
                    vm.role = data.result.role;
              vm.featureTypes = data.result.featureTypes;
              },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            roleService.updateRoleFeatures(vm.roleId, vm.roleFeatures).then(function (data) {
                    notificationService.displaySuccess("Role Features added successfully");
                    Close();
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('roles');
        }

        function assignFeature(roleFeature) {
           
            roleService.assignFeature(vm.roleId, roleFeature).then(function (data) {
               
                notificationService.displaySuccess("Role Features update successfully");
               
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
    }
})();
