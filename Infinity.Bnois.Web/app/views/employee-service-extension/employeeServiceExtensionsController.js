/// <reference path="../../services/employeeServiceExtensionService.js" />

(function () {

    'use strict';
    var controllerId = 'employeeServiceExtensionsController';
    angular.module('app').controller(controllerId, employeeServiceExtensionsController);
    employeeServiceExtensionsController.$inject = ['$state', 'employeeServiceExtensionService', 'notificationService', '$location'];

    function employeeServiceExtensionsController($state, employeeServiceExtensionService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.employeeServiceExtensions = [];
        vm.addEmployeeServiceExtension = addEmployeeServiceExtension;
        vm.updateEmployeeServiceExtension = updateEmployeeServiceExtension;
        vm.deleteEmployeeServiceExtension = deleteEmployeeServiceExtension;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

        

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            employeeServiceExtensionService.getEmployeeServiceExtensions(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.employeeServiceExtensions = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addEmployeeServiceExtension() {
            $state.go('employee-service-extension-create');
        }

        function updateEmployeeServiceExtension(employeeServiceExtension) {
            $state.go('employee-service-extension-modify', { id: employeeServiceExtension.empSvrExtId });
        }

        function deleteEmployeeServiceExtension(employeeServiceExtension) {
            employeeServiceExtensionService.deleteEmployeeServiceExtension(employeeServiceExtension.empSvrExtId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('employee-service-extensions', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }

   
    }

})();
