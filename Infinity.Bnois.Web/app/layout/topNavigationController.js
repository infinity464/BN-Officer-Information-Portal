
(function () {
    "use strict";
    var controllerId = 'TopNavigationController';

    angular.module("app").controller(controllerId, ['$state', 'feedBackService','employeeService', '$route', 'employeeLeaveService', 'OidcManager', 'userService', 'notificationService', TopNavigationController]);

    function TopNavigationController($state, feedBackService, employeeService, $route, employeeLeaveService, OidcManager, userService, notificationService) {
        var vm = this;
        vm.accountInfo = {};

        vm.manager = OidcManager.OidcTokenManager();
        vm.userName = vm.manager.profile.preferred_username;
        vm.GetEmployeeLeaveNotifications = GetEmployeeLeaveNotifications;
        vm.notificationMsg = '';
        vm.reload = reload;
        vm.homeButton = homeButton;
        vm.leaveInbox = leaveInbox;
        vm.changePass = changePass;
        vm.notificationUser = 0;
        vm.logOut = function () {
            vm.manager.removeToken();
            window.location = "/";
        }

        vm.logOutOfIdSrv = function () {
            vm.manager.redirectForLogout();
        }


        vm.changePassword = function (accountInfo) {

            userService.changePassword(accountInfo).then(function (data) {
                notificationService.displaySuccess('Password Changed Successfully.!!');
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        };
        Init();
        function Init() {
            feedBackService.getFeedbackForUserNotification().then(function (data) {
                vm.notificationUser = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            //GetEmployeeLeaveNotifications();
            angular.element(document.querySelector(".modal-backdrop")).removeClass("modal-backdrop");

            
        };


        function reload() {
            location.reload();
        }
        function changePass() {
            $state.go('change-password');
        }
        function homeButton() {
            $state.go('employee-current-status');
        }
        function GetEmployeeLeaveNotifications() {
            employeeLeaveService.GetEmployeeLeaveNotifications().then(function (data) {
                vm.notificationMsg = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function leaveInbox() {
            if (vm.userName === 'pinkDesk' || vm.userName === 'nominationDesk' || vm.userName === 'aocw' || vm.userName === 'sona1' || vm.userName === 'nominationDesk' || vm.userName === 'nominationDesk') {
                $state.go('ns-dashboard');

            } else {
                $state.go('employee-leave-inbox');

            }
        }

    }

}());
