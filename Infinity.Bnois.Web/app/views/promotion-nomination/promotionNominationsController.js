(function () {
    'use strict';
    var controllerId = 'promotionNominationsController';
    angular.module('app').controller(controllerId, promotionNominationsController);
    promotionNominationsController.$inject = ['$stateParams', '$state', 'promotionNominationService', 'notificationService', '$location'];

    function promotionNominationsController($stateParams, $state, promotionNominationService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.promotionBoardId = 0;
        vm.promotionNominations = [];
        vm.addPromotionNomination = addPromotionNomination;
        vm.updatePromotionNomination = updatePromotionNomination;
        vm.deletePromotionNomination = deletePromotionNomination;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;
        vm.close = close;
        vm.type = 0;
        vm.title = '';
        vm.promotionHide = false;



        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.type = $stateParams.type;
            if (vm.type == 1) {
                vm.typeName = "Promotion";
                vm.promotionHide = true;
            }
            else {
                vm.typeName = "SASB";
                vm.promotionHide = false;
            }
        }

        if ($stateParams.title !== undefined && $stateParams.title !== null) {
            vm.title = $stateParams.title;

        }


        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }

        if ($stateParams.promotionBoardId !== undefined && $stateParams.promotionBoardId !== null) {
            vm.promotionBoardId = $stateParams.promotionBoardId;
        }
        init();
        function init() {
            promotionNominationService.getPromotionNominations(vm.promotionBoardId, vm.pageSize, vm.pageNumber, vm.searchText,vm.type).then(function (data) {
                vm.promotionNominations = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addPromotionNomination() {
            $state.go('promotion-nomination-create', { type:vm.type,title:vm.title, promotionBoardId: vm.promotionBoardId });
        }

        function updatePromotionNomination(promotionNomination) {
            $state.go('promotion-nomination-modify', { type: vm.type, title: vm.title,promotionBoardId: vm.promotionBoardId, promotionNominationId: promotionNomination.promotionNominationId });
        }

        function deletePromotionNomination(promotionNomination) {
            promotionNominationService.deletePromotionNomination(promotionNomination.promotionNominationId).then(function (data) {
                $state.go($state.current, { type: vm.type,promotionBoardId: vm.promotionBoardId, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('promotion-nominations', { type: vm.type, title: vm.title,promotionBoardId: promotionBoard.promotionBoardId, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }

        function close() {
            $state.go('promotion-boards', { type:vm.type,pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }
    }

})();
