

(function () {

    'use strict';

    var controllerId = 'leaveOfficerController'; 

    angular.module('app').controller(controllerId, leaveOfficerController);
    leaveOfficerController.$inject = ['$stateParams', '$rootScope', 'dashboardService', 'downloadService','reportService', 'notificationService', '$state'];

    function leaveOfficerController($stateParams, $rootScope, dashboardService, downloadService, reportService ,notificationService, $state) {
        var vm = this;
        vm.officerDetails = officerDetails;
        vm.leaveOfficers = [];
        vm.leaveType = 0;
        vm.rankLevel = 0;
        vm.type = 0;
        vm.printSection = printSection;

        if ($stateParams.leaveType !== undefined && $stateParams.leaveType !== null) {
            vm.leaveType = $stateParams.leaveType;
          
        }

        if ($stateParams.rankLevel !== undefined && $stateParams.rankLevel !== null) {
            vm.rankLevel = $stateParams.rankLevel;
          
        }


        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.type = $stateParams.type;
          
        }



        Init();
        function Init() {

            if (vm.type == 1) {
                dashboardService.getLeaveOfficer(vm.leaveType, vm.rankLevel).then(function(data) {

                        vm.leaveOfficers = data.result;
                        vm.totalResult = vm.leaveOfficers.length;

                    },
                    function(errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
            }
            else {
                dashboardService.getExBDLeaveOfficer(vm.rankLevel).then(function (data) {

                        vm.leaveOfficers = data.result;
                        vm.totalResult = vm.leaveOfficers.length;

                    },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
            }
          
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
      

        function officerDetails(pNo) {
            $state.goNewTab('current-status-tab', { pno: pNo });

        }

    }
})();
