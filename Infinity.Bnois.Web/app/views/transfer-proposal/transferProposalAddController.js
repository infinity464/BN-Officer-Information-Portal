(function () {

    'use strict';

    var controllerId = 'transferProposalAddController';

    angular.module('app').controller(controllerId, transferProposalAddController);
    transferProposalAddController.$inject = ['$stateParams', 'transferProposalService', 'notificationService', '$state'];

    function transferProposalAddController($stateParams,  transferProposalService, notificationService, $state) {
        var vm = this;
        vm.transferProposalId = 0;
        vm.title = 'ADD MODE';
        vm.transferProposal = {};
        vm.ltCdrLevels = [{ text: 'Cdr & Above', value: 1 }, { text: 'Lt Cdr & Below', value: 2 }];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.transferProposalForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.transferProposalId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
             transferProposalService.getTransferProposal(vm.transferProposalId).then(function (data) {
                 vm.transferProposal = data.result;

                if (vm.transferProposal.proposalDate!=null) {
                    vm.transferProposal.proposalDate = new Date(vm.transferProposal.proposalDate);
                 }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.transferProposalId !== 0 && vm.transferProposalId !== '') {
                updateTransferProposal();
            } else {
                insertTransferProposal();
            }
        }

        function insertTransferProposal() {
             transferProposalService.saveTransferProposal(vm.transferProposal).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateTransferProposal() {
             transferProposalService.updateTransferProposal(vm.transferProposalId, vm.transferProposal).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('transfer-proposals');
        }
    }
})();
