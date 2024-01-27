(function () {
    'use strict';
    var controllerId = 'executionRemarksController';
    angular.module('app').controller(controllerId, executionRemarksController);
    executionRemarksController.$inject = ['$state','$stateParams', 'executionRemarkService', 'notificationService', '$location'];

    function executionRemarksController($state, $stateParams, executionRemarkService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.executionRemarks = [];
        vm.addExecutionRemark = addExecutionRemark;
        vm.updateExecutionRemark = updateExecutionRemark;
        vm.deleteExecutionRemark = deleteExecutionRemark;
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
            executionRemarkService.getExecutionRemarkes(vm.pageSize, vm.pageNumber, vm.searchText, vm.type).then(function (data) {
                vm.executionRemarks = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addExecutionRemark() {
            $state.go('execution-remark-create', {type:vm.type});
        }

        function updateExecutionRemark(executionRemark) {
            $state.go('execution-remark-modify', { type: vm.type, id: executionRemark.executionRemarkId });
        }

        function deleteExecutionRemark(executionRemark) {
            executionRemarkService.deleteExecutionRemark(executionRemark.executionRemarkId).then(function (data) {
                $state.go($state.current, { type: vm.type, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('execution-remarks', { type: vm.type, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
