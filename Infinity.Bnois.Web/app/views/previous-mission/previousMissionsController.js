(function () {

    'use strict';
    var controllerId = 'previousMissionsController';
    angular.module('app').controller(controllerId, previousMissionsController);
    previousMissionsController.$inject = ['$stateParams', '$state', 'previousMissionService', 'notificationService'];

    function previousMissionsController($stateParams, $state, previousMissionService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeId = 0;
        vm.previousMissionId = 0;
        vm.previousMissions = [];
        vm.title = 'Previous Mission';
        vm.addPreviousMission = addPreviousMission;
        vm.updatePreviousMission = updatePreviousMission;
        vm.deletePreviousMission = deletePreviousMission;
        vm.back = back;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            previousMissionService.getPreviousMissions(vm.employeeId).then(function (data) {
                vm.previousMissions = data.result;  
                    vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
        function addPreviousMission() {
            $state.go('employee-tabs.previous-mission-create', { id: vm.employeeId, previousMissionId: vm.previousMissionId });
        }
        
        function updatePreviousMission(previousMission) {
            $state.go('employee-tabs.previous-mission-modify', { id: vm.employeeId, previousMissionId: previousMission.previousMissionId });
        }


        function deletePreviousMission(previousMission) {
            previousMissionService.deletePreviousMission(previousMission.previousMissionId).then(function (data) {

                previousMissionService.getPreviousMissions(vm.employeeId).then(function(data) {
                    vm.previousMissions = data.result;
                });
            });
        }


        function back() {
            $state.go('employee-tabs.employee-previous-experiences');
        }

    }

})();
