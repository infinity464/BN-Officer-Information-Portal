(function () {
    'use strict';
    angular.module('app').service('foreignProjectService', ['dataConstants', 'apiHttpService', foreignProjectService]);

    function foreignProjectService(dataConstants, apiHttpService) {
        var service = {
            getForeignProjects: getForeignProjects,
            getForeignProject: getForeignProject,  
            saveForeignProject: saveForeignProject,
            updateForeignProject: updateForeignProject,
            deleteForeignProject: deleteForeignProject
        };

        return service;
        function getForeignProjects(pageSize, pageNumber,searchText) {
            var url = dataConstants.FOREIGN_PROJECT_URL + 'get-foreign-projects?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getForeignProject(foreignProjectId) {
            var url = dataConstants.FOREIGN_PROJECT_URL + 'get-foreign-project?id=' + foreignProjectId;
            return apiHttpService.GET(url);
        }

        function saveForeignProject(data) {
            var url = dataConstants.FOREIGN_PROJECT_URL + 'save-foreign-project';
            return apiHttpService.POST(url, data);
        }

        function updateForeignProject(foreignProjectId, data) {
            var url = dataConstants.FOREIGN_PROJECT_URL + 'update-foreign-project/' + foreignProjectId;
            return apiHttpService.PUT(url, data);
        }

        function deleteForeignProject(foreignProjectId) {
            var url = dataConstants.SEA_SERVICE_URL + 'delete-foreign-project/' + foreignProjectId;
            return apiHttpService.DELETE(url);
        }
        
    }
})();