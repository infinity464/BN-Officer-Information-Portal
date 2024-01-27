(function () {

    'use strict';

    var controllerId = 'missionScheduleAddController';

    angular.module('app').controller(controllerId, missionScheduleAddController);
    missionScheduleAddController.$inject = ['$stateParams', 'nominationScheduleService', 'notificationService', '$state'];

    function missionScheduleAddController($stateParams, nominationScheduleService, notificationService, $state) {
        var vm = this;
        vm.missionScheduleId = 0;
        vm.title = 'ADD MODE';
        vm.missionSchedule = {};
        vm.countries = [];

        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.missionScheduleForm = {};


  

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.missionScheduleId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            nominationScheduleService.getNominationSchedule(vm.missionScheduleId).then(function (data) {
                vm.missionSchedule = data.result.nominationSchedule;
               
                 vm.countries = data.result.countries;

                if (vm.missionScheduleId !== 0 && vm.missionScheduleId !== '') {
 
                    vm.missionSchedule.fromDate = new Date(data.result.nominationSchedule.fromDate);
                    vm.missionSchedule.toDate = new Date(data.result.nominationSchedule.toDate);

                  

                }  
             

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            vm.missionSchedule.nominationScheduleType = 1;
            if (vm.missionScheduleId !== 0 && vm.missionScheduleId !== '') {
                updateMissionSchedule();
            } else {
                insertMissionSchedule();
            }
        }

        function insertMissionSchedule() {
            nominationScheduleService.saveNominationSchedule(vm.missionSchedule).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateMissionSchedule() {
            nominationScheduleService.updateNominationSchedule(vm.missionScheduleId, vm.missionSchedule).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('mission-schedules');
        }



  
    
    }
})();
