(function () {

    'use strict';
    var controllerId = 'employeeOthersController';
    angular.module('app').controller(controllerId, employeeOthersController);
    employeeOthersController.$inject = ['$stateParams', '$state', 'employeeOtherService', 'notificationService'];

    function employeeOthersController($stateParams, $state, employeeOtherService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeOther = {};
        vm.title = "Other's Information";
        vm.updateEmployeeOther = updateEmployeeOther;
        vm.uploadEmployeeOther = uploadEmployeeOther;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            employeeOtherService.getEmployeeOthers(vm.employeeId).then(function (data) {
                vm.employeeOther = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function updateEmployeeOther() {
            $state.go('employee-tabs.employee-other-modify', { id: vm.employeeId});
        }
        function uploadEmployeeOther() {
            $state.go('employee-tabs.employee-other-image', { id: vm.employeeId });
        }

        
    }

})();
