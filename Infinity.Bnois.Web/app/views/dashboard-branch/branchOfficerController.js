

(function () {

    'use strict';

    var controllerId = 'branchOfficerController'; 

    angular.module('app').controller(controllerId, branchOfficerController);
    branchOfficerController.$inject = ['$stateParams', '$rootScope', 'dashboardService', 'downloadService','reportService', 'notificationService', '$state'];

    function branchOfficerController($stateParams, $rootScope, dashboardService, downloadService, reportService ,notificationService, $state) {
        var vm = this;
        vm.officerDetails = officerDetails;
        vm.branchOfficers = [];
        vm.rankId = 0;
        vm.branch = '';
        vm.printSection = printSection;

        if ($stateParams.rankId !== undefined && $stateParams.rankId !== null) {
            vm.rankId = $stateParams.rankId;

        }

        if ($stateParams.branch !== undefined && $stateParams.branch !== null) {
            vm.branch = $stateParams.branch;

        }



        Init();
        function Init() {
            dashboardService.getBranchOfficer(vm.rankId, vm.branch).then(function (data) {

                vm.branchOfficers = data.result;
                vm.totalResult = vm.branchOfficers.length;

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
