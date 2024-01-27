(function () {

    'use strict';
    var controllerId = 'batchesController';
    angular.module('app').controller(controllerId, batchesController);
    batchesController.$inject = ['$state', 'batchService', 'notificationService', '$location'];

    function batchesController($state, batchService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.batches = [];
        vm.addBatch = addBatch;
        vm.updateBatch = updateBatch;
        vm.deleteBatch = deleteBatch;
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
            batchService.getBatches(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.batches = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addBatch() {
            $state.go('batch-create');
        }

        function updateBatch(batch) {
            $state.go('batch-modify', { id: batch.batchId});
        }

        function deleteBatch(batch) {
            batchService.deleteBatch(batch.batchId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });  
            });
        }

        function pageChanged() {
            $state.go('batches', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
