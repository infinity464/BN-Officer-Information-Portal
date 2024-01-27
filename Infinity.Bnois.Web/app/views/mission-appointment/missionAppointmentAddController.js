(function () {

    'use strict';

    var controllerId = 'missionAppointmentAddController';

    angular.module('app').controller(controllerId, missionAppointmentAddController);
    missionAppointmentAddController.$inject = ['$stateParams', '$rootScope','$scope', 'missionAppointmentService', 'officeAppointmentService', 'notificationService', '$state'];

    function missionAppointmentAddController($stateParams, $rootScope, $scope, missionAppointmentService, officeAppointmentService ,notificationService, $state) {
        var vm = this;
        vm.missionAppointmentId = 0;
        vm.title = 'ADD MODE';
        vm.missionAppointment = {};
        vm.missions = [];
        vm.aptNats = [];
        vm.aptCats = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.missionAppointmentForm = {};
        vm.getCategoryByNature = getCategoryByNature;
        vm.localSearch = localSearch;
        vm.selected = selected;


        if ($stateParams.id > 0 && $stateParams.id !== null) {
            vm.missionAppointmentId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            missionAppointmentService.getMissionAppointment(vm.missionAppointmentId).then(function (data) {
                vm.missionAppointment = data.result.missionAppointment;
                vm.missions = data.result.missions;

                vm.aptNats = data.result.aptNats;

                vm.rankList = data.result.rankList;
                vm.branchList = data.result.branchList;
                if (vm.missionAppointmentId !== 0 && vm.missionAppointmentId !== '') {
                    vm.aptCats = data.result.aptCats;
                    changeInputValue();
                }     

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.missionAppointmentId !== 0 && vm.missionAppointmentId !== '') {

                updateMissionAppointment();
            } else {
                insertMissionAppointment();
            }
        }

        function insertMissionAppointment() {
            vm.missionAppointment.missionId = $rootScope.missionId;
            missionAppointmentService.saveMissionAppointment(vm.missionAppointment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateMissionAppointment() {
            missionAppointmentService.updateMissionAppointment(vm.missionAppointmentId, vm.missionAppointment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('mission-appointments');
        }



        function getCategoryByNature(appNatId) {
            officeAppointmentService.getCategoryByNature(appNatId).then(function (data) {
                vm.aptCats = data.result.aptCats;
            });
        }


        function selected(object) {
            vm.missionAppointment.missionScheduleId = object.originalObject.value;
           
        }


        function localSearch(str) {
            var matches = [];
            vm.missions.forEach(function (mission) {

                if ((mission.text.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0)) {
                    matches.push(mission);

                }
            });
            return matches;
        }

        function changeInputValue() {
            if (vm.missionAppointmentId > 0) {

                missionAppointmentService.getMissionSchedule(vm.missionAppointment.missionScheduleId).then(function (data) {
                    var obj = { value: vm.missionAppointment.missionScheduleId, text: data.result };
                    $scope.$broadcast('angucomplete-alt:changeInput', 'missionScheduleId', obj);

                });

            }
        }


    }
})();
