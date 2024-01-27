/// <reference path="../../services/empRejoinService.js" />
(function () {

    'use strict';

    var controllerId = 'empRejoinAddController';

    angular.module('app').controller(controllerId, empRejoinAddController);
    empRejoinAddController.$inject = ['$stateParams', 'empRejoinService', 'notificationService', '$state'];

    function empRejoinAddController($stateParams, empRejoinService, notificationService, $state) {
        var vm = this;
        vm.empRejoinId = 0;
        vm.title = 'ADD MODE';
        vm.empRejoin = {};
        vm.ranks = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.empRejoinForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.empRejoinId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            empRejoinService.getEmpRejoin(vm.empRejoinId).then(function (data) {
                vm.empRejoin = data.result.empRejoin;
                    if (vm.empRejoinId !== 0 && vm.empRejoinId !== '') {
                        vm.empRejoin.rejoinDate = new Date(data.result.empRejoin.rejoinDate);
                    } 
                vm.ranks = data.result.ranks;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.empRejoin.employee.employeeId > 0) {
                vm.empRejoin.employeeId = vm.empRejoin.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }

            if (vm.empRejoinId !== 0 && vm.empRejoinId !== '') {
                updateEmpRejoin();
            } else {
                insertEmpRejoin();
            }
        }

        function insertEmpRejoin() {
            empRejoinService.saveEmpRejoin(vm.empRejoin).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEmpRejoin() {
            empRejoinService.updateEmpRejoin(vm.empRejoinId, vm.empRejoin).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-rejoins');
        }
    }
})();
