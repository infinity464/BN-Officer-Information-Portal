(function () {

    'use strict';
    var controllerId = 'transferProposalsController';
    angular.module('app').controller(controllerId, transferProposalsController);
    transferProposalsController.$inject = ['$state', 'transferProposalService', 'downloadService','reportService', 'notificationService', '$location'];

    function transferProposalsController($state, transferProposalService, downloadService, reportService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.transferProposals = [];
        vm.addTransferProposal = addTransferProposal;
        vm.updateTransferProposal = updateTransferProposal;
        vm.deleteTransferProposal = deleteTransferProposal;
        vm.getTransferProposalDetails = getTransferProposalDetails;
        vm.downlaodTransferProposal = downlaodTransferProposal;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;


        vm.reportType = 1;
        vm.reportTypes = [{ text: 'PDF', value: 1 }, { text: 'WORD', value: 2 }, { text: 'EXCEL', value: 3 }];

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            transferProposalService.getTransferProposals(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.transferProposals = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addTransferProposal() {
            $state.go('transfer-proposal-create');
        }

        function updateTransferProposal(transferProposal) {
            $state.go('transfer-proposal-modify', { id: transferProposal.transferProposalId });
        }

        function deleteTransferProposal(transferProposal) {
            transferProposalService.deleteTransferProposal(transferProposal.transferProposalId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function getTransferProposalDetails(transferProposal) {
            $state.go('proposal-details', { transferProposalId: transferProposal.transferProposalId,pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        
        function pageChanged() {
            $state.go('transfer-proposals', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }
        
        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
        

        function downlaodTransferProposal(transferProposal) {
            var url = reportService.downlaodTransferProposalUrl(transferProposal.transferProposalId, vm.reportType );
            downloadService.downloadFile(url);
        }

    }

})();
