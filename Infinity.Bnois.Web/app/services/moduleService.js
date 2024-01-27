(function () {
    'use strict';
    angular.module('app').service('moduleService', ['identityDataConstants', 'apiHttpService', moduleService]);
    function moduleService(identityDataConstants, apiHttpService) {
        var service = {
            getModules: getModules,
            getModule: getModule,
            saveModule: saveModule,
            updateModule: updateModule,
            deleteModule: deleteModule,
            getModuleFeaturs: getModuleFeaturs,
            getCurrentStatusMenu: getCurrentStatusMenu,
            getModuleReports: getModuleReports,
            downloadModuleReport: downloadModuleReport
        };

        return service;
        function getModules(pageSize, pageNumber, searchString) {
          var url = identityDataConstants.MODULE_URL + 'get-modules?pageSize=' + pageSize + "&pageNumber=" + pageNumber + "&searchString=" + searchString;
            return apiHttpService.GET(url);
        }

        function getModule(moduleId) {
            var url = identityDataConstants.MODULE_URL + 'get-module?moduleId=' + moduleId;
            return apiHttpService.GET(url);
        }

        function saveModule(data) {
            var url = identityDataConstants.MODULE_URL + 'save-module';
            return apiHttpService.POST(url, data);
        }

        function updateModule(moduleId, data) {
            var url = identityDataConstants.MODULE_URL + 'update-module/' + moduleId;
            return apiHttpService.PUT(url, data);
        }

        function deleteModule(moduleId) {
            var url = identityDataConstants.MODULE_URL + 'delete-module/' + moduleId;
            return apiHttpService.DELETE(url);
        }

        function getModuleFeaturs() {
            var url = identityDataConstants.MODULE_URL + 'get-module-features';
            return apiHttpService.GET(url);
        }

        function getCurrentStatusMenu() {
            var url = identityDataConstants.MODULE_URL + 'get-current-status-menu';
            return apiHttpService.GET(url);
        }
        function getModuleReports(featureType) {
            var url = identityDataConstants.MODULE_URL + 'get-module-reports?featureType=' + featureType;
            return apiHttpService.GET(url);
        }
        function downloadModuleReport() {
            return identityDataConstants.MODULE_URL + 'download-module';
        }
    }
})();