(function () {

    'use strict';

    var controllerId = 'trainingResultAddController';

    angular.module('app').controller(controllerId, trainingResultAddController);
    trainingResultAddController.$inject = ['$stateParams','$scope', 'trainingResultService', 'courseService','trainingPlanService' ,'notificationService', '$state'];

    function trainingResultAddController($stateParams, $scope,trainingResultService, courseService, trainingPlanService,notificationService, $state) {
        var vm = this;
        vm.trainingResultId = 0;
        vm.title = 'ADD MODE';
        vm.trainingResult = {};
        vm.resultStatus = [];
        vm.trainingPlans = [];
        vm.resultTypes = [];
        vm.courseCategories = [];
        vm.courseSubCategories = [];
        vm.countries = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.trainingResultForm = {};
//        vm.getTrainingPlansByCategorySubCategory = getTrainingPlansByCategorySubCategory;
//        vm.getCourseSubCategory = getCourseSubCategory;
        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.trainingResultId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        vm.localSearch = localSearch;
        vm.selected = selected;
        Init();
        function Init() {
            trainingResultService.getTrainingResult(vm.trainingResultId).then(function (data) {
                vm.trainingResult = data.result.trainingResult;

                    vm.resultTypes = data.result.resultTypes;
                    vm.trainingPlans = data.result.trainingPlans;
                    vm.resultStatus = data.result.resultStatus;  
                    vm.courseCategories = data.result.courseCategories;  
                    vm.courseSubCategories = data.result.courseSubCategories;  
                    vm.resultStatus = data.result.resultStatus;  
                    vm.countries = data.result.countries;  
                    if (vm.trainingResultId !== 0 && vm.trainingResultId !== '') {
                        vm.trainingResult.resultDate = new Date(data.result.trainingResult.resultDate);
                        changeInputValue();
                    } else {
                        vm.trainingResult.resultStatus = 1;
                       
                    }
           

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.trainingResult.employee.employeeId > 0) {
                vm.trainingResult.employeeId = vm.trainingResult.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }

            if (vm.trainingResultId !== 0 && vm.trainingResultId !== '') {
                updateTrainingResult();
            } else {
                insertTrainingResult();
            }
        }

        function insertTrainingResult() {
            trainingResultService.saveTrainingResult(vm.trainingResult).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateTrainingResult() {
            trainingResultService.updateTrainingResult(vm.trainingResultId, vm.trainingResult).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('training-results');
        }



//        function getCourseSubCategory(categoryId) {
//            courseService.getCourseSubCategories(categoryId).then(function (data) {
//                vm.courseSubCategories = data.result.courseSubCategories;
//            });
//        }
//
//     
//        function getTrainingPlansByCategorySubCategory(subCategoryId) {
//            trainingPlanService.getTrainingPlansByCategorySubCategory(vm.trainingResult.courseCategoryId, subCategoryId, vm.trainingResult.trainingPlan.countryId).then(function (data) {
//                vm.trainingPlans = data.result;
//            });
//        }



        function selected(object) {
            vm.trainingResult.trainingPlanId = object.originalObject.value;
            
        }


        function localSearch(str) {
            var matches = [];
            vm.trainingPlans.forEach(function (nomination) {

                if ((nomination.text.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0)) {
                    matches.push(nomination);

                }
            });
            return matches;
        }

        function changeInputValue() {
            if (vm.trainingResultId > 0) {
                trainingPlanService.getTrainingPlanSelectModel(vm.trainingResult.trainingPlanId).then(function (data) {
                    var obj = { value: vm.trainingResult.trainingPlanId, text: data.result[0].text };
                    $scope.$broadcast('angucomplete-alt:changeInput', 'trainingPlanId', obj);

                });
                
            }
        }


    }

  

})();
