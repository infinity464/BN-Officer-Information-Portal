

(function () {

    'use strict';
    var controllerId = 'transferFuturePlansController';
    angular.module('app').controller(controllerId, transferFuturePlansController);
    transferFuturePlansController.$inject = ['$state','$stateParams', 'transferFuturePlanService', 'notificationService', '$location'];

    function transferFuturePlansController($state, $stateParams, transferFuturePlanService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.transferFuturePlans = [];
        vm.addTransferFuturePlan = addTransferFuturePlan;
        vm.updateTransferFuturePlan = updateTransferFuturePlan;
        vm.deleteTransferFuturePlan = deleteTransferFuturePlan;
        vm.searchText = "";
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;
        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }
       
        init();
        function init() {
            transferFuturePlanService.getTransferFuturePlans(vm.pNo).then(function (data) {
                vm.transferFuturePlans = data.result;
                    vm.permission = data.permission;
               
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addTransferFuturePlan() {
            $state.go('current-status-tab.transfer-future-plan-create');
        }

        function updateTransferFuturePlan(transferFuturePlan) {
            
            $state.go('current-status-tab.transfer-future-plan-modify', { id: transferFuturePlan.employeeTransferFuturePlanId });
        }

        function deleteTransferFuturePlan(transferFuturePlan) {

            transferFuturePlanService.deleteTransferFuturePlan(transferFuturePlan.employeeTransferFuturePlanId).then(function (data) {
                transferFuturePlanService.getTransferFuturePlans(vm.pNo).then(function(data) {
                    vm.transferFuturePlans = data.result;

                });
            });
        }

       

    }

})();

