

(function () {

    'use strict';

    var controllerId = 'shipMovementController';

    angular.module('app').controller(controllerId, shipMovementController);
    shipMovementController.$inject = ['dashboardService', 'rankService','shipMovementService','notificationService', '$state'];

    function shipMovementController(dashboardService, rankService, shipMovementService,notificationService, $state) {
        var vm = this;
        vm.streamTable = false;
        vm.dashboardStreams = [];
        vm.ships = [];
        vm.offices = [];
        vm.officerFroms = [];
        vm.shipMovement = shipMovement;
        vm.localSearchAttach = localSearchAttach;
        vm.selectedAttach = selectedAttach;

        vm.office = {};
    

        Init();
        function Init() {

            shipMovementService.getShipMovementSelectModels().then(function(data) {
                vm.ships = data.result.ships;
                vm.offices = data.result.offices;

            });
          
        }

        function shipMovement(officeId) {
            if (officeId != null && vm.office.parentId != null) {
                if (officeId != vm.office.parentId) {
                    shipMovementService.updateShipMovement(officeId, vm.office).then(function(data) {
                            notificationService.displaySuccess("Ship moved successfully");
                        },
                        function(errorMessage) {
                            notificationService.displayError(errorMessage.message);
                        });
                } else {
                    notificationService.displayError('Same Ship Movement not allowed.');
                }
            
            }
            else {
                notificationService.displayError("Select an office.");
            }
            
        }


        function localSearchAttach(str) {
            var matches = [];
            vm.offices.forEach(function (transfer) {

                if ((transfer.text.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0)) {
                    matches.push(transfer);

                }
            });
            return matches;
        }


        function selectedAttach(object) {
            vm.office.parentId = object.originalObject.value;
           
        }

       
    }
})();
