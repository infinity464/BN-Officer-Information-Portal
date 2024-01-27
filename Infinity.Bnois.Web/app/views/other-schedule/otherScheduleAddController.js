(function () {

    'use strict';

    var controllerId = 'otherScheduleAddController';

    angular.module('app').controller(controllerId, otherScheduleAddController);
    otherScheduleAddController.$inject = ['$stateParams', 'nominationScheduleService', 'notificationService', '$state'];

    function otherScheduleAddController($stateParams, nominationScheduleService, notificationService, $state) {
        var vm = this;
        vm.otherScheduleId = 0;
        vm.title = 'ADD MODE';
        vm.otherSchedule = {};

        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.otherScheduleForm = {};


  

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.otherScheduleId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            nominationScheduleService.getNominationSchedule(vm.otherScheduleId).then(function (data) {
                vm.otherSchedule = data.result.nominationSchedule;
               

                if (vm.otherScheduleId !== 0 && vm.otherScheduleId !== '') {
 
                    vm.otherSchedule.fromDate = new Date(data.result.nominationSchedule.fromDate);
                    vm.otherSchedule.toDate = new Date(data.result.nominationSchedule.toDate);

                  

                }  
             

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            vm.otherSchedule.nominationScheduleType = 3;
            if (vm.otherScheduleId !== 0 && vm.otherScheduleId !== '') {
                updateOtherSchedule();
            } else {
                insertOtherSchedule();
            }
        }

        function insertOtherSchedule() {
            nominationScheduleService.saveNominationSchedule(vm.otherSchedule).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateOtherSchedule() {
            nominationScheduleService.updateNominationSchedule(vm.otherScheduleId, vm.otherSchedule).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('other-schedules');
        }



  
    
    }
})();
