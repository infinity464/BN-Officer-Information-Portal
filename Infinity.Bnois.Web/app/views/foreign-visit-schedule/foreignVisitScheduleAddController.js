(function () {

    'use strict';

    var controllerId = 'foreignVisitScheduleAddController';

    angular.module('app').controller(controllerId, foreignVisitScheduleAddController);
    foreignVisitScheduleAddController.$inject = ['$stateParams', 'nominationScheduleService', 'visitSubCategoryService','notificationService', '$state'];

    function foreignVisitScheduleAddController($stateParams, nominationScheduleService,visitSubCategoryService, notificationService, $state) {
        var vm = this;
        vm.foreignVisitScheduleId = 0;
        vm.title = 'ADD MODE';
        vm.foreignVisitSchedule = {};
        vm.countries = [];
        vm.visitCategories = [];
        vm.visitSubCategories = [];
        vm.minDateOfBirthDate = new Date("01/01/1900");
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.foreignVisitScheduleForm = {};
        vm.getVisitSubCategorySelectModelsByVisitCategory = getVisitSubCategorySelectModelsByVisitCategory;

  

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.foreignVisitScheduleId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            nominationScheduleService.getNominationSchedule(vm.foreignVisitScheduleId).then(function (data) {
                vm.foreignVisitSchedule = data.result.nominationSchedule;
                 vm.countries = data.result.countries;
                 vm.visitCategories = data.result.visitCategories;
                 vm.visitSubCategories = data.result.visitSubCategories;

                if (vm.foreignVisitScheduleId !== 0 && vm.foreignVisitScheduleId !== '') {
 
                    vm.foreignVisitSchedule.fromDate = new Date(data.result.nominationSchedule.fromDate);
                    vm.foreignVisitSchedule.toDate = new Date(data.result.nominationSchedule.toDate);

                  

                }  
             

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            vm.foreignVisitSchedule.nominationScheduleType = 2;

            if (vm.foreignVisitScheduleId !== 0 && vm.foreignVisitScheduleId !== '') {
                updateForeignVisitSchedule();
            } else {
                insertForeignVisitSchedule();
            }
        }

        function insertForeignVisitSchedule() {
            nominationScheduleService.saveNominationSchedule(vm.foreignVisitSchedule).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateForeignVisitSchedule() {
            nominationScheduleService.updateNominationSchedule(vm.foreignVisitScheduleId, vm.foreignVisitSchedule).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('foreign-visit-schedules');
        }


        function getVisitSubCategorySelectModelsByVisitCategory(visitCategoryId) {
            visitSubCategoryService.getVisitSubCategorySelectModelsByVisitCategory(visitCategoryId).then(function (data) {
                vm.visitSubCategories = data.result;
            });
        }
  
    
    }
})();
