(function () {

    'use strict';
    var controllerId = 'heirNextOfKinInfoAddController';
    angular.module('app').controller(controllerId, heirNextOfKinInfoAddController);
    heirNextOfKinInfoAddController.$inject = ['$stateParams', '$state', 'heirNextOfKinInfoService', 'notificationService'];

    function heirNextOfKinInfoAddController($stateParams, $state, heirNextOfKinInfoService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.heirNextOfKinInfoId = 0;
        vm.heirNextOfKinInfo = {};
        vm.heirKinTypes = [];
        vm.relations = [];
        vm.genders = [];
        vm.occupations = [];
        vm.heirTypes = [];

        vm.title = 'ADD MODE';

        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.heirNextOfKinInfoForm = {};
        vm.showHeirType = showHeirType;
        vm.isHeirType = false;


        

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.heirNextOfKinInfoId > 0) {
            vm.heirNextOfKinInfoId = $stateParams.heirNextOfKinInfoId;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
        }
        init();
        function init() {
            heirNextOfKinInfoService.getHeirNextOfKinInfo(vm.employeeId, vm.heirNextOfKinInfoId).then(function (data) {
                vm.heirNextOfKinInfo = data.result.heirNextOfKinInfo;
                vm.heirKinTypes = data.result.heirKinTypes;
                vm.heirTypes = data.result.heirTypes;
                vm.genders = data.result.genders;
                vm.relations = data.result.relations;
                vm.occupations = data.result.occupations;
                if (vm.heirNextOfKinInfoId > 0) {
                    showHeirType(vm.heirNextOfKinInfo.heirKinType);
                }
                else {
                    vm.heirNextOfKinInfo.heirKinType = 1;
                }
                   

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }



        function save() {
            if (vm.heirNextOfKinInfoId > 0 && vm.heirNextOfKinInfoId !== '') {
                updateHeirNextOfKinInfo();
            } else {
                insertHeirNextOfKinInfo();
            }
        }
        function insertHeirNextOfKinInfo() {
            heirNextOfKinInfoService.saveHeirNextOfKinInfo(vm.employeeId, vm.heirNextOfKinInfo).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateHeirNextOfKinInfo() {
            heirNextOfKinInfoService.updateHeirNextOfKinInfo(vm.heirNextOfKinInfoId, vm.heirNextOfKinInfo).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-tabs.employee-heir-next-of-kin-info-list');
        }

        function showHeirType(result) {
            if (result == 2) {
                vm.isHeirType = true;

            } else {
                vm.isHeirType = false;
                vm.heirNextOfKinInfo.heirTypeId = null;
            }
        }
      
    }

})();
