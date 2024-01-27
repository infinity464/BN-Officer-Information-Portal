(function () {

    'use strict';
    var controllerId = 'previousPunishmentAddController';
    angular.module('app').controller(controllerId, previousPunishmentAddController);
    previousPunishmentAddController.$inject = ['$stateParams', '$state', 'previousPunishmentService', 'notificationService'];

    function previousPunishmentAddController($stateParams, $state, previousPunishmentService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.previousPunishmentId = 0;
        vm.previousPunishment = {};
        vm.title = 'ADD MODE';
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.previousPunishmentForm = {};



        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeId = $stateParams.id;
        }
        if ($stateParams.previousPunishmentId > 0) {
            vm.previousPunishmentId = $stateParams.previousPunishmentId;
            vm.title = 'UPDATE MODE';
            vm.saveButtonText = 'Update';
            
        }
        init();
        function init() {
            previousPunishmentService.getPreviousPunishment(vm.previousPunishmentId).then(function (data) {
                vm.previousPunishment = data.result;
               
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        
        function save() {
            if (vm.previousPunishmentId > 0 && vm.previousPunishmentId !== '') {
                updatePreviousPunishment();
            } else {
                insertPreviousPunishment();
            }
        }
        function insertPreviousPunishment() {
            previousPunishmentService.savePreviousPunishment(vm.employeeId, vm.previousPunishment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePreviousPunishment() {
            previousPunishmentService.updatePreviousPunishment(vm.previousPunishmentId, vm.previousPunishment).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function close() {
            $state.go('employee-tabs.previous-punishments');
        }
    }

})();
