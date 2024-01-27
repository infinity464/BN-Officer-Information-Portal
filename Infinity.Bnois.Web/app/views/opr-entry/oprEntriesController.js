

(function () {

    'use strict';
    var controllerId = 'oprEntriesController';
    angular.module('app').controller(controllerId, oprEntriesController);
    oprEntriesController.$inject = ['$state', 'downloadService', 'codeValue','oprEntryService','notificationService', '$location'];

    function oprEntriesController($state, downloadService, codeValue,oprEntryService,  notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.URL = codeValue.FILE_URL;
        vm.oprEntries = [];
        vm.addoprEntry = addoprEntry;
        vm.updateoprEntry = updateoprEntry;
        vm.deleteoprEntry = deleteoprEntry;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.uploadOprFile = uploadOprFile;
        vm.downloadOprFile = downloadOprFile;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

        vm.oprSpecialAppointment = oprSpecialAppointment;

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
            oprEntryService.getoprEntries(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.oprEntries = data.result;              
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addoprEntry() {
            $state.go('opr-entry-create');
        }

        function updateoprEntry(oprEntry) {
            $state.go('opr-entry-modify', { id: oprEntry.id });
        }

        function deleteoprEntry(oprEntry) {
            oprEntryService.deleteoprEntry(oprEntry.id).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('opr-entries', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }

        function oprSpecialAppointment(oprEntry) {
            $state.go('opr-special-appointments', { id: oprEntry.id });
        }
        function uploadOprFile(oprEntry) {
            $state.go('opr-file-upload', { id: oprEntry.id });
        }
        function downloadOprFile(oprEntry) {
            var url = oprEntryService.getOprFileDownloadUrl(oprEntry.id);
           downloadService.downloadFile(url);
        }

    }

})();

