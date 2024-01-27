

(function () {

    'use strict';

    var controllerId = 'remarkController';

    angular.module('app').controller(controllerId, remarkController);
    remarkController.$inject = ['$stateParams', 'notificationService','currentStatusService', '$state'];

    function remarkController($stateParams, notificationService, currentStatusService, $state) {
        var vm = this;
        vm.pNo = 0;
        vm.remarks = [];

        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }

        Init();
        function Init() {
            currentStatusService.getRemark(vm.pNo).then(function (data) {
                vm.remarks = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

     
       
    }
})();
