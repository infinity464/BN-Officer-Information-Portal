

(function () {

    'use strict';
    var controllerId = 'changePasswordController';
    angular.module('app').controller(controllerId, changePasswordController);
    changePasswordController.$inject = ['$state','OidcManager', 'currentStatusService','notificationService', '$location'];

    function changePasswordController($state, OidcManager, currentStatusService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.manager = OidcManager.OidcTokenManager();
        vm.accountInfo = {};
        vm.PasswordChange = PasswordChange;
        vm.accountInfoForm = {};
        vm.showPass = showPass;
        init();
        function init() {
        }
        function PasswordChange(accountInfo) {
            currentStatusService.changePassword(accountInfo).then(function (data) {
                notificationService.displaySuccess("Password updated successfully");

                vm.manager.redirectForLogout();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }
        function showPass() {
            var x = document.getElementById("oldPassword");
            var y = document.getElementById("newPassword");
            var z = document.getElementById("confirmPassword");
            if (x.type === "password") {
                x.type = "text";
                y.type = "text";
                z.type = "text";
            } else {
                x.type = "password";
                y.type = "password";
                z.type = "password";
            }
        }
    }

})();

