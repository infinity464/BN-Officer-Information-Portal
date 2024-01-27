

(function () {

    'use strict';

    var controllerId = 'medicalCategoryAddController';

    angular.module('app').controller(controllerId, medicalCategoryAddController);
    medicalCategoryAddController.$inject = ['$stateParams', 'medicalCategoryService', 'notificationService', '$state'];

    function medicalCategoryAddController($stateParams, medicalCategoryService, notificationService, $state) {
        var vm = this;
        vm.medicalCategoryId = 0;
        vm.title = 'ADD MODE';
        vm.medicalCategory = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.medicalCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.medicalCategoryId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            medicalCategoryService.getMedicalCategory(vm.medicalCategoryId).then(function (data) {
                    vm.medicalCategory = data.result;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {

            if (vm.medicalCategoryId !== 0) {
                updateMedicalCategory();
            } else {
                insertMedicalCategory();
            }
        }

        function insertMedicalCategory() {
            medicalCategoryService.saveMedicalCategory(vm.medicalCategory).then(function (data) {
                    close();
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function updateMedicalCategory() {
            medicalCategoryService.updateMedicalCategory(vm.medicalCategoryId, vm.medicalCategory).then(function (data) {
                    close();
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('medical-categories');
        }

    }
})();
