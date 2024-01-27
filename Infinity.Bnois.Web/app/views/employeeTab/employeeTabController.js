(function () {

    'use strict';
    var controllerId = 'employeeTabController';
    angular.module('app').controller(controllerId, employeeTabController);
    employeeTabController.$inject = ['$stateParams', '$state', 'photoService','employeeService', 'codeValue','notificationService'];

    function employeeTabController($stateParams, $state, photoService, employeeService, codeValue, notificationService) {
        /* jshint validthis:true */
        var vm = this;
         vm.employeeId = 0;
        vm.fileModel = {};
        vm.searchByPno = searchByPno;
        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }
        init();
        function init() {
            photoService.getPhoto(vm.employeeId, 1).then(function(data) {
                vm.fileModel = data.result;
                vm.fileModel.filePath = codeValue.API_OFFICER_PICTURE_URL + vm.fileModel.fileName;
            });

            employeeService.getEmployee(vm.employeeId).then(function (data) {
                    vm.employee = data.result.employee;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });



            $state.go('employee-tabs.employee-modify', { id: vm.employeeId });
        }




        function searchByPno(pNo) {
            if (pNo != null) {
                employeeService.getEmployeeByPno(pNo).then(function (data) {
                    vm.employeeInfo = data.result;
                    if (vm.employeeInfo.employeeId != vm.employeeId) {
                    $state.go('employee-tabs', { employeeId: vm.employeeInfo.employeeId });

                      }
                    },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });

            }


        }

    }

})();
