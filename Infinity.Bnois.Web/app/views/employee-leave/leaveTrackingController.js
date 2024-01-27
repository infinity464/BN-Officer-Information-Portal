(function () {

    'use strict';
    var controllerId = 'leaveTrackingController';
    angular.module('app').controller(controllerId, leaveTrackingController);
    leaveTrackingController.$inject = ['employeeLeaveService', 'notificationService', '$stateParams'];

    function leaveTrackingController(employeeLeaveService, notificationService, $stateParams) {

        /* jshint validthis:true */
        var vm = this;
        vm.leaveId = 0;
        vm.leaveTranckingList = [];



        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.leaveId = $stateParams.id;
        }

        init();
        function init() {

            employeeLeaveService.leaveTrackingByLeaveId(vm.leaveId).then(function (data) {
                vm.leaveTranckingList = data.result;

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }

       
    }

})();
