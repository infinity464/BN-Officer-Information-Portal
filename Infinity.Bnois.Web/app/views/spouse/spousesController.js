(function () {

    'use strict';
    var controllerId = 'spousesController';
    angular.module('app').controller(controllerId, spousesController);
    spousesController.$inject = ['$stateParams', '$state', 'spouseService', 'notificationService'];

    function spousesController($stateParams, $state, spouseService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.spouseId = 0;
        vm.spouses = [];
        vm.title = 'Spouse';
        vm.addSpouse = addSpouse;
        vm.updateSpouse = updateSpouse;
        vm.getSpouseForeignVisit = getSpouseForeignVisit;
        vm.deleteSpouse = deleteSpouse;
        vm.getSpouseImage = getSpouseImage;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
            spouseService.getSpouses(vm.employeeId).then(function (data) {
                vm.spouses = data.result;
                    vm.permission = data.permission;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function addSpouse() {
            $state.go('employee-tabs.employee-spouse-create', { id: vm.employeeId, spouseId: vm.spouseId });
        }
        
        function updateSpouse(spouse) {
            $state.go('employee-tabs.employee-spouse-modify', { id: vm.employeeId, spouseId: spouse.spouseId });
          
        }

        function getSpouseForeignVisit(spouse) {
            $state.go('employee-tabs.employee-spouse-foreign-visits', {spouseId: spouse.spouseId });
        }

        function deleteSpouse(spouse) {
            spouseService.deleteSpouse(spouse.spouseId).then(function (data) {
                spouseService.getSpouses(vm.employeeId).then(function (data) {
                    vm.spouses = data.result;
                });
                $state.go('employee-tabs.employee-spouses');
            },
                function (errorMessage) {
                notificationService.displayError('Delete all relation first.');
            });
        }

        function getSpouseImage(spouse) {
            $state.go('employee-tabs.employee-spouse-image', { spouseId: spouse.spouseId });
        }
        

    }

})();
