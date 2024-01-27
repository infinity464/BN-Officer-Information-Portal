(function () {
    'use strict';
    angular.module('app').service('budgetAllocationService', ['dataConstants', 'apiHttpService', budgetAllocationService]);

    function budgetAllocationService(dataConstants, apiHttpService) {
        var service = {
            getBudgetAllocations: getBudgetAllocations,
            getBudgetAllocation: getBudgetAllocation,
            saveBudgetAllocation: saveBudgetAllocation,
            updateBudgetAllocation: updateBudgetAllocation,
            deleteBudgetAllocation: deleteBudgetAllocation

        };

        return service;
        function getBudgetAllocations(trainingId) {
            var url = dataConstants.BUDGET_ALLOCATION_URL + 'get-budget-allocations?trainingId='+trainingId;
            return apiHttpService.GET(url);
        }

        function getBudgetAllocation(budgetAllocationId) {
            var url = dataConstants.BUDGET_ALLOCATION_URL + 'get-budget-allocation?budgetAllocationId=' + budgetAllocationId;
            return apiHttpService.GET(url);
        }

        function saveBudgetAllocation(data) {
            var url = dataConstants.BUDGET_ALLOCATION_URL + 'save-budget-allocation';
            return apiHttpService.POST(url, data);
        }

        function updateBudgetAllocation(budgetAllocationId, data) {
            var url = dataConstants.BUDGET_ALLOCATION_URL + 'update-budget-allocation/' + budgetAllocationId;
            return apiHttpService.PUT(url, data);
        }

        function deleteBudgetAllocation(budgetAllocationId) {
            var url = dataConstants.BUDGET_ALLOCATION_URL + 'delete-budget-allocation/' + budgetAllocationId;
            return apiHttpService.DELETE(url);
        }

    }
})();