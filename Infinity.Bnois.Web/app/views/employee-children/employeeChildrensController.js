(function () {

    'use strict';
    var controllerId = 'employeeChildrensController';
    angular.module('app').controller(controllerId, employeeChildrensController);
    employeeChildrensController.$inject = ['$stateParams', '$state', 'employeeChildrenService', 'notificationService'];

    function employeeChildrensController($stateParams, $state, employeeChildrenService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeId = 0;
        vm.employeeChildrenId = 0;
        vm.employeeChildrens = [];
        vm.title = 'Childrens';
        vm.addEmployeeChildren = addEmployeeChildren;
        vm.updateEmployeeChildren = updateEmployeeChildren;
        vm.deleteEmployeeChildren = deleteEmployeeChildren;
        vm.uploadChildrenImage = uploadChildrenImage;
        vm.close = close;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            employeeChildrenService.getEmployeeChildrens(vm.employeeId).then(function (data) {
                vm.employeeChildrens = data.result;
                    vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addEmployeeChildren() {
            $state.go('employee-tabs.employee-children-create', { id: vm.employeeId, employeeChildrenId: vm.employeeChildrenId });
        }
        
        function updateEmployeeChildren(employeeChildren) {
            $state.go('employee-tabs.employee-children-modify', { id: vm.employeeId, employeeChildrenId: employeeChildren.employeeChildrenId });
        }
        function close() {
            $state.go('employee-tabs.employee-childrens');
        }

        function deleteEmployeeChildren(employeeChildren) {
            employeeChildrenService.deleteEmployeeChildren(employeeChildren.employeeChildrenId).then(function (data) {
                employeeChildrenService.getEmployeeChildrens(vm.employeeId).then(function (data) {
                    vm.employeeChildrens = data.result;
                });
                $state.go('employee-tabs.employee-childrens');
            });
        }

        function uploadChildrenImage(children) {
            $state.go('employee-tabs.employee-children-image', { employeeChildrenId: children.employeeChildrenId });
        }
    }

})();
