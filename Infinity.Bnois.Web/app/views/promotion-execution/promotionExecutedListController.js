(function () {

    'use strict';
    var controllerId = 'promotionExecutedListController';
    angular.module('app').controller(controllerId, promotionExecutedListController);
    promotionExecutedListController.$inject = ['$stateParams', '$state', 'promotionExecutionService', 'notificationService', '$location'];

    function promotionExecutedListController($stateParams, $state, promotionExecutionService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'UPDATE MODE';
        vm.promotionBoardId = 0;
        vm.promotionNominations = [];
        vm.executionRemarks = [];
        vm.getNominationListToUpdate = getNominationListToUpdate;
        vm.save = save;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;
        vm.close = close;
        vm.goToList = goToList;
        vm.disableDate = disableDate;

        vm.isDisableEffectiveDate = [];


        vm.type = 0;
        vm.promotionHide = true;



        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.type = $stateParams.type;
            if (vm.type == 1) {
                vm.typeName = "Promotion";
                vm.promotionHide = false;
            }
            else {

                vm.typeName = "SASB";
                vm.promotionHide = true;
            }
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
            promotionExecutionService.getPromotionExecutedList(vm.promotionBoardId,vm.type).then(function (data) {
                vm.promotionNominations = data.result.promotionNominations;
                for (var i = 0; i < vm.promotionNominations.length; i++) {
                    if (vm.promotionNominations[i].effectiveDate !== null) {
                        vm.promotionNominations[i].effectiveDate = new Date(vm.promotionNominations[i].effectiveDate);
                        vm.isDisableEffectiveDate[i] = true;
                    }
                }
                vm.executionRemarks = data.result.executionRemarks;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            promotionExecutionService.updatePromotionExecutionList(vm.promotionBoardId, vm.promotionNominations).then(function (data) {
                vm.promotionNominations = data.result.promotionNominations;
                goToList();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }

        function getNominationListToUpdate() {
            $state.go('promotion-execution-list-modify', { type: vm.type, promotionBoardId: vm.promotionBoardId });
        }
        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
      
        function close() {
            $state.go('promotion-executions', { type: vm.type, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function goToList() {
            $state.go('promotion-executed-list', { type: vm.type, promotionBoardId: vm.promotionBoardId });
        }

        
        function disableDate(value, index) {
            if (value != 2 && value !=11) {
                vm.isDisableEffectiveDate[index] = false;
                vm.promotionNominations[index].effectiveDate = null;
            }
            else {
                vm.isDisableEffectiveDate[index] = true;
            }
        }
        
    }

})();
