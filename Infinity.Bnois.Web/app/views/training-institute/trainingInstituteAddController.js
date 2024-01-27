
(function () {

    'use strict';

    var controllerId = 'trainingInstituteAddController';

    angular.module('app').controller(controllerId, trainingInstituteAddController);
    trainingInstituteAddController.$inject = ['$stateParams', 'trainingInstituteService', 'notificationService', '$state'];

    function trainingInstituteAddController($stateParams, trainingInstituteService, notificationService, $state) {
        var vm = this;
        vm.instituteId = 0;
        vm.title = 'ADD MODE';
        vm.trainingInstitute = {};
        vm.countries = [];
        vm.countryTypes = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.trainingInstituteForm = {};
        vm.countriesName = countriesName;
        vm.countryDisable = false;
     

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.instituteId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            trainingInstituteService.getTrainingInstitute(vm.instituteId).then(function (data) {
                vm.trainingInstitute = data.result.trainingInstitute;
                vm.countryTypes = data.result.countryTypes;
                vm.countries = data.result.countries;

                if (vm.instituteId !== 0 && vm.instituteId !== '') {

                    if (vm.trainingInstitute.countryId == 12) {
                        vm.countryDisable = true;
                    }


                } else {
                    vm.trainingInstitute.countryType = 1;
                    countriesName(vm.trainingInstitute.countryType);
                }

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.instituteId !== 0 && vm.instituteId !== '') {
                updateTrainingInstitute();
            } else {
                insertTrainingInstitute();
            }
        }

        function insertTrainingInstitute() {
            trainingInstituteService.saveTrainingInstitute(vm.trainingInstitute).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateTrainingInstitute() {
            trainingInstituteService.updateTrainingInstitute(vm.instituteId, vm.trainingInstitute).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('training-institutes');
        }

      



        function countriesName(countyType) {
            if (countyType == 2) {
                vm.trainingInstitute.countryId = null;
                vm.countryDisable = false;
            } else {
                vm.trainingInstitute.countryId = 12;
                vm.countryDisable = true;

            }
        }

            


    }
})();
