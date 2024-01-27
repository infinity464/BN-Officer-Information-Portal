(function () {
    'use strict';
    angular.module('app').service('branchService', ['dataConstants', 'apiHttpService', branchService]);

    function branchService(dataConstants, apiHttpService) {
        var service = {
            getBranches: getBranches,
            getBranch: getBranch,
            saveBranch: saveBranch,
            updateBranch: updateBranch,
            deleteBranch: deleteBranch
        };

        return service;
        function getBranches(pageSize, pageNumber, searchText) {
            var url = dataConstants.BRANCH_URL + 'get-branches?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getBranch(branchId) {
			var url = dataConstants.BRANCH_URL + 'get-branch?id=' + branchId;
     
            return apiHttpService.GET(url);
        }

        function saveBranch(data) {
            var url = dataConstants.BRANCH_URL + 'save-branch';
            return apiHttpService.POST(url, data);
        }

        function updateBranch(branchId, data) {
            var url = dataConstants.BRANCH_URL + 'update-branch/' + branchId;
            return apiHttpService.PUT(url, data);
        }

        function deleteBranch(branchId) {
            var url = dataConstants.BRANCH_URL + 'delete-branch/' + branchId;
            return apiHttpService.DELETE(url);
        }


    }
})();