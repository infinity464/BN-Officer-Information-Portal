/// <reference path="../../services/punishmentsubcategoryservice.js" />


(function () {

    'use strict';
    var controllerId = 'ptDeductPunishmentAddController';
    angular.module('app').controller(controllerId, ptDeductPunishmentAddController);
    ptDeductPunishmentAddController.$inject = ['$stateParams', '$state', 'traceSettingService', 'punishmentNatureService', 'punishmentSubCategoryService', 'notificationService'];

    function ptDeductPunishmentAddController($stateParams, $state, traceSettingService, punishmentNatureService, punishmentSubCategoryService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.ptDeductPunishmentId = 0;
        vm.traceSettingId = 0;
        vm.ptDeductPunishment = {};
        vm.punishmentCategories = [];
        vm.punishmentNatures = [];
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.ptDeductPunishmentForm = {};
        vm.getPunishmentSubCategoryByPunishmentCategory = getPunishmentSubCategoryByPunishmentCategory;
        vm.checkPunimentNature = checkPunimentNature;
        vm.isNormalOffence = false;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.traceSettingId = $stateParams.id;
        }
        if ($stateParams.ptDeductPunishmentId > 0) {
            vm.ptDeductPunishmentId = $stateParams.ptDeductPunishmentId;
            vm.title = 'UPDATE MODE';
        }
        init();
        function init() {
            traceSettingService.getPtDeductPunishment(vm.traceSettingId, vm.ptDeductPunishmentId).then(function (data) {
                vm.ptDeductPunishment = data.result.ptDeductPunishment;
                if (vm.ptDeductPunishment.punishmentNatureId>0) {
                    checkPunimentNature(vm.ptDeductPunishment.punishmentNatureId);
                }
                vm.punishmentSubCategories = data.result.punishmentSubCategories;
                vm.punishmentNatures = data.result.punishmentNatures;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            if (vm.ptDeductPunishmentId > 0 && vm.ptDeductPunishmentId !== '') {
                updatePtDeductPunishment();
            } else {
                insertPtDeductPunishment();
            }
        }
        function insertPtDeductPunishment() {
            traceSettingService.savePtDeductPunishment(vm.traceSettingId, vm.ptDeductPunishment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePtDeductPunishment() {
            traceSettingService.updatePtDeductPunishment(vm.ptDeductPunishmentId, vm.ptDeductPunishment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function getPunishmentSubCategoryByPunishmentCategory(punishmentCategoryId) {
            punishmentSubCategoryService.getPunishmentSubCategorySelectModelsByPunishmentCategory(punishmentCategoryId).then(function (data) {
                vm.punishmentSubCategories = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }

        function close() {
            $state.go('trace-setting-tabs.point-deduction-punishments');
        }

        function checkPunimentNature(punishmentNatureId) {
            punishmentNatureService.getPunishmentNature(punishmentNatureId).then(function (data) {
                vm.punishmentNature = data.result;
                if (vm.punishmentNature.shortName == 'NO') {
                    vm.isNormalOffence = true;
                }
            });
        }
    }

})();
