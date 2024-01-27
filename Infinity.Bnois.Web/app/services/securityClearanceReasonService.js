(function () {
    'use strict';
    angular.module('app').service('securityClearanceReasonService', ['dataConstants', 'apiHttpService', securityClearanceReasonService]);

    function securityClearanceReasonService(dataConstants, apiHttpService) {
        var service = {
            getSecurityClearanceReasons: getSecurityClearanceReasons,
            getSecurityClearanceReason: getSecurityClearanceReason,
            saveSecurityClearanceReason: saveSecurityClearanceReason,
            updateSecurityClearanceReason: updateSecurityClearanceReason,
            deleteSecurityClearanceReason: deleteSecurityClearanceReason
        };

        return service;
        function getSecurityClearanceReasons(pageSize, pageNumber, searchText) {
            var url = dataConstants.SECURITY_CLEARANCE_REASON_URL + 'get-security-clearance-reasons?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getSecurityClearanceReason(id) {
            var url = dataConstants.SECURITY_CLEARANCE_REASON_URL + 'get-security-clearance-reason?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveSecurityClearanceReason(data) {
            var url = dataConstants.SECURITY_CLEARANCE_REASON_URL + 'save-security-clearance-reason';
            return apiHttpService.POST(url, data);
        }

        function updateSecurityClearanceReason(id, data) {
            var url = dataConstants.SECURITY_CLEARANCE_REASON_URL + 'update-security-clearance-reason/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteSecurityClearanceReason(id) {
            var url = dataConstants.SECURITY_CLEARANCE_REASON_URL + 'delete-security-clearance-reason/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();