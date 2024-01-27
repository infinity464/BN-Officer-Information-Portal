(function () {

    'use strict';
    var controllerId = 'previousPunishmentsController';
    angular.module('app').controller(controllerId, previousPunishmentsController);
    previousPunishmentsController.$inject = ['$stateParams', '$state', 'previousPunishmentService', 'notificationService'];

    function previousPunishmentsController($stateParams, $state, previousPunishmentService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeId = 0;
        vm.previousPunishmentId = 0;
        vm.previousPunishments = [];
        vm.title = 'Previous Punishment';
        vm.addPreviousPunishment = addPreviousPunishment;
        vm.updatePreviousPunishment = updatePreviousPunishment;
        vm.deletePreviousPunishment = deletePreviousPunishment;
        vm.back = back;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            previousPunishmentService.getPreviousPunishments(vm.employeeId).then(function (data) {
                vm.previousPunishments = data.result;  
                    vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
        function addPreviousPunishment() {
            $state.go('employee-tabs.previous-punishment-create', { id: vm.employeeId, previousPunishmentId: vm.previousPunishmentId });
        }
        
        function updatePreviousPunishment(previousPunishment) {
            $state.go('employee-tabs.previous-punishment-modify', { id: vm.employeeId, previousPunishmentId: previousPunishment.previousPunishmentId });
        }


        function deletePreviousPunishment(previousPunishment) {
            previousPunishmentService.deletePreviousPunishment(previousPunishment.previousPunishmentId).then(function (data) {

                previousPunishmentService.getPreviousPunishments(vm.employeeId).then(function(data) {
                    vm.previousPunishments = data.result;
                });
            });
        }


        function back() {
            $state.go('employee-tabs.employee-previous-experiences');
        }

    }

})();
