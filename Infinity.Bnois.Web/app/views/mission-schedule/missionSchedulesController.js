

(function () {

    'use strict';
    var controllerId = 'missionSchedulesController';
    angular.module('app').controller(controllerId, missionSchedulesController);
    missionSchedulesController.$inject = ['$state', 'nominationScheduleService', 'notificationService', '$location'];

    function missionSchedulesController($state, nominationScheduleService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.missionSchedules = [];
        vm.addMissionSchedule = addMissionSchedule;
        vm.updateMissionSchedule = updateMissionSchedule;
        vm.deleteMissionSchedule = deleteMissionSchedule;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            nominationScheduleService.getNominationSchedules(vm.pageSize, vm.pageNumber, vm.searchText,1).then(function (data) {
                vm.missionSchedules = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addMissionSchedule() {
            $state.go('mission-schedule-create');
        }

        function updateMissionSchedule(missionSchedule) {
            $state.go('mission-schedule-modify', { id: missionSchedule.nominationScheduleId });
        }

        function deleteMissionSchedule(missionSchedule) {
            nominationScheduleService.deleteNominationSchedule(missionSchedule.nominationScheduleId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('mission-schedules', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();

