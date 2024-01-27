(function () {

    'use strict';
    var controllerId = 'previousMissionAddController';
    angular.module('app').controller(controllerId, previousMissionAddController);
    previousMissionAddController.$inject = ['$stateParams', '$state', 'previousMissionService', 'notificationService'];

    function previousMissionAddController($stateParams, $state, previousMissionService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.previousMissionId = 0;
        vm.previousMission = {};
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.previousMissionForm = {};
        vm.countries = [];


        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.previousMissionId > 0) {
            vm.previousMissionId = $stateParams.previousMissionId;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
            
        }
        init();
        function init() {
            previousMissionService.getPreviousMission(vm.previousMissionId).then(function (data) {
                vm.previousMission = data.result.previousMission;
                if (vm.previousMission.fromDate != null) {
                    vm.previousMission.fromDate = new Date(vm.previousMission.fromDate);

                }
                    if (vm.previousMission.toDate != null) {
                        vm.previousMission.toDate = new Date(vm.previousMission.toDate);

                    }
                vm.countries = data.result.countries;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        
        function save() {
            if (vm.previousMissionId > 0 && vm.previousMissionId !== '') {
                updatePreviousMission();
            } else {
                insertPreviousMission();
            }
        }
        function insertPreviousMission() {
            previousMissionService.savePreviousMission(vm.employeeId, vm.previousMission).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePreviousMission() {
            previousMissionService.updatePreviousMission(vm.previousMissionId, vm.previousMission).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function close() {
            $state.go('employee-tabs.previous-missions');
        }
    }

})();
