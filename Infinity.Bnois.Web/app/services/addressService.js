(function () {
    'use strict';
    angular.module('app').service('addressService', ['dataConstants', 'apiHttpService', addressService]);

    function addressService(dataConstants, apiHttpService) {
        var service = {
            getAddresses: getAddresses,
            getAddress: getAddress,
            saveAddress: saveAddress,
            //deleteAddress: deleteAddress,
            updateAddress: updateAddress,
            getDistrictsByDivision: getDistrictsByDivision,
            getUpazilasByDistrict: getUpazilasByDistrict
        };

        return service;
        function getAddresses(employeeId) {
            var url = dataConstants.ADDRESS_URL + 'get-addresses?employeeId=' + employeeId;
            return apiHttpService.GET(url);
        }

        function getAddress(employeeId, addressId) {
            var url = dataConstants.ADDRESS_URL + 'get-address?employeeId=' + employeeId + '&addressId=' + addressId;
            return apiHttpService.GET(url);
        }

        function saveAddress(employeeId,data) {
            var url = dataConstants.ADDRESS_URL + 'save-address/' + employeeId;
            return apiHttpService.POST(url, data);
        }

        function updateAddress(addressId, data) {
            var url = dataConstants.ADDRESS_URL + 'update-address/' + addressId;
            return apiHttpService.PUT(url, data);
        }

        function getDistrictsByDivision(divisionId) {
            var url = dataConstants.ADDRESS_URL + 'get-districts/' + divisionId;
            return apiHttpService.GET(url);
        }

        function getUpazilasByDistrict(districtId) {
            var url = dataConstants.ADDRESS_URL + 'get-upazilas/' + districtId;
            return apiHttpService.GET(url);
        }
    }
})();