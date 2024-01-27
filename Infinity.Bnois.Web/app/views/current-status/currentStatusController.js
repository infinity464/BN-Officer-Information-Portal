

(function () {

    'use strict';

    var controllerId = 'currentStatusController';

    angular.module('app').controller(controllerId, currentStatusController);
    currentStatusController.$inject = ['$stateParams', 'employeeService', 'currentStatusService', 'notificationService','codeValue', '$state'];

    function currentStatusController($stateParams, employeeService, currentStatusService, notificationService, codeValue, $state) {
        var vm = this;
        vm.pNo = 0;
        vm.currentStatus = {};
        vm.notifications = [];
        vm.updateStatus = '';
        vm.printSection = printSection;
        vm.specialNotification = true;
        vm.UpdateEmployeeEmailContact = UpdateEmployeeEmailContact;
        vm.ContactForm = '';
        vm.photo = '';
        Init();
        function Init() {
            currentStatusService.getCurrentStatus().then(function (data) {
                    vm.currentStatus = data.result;
                if (vm.currentStatus.photo != null) {
                    vm.currentStatus.photo = codeValue.API_OFFICER_PICTURE_URL + vm.currentStatus.photo;
                    vm.photo ="'"+vm.currentStatus.photo+"'";
                } else {
                    vm.currentStatus.photo = "../../../Images/officer.png";
                    vm.photo = "../../../Images/officer.png";
                }
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function UpdateEmployeeEmailContact() {
            currentStatusService.UpdateEmployeeEmailContact(vm.currentStatus.pNo, vm.currentStatus.email, vm.currentStatus.contactNo).then(function (data) {
                if (data.result == true) {
                    notificationService.displaySuccess("Updated successfully.");
                }
                else {
                    notificationService.displayError("Something went wrong.");
                }
                vm.updateStatus = data.result;
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
