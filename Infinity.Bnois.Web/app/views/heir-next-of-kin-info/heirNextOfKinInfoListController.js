(function () {

    'use strict';
    var controllerId = 'heirNextOfKinInfoListController';
    angular.module('app').controller(controllerId, heirNextOfKinInfoListController);
    heirNextOfKinInfoListController.$inject = ['$stateParams', '$state', 'heirNextOfKinInfoService', 'notificationService'];

    function heirNextOfKinInfoListController($stateParams, $state, heirNextOfKinInfoService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.heirNextOfKinInfoId = 0;
        vm.heirNextOfKinInfoList = [];
        vm.title = 'Heir & Next Of Kin Information';
        vm.addHeirNextOfKinInfo = addHeirNextOfKinInfo;
        vm.updateHeirNextOfKinInfo = updateHeirNextOfKinInfo;
        vm.deleteHeirNextOfKinInfo = deleteHeirNextOfKinInfo;
        vm.uploadHeirNextOfKinImage = uploadHeirNextOfKinImage;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            heirNextOfKinInfoService.getHeirNextOfKinInfoList(vm.employeeId).then(function (data) {
                vm.heirNextOfKinInfoList = data.result;
                    vm.permission = data.permission;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addHeirNextOfKinInfo() {
            $state.go('employee-tabs.employee-heir-next-of-kin-info-create', { id: vm.employeeId, heirNextOfKinInfoId: vm.heirNextOfKinInfoId });
        } 

  

        function updateHeirNextOfKinInfo(heirNextOfKinInfo) {
            $state.go('employee-tabs.employee-heir-next-of-kin-info-modify', { id: vm.employeeId, heirNextOfKinInfoId: heirNextOfKinInfo.heirNextOfKinInfoId });
        }

        function deleteHeirNextOfKinInfo(heirNextOfKinInfo) {
            heirNextOfKinInfoService.deleteHeirNextOfKinInfo(heirNextOfKinInfo.heirNextOfKinInfoId).then(function (data) {
                heirNextOfKinInfoService.getHeirNextOfKinInfoList(vm.employeeId).then(function (data) {
                    vm.heirNextOfKinInfoList = data.result;
                });
                $state.go('employee-tabs.employee-heir-next-of-kin-info-list');
            });
        }

        function uploadHeirNextOfKinImage(heirNextOfKinInfo) {
            $state.go('employee-tabs.employee-heir-next-of-kin-image', { heirNextOfKinInfoId: heirNextOfKinInfo.heirNextOfKinInfoId });
        }

    }

})();
