


(function () {

    'use strict';

    var controllerId = 'userLogController';

    angular.module('app').controller(controllerId, userLogController);
    userLogController.$inject = ['$stateParams', 'codeValue', 'notificationService', 'currentStatusService', '$state'];

    function userLogController($stateParams, codeValue, notificationService, currentStatusService, $state) {
        var vm = this;
        vm.userLogForm = {};
        vm.userLogModel = {};
        vm.userLogData = {};
        vm.SearchUserLog = SearchUserLog;
        Init();
        function Init() {
            currentStatusService.GetUserEmailContactLog().then(function (data) {
                vm.userLogModel = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function SearchUserLog() {
            currentStatusService.GetUserEmailContactLogData(vm.userLogModel).then(function (data) {
                vm.userLogData = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
    }
})();
