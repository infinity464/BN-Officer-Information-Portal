

(function () {

    'use strict';

    var controllerId = 'costguardController';

    angular.module('app').controller(controllerId, costguardController);
    costguardController.$inject = ['$stateParams','employeeService','currentStatusService', 'notificationService', '$state'];

    function costguardController($stateParams, employeeService, currentStatusService, notificationService, $state) {
        var vm = this;
        vm.costGuardHistories = [];
        vm.temporaryCostGuardHistories = [];
        vm.getTemporaryTransfer = getTemporaryTransfer;
        vm.printSection = printSection;

        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }

        Init();
        function Init() {

            employeeService.getEmployeeByPno(vm.pNo).then(function (data) {
                    vm.officerName = data.result.fullNameEng;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });


            currentStatusService.getCostGuardHistory(vm.pNo).then(function (data) {
                vm.costGuardHistories = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });


        }

        function getTemporaryTransfer(transferId) {
            currentStatusService.getTemporaryTransferHistory(transferId).then(function (data) {
                vm.temporaryCostGuardHistories = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function printSection(printSectionId) {
            var innerContents = document.getElementById(printSectionId).innerHTML;
            var popupWindow = window.open('', '', 'width=1000,height=700,left=0,top=0,toolbar=0,status=0');
            popupWindow.document.open();
            popupWindow.document.write('<HTML>\n<HEAD>\n');
            popupWindow.document.write('<link rel="stylesheet" type="text/css" href="../../../Content/addmin/PdfReport.css"/>');
            popupWindow.document.write('</HEAD>\n');
            popupWindow.document.write('<BODY  onload="window.print()">\n');
            popupWindow.document.write(innerContents);
            popupWindow.document.write('</BODY>\n');
            popupWindow.document.write('</HTML>\n');
            popupWindow.document.close();
            popupWindow.focus();
        }

    }
})();
