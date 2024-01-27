(function () {

    'use strict';

    var controllerId = 'nominationAddController';

    angular.module('app').controller(controllerId, nominationAddController);
    nominationAddController.$inject = ['$stateParams','$scope', 'nominationService','missionAppointmentService', 'notificationService', '$state'];

    function nominationAddController($stateParams, $scope, nominationService, missionAppointmentService, notificationService, $state) {
        var vm = this;
        vm.nominationId = 0;
        vm.title = 'ADD MODE';
        vm.nomination = {};
        vm.nominationFor = {};
        vm.nominationTypes = [];
        vm.missionAppointments = [];
        vm.nominationTypeResults = [];

        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.nominationForm = {};
        vm.type = 0;
        vm.typeName = null;
        vm.appointment = false;
        vm.transfer = true;
        vm.getNominationResultsByType = getNominationResultsByType;
        vm.getMissionAppointmentByMission = getMissionAppointmentByMission;
        vm.localSearch = localSearch;
        vm.selected = selected;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.nominationId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }


        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.type = $stateParams.type;
            if (vm.type == 1) {
                vm.typeName = "Course";
                vm.transfer = false;
            }
            else if (vm.type == 2) {
                vm.typeName = "Mission";
                vm.appointment = true;
            } else if (vm.type == 3) {
                vm.typeName = "Foreign Visit";
            }
            else {

                vm.typeName = "Other";
            }
        }


        Init();
        function Init() {
            nominationService.getNomination(vm.type,vm.nominationId).then(function (data) {
                vm.nomination = data.result.nomination;
          
                vm.nominationTypes = data.result.nominationTypes;
                vm.nominationTypeResults = data.result.nominationTypeResults;
                vm.nomination.enitityType = vm.type;


//                getNominationResultsByType(vm.type);

                if (vm.nominationId !== 0 && vm.nominationId !== '') {
                    if (vm.nomination.entryDate != null) {
                        vm.nomination.entryDate = new Date(data.result.nomination.entryDate);

                    }
                    getMissionAppointmentByMission(data.result.nomination.entityId);
                    changeInputValue();
                } 

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {


            if (vm.nominationId !== 0 && vm.nominationId !== '') {
                updateNomination();
            } else {
                insertNomination();

            }
        }

        function insertNomination() {

            nominationService.saveNomination(vm.nomination).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateNomination() {
            nominationService.updateNomination(vm.nominationId, vm.nomination).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('nominations', {type:vm.type});
        }

        function getMissionAppointmentByMission(missionId) {
            if (vm.type == 2) {
                missionAppointmentService.getMissionAppointmentByMission(missionId).then(function (data) {
                    vm.missionAppointments = data.result;
                  
                });
            }        

        }

        function getNominationResultsByType(entityType) {
            nominationService.getNominationResultsByType(entityType).then(function (data) {
                vm.nominationTypeResults = data.result.nominationTypeResults;
            });

        }


        function selected(object) {
            vm.nomination.entityId = object.originalObject.value;
            if (vm.type == 2) {
                missionAppointmentService.getMissionAppointmentByMission(vm.nomination.entityId).then(function (data) {
                    vm.missionAppointments = data.result;

                });
            }    
        }


        function localSearch(str) {
            var matches = [];
            vm.nominationTypeResults.forEach(function (nomination) {
               
                if ((nomination.text.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0))
                    {
                    matches.push(nomination);
                        
                   }
            });
            return matches;
        }

        function changeInputValue() {
            if (vm.nominationId > 0) {

                nominationService.getNominationSchedule(vm.nomination.entityId, vm.nomination.enitityType).then(function (data) {
                    var obj = { value: vm.nomination.entityId, text: data.result };
                    $scope.$broadcast('angucomplete-alt:changeInput', 'scheduleId', obj);

                });

            }
        }


    }

  

})();
