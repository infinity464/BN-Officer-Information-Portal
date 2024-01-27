

(function () {

    'use strict';

    var controllerId = 'oprGradingController';

    angular.module('app').controller(controllerId, oprGradingController);
    oprGradingController.$inject = ['$stateParams','employeeService', 'notificationService','currentStatusService', '$state'];

    function oprGradingController($stateParams, employeeService, notificationService, currentStatusService, $state) {
        var vm = this;
        vm.pNo = 0;
        vm.oprGradings = [];
        vm.printSection = printSection;
        vm.intialHide;
        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }

        Init();
        function Init() {
            employeeService.getEmployeeByPno().then(function (data) {
                vm.officerName = data.result.fullNameEng;
                vm.pNo = data.result.pNo;
                
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });



            currentStatusService.getOprGrading(vm.pNo).then(function (data) {
                vm.oprGradings = data.result;
                if (vm.oprGradings.length > 0) {
                    vm.intialHide = false;
                }
                else {
                    vm.intialHide = true;
                }
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function printSection(printSectionId) {
            var innerContents = document.getElementById(printSectionId).innerHTML;
            var popupWindow = window.open('', '', 'width=810,height=520,left=0,top=0,toolbar=0,status=0');
            popupWindow.document.open();
            popupWindow.document.write('<HTML>\n<HEAD>\n');
            popupWindow.document.write('<link rel="stylesheet" type="text/css" href="../../../Content/addmin//PdfReport.css"/>');
            popupWindow.document.write('</HEAD>\n');
            popupWindow.document.write('<BODY >\n');
            popupWindow.document.write(innerContents);
            popupWindow.document.write('</BODY>\n');
            popupWindow.document.write('</HTML>\n');
            popupWindow.document.close();
            popupWindow.focus();
        }
       
    }
})();
