

(function () {

    'use strict';

    var controllerId = 'logOutController';

    angular.module('app').controller(controllerId, logOutController);
    logOutController.$inject = ['$stateParams', 'OidcManager', 'codeValue', 'notificationService', '$state'];

    function logOutController($stateParams, OidcManager, codeValue, notificationService, $state) {
        var vm = this;
        vm.manager = OidcManager.OidcTokenManager();
        vm.userName = vm.manager.profile.preferred_username;
        //vm.homeButton1 = homeButton1;
        vm.role = vm.manager.profile.role;
        Init();
        function Init() {

        }

        //function homeButton1() {
        //    employeeService.GetLoginEmployeePno().then(function (data) {
        //        vm.pno = data.result;
        //        if (vm.pno !== null) {
        //            $state.go('employee-current-status', { pno: vm.pno });
        //        }

        //    },
        //        function (errorMessage) {
        //            notificationService.displayError(errorMessage.message);
        //        });
        //}

        vm.logOutOfIdSrv = function () {
            vm.manager.redirectForLogout();
        };

        vm.stayLogout = function () {
            
                if (vm.role === 'MemberUser') {
                    $state.go('employee-current-status', { pno: vm.pno });
                }
                if (vm.role === 'System Administrator : SO(IT), In-charge Data Room') {
                    $state.go('admin-dashboard');
                }
            
        };
    }
})();
