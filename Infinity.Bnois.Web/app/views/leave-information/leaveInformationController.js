

(function () {

    'use strict';

    var controllerId = 'leaveInformationController';

    angular.module('app').controller(controllerId, leaveInformationController);
    leaveInformationController.$inject = ['$stateParams', 'employeeService', 'currentStatusService', 'employeeLeaveService', 'notificationService', '$state'];

    function leaveInformationController($stateParams, employeeService, currentStatusService, employeeLeaveService, notificationService, $state) {
        var vm = this;
        vm.pNo = 0;
        vm.leaveInfos = [];
        vm.leaveInfo = {};
        vm.printSection = printSection;
        vm.leaveTracking = leaveTracking;
        vm.leaveTranckingList = [];
        vm.intialHide;
        vm.intialHide1 = false;

        //if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
        //    vm.pNo = $stateParams.pno;
        //}

        Init();
        function Init() {
            //employeeService.getEmployeeByPno().then(function (data) {
            //    vm.officerName = data.result.fullNameEng;
            //    vm.pNo = data.result.pNo;
            //    if (vm.pNo !== null) {
            //        currentStatusService.getLeaveInfo(vm.PNo).then(function (data) {
            //            vm.leaveInfo = data.result;
            //            vm.plAvailable = Math.abs(vm.leaveInfo.plAvailable);
            //            if (Object.keys(vm.leaveInfo).length > 0) {
            //                vm.intialHide1 = true;
            //            }
            //        },
            //            function (errorMessage) {
            //                notificationService.displayError(errorMessage.message);
            //            });
            //        employeeLeaveService.getEmployeeLeaveAndEmployeeInfo(vm.PNo).then(function (data) {
            //            vm.leaveInfos = data.result.leaveDetails;
            //        },
            //            function (errorMessage) {
            //                notificationService.displayError(errorMessage.message);
            //            });

                    

            //        if (Object.keys(vm.leaveInfo).length > 0 && vm.leaveInfos.length === 0) {
            //            vm.intialHide = true;
            //        }
            //        else {
            //            vm.intialHide = false;
            //        }
            //    }
                
            //},
            //    function (errorMessage) {
            //        notificationService.displayError(errorMessage.message);
            //    });

            currentStatusService.getLeaveInfo(vm.searchText).then(function (data) {
                vm.leaveInfo = data.result;
                vm.plAvailable = Math.abs(vm.leaveInfo.plAvailable);
                if (Object.keys(vm.leaveInfo).length > 0) {
                    vm.intialHide1 = true;
                }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getEmployeeLeaveAndEmployeeInfo(vm.searchText).then(function (data) {
                //vm.leaveInfos = data.result.leaveDetails;
                vm.leaveInfos = data.result.leaveDetails;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
            if (Object.keys(vm.leaveInfo).length > 0 && vm.leaveInfos.length === 0) {
                vm.intialHide = true;
            }
            else {
                vm.intialHide = false;
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

        function leaveTracking(employeeLeave) {

            $state.go('leave-tracking', { id: employeeLeave });

        }



    }
})();
