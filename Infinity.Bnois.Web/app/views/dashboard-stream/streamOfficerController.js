

(function () {

    'use strict';

    var controllerId = 'streamOfficerController'; 

    angular.module('app').controller(controllerId, streamOfficerController);
    streamOfficerController.$inject = ['$stateParams', '$rootScope', 'dashboardService', 'downloadService','reportService', 'notificationService', '$state'];

    function streamOfficerController($stateParams, $rootScope, dashboardService, downloadService, reportService ,notificationService, $state) {
        var vm = this;
        vm.officerDetails = officerDetails;
        vm.streamOfficers = [];
        vm.rankId = 0;
        vm.branch = '';
        vm.streamId = 0;
        vm.printSection = printSection;

        if ($stateParams.rankId !== undefined && $stateParams.rankId !== null) {
            vm.rankId = $stateParams.rankId;
          
        }

        if ($stateParams.branch !== undefined && $stateParams.branch !== null) {
            vm.branch = $stateParams.branch;
          
        }

        if ($stateParams.streamId !== undefined && $stateParams.streamId !== null) {
            vm.streamId = $stateParams.streamId;
          
        }



        Init();
        function Init() {
            dashboardService.getStreamOfficer(vm.rankId, vm.branch, vm.streamId).then(function (data) {

                vm.streamOfficers = data.result;
                vm.totalResult = vm.streamOfficers.length;

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
