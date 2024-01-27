
(function () {

    'use strict';
    var controllerId = 'employeeReportsController';
    angular.module('app').controller(controllerId, employeeReportsController);
    employeeReportsController.$inject = ['$state','$window', 'employeeReportService', 'downloadService','notificationService', '$location'];

    function employeeReportsController($state, $window, employeeReportService, downloadService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.employeeReports = [];
        vm.deleteEmployeeReport = deleteEmployeeReport;
        vm.deleteAllEmployeeReport = deleteAllEmployeeReport;
        vm.downlaodTrace = downlaodTrace;
        vm.saveButtonText = 'Save';
        vm.save = save;

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
            employeeReportService.getEmployeeReports().then(function (data) {
                vm.employeeReports = data.result.employeeReports;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            if (vm.employeeReport.employee.employeeId > 0) {
                vm.employeeReport.employeeId = vm.employeeReport.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by PNo!");
            }
            insertEmployeeReport();
        }

        function insertEmployeeReport() {
            employeeReportService.saveEmployeeReport(vm.employeeReport).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
            vm.employeeReport.employee = null;
            init();
        }
        function deleteEmployeeReport(employeeReport) {
            employeeReportService.deleteEmployeeReport(employeeReport.id).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function deleteAllEmployeeReport() {
            employeeReportService.deleteAllEmployeeReport().then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function downlaodTrace() {
            var url = employeeReportService.getDownlaodTrace();
            downloadService.downloadReport(url);
        }
    }

})();
