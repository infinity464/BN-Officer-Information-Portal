(function () {

    'use strict';

    var controllerId = 'downloadViewController';

    angular.module('app').controller(controllerId, downloadViewController);
    downloadViewController.$inject = ['$stateParams','downloadfileService', 'codeValue', 'notificationService', '$state', '$location'];

    function downloadViewController($stateParams, downloadfileService, codeValue, notificationService, $state, location) {
        var vm = this;
        vm.childrens = [];
        vm.intialHide;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;
        vm.pageChanged = pageChanged;
        vm.downloads = {};
        vm.createDownload = createDownload;
        vm.updateDownload = updateDownload;
        vm.deleteDownload = deleteDownload;


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
            downloadfileService.getDownloads(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.downloads = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function pageChanged() {
            $state.go('download-list', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function createDownload() {
            $state.go('download-create');
        }

        function updateDownload(user) {
            $state.go('download-modify', { id: user.downloadId });
        }

        function deleteDownload(id) {
            downloadfileService.deleteDownload(id).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }
})();