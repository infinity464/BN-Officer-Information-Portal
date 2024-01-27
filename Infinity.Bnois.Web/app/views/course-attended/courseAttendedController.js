

(function () {

    'use strict';

    var controllerId = 'courseAttendedController';

    angular.module('app').controller(controllerId, courseAttendedController);
    courseAttendedController.$inject = ['$stateParams','employeeService','currentStatusService', 'notificationService', '$state'];

    function courseAttendedController($stateParams, employeeService, currentStatusService, notificationService, $state) {
        var vm = this;
        vm.courseAttendeds = [];
        vm.printSection = printSection;
        vm.intialHide;
    
        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }

        Init();
        function Init() {
            currentStatusService.getCourseAttended(vm.pNo).then(function (data) {
                vm.courseAttendeds = data.result;
                if (vm.courseAttendeds.length > 0) {
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
