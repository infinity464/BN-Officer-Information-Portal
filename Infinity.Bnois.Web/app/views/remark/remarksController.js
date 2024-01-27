

(function () {

    'use strict';
    var controllerId = 'remarksController';
    angular.module('app').controller(controllerId, remarksController);
    remarksController.$inject = ['$state', '$stateParams', 'employeeService','remarkService', 'notificationService', '$location'];

    function remarksController($state, $stateParams, employeeService,remarkService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.remarks = [];
        vm.addRemark = addRemark;
        vm.updateRemark = updateRemark;
        vm.deleteRemark = deleteRemark;
     
        vm.type = 0;
        vm.typeName = '';

        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.type = $stateParams.type;
            if (vm.type == 1) {
                vm.typeName = 'Remark';
            }
            else if (vm.type == 2) {
                vm.typeName = 'Persuasion';
            }
            else if (vm.type == 3) {
                vm.typeName = 'NS Note';
            }
        }
        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }


      
        init();
        function init() {
            employeeService.getEmployeeByPno(vm.pNo).then(function (data) {
                vm.employeeinfo = data.result;

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            remarkService.getRemarks(vm.pNo,vm.type).then(function (data) {
                vm.remarks = data.result;
                    vm.permission = data.permission;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addRemark() {
            $state.go('current-status-tab.remark-create' ,{ type: vm.type });
        }

        function updateRemark(remark) {
            $state.go('current-status-tab.remark-modify', { id: remark.remarkId, type: vm.type });
        }

        function deleteRemark(remark) {
            remarkService.deleteRemark(remark.remarkId).then(function (data) {
                remarkService.getRemarks(vm.pNo, vm.type).then(function(data) {
                    vm.remarks = data.result;

                });
            });
        }


  


    }

})();

