(function () {

    'use strict';

    var controllerId = 'resultsController';
    angular.module('app').controller(controllerId, resultsController);
    resultsController.$inject = ['$state', 'resultService', 'notificationService', '$location'];

    function resultsController($state, resultService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.results = [];
        vm.addResult = addResult;
        vm.updateResult = updateResult;
        vm.deleteResult = deleteResult;

        vm.pageChanged = pageChanged;
        vm.onSearch = onSearch;
        vm.searchText = "";
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

        if (location.search().ps !== undefined && location.search().ps != null && location.search().ps != '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn != null && location.search().pn != '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q != null && location.search().q != '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            resultService.getResults(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.results = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addResult() {
            $state.go('result-create');
        }

        function updateResult(result) {
            $state.go('result-modify', { id: result.resultId });
        }

        function deleteResult(result) {
            resultService.deleteResult(result.resultId).then(function (data) {
                init();
            });
        }

        function pageChanged() {
            $state.go('results', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
