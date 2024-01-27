(function () {

    'use strict';

    var controllerId = 'spouseForeignVisitAddController';

    angular.module('app').controller(controllerId, spouseForeignVisitAddController);
    spouseForeignVisitAddController.$inject = ['$stateParams', 'spouseForeignVisitService', 'notificationService', '$state'];

    function spouseForeignVisitAddController($stateParams, spouseForeignVisitService, notificationService, $state) {
        var vm = this;
        vm.spouseForeignVisitId = 0;
        vm.title = 'ADD MODE';
        vm.spouseForeignVisit = {};
        vm.countries = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.spouseForeignVisitForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.spouseForeignVisitId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }
        if ($stateParams.spouseId !== undefined && $stateParams.spouseId !== null) {
            vm.spouseId = $stateParams.spouseId;
        }
        if ($stateParams.spouseForeignVisitId !== undefined && $stateParams.spouseForeignVisitId !== null) {
            vm.spouseForeignVisitId = $stateParams.spouseForeignVisitId;
        }

        Init();
        function Init() {
            spouseForeignVisitService.getSpouseForeignVisit(vm.spouseId, vm.spouseForeignVisitId).then(function (data) {
                vm.spouseForeignVisit = data.result.spouseForeignVisit;

                if (vm.spouseForeignVisit.stayFromDate != null) {
                    vm.spouseForeignVisit.stayFromDate = new Date(data.result.spouseForeignVisit.stayFromDate);

                }
                if (vm.spouseForeignVisit.stayToDate != null) {
                    vm.spouseForeignVisit.stayToDate = new Date(data.result.spouseForeignVisit.stayToDate);

                    }
                vm.countries = data.result.countries;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.spouseForeignVisitId >0 && vm.spouseForeignVisitId !== '') {
                updateSpouseForeignVisit();
            } else {
                insertSpouseForeignVisit();
            }
        }

        function insertSpouseForeignVisit() {
            vm.spouseForeignVisit.spouseId = vm.spouseId;
            spouseForeignVisitService.saveSpouseForeignVisit(vm.spouseForeignVisit).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateSpouseForeignVisit() {
            spouseForeignVisitService.updateSpouseForeignVisit(vm.spouseForeignVisitId, vm.spouseForeignVisit).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('employee-tabs.employee-spouse-foreign-visits', { spouseId: vm.spouseId });
        }
    }
})();
