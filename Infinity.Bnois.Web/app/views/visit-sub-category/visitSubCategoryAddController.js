/// <reference path="../../services/visitSubCategoryService.js" />
(function () {

    'use strict';

    var controllerId = 'visitSubCategoryAddController';

    angular.module('app').controller(controllerId, visitSubCategoryAddController);
    visitSubCategoryAddController.$inject = ['$stateParams', 'visitSubCategoryService', 'notificationService', '$state'];

    function visitSubCategoryAddController($stateParams, visitSubCategoryService, notificationService, $state) {
        var vm = this;
        vm.visitSubCategoryId = 0;
        vm.title = 'ADD MODE';
        vm.visitSubCategory = {};
        vm.visitCategories = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.visitSubCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.visitSubCategoryId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            visitSubCategoryService.getVisitSubCategory(vm.visitSubCategoryId).then(function (data) {
                vm.visitSubCategory = data.result.visitSubCategory;
                vm.visitCategories = data.result.visitCategories;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.visitSubCategoryId !== 0 && vm.visitSubCategoryId !== '') {
                updateVisitSubCategory();
            } else {
                insertVisitSubCategory();
            }
        }

        function insertVisitSubCategory() {
            visitSubCategoryService.saveVisitSubCategory(vm.visitSubCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateVisitSubCategory() {
            visitSubCategoryService.updateVisitSubCategory(vm.visitSubCategoryId, vm.visitSubCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('visit-sub-categories');
        }
    }
})();
