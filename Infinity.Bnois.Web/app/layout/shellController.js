(function () {
    'use strict';

    var controllerId = 'ShellController';
    angular.module('app').controller(controllerId, ['$state', 'feedBackService','employeeService', '$location', 'OidcManager', 'notificationService', '$rootScope', 'common', 'config', '$routeParams', shell]);

    function shell($state, feedBackService, employeeService, $location, OidcManager, notificationService, $rootScope, common,config, $routeParams) {

        var vm = this;
        vm.manager = OidcManager.OidcTokenManager();
        vm.userName = vm.manager.profile.preferred_username;
        vm.role = vm.manager.profile.role;
        vm.check = false;
        //vm.showLayout = true;

        vm.insertFeedBack = insertFeedBack;
        vm.feedBackFloat = {};
        vm.feedBackForm = {};


        vm.printSection = printSection;
        if ($location.path() === '/') {
            if (vm.role === 'MemberUser') {
                $state.go('employee-current-status', { pno: vm.pno });
            }
            if (vm.role === 'System Administrator : SO(IT), In-charge Data Room') {
                $state.go('admin-dashboard');
            }
        }
        vm.title = 'shell';
        var events = config.events;
        vm.busyMessage = 'Please wait ...';

        vm.spinnerOptions = {
            radius: 20,
            lines: 5,
            length: 0,
            width: 15,
            speed: 1.7,
            corners: 1.0,
            trail: 100,
            color: '#3BBF38',
            className: 'dw-spinner'
        };

        vm.logOutOfIdSrv = function () {
            vm.manager.redirectForLogout();
        };
        activate();

        function activate() {
            common.activateController([], controllerId);
        }
        function insertFeedBack() {
            feedBackService.saveFeedBack(vm.feedBackFloat).then(function (data) {
                vm.feedBackFloat = {};
                var x = document.getElementById("floatBerBox");
                x.style.display = "none";
                notificationService.displaySuccess("Your feedback submitted successfully");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function printSection(printSectionId) {
            console.log($location.path());
            if ($location.path() === '/log-out') {
                notificationService.displayError("Printing is  not allowed....");
            }
            else if ($location.path() === '/current-status-opr-grading' ) {
                notificationService.displayError("Printing is  not allowed....");

            }
            else if ($location.path() === '/useful-link') {
                notificationService.displayError("Printing is  not allowed....");

            }
            else if ($location.path() === '/contact') {
                notificationService.displayError("Printing is  not allowed....");

            }
            else {
                console.log($location.path());
                var innerContents = document.getElementById(printSectionId).innerHTML;
                var popupWindow = window.open('', '', 'width=1000,height=700,left=0,top=0,toolbar=0,status=0');
                popupWindow.document.open();
                popupWindow.document.write('<HTML>\n<HEAD>\n');
                //popupWindow.document.write('<link rel="stylesheet" type="text/css" href="../../../Content/addmin/PdfReport.css"/>');
                popupWindow.document.write('<link rel="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css"/>');
                popupWindow.document.write('</HEAD>\n');
                popupWindow.document.write('<BODY  onload="window.print()"> \n');
                popupWindow.document.write(innerContents);
                popupWindow.document.write('</BODY>\n');
                popupWindow.document.write('</HTML>\n');
                popupWindow.document.close();
                popupWindow.focus();
                popupWindow.print();
                popupWindow.close();
            }
        }
    }
})();
