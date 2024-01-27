/// <reference path="../../services/punishmentSubCategoryService.js" />
(function () {

    'use strict';

    var controllerId = 'punishmentSubCategoryAddController';

    angular.module('app').controller(controllerId, punishmentSubCategoryAddController);
    punishmentSubCategoryAddController.$inject = ['$stateParams', 'punishmentSubCategoryService', 'notificationService', '$state'];

    function punishmentSubCategoryAddController($stateParams, punishmentSubCategoryService, notificationService, $state) {
        var vm = this;
        vm.punishmentSubCategoryId = 0;
        vm.title = 'ADD MODE';
        vm.punishmentSubCategory = {};
        vm.punishmentCategories = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.punishmentSubCategoryForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.punishmentSubCategoryId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            punishmentSubCategoryService.getPunishmentSubCategory(vm.punishmentSubCategoryId).then(function (data) {
                vm.punishmentSubCategory = data.result.punishmentSubCategory;
                vm.punishmentCategories = data.result.punishmentCategories;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.punishmentSubCategoryId !== 0 && vm.punishmentSubCategoryId !== '') {
                updatePunishmentSubCategory();
            } else {
                insertPunishmentSubCategory();
            }
        }

        function insertPunishmentSubCategory() {
            punishmentSubCategoryService.savePunishmentSubCategory(vm.punishmentSubCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePunishmentSubCategory() {
            punishmentSubCategoryService.updatePunishmentSubCategory(vm.punishmentSubCategoryId, vm.punishmentSubCategory).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('punishment-sub-categories');
        }
    }
})();
