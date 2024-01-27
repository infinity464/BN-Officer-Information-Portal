(function () {
    'use strict';
    angular.module('app').service('previousLeaveService', ['dataConstants', 'apiHttpService', previousLeaveService]);

    function previousLeaveService(dataConstants, apiHttpService) {
        var service = {
            getPreviousLeaves: getPreviousLeaves,
            getPreviousLeave: getPreviousLeave,
            savePreviousLeave: savePreviousLeave,
            updatePreviousLeave: updatePreviousLeave,
            deletePreviousLeave: deletePreviousLeave
        };

        return service;


        function getPreviousLeaves(employeeId) {
            var url = dataConstants.PREVIOUS_LEAVE_URL + 'get-previous-leaves?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getPreviousLeave(previousLeaveId) {
            var url = dataConstants.PREVIOUS_LEAVE_URL + 'get-previous-leave?&previousLeaveId=' + previousLeaveId;
            return apiHttpService.GET(url);
        }

        function savePreviousLeave(employeeId, data) {
            var url = dataConstants.PREVIOUS_LEAVE_URL + 'save-previous-leave/' + employeeId;
            return apiHttpService.POST(url, data);
        }
        function updatePreviousLeave(previousLeaveId, data) {
            var url = dataConstants.PREVIOUS_LEAVE_URL + 'update-previous-leave/' + previousLeaveId;
            return apiHttpService.PUT(url, data);
        }

        function deletePreviousLeave(id) {
            var url = dataConstants.PREVIOUS_LEAVE_URL + 'delete-previous-leave/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();