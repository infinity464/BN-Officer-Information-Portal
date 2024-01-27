(function () {
    'use strict';
    angular.module('app').service('remarkService', ['dataConstants', 'apiHttpService', remarkService]);

    function remarkService(dataConstants, apiHttpService) {
        var service = {
            getRemarks: getRemarks,
            getRemark: getRemark,
            saveRemark: saveRemark,
            updateRemark: updateRemark,
            deleteRemark: deleteRemark

        };

        return service;

     


        function getRemarks(pNo,type) {
            var url = dataConstants.REMARK_URL + 'get-remarks?pNo=' + pNo +'&type='+type;
            return apiHttpService.GET(url);
        }

        function getRemark(remarkId) {
            var url = dataConstants.REMARK_URL + 'get-remark?id=' + remarkId;
            return apiHttpService.GET(url);
        }


        function saveRemark(data) {
            var url = dataConstants.REMARK_URL + 'save-remark';
            return apiHttpService.POST(url, data);
        }

        function updateRemark(remarkId, data) {
            var url = dataConstants.REMARK_URL + 'update-remark/' + remarkId;
            return apiHttpService.PUT(url, data);
        }

        function deleteRemark(remarkId) {
            var url = dataConstants.REMARK_URL + 'delete-remark/' + remarkId;
            return apiHttpService.DELETE(url);
        }

    }
})();