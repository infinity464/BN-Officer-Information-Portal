(function () {
    'use strict';
    angular.module('app').service('subBranchService', ['dataConstants', 'apiHttpService', subBranchService]);

    function subBranchService(dataConstants, apiHttpService) {
        var service = {
            getSubBranches: getSubBranches,
            getSubBranch: getSubBranch,
            saveSubBranch: saveSubBranch,
            updateSubBranch: updateSubBranch,
            deleteSubBranch: deleteSubBranch
        };

        return service;
        function getSubBranches(pageSize, pageNumber, searchText) {
            var url = dataConstants.SUB_BRANCH_URL + 'get-sub-branches?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getSubBranch(id) {
            var url = dataConstants.SUB_BRANCH_URL + 'get-sub-branch?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveSubBranch(data) {
            var url = dataConstants.SUB_BRANCH_URL + 'save-sub-branch';
            return apiHttpService.POST(url, data);
        }

        function updateSubBranch(id, data) {
            var url = dataConstants.SUB_BRANCH_URL + 'update-sub-branch/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteSubBranch(id) {
            var url = dataConstants.SUB_BRANCH_URL + 'delete-sub-branch/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();