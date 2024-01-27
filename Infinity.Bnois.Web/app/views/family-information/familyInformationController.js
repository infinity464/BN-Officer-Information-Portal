

(function () {

    'use strict';

    var controllerId = 'familyInformationController';

    angular.module('app').controller(controllerId, familyInformationController);
    familyInformationController.$inject = ['$stateParams','codeValue','employeeService','currentStatusService', 'notificationService', '$state'];

    function familyInformationController($stateParams, codeValue, employeeService,currentStatusService, notificationService, $state) {
        var vm = this;

        vm.url = codeValue.IMAGE_URL;
        vm.childrens = [];
        vm.siblings = [];
        vm.nextOfKins = [];
        vm.parentInfos = [];
        vm.spouseInfos = [];
        vm.heirs = [];
        vm.hide = false;
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

            currentStatusService.getChildren(vm.pNo).then(function (data) {
                vm.childrens = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getSibling(vm.pNo).then(function (data) {
                    vm.siblings = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getNextOfKin(vm.pNo).then(function (data) {
                vm.nextOfKins = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getHeir(vm.pNo).then(function (data) {
                vm.heirs = data.result;
                
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getParentInfo(vm.pNo).then(function (data) {
                    vm.parentInfos = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getSpouseInfo(vm.pNo).then(function (data) {
                vm.spouseInfos = data.result;

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
