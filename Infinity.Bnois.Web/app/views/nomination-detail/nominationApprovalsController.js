

(function () {

    'use strict';
    var controllerId = 'nominationApprovalsController';
    angular.module('app').controller(controllerId, nominationApprovalsController);
    nominationApprovalsController.$inject = ['$state','$scope', '$stateParams', 'nominationDetailService', 'nominationService','missionAppointmentService', 'notificationService', '$location'];

    function nominationApprovalsController($state, $scope, $stateParams, nominationDetailService, nominationService, missionAppointmentService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.nominationDetails = [];

      
        vm.nominationDetailId = 0;
        vm.nominationDetail = {};
        vm.nominationId = 0;
        vm.entityId = 0;
        vm.nominationDetailForm = {};
        vm.nominationTypes = [];
        vm.nominationTypeResults = [];
        vm.missionAppointments = [];
        vm.update = update;
        vm.backToApproval = backToApproval;
        vm.saveButtonText = "ADD";
        vm.listShow = false;
        vm.buttonShow = false;
        vm.appointment = false;
        vm.entityType = 0;
        vm.getNominationResultsByType = getNominationResultsByType;
        vm.getNominationDetailsByNomination = getNominationDetailsByNomination;
        vm.localSearch = localSearch;
        vm.selected = selected;
     


        if (location.search().title !== undefined && location.search().title !== null && location.search().title !== '') {
            vm.title = location.search().title;
        }


        init();
        function init() {

            nominationService.getNominationType().then(function (data) {
                vm.nominationTypes = data.result.nominationTypes;
                });

        }

        function getNominationResultsByType(entityType) {
            $scope.$broadcast('angucomplete-alt:changeInput', 'nominationId', ' ');
            backToApproval();
            vm.listShow = false;
            nominationService.getNominationByType(entityType).then(function (data) {
                vm.nominationTypeResults = data.result;
              
            });
        }

        function getNominationDetailsByNomination(nominationId) {


                vm.buttonShow = false;
                vm.nominationDetails = [];
              
                vm.listShow = true;
            vm.nominationId = nominationId;

                nominationDetailService.getNominationDetails(nominationId).then(function (data) {
                vm.nominationDetails = data.result;
                if (vm.nominationDetails.length > 0) {
                    vm.buttonShow = true;

                } else {
                    vm.buttonShow = false;
                }

            });


        }

        function localSearch(str) {
            var matches = [];
            vm.nominationTypeResults.forEach(function (nomination) {

                if ((nomination.text.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0)) {
                    matches.push(nomination);

                }
            });
            return matches;
        }


        function selected(object) {
            getNominationDetailsByNomination(object.originalObject.value);

        }





        function update() {

            updateNominationDetails();
        }

      
        function updateNominationDetails() {
            nominationDetailService.updateNominationDetail(vm.nominationId, vm.nominationDetails).then(function (data) {


                
                    vm.buttonShow = false;
                  notificationService.displaySuccess("Updated Successfully.!!!");

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function backToApproval() {
            vm.nominationDetails = null;
            

        }
    }

})();

