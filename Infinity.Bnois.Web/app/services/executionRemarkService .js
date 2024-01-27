(function () {
    'use strict';
    angular.module('app').service('executionRemarkService', ['dataConstants', 'apiHttpService', executionRemarkService]);

    function executionRemarkService(dataConstants, apiHttpService) {
        var service = {
            getExecutionRemarkes: getExecutionRemarkes,
            getExecutionRemark: getExecutionRemark,
            saveExecutionRemark: saveExecutionRemark,
            updateExecutionRemark: updateExecutionRemark,
            deleteExecutionRemark: deleteExecutionRemark
        };

        return service;
        function getExecutionRemarkes(pageSize, pageNumber, searchText,type) {
            var url = dataConstants.EXECUTION_REMARK_URL + 'get-execution-remarks?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText+"&type="+type;
            return apiHttpService.GET(url);
        }

        function getExecutionRemark(id) {
            var url = dataConstants.EXECUTION_REMARK_URL + 'get-execution-remark?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveExecutionRemark(data) {
            var url = dataConstants.EXECUTION_REMARK_URL + 'save-execution-remark';
            return apiHttpService.POST(url, data);
        }

        function updateExecutionRemark(id, data) {
            var url = dataConstants.EXECUTION_REMARK_URL + 'update-execution-remark/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteExecutionRemark(id) {
            var url = dataConstants.EXECUTION_REMARK_URL + 'delete-execution-remark/' + id;
            return apiHttpService.DELETE(url);
        }
        
    }
})();