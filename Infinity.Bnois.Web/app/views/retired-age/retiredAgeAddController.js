
(function () {

    'use strict';

    var controllerId = 'retiredAgeAddController';

    angular.module('app').controller(controllerId, retiredAgeAddController);
    retiredAgeAddController.$inject = ['$stateParams', 'retiredAgeService','employeeGeneralService', 'notificationService', '$state'];

    function retiredAgeAddController($stateParams, retiredAgeService, employeeGeneralService, notificationService, $state) {
        var vm = this;
        vm.retiredAgeId = 0;
        vm.title = 'ADD MODE';
        vm.retiredAge = {};
        vm.categories = [];
        vm.subCategories = [];
        vm.ranks = [];
        vm.listTypes = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.retiredAgeForm = {};
        vm.getSubCategoryByCategoryId = getSubCategoryByCategoryId;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.retiredAgeId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            retiredAgeService.getRetiredAge(vm.retiredAgeId).then(function (data) {
                vm.retiredAge = data.result.retiredAge;
                vm.categories = data.result.categories;
                vm.ranks = data.result.ranks;
                vm.listTypes = data.result.listTypes;

                if (vm.retiredAgeId !== 0 && vm.retiredAgeId !== '') {
                    vm.subCategories = data.result.subCategories;

                }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.retiredAgeId !== 0 && vm.retiredAgeId !== '') {
                updateRetiredAge();
            } else {
                insertRetiredAge();
            }
        }

        function insertRetiredAge() {
            retiredAgeService.saveRetiredAge(vm.retiredAge).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateRetiredAge() {
            retiredAgeService.updateRetiredAge(vm.retiredAgeId, vm.retiredAge).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('retired-ages');
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
