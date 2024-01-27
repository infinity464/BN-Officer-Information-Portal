

(function () {

    'use strict';
    var controllerId = 'observationIntelligentsController';
    angular.module('app').controller(controllerId, observationIntelligentsController);
    observationIntelligentsController.$inject = ['$state', 'observationIntelligentService', 'notificationService', '$location'];

    function observationIntelligentsController($state, observationIntelligentService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.observationIntelligents = [];
        vm.addObservationIntelligent = addObservationIntelligent;
        vm.updateObservationIntelligent = updateObservationIntelligent;
        vm.deleteObservationIntelligent = deleteObservationIntelligent;
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
            observationIntelligentService.getObservationIntelligents(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.observationIntelligents = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addObservationIntelligent() {
            $state.go('observation-intelligent-report-create');
        }

        function updateObservationIntelligent(observationIntelligent) {
            $state.go('observation-intelligent-report-modify', { id: observationIntelligent.observationIntelligentId });
        }

        function deleteObservationIntelligent(observationIntelligent) {
            observationIntelligentService.deleteObservationIntelligent(observationIntelligent.observationIntelligentId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('observation-intelligent-reports', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();

