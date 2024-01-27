(function () {

    'use strict';
    var controllerId = 'employeeChildrenAddController';
    angular.module('app').controller(controllerId, employeeChildrenAddController);
    employeeChildrenAddController.$inject = ['$stateParams', '$state', 'employeeChildrenService', 'notificationService'];

    function employeeChildrenAddController($stateParams, $state, employeeChildrenService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeChildrenId = 0;
        vm.employeeId = 0;
        vm.employeeChildren = {};
        vm.childrenTypes = [];
        vm.occupations = [];
        vm.spouses = [];
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.employeeChildrenForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.employeeChildrenId > 0) {
            vm.employeeChildrenId = $stateParams.employeeChildrenId;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
        }
        init();
        function init() {
            employeeChildrenService.getEmployeeChildren(vm.employeeId, vm.employeeChildrenId).then(function (data) {
                vm.employeeChildren = data.result.employeeChildren;
                vm.childrenTypes = data.result.childrenTypes;
                vm.occupations = data.result.occupations;
                vm.spouses = data.result.spouses;

                if (vm.employeeChildren.dateOfBirth != null) {
                    vm.employeeChildren.dateOfBirth = new Date(vm.employeeChildren.dateOfBirth);
                    }

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
     
        function save() {
            if (vm.employeeChildrenId > 0 && vm.employeeChildrenId !== '') {
                updateEmployeeChildren();
            } else {  
                insertEmployeeChildren();
            }
        }
        function insertEmployeeChildren() {
            employeeChildrenService.saveEmployeeChildren(vm.employeeId, vm.employeeChildren).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEmployeeChildren() {
            employeeChildrenService.updateEmployeeChildren(vm.employeeChildrenId, vm.employeeChildren).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function close() {
            $state.go('employee-tabs.employee-childrens');
        }
    }

})();
