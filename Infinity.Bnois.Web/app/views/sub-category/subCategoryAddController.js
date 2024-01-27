/// <reference path="../../services/employeegeneralservice.js" />
(function () {

    'use strict';

    var controllerId = 'subCategoryAddController';

    angular.module('app').controller(controllerId, subCategoryAddController);
    subCategoryAddController.$inject = ['$stateParams', 'subCategoryService', 'notificationService', '$state'];

    function subCategoryAddController($stateParams, subCategoryService, notificationService, $state) {
        var vm = this;
        vm.subCategoryId = 0;
        vm.title = 'ADD MODE';
        vm.subCategory = {};
        vm.subCategories = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.subCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.subCategoryId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            subCategoryService.getSubCategory(vm.subCategoryId).then(function (data) {
                vm.subCategory = data.result.subCategory;
                vm.subCategories = data.result.subCategories;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.subCategoryId !== 0 && vm.subCategoryId !== '') {
                updateSubCategory();
            } else {
                insertSubCategory();
            }
        }

        function insertSubCategory() {
            subCategoryService.saveSubCategory(vm.subCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateSubCategory() {
            subCategoryService.updateSubCategory(vm.subCategoryId, vm.subCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('sub-categories');
        }
    }
})();
