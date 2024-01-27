(function () {

    'use strict';
    var controllerId = 'currentStatusTabController';
    angular.module('app').controller(controllerId, currentStatusTabController);
    currentStatusTabController.$inject = ['$stateParams', '$state', 'notificationService', 'moduleService','employeeService'];

    function currentStatusTabController($stateParams, $state, notificationService, moduleService, employeeService) {
        /* jshint validthis:true */
        var vm = this; 
       
        vm.pNo = 0;
        vm.searchPno = '';
        vm.currentStatusMenues = [];
        vm.searchByPno = searchByPno;
        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
            vm.searchPno = $stateParams.pno;
            
        }
        init();
        function init() {

            moduleService.getCurrentStatusMenu().then(function (data) {
                vm.currentStatusMenues = data.result;
            }, function (errorMessage) {
                notificationService.displayError(errorMessage.message);
            });


            $state.go('current-status-tab.current-status');
        }



        function searchByPno(pNo) {
            if (pNo != null && pNo != vm.pNo) {
                employeeService.getEmployeeByPno(pNo).then(function (data) {
                    $state.go('current-status-tab', { pno: pNo });

                    },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
            }
            

        }

    }

})();
