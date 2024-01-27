(function () {

    'use strict';

    var controllerId = 'transferHistoryController';

    angular.module('app').controller(controllerId, transferHistoryController);
    transferHistoryController.$inject = ['$stateParams','currentStatusService', 'notificationService', '$state'];

    function transferHistoryController($stateParams, currentStatusService, notificationService, $state) {
        var vm = this;
        vm.permanentTransferHistories = [];
        vm.printSection = printSection;

        Init();
        function Init() {
            currentStatusService.getTransferHistory(vm.pNo).then(function (data) {
                vm.permanentTransferHistories = data.result;
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
