(function () {
    'use strict';
    angular.module('app').service('budgetService', ['dataConstants', 'apiHttpService', budgetService]);

    function budgetService(dataConstants, apiHttpService) {
        var service = {
            getBudgets: getBudgets,
            getBudget: getBudget,
            saveBudget: saveBudget,
            updateBudget: updateBudget,
            deleteBudget: deleteBudget,
            GetMainHeadByAccountHeadId: GetMainHeadByAccountHeadId
        };

        return service;
        function getBudgets(pageSize, pageNumber, searchText) {
            var url = dataConstants.BUDGET_URL + 'get-budgets?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getBudget(budgetId) {
            var url = dataConstants.BUDGET_URL + 'get-budget?budgetId=' + budgetId;
            return apiHttpService.GET(url);
        }

        function saveBudget(data) {
            var url = dataConstants.BUDGET_URL + 'save-budget';
            return apiHttpService.POST(url, data);
        }

        function updateBudget(budgetId, data) {
            var url = dataConstants.BUDGET_URL + 'update-budget/' + budgetId;
            return apiHttpService.PUT(url, data);
        }

        function deleteBudget(budgetId) {
            var url = dataConstants.BUDGET_URL + 'delete-budget/' + budgetId;
            return apiHttpService.DELETE(url);
        }
        function GetMainHeadByAccountHeadId(accountHeadId) {
            var url = dataConstants.MAIN_HEAD_URL + 'get-main-heads?accountHeadId=' + accountHeadId;
            return apiHttpService.GET(url);
        }
        
    }
})();