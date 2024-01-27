(function () {

    'use strict';
    var controllerId = 'addressAddController';
    angular.module('app').controller(controllerId, addressAddController);
    addressAddController.$inject = ['$stateParams', '$state', 'addressService', 'notificationService'];

    function addressAddController($stateParams, $state, addressService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.addressId = 0;
        vm.employeeId = 0;
        vm.address = {};
        vm.addressTypes = [];
        vm.divisions = [];
        vm.districts = [];
        vm.upazilas = [];

        vm.title = 'ADD MODE';
        vm.getDistrictsByDivision = getDistrictsByDivision;
        vm.getUpazilasByDistrict = getUpazilasByDistrict;

        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.addressForm = {};


        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.addressId > 0) {
            vm.addressId = $stateParams.addressId;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
        }
        init();
        function init() {
            addressService.getAddress(vm.employeeId, vm.addressId).then(function (data) {
                vm.address = data.result.address;
                vm.addressTypes = data.result.addressTypes;
                vm.divisions = data.result.divisions;
                vm.districts = data.result.districts;
                console.log("District:"+vm.districts );
                vm.upazilas = data.result.upazilas;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function getDistrictsByDivision(divisionId) {
            addressService.getDistrictsByDivision(divisionId).then(function (data) {
                vm.districts = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }


        function getUpazilasByDistrict(districtId) {
            addressService.getUpazilasByDistrict(districtId).then(function (data) {
                vm.upazilas = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }

        function save() {
            if (vm.addressId > 0 && vm.addressId !== '') {
                updateAddress();
            } else {  
                insertAddress();
            }
        }
        function insertAddress() {
            addressService.saveAddress(vm.employeeId, vm.address).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateAddress() {
            addressService.updateAddress(vm.addressId, vm.address).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-tabs.employee-addresses');
        }
    }

})();
