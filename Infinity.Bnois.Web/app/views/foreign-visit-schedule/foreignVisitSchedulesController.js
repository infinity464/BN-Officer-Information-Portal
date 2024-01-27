

(function () {

    'use strict';
    var controllerId = 'foreignVisitSchedulesController';
    angular.module('app').controller(controllerId, foreignVisitSchedulesController);
    foreignVisitSchedulesController.$inject = ['$state', 'nominationScheduleService', 'notificationService', '$location'];

    function foreignVisitSchedulesController($state, nominationScheduleService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.foreignVisitSchedules = [];
        vm.addForeignVisitSchedule = addForeignVisitSchedule;
        vm.updateForeignVisitSchedule = updateForeignVisitSchedule;
        vm.deleteForeignVisitSchedule = deleteForeignVisitSchedule;
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
            nominationScheduleService.getNominationSchedules(vm.pageSize, vm.pageNumber, vm.searchText,2).then(function (data) {
                vm.foreignVisitSchedules = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addForeignVisitSchedule() {
            $state.go('foreign-visit-schedule-create');
        }

        function updateForeignVisitSchedule(foreignVisitSchedule) {
            $state.go('foreign-visit-schedule-modify', { id: foreignVisitSchedule.nominationScheduleId });
        }

        function deleteForeignVisitSchedule(foreignVisitSchedule) {
            nominationScheduleService.deleteNominationSchedule(foreignVisitSchedule.nominationScheduleId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('foreign-visit-schedules', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();

