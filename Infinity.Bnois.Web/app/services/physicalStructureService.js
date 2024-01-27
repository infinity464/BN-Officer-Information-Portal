(function () {
    'use strict';
    angular.module('app').service('physicalStructureService', ['dataConstants', 'apiHttpService', physicalStructureService]);

    function physicalStructureService(dataConstants, apiHttpService) {
        var service = {
            getPhysicalStructures: getPhysicalStructures,
            getPhysicalStructure: getPhysicalStructure,
            savePhysicalStructure: savePhysicalStructure,
            updatePhysicalStructure: updatePhysicalStructure,
            deletePhysicalStructure: deletePhysicalStructure
        };

        return service;
        function getPhysicalStructures(pageSize, pageNumber, searchText) {
            var url = dataConstants.PHYSICAL_STRUCTURE_URL + 'get-physical-structures?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getPhysicalStructure(physicalStructureId) {
            var url = dataConstants.PHYSICAL_STRUCTURE_URL + 'get-physical-structure?id=' + physicalStructureId;
            return apiHttpService.GET(url);
        }

        function savePhysicalStructure(data) {
            var url = dataConstants.PHYSICAL_STRUCTURE_URL + 'save-physical-structure';
            return apiHttpService.POST(url, data);
        }

        function updatePhysicalStructure(physicalStructureId, data) {
            var url = dataConstants.PHYSICAL_STRUCTURE_URL + 'update-physical-structure/' + physicalStructureId;
            return apiHttpService.PUT(url, data);
        }

        function deletePhysicalStructure(physicalStructureId) {
            var url = dataConstants.PHYSICAL_STRUCTURE_URL + 'delete-physical-structure/' + physicalStructureId;
            return apiHttpService.DELETE(url);
        }


    }
})();