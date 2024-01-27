(function () {

    'use strict';
    var controllerId = 'promotionExecutionWithoutBoardsController';
    angular.module('app').controller(controllerId, promotionExecutionWithoutBoardsController);
    promotionExecutionWithoutBoardsController.$inject = ['$state', 'promotionNominationService', 'promotionExecutionService', 'notificationService', '$location'];

    function promotionExecutionWithoutBoardsController($state,promotionNominationService, promotionExecutionService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.promotionExecutionId = 0;
        vm.promotionExecutions = [];
        vm.addPromotionExecutionWithoutBoard = addPromotionExecutionWithoutBoard;
        vm.updatePromotionExecutionWithoutBoard = updatePromotionExecutionWithoutBoard;
        vm.deletePromotionExecutionWithoutBoard = deletePromotionExecutionWithoutBoard;
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
            promotionExecutionService.getPromotionExecutionWithoutBoards(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.promotionNominations = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }



        function pageChanged() {
            $state.go('promotion-execution-without-boards', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

        }

        function addPromotionExecutionWithoutBoard(promotionExecution) {
            $state.go('promotion-execution-without-board-create');
        }

        function updatePromotionExecutionWithoutBoard(promotionNomination) {
            console.log(promotionNomination);
            $state.go('promotion-execution-without-board-modify', { promotionNominationId: promotionNomination.promotionNominationId });
        }

        
        function deletePromotionExecutionWithoutBoard(promotionNomination) {
            promotionNominationService.deletePromotionNomination(promotionNomination.promotionNominationId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }


        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
