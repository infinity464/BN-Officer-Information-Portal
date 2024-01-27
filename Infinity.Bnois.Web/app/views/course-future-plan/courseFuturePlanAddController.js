(function () {

    'use strict';

    var controllerId = 'courseFuturePlanAddController';

    angular.module('app').controller(controllerId, courseFuturePlanAddController);
    courseFuturePlanAddController.$inject = ['$stateParams', 'courseFuturePlanService','employeeService', 'courseService','trainingPlanService', 'notificationService', '$state'];

    function courseFuturePlanAddController($stateParams, courseFuturePlanService, employeeService, courseService, trainingPlanService, notificationService, $state) {
        var vm = this;
        vm.courseFuturePlanId = 0;
        vm.title = 'ADD MODE';
        vm.courseFuturePlan = {};
        vm.courses = [];
        vm.courseCategories = [];
        vm.courseSubCategories = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.courseFuturePlanForm = {};

        vm.getCourseSubCategory = getCourseSubCategory;
        vm.getCourses = getCourses;

        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }


        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.courseFuturePlanId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            courseFuturePlanService.getCourseFuturePlan(vm.courseFuturePlanId).then(function (data) {
                vm.courseFuturePlan = data.result.courseFuturePlan;

                employeeService.getEmployeeByPno(vm.pNo).then(function (data) {
                         vm.courseFuturePlan.employeeId = data.result.employeeId;                       
                    },

                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });

                    vm.courses = data.result.courses;
                    vm.courseCategories = data.result.courseCategories;  
                    vm.courseSubCategories = data.result.courseSubCategories; 

                if (vm.courseFuturePlanId >0) {
                    getCourseSubCategory(vm.courseFuturePlan.courseCategoryId);
                    getCourses(vm.courseFuturePlan.courseSubCategoryId);
                } 


                if (vm.courseFuturePlan.planDate !== null) {
                        vm.courseFuturePlan.planDate = new Date(data.result.courseFuturePlan.planDate);

                    } 
                    if (vm.courseFuturePlan.endDate !== null) {
                        vm.courseFuturePlan.endDate = new Date(data.result.courseFuturePlan.endDate);

                    } 
           

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
          

            if (vm.courseFuturePlanId !== 0 && vm.courseFuturePlanId !== '') {
                updateCourseFuturePlan();
            } else {
                insertCourseFuturePlan();
            }
        }

        function insertCourseFuturePlan() {
            courseFuturePlanService.saveCourseFuturePlan(vm.courseFuturePlan).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateCourseFuturePlan() {
            courseFuturePlanService.updateCourseFuturePlan(vm.courseFuturePlanId, vm.courseFuturePlan).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('current-status-tab.course-future-plans');
        }


        function getCourseSubCategory(categoryId) {
            courseService.getCourseSubCategories(categoryId).then(function (data) {
                vm.courseSubCategories = data.result.courseSubCategories;
            });
        }

        function getCourses(subCategoryId) {
            courseService.getCourseBySubCategory(subCategoryId).then(function (data) {
                vm.courses = data.result;
            });
        }
    }

  

})();
