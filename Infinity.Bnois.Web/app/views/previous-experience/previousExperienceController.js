
(function () {

    'use strict';
    var controllerId = 'previousExperienceController';
    angular.module('app').controller(controllerId, previousExperienceController);
    previousExperienceController.$inject = ['$stateParams', '$state', 'previousExperienceService', 'notificationService'];

    function previousExperienceController($stateParams, $state, previousExperienceService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        
        vm.title = 'ADD MODE';
        vm.previousExperience = {};
        vm.employeeId = 0;
        vm.previousExperienceForm = {};
        vm.preCommissionRanks = [];
        vm.categories = [];
        vm.previousLeave = previousLeave;
        vm.previousTransfer = previousTransfer;
        vm.previousPunishment = previousPunishment;
        vm.previousMission = previousMission;

        vm.issb = [
            { 'value': 1, 'text': 'Attained' },
            { 'value': 2, 'text': 'Not Attained' }   
        ];

        vm.issbResults = [
            { 'value': 1, 'text': 'Qualified' },
            { 'value': 2, 'text': 'Not Qualified' }
        ];

        
        vm.save = save;
        vm.updatePreviousExperience = updatePreviousExperience;
        vm.close = close;
        vm.saveButtonText = 'Save';
        vm.title = 'UPDATE MODE';

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
            vm.saveButtonText = 'Update';
        }
        init();
        function init() {
            previousExperienceService.getPreviousExperience(vm.employeeId).then(function (data) {
                vm.previousExperience = data.result.previousExperience;
                if (vm.previousExperience != null) {
                    if (vm.previousExperience.joiningDate != null) {
                        vm.previousExperience.joiningDate = new Date(data.result.previousExperience.joiningDate);

                    }
                }

                vm.categories = data.result.categories;
                
                    vm.preCommissionRanks = data.result.preCommissionRanks;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            update();
        }

        function updatePreviousExperience() {
            $state.go('employee-tabs.employee-previous-experience-modify');
        }

        function update() {
            previousExperienceService.updatePreviousExperience(vm.employeeId, vm.previousExperience).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
            $state.go('employee-tabs.employee-previous-experiences');
        }


        function previousLeave() {
            $state.go('employee-tabs.previous-leaves');
        }

        function previousTransfer() {
            $state.go('employee-tabs.previous-transfers');
        }

        function previousPunishment() {
            $state.go('employee-tabs.previous-punishments');
        }

        function previousMission() {
            $state.go('employee-tabs.previous-missions');
        }
    }

})();
