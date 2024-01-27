(function () {

    'use strict';
    var controllerId = 'addressesController';
    angular.module('app').controller(controllerId, addressesController);
    addressesController.$inject = ['$stateParams', '$state', 'addressService', 'notificationService'];

    function addressesController($stateParams, $state, addressService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeId = 0;
        vm.addressId = 0;
        vm.addresses = [];
        vm.title = 'Address';
        vm.addAddress = addAddress;
        vm.updateAddress = updateAddress;
        vm.close = close;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            addressService.getAddresses(vm.employeeId).then(function (data) {
                vm.addresses = data.result;
                    vm.permission = data.permission;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addAddress() {
            $state.go('employee-tabs.employee-address-create', { id: vm.employeeId, addressId: vm.addressId });
        }
        
        function updateAddress(address) {
            $state.go('employee-tabs.employee-address-modify', { id: vm.employeeId, addressId: address.addressId });
        }
        function close() {
            $state.go('employee-tabs.employee-addresses')
        }
    }

})();
