(function () {

    'use strict';
    var controllerId = 'officerStreamsController';
    angular.module('app').controller(controllerId, officerStreamsController);
    officerStreamsController.$inject = ['$state', 'officerStreamService', 'notificationService', '$location'];

    function officerStreamsController($state, officerStreamService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.officerStreams = [];
        vm.addOfficerStream = addOfficerStream;
        vm.updateOfficerStream = updateOfficerStream;
        vm.deleteOfficerStream = deleteOfficerStream;
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
            officerStreamService.getOfficerStreams(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.officerStreams = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addOfficerStream() {
            $state.go('officer-stream-create');
        }

        function updateOfficerStream(officerStream) {
            $state.go('officer-stream-modify', {id:officerStream.officerStreamId});
        }

        function deleteOfficerStream(officerStream) {
            officerStreamService.deleteOfficerStream(officerStream.officerStreamId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });  
            });
        }

        function pageChanged() {
            $state.go('officer-streams', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
