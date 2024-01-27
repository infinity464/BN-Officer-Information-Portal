

(function () {

    'use strict';

    var controllerId = 'outsideNavyOfficerController'; 

    angular.module('app').controller(controllerId, outsideNavyOfficerController);
    outsideNavyOfficerController.$inject = ['$stateParams', '$rootScope', 'dashboardService', 'downloadService','reportService', 'notificationService', '$state'];

    function outsideNavyOfficerController($stateParams, $rootScope, dashboardService, downloadService, reportService ,notificationService, $state) {
        var vm = this;
        vm.officerDetails = officerDetails;
        vm.outsideNavyOfficers = [];
        vm.officeId = 0;
        vm.rankLevel = 0;
        vm.parentId = 0;
        vm.printSection = printSection;

        if ($stateParams.officeId !== undefined && $stateParams.officeId !== null) {
            vm.officeId = $stateParams.officeId;
          
        }

        if ($stateParams.rankLevel !== undefined && $stateParams.rankLevel !== null) {
            vm.rankLevel = $stateParams.rankLevel;
          
        }

        if ($stateParams.parentId !== undefined && $stateParams.parentId !== null) {
            vm.parentId = $stateParams.parentId;
          
        }



        Init();
        function Init() {
            dashboardService.getOutsideNavyOfficer(vm.officeId, vm.rankLevel, vm.parentId).then(function (data) {

                    vm.outsideNavyOfficers = data.result;
                    vm.totalResult = vm.outsideNavyOfficers.length;

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
      

        function officerDetails(pNo) {
            $state.goNewTab('current-status-tab', { pno: pNo });

        }

    }
})();
