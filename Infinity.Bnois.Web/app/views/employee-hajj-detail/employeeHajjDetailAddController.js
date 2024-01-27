

(function () {

    'use strict';

    var controllerId = 'employeeHajjDetailAdd';

    angular.module('app').controller(controllerId, employeeHajjDetailAdd);
    employeeHajjDetailAdd.$inject = ['$stateParams', 'employeeService', 'employeeHajjDetailService', 'notificationService', '$state'];

    function employeeHajjDetailAdd($stateParams, employeeService, employeeHajjDetailService, notificationService, $state) {
        var vm = this;
        vm.employeeHajjDetailId = 0;
        vm.pNo = '';
        vm.title = 'ADD MODE';
        vm.employeeHajjDetail = {};
        vm.employeeInfo = {};
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.getEmployeeInfo = getEmployeeInfo;
        vm.getEmployeeInfoByEmployeeId = getEmployeeInfoByEmployeeId;
        vm.employeeHajjDetailForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== '') {
            vm.employeeHajjDetailId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            employeeHajjDetailService.getEmployeeHajjDetail(vm.employeeHajjDetailId).then(function (data) {
                vm.employeeHajjDetail = data.result;
                if (vm.employeeHajjDetail.fromDate != null && vm.employeeHajjDetail.toDate != null) {
                    vm.employeeHajjDetail.fromDate = new Date(vm.employeeHajjDetail.fromDate);
                    vm.employeeHajjDetail.toDate = new Date(vm.employeeHajjDetail.toDate); 
                }
  
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };
        function getEmployeeInfo() {

            employeeService.getEmployeeByPno(vm.employeeHajjDetail.employee.pNo).then(function (data) {
                vm.employeeHajjDetail.employee = data.result;
                vm.employeeHajjDetail.employeeId = vm.employeeHajjDetail.employee.employeeId
                
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.message);
            });
        }
        function getEmployeeInfoByEmployeeId() {

            employeeService.getEmployee(vm.employeeHajjDetail.employeeId).then(function (data) {
                vm.employeeInfo = data.result.employee;
                vm.pNo = vm.employeeInfo.pNo;
                vm.employeeHajjDetail.employeeId = vm.employeeInfo.employeeId
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.message);
            });
        }
        function save() {

            if (vm.employeeHajjDetailId !== 0) {
                updateEmployeeHajjDetail();
            } else {
                insertEmployeeHajjDetail();
            }
        }

        function insertEmployeeHajjDetail() {
            employeeHajjDetailService.saveEmployeeHajjDetail(vm.employeeHajjDetail).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function updateEmployeeHajjDetail() {
            employeeHajjDetailService.updateEmployeeHajjDetail(vm.employeeHajjDetailId, vm.employeeHajjDetail).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-hajj-detail');
        }

    }
})();
