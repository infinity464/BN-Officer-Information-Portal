(function () {
    'use strict';
    angular.module('app').service('backLogService', ['dataConstants', 'apiHttpService', backLogService]);

    function backLogService(dataConstants, apiHttpService) {
        var service = {
            getBackLogSelectModels: getBackLogSelectModels
           
        };

        return service;
    
        function getBackLogSelectModels(id) {
            var url = dataConstants.BACK_LOG_URL + 'get-back-log-select-models?employeeId=' + id;
            return apiHttpService.GET(url);
        }
    }
})();