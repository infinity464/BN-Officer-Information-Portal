(function () {

    'use strict';
    var controllerId = 'preCommissionCourseAddController';
    angular.module('app').controller(controllerId, preCommissionCourseAddController);
    preCommissionCourseAddController.$inject = ['$stateParams', '$state', 'preCommissionCourseService', 'punishmentSubCategoryService', 'notificationService'];

    function preCommissionCourseAddController($stateParams, $state, preCommissionCourseService, punishmentSubCategoryService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.preCommissionCourseId = 0;
        vm.preCommissionCourse = {};
        vm.punishmentCategories = [];
        vm.punishmentSubCategories = [];
        vm.punishmentNatures = [];
        vm.countries = [];
        vm.medals = [];

        vm.title = 'ADD MODE';
        vm.getPunishmentSubCategorySelectModelsByPunishmentCategory = getPunishmentSubCategorySelectModelsByPunishmentCategory;

        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.preCommissionCourseForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.preCommissionCourseId !== undefined && $stateParams.preCommissionCourseId !== null && $stateParams.preCommissionCourseId > 0) {
            vm.preCommissionCourseId = $stateParams.preCommissionCourseId;
            vm.title = 'UPDATE MODE';
        }
        init();
        function init() {
            preCommissionCourseService.getPreCommissionCourse(vm.employeeId, vm.preCommissionCourseId).then(function (data) {
                vm.preCommissionCourse = data.result.preCommissionCourse;
                vm.punishmentCategories = data.result.punishmentCategories;
                if (vm.preCommissionCourseId > 0) {
                    vm.punishmentSubCategories = data.result.punishmentSubCategories;
                }
                vm.punishmentNatures = data.result.punishmentNatures;
                vm.countries = data.result.countries;
                vm.medals = data.result.medals;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }



        function getPunishmentSubCategorySelectModelsByPunishmentCategory(punishmentCategoryId) {
            if (punishmentCategoryId > 0 && punishmentCategoryId != 'undefined') {
                punishmentSubCategoryService.getPunishmentSubCategorySelectModelsByPunishmentCategory(punishmentCategoryId).then(function (data) {
                    vm.panishmentSubCategories = data.result;
                },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
            }
        }



        function save() {
            if (!vm.preCommissionCourse.isAbroad) {
                vm.preCommissionCourse.countryId = null;
            }
            if (vm.preCommissionCourseId > 0 && vm.preCommissionCourseId !== '') {
                updatePreCommissionCourse();
            } else {
                insertPreCommissionCourse();
            }
        }
        function insertPreCommissionCourse() {
            preCommissionCourseService.savePreCommissionCourse(vm.employeeId, vm.preCommissionCourse).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePreCommissionCourse() {
            preCommissionCourseService.updatePreCommissionCourse(vm.preCommissionCourseId, vm.preCommissionCourse).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-tabs.pre-commission-courses');
        }
    }

})();
