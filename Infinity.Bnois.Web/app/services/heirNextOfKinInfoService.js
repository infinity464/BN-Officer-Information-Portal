(function () {
    'use strict';
    angular.module('app').service('heirNextOfKinInfoService', ['dataConstants', 'apiHttpService', heirNextOfKinInfoService]);

    function heirNextOfKinInfoService(dataConstants, apiHttpService) {
        var service = {
            getHeirNextOfKinInfoList: getHeirNextOfKinInfoList,
            getHeirNextOfKinInfo: getHeirNextOfKinInfo,
            saveHeirNextOfKinInfo: saveHeirNextOfKinInfo,
            updateHeirNextOfKinInfo: updateHeirNextOfKinInfo,
            deleteHeirNextOfKinInfo: deleteHeirNextOfKinInfo,
            imageUploadUrl: imageUploadUrl
           
        };

        return service;

        

        function imageUploadUrl(employeeId, heirNextOfKinInfoId) {
            var url = dataConstants.HEIR_NEXT_OF_KIN_INFO_URL + 'upload-heir-next-of-kin-image?employeeId=' + employeeId + '&heirNextOfKinInfoId=' + heirNextOfKinInfoId;
            return url;
        }
        function getHeirNextOfKinInfoList(employeeId) {
            var url = dataConstants.HEIR_NEXT_OF_KIN_INFO_URL + 'get-heir-next-of-kin-info-list?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getHeirNextOfKinInfo(employeeId, heirNextOfKinInfoId) {
            var url = dataConstants.HEIR_NEXT_OF_KIN_INFO_URL + 'get-heir-next-of-kin-info?employeeId=' + employeeId + '&heirNextOfKinInfoId=' + heirNextOfKinInfoId;
            return apiHttpService.GET(url);
        }

        function saveHeirNextOfKinInfo(employeeId,data) {
            var url = dataConstants.HEIR_NEXT_OF_KIN_INFO_URL + 'save-heir-next-of-kin-info/' + employeeId;
            return apiHttpService.POST(url, data);
        }

        function updateHeirNextOfKinInfo(heirNextOfKinInfoId, data) {
            var url = dataConstants.HEIR_NEXT_OF_KIN_INFO_URL + 'update-heir-next-of-kin-info/' + heirNextOfKinInfoId;
            return apiHttpService.PUT(url, data);
        }

        function deleteHeirNextOfKinInfo(id) {
            var url = dataConstants.HEIR_NEXT_OF_KIN_INFO_URL + 'delete-heir-next-of-kin-info/' + id;
            return apiHttpService.DELETE(url);
        }
    }
})();