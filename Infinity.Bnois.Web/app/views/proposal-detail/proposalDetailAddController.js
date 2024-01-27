
(function () {

    'use strict';

    var controllerId = 'proposalDetailAddController';

    angular.module('app').controller(controllerId, proposalDetailAddController);
    proposalDetailAddController.$inject = ['$stateParams','$scope', 'proposalDetailService', 'officeService','officeAppointmentService', 'notificationService', '$state'];

    function proposalDetailAddController($stateParams, $scope, proposalDetailService, officeService, officeAppointmentService, notificationService, $state) {
        var vm = this;
        vm.proposalDetailId = 0;
        vm.transferProposalId = 0;
        vm.title = 'ADD MODE';
        vm.proposalDetail = {};
        vm.transferTypes = [];
        vm.offices = [];
        vm.officeAppointments = [];

        vm.getOfficeSelectModelByType = getOfficeSelectModelByType;
        vm.getOfficeAppointmentByOffice = getOfficeAppointmentByOffice;
        vm.localSearchAttach = localSearchAttach;
        vm.selectedAttach = selectedAttach;
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.proposalDetailForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.proposalDetailId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }


        if ($stateParams.transferProposalId !== undefined && $stateParams.transferProposalId !== null) {
            vm.transferProposalId = $stateParams.transferProposalId;
        }

        Init();
        function Init() {
            proposalDetailService.getProposalDetail(vm.proposalDetailId).then(function (data) {
                vm.proposalDetail = data.result.proposalDetail;
                vm.transferTypes = data.result.transferTypes;
                vm.offices = data.result.offices;
                vm.officeAppointments = data.result.officeAppointments;
                    changeInputValue();
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.proposalDetailId !== 0 && vm.proposalDetailId !== '') {
                updateProposalDetail();
            } else {
                insertProposalDetail();
            }
        }

        function insertProposalDetail() {
            proposalDetailService.saveProposalDetail(vm.transferProposalId,vm.proposalDetail).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateProposalDetail() {
            proposalDetailService.updateProposalDetail(vm.proposalDetailId, vm.proposalDetail).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function getOfficeSelectModelByType(transferType) {
            officeService.getOfficeSelectModelByType(transferType).then(function (data) {
                vm.offices = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        } 

        function getOfficeAppointmentByOffice(attachOfficeId) {
            officeAppointmentService.getOfficeAppointmentByOffice(attachOfficeId).then(function (data) {
                vm.officeAppointments = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
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


        function changeInputValue() {
            if (vm.proposalDetailId > 0) {
                var obj = { value: vm.proposalDetail.office.officeId, text: vm.proposalDetail.office.name };
                $scope.$broadcast('angucomplete-alt:changeInput', 'attachOfficeId', obj);
            }
        }


        function selectedAttach(object) {
            vm.proposalDetail.attachOfficeId = object.originalObject.value;
            getOfficeAppointmentByOffice(vm.proposalDetail.attachOfficeId);
        }



        function close() {
            $state.go('proposal-details', { transferProposalId: vm.transferProposalId });
        }
    }
})();
