

(function () {

    'use strict';

    var controllerId = 'categoryOfficerController'; 

    angular.module('app').controller(controllerId, categoryOfficerController);
    categoryOfficerController.$inject = ['$stateParams', '$rootScope', 'dashboardService', 'downloadService','reportService', 'notificationService', '$state'];

    function categoryOfficerController($stateParams, $rootScope, dashboardService, downloadService, reportService ,notificationService, $state) {
        var vm = this;
        vm.officerDetails = officerDetails;
        vm.categoryOfficers = [];
        vm.rankId = 0;
        vm.branch = '';
        vm.categoryId = 0;
        vm.printSection = printSection;

        if ($stateParams.rankId !== undefined && $stateParams.rankId !== null) {
            vm.rankId = $stateParams.rankId;
          
        }

        if ($stateParams.branch !== undefined && $stateParams.branch !== null) {
            vm.branch = $stateParams.branch;
          
        }

        if ($stateParams.categoryId !== undefined && $stateParams.categoryId !== null) {
            vm.categoryId = $stateParams.categoryId;
          
        }



        Init();
        function Init() {
            dashboardService.getCategoryOfficer(vm.rankId, vm.branch, vm.categoryId).then(function (data) {

                vm.categoryOfficers = data.result;
                vm.totalResult = vm.categoryOfficers.length;

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
