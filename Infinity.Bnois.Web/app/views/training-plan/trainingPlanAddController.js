(function () {

    'use strict';

    var controllerId = 'trainingPlanAddController';

    angular.module('app').controller(controllerId, trainingPlanAddController);
    trainingPlanAddController.$inject = ['$stateParams', 'trainingPlanService', 'courseService', 'trainingInstituteService', 'notificationService', '$state'];

    function trainingPlanAddController($stateParams, trainingPlanService, courseService, trainingInstituteService, notificationService, $state) {
        var vm = this;
        vm.trainingPlanId = 0;
        vm.title = 'ADD MODE';
        vm.trainingPlan = {};
        vm.courseCategories = [];
        vm.courseSubCategories = [];
        vm.courses = [];
        vm.countries = [];
        vm.trainingInstitutes = [];
        vm.countryTypes = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.trainingPlanForm = {};
        vm.countriesName = countriesName;
        vm.getCourseSubCategory = getCourseSubCategory;
        vm.getTrainingInstitute = getTrainingInstitute;
        vm.getCourses = getCourses;
        vm.countryDisable = false;


        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.trainingPlanId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            trainingPlanService.getTrainingPlan(vm.trainingPlanId).then(function (data) {
                vm.trainingPlan = data.result.trainingPlan;

                vm.countries = data.result.countries;
                vm.courseSubCategories = data.result.courseSubCategories;
                vm.courses = data.result.courses;

                vm.courseCategories = data.result.courseCategories;
                vm.trainingInstitutes = data.result.trainingInstitutes;
                vm.countryTypes = data.result.countryTypes;
                vm.rankList = data.result.rankList;
                vm.branchList = data.result.branchList;
                if (vm.trainingPlanId !== 0 && vm.trainingPlanId !== '') {

                    if (vm.trainingPlan.pDate != null) {
                        vm.trainingPlan.pDate = new Date(data.result.trainingPlan.pDate);

                    }
                    if (vm.trainingPlan.pToDate != null) {
                        vm.trainingPlan.pToDate = new Date(data.result.trainingPlan.pToDate);

                    }
                    vm.trainingPlan.fromDate = new Date(data.result.trainingPlan.fromDate);
                    vm.trainingPlan.toDate = new Date(data.result.trainingPlan.toDate);

                    if (vm.trainingPlan.countryId == 12) {
                        getTrainingInstitute(vm.trainingPlan.countryId);
                        getCourses(vm.trainingPlan.courseSubCategoryId, vm.trainingPlan.countryId);
                        vm.countryDisable = true;
                    } else {
                        getTrainingInstitute(vm.trainingPlan.countryId);
                    }

                }
                else {
                    vm.trainingPlan.countryType = 1;
                    countriesName(vm.trainingPlan.countryType);
                }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.trainingPlanId !== 0 && vm.trainingPlanId !== '') {
                updateTrainingPlan();
            } else {
                insertTrainingPlan();
            }
        }

        function insertTrainingPlan() {
            trainingPlanService.saveTrainingPlan(vm.trainingPlan).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateTrainingPlan() {
            trainingPlanService.updateTrainingPlan(vm.trainingPlanId, vm.trainingPlan).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('training-plans');
        }


        function countriesName(countyType) {
            if (countyType == 2) {
                vm.trainingPlan.countryId = null;
                vm.countryDisable = false;
                getTrainingInstitute(0);
            } else {
                vm.trainingPlan.countryId = 12;
                vm.countryDisable = true;
                getTrainingInstitute(vm.trainingPlan.countryId);

            }
        }


        function getCourseSubCategory(categoryId) {
            courseService.getCourseSubCategories(categoryId).then(function (data) {
                vm.courseSubCategories = data.result.courseSubCategories;
            });
        }

        function getTrainingInstitute(countryId) {
            getCourses(vm.trainingPlan.courseSubCategoryId);
            trainingInstituteService.getTrainingInstituteSelectModel(countryId).then(function (data) {
                vm.trainingInstitutes = data.result;
            });
        }
        function getCourses(subCategoryId) {
            trainingPlanService.getCourses(vm.trainingPlan.courseCategoryId, subCategoryId, vm.trainingPlan.countryId).then(function (data) {
                vm.courses = data.result.courses;
            });
        }

    }
})();
