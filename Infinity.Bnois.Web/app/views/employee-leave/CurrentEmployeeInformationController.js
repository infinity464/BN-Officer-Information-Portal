(function () {

    'use strict';
    var controllerId = 'CurrentEmployeeInformationController';
    angular.module('app').controller(controllerId, CurrentEmployeeInformationController);
    CurrentEmployeeInformationController.$inject = ['$state', 'employeeLeaveService', 'notificationService', '$location'];

    function CurrentEmployeeInformationController($state, employeeLeaveService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.empoloyeeLeaves = [];
        //vm.pno = '';
        //vm.leaveTypeId = 0;
        //vm.leaveTypes = [];
        //vm.searchText = "";
        //vm.IndividualEmployeeLeave = IndividualEmployeeLeave;



        //vm.onSearch = onSearch;

        //vm.pageSize = 50;
        //vm.pageNumber = 1;
        //vm.total = 0;

        init();
        function init() {

            employeeLeaveService.GetCurrentEmployeeInformation().then(function (data) {
                vm.empoloyeeLeaves = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        //function IndividualEmployeeLeave(empLeaveId) {
        //    $state.go('employee-leave-details', { empLeaveId: empLeaveId });
        //}

        //function onSearch() {
        //    employeeLeaveService.GetEmployeeLeavesForNsUser(vm.searchText, vm.leaveTypeId).then(function (data) {
        //        vm.empoloyeeLeaves = data.result.employeeLeaveForNS;
        //        vm.leaveTypes = data.result.leaveTypes;
        //        vm.total = vm.empoloyeeLeaves.total;
        //        vm.leaveTypeId = 0;
        //        vm.searchText = "";
        //    },
        //        function (errorMessage) {
        //            notificationService.displayError(errorMessage.message);
        //        });
        //}
    }

})();
