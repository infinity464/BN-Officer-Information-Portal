

(function () {

    'use strict';

    var controllerId = 'missionForeignProjectsController';

    angular.module('app').controller(controllerId, missionForeignProjectsController);
    missionForeignProjectsController.$inject = ['$stateParams', 'employeeService', 'currentStatusService', 'employeeHajjDetailService', 'notificationService', '$state'];

    function missionForeignProjectsController($stateParams, employeeService, currentStatusService, employeeHajjDetailService, notificationService, $state) {
        var vm = this;
        vm.pNo = 0;
        vm.foreignProjects = [];
        vm.missions = [];
        vm.hajjDetails = [];
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

            currentStatusService.getForeignProjects(vm.pNo).then(function (data) {
                vm.foreignProjects = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });



            currentStatusService.getMissions(vm.pNo).then(function (data) {
                    vm.missions = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });


            employeeHajjDetailService.GetEmployeeHajjDetailsByPno(vm.pNo).then(function (data) {
                vm.hajjDetails = data.result;

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
