(function () {

    'use strict';

    var controllerId = 'employeeServiceExamResultAddController';

    angular.module('app').controller(controllerId, employeeServiceExamResultAddController);
    employeeServiceExamResultAddController.$inject = ['$stateParams', 'employeeServiceExamResultService', 'serviceExamService','backLogService', 'notificationService', '$state'];

    function employeeServiceExamResultAddController($stateParams, employeeServiceExamResultService, serviceExamService,backLogService,notificationService, $state) {
        var vm = this;
        vm.employeeServiceExamResultId = 0;
        vm.title = 'ADD MODE';
        vm.employeeServiceExamResult = {};
        vm.serviceExams = [];
        vm.serviceExamCategories = [];
        vm.ranks = [];
        vm.transfers = [];
  
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.employeeServiceExamResultForm = {};
        vm.getServiceExamByServiceExamCategory = getServiceExamByServiceExamCategory;
        vm.isBackLogChecked = isBackLogChecked;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeServiceExamResultId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            employeeServiceExamResultService.getEmployeeServiceExamResult(vm.employeeServiceExamResultId).then(function (data) {
                vm.employeeServiceExamResult = data.result.employeeServiceExamResult;

                    vm.serviceExams = data.result.serviceExams;
                    vm.serviceExamCategories = data.result.serviceExamCategories;  
                    if (vm.employeeServiceExamResultId !== 0 && vm.employeeServiceExamResultId !== '') {
                        isBackLogChecked(vm.employeeServiceExamResult.isBackLog);
                        vm.employeeServiceExamResult.examDate = new Date(data.result.employeeServiceExamResult.examDate);

                        if (vm.employeeServiceExamResult.exemptedDate != null) {
                            vm.employeeServiceExamResult.exemptedDate = new Date(data.result.employeeServiceExamResult.exemptedDate);
                        }
                    }

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.employeeServiceExamResult.employee.employeeId > 0) {
                vm.employeeServiceExamResult.employeeId = vm.employeeServiceExamResult.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }

            if (vm.employeeServiceExamResultId !== 0 && vm.employeeServiceExamResultId !== '') {
                updateEmployeeServiceExamResult();
            } else {
                insertEmployeeServiceExamResult();
            }
        }

        function insertEmployeeServiceExamResult() {
            employeeServiceExamResultService.saveEmployeeServiceExamResult(vm.employeeServiceExamResult).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEmployeeServiceExamResult() {
            employeeServiceExamResultService.updateEmployeeServiceExamResult(vm.employeeServiceExamResultId, vm.employeeServiceExamResult).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-service-exam-results');
        }


        function getServiceExamByServiceExamCategory(categoryId) {
            serviceExamService.getServiceExamByServiceExamCategory(categoryId).then(function (data) {

                vm.serviceExams = data.result;
            });
        }

        function isBackLogChecked(isBackLog) {
            if (isBackLog) {
                if (vm.employeeServiceExamResult.employee.employeeId > 0) {
                    backLogService.getBackLogSelectModels(vm.employeeServiceExamResult.employee.employeeId).then(function (data) {
                        vm.ranks = data.result.ranks;
                        vm.transfers = data.result.transfers;
                    });
                }
            }
        }

    }

  

})();
