(function () {

    'use strict';
    var controllerId = 'promotionExecutionsController';
    angular.module('app').controller(controllerId, promotionExecutionsController);
    promotionExecutionsController.$inject = ['$state','$stateParams' ,'promotionExecutionService', 'notificationService', '$location'];

    function promotionExecutionsController($state, $stateParams, promotionExecutionService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.promotionExecutionId = 0;
        vm.promotionExecutions = [];
        vm.getPromotionExecutions = getPromotionExecutions;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

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
        init();
        function init() {
            promotionExecutionService.getPromotionExecutions(vm.pageSize, vm.pageNumber, vm.searchText, vm.type).then(function (data) {
                vm.promotionExecutions = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        

        function pageChanged() {
            $state.go('promotion-executions', { type: vm.type, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

        }

        function getPromotionExecutions(promotionExecution) {
            $state.go('promotion-executed-list', { type:vm.type,promotionBoardId: promotionExecution.promotionBoardId});
        }
        
        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
