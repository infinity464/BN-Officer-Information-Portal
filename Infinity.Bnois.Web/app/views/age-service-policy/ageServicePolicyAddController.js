
(function () {

    'use strict';

    var controllerId = 'ageServicePolicyAddController';

    angular.module('app').controller(controllerId, ageServicePolicyAddController);
    ageServicePolicyAddController.$inject = ['$stateParams', 'ageServicePolicyService','employeeGeneralService', 'notificationService', '$state'];

    function ageServicePolicyAddController($stateParams, ageServicePolicyService, employeeGeneralService, notificationService, $state) {
        var vm = this;
        vm.ageServiceId = 0;
        vm.title = 'ADD MODE';
        vm.ageServicePolicy = {};
        vm.categories = [];
        vm.subCategories = [];
        vm.ranks = [];
        vm.earlyStatus = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.ageServicePolicyForm = {};
        vm.getSubCategoryByCategoryId = getSubCategoryByCategoryId;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.ageServiceId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            ageServicePolicyService.getAgeServicePolicy(vm.ageServiceId).then(function (data) {
                vm.ageServicePolicy = data.result.ageServicePolicy;
                vm.categories = data.result.categories;
                vm.ranks = data.result.ranks;
                vm.earlyStatus = data.result.earlyStatus;
                if (vm.ageServiceId !== 0 && vm.ageServiceId !== '') {
                    vm.subCategories = data.result.subCategories;
                } else {
                    vm.ageServicePolicy.earlyStatus = 1;
                }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.ageServiceId !== 0 && vm.ageServiceId !== '') {
                updateAgeServicePolicy();
            } else {
                insertAgeServicePolicy();
            }
        }

        function insertAgeServicePolicy() {
            ageServicePolicyService.saveAgeServicePolicy(vm.ageServicePolicy).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateAgeServicePolicy() {
            ageServicePolicyService.updateAgeServicePolicy(vm.ageServiceId, vm.ageServicePolicy).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('age-service-policies');
        }


        function getSubCategoryByCategoryId(categoryId) {
            employeeGeneralService.getSubCategoryByCategoryId(categoryId).then(function (data) {
                    vm.subCategories = data.result.subCategories;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
    }
})();
