(function () {

    'use strict';
    var controllerId = 'proposalDetailsController';
    angular.module('app').controller(controllerId, proposalDetailsController);
    proposalDetailsController.$inject = ['$state', '$stateParams', 'proposalDetailService', 'notificationService', '$location'];

    function proposalDetailsController($state, $stateParams, proposalDetailService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.proposalDetails = [];
        vm.transferProposalId = 0;
        vm.addProposalDetail = addProposalDetail;
        vm.updateProposalDetail = updateProposalDetail;
        vm.deleteProposalDetail = deleteProposalDetail;
        vm.getCandidates = getCandidates;
        vm.back = back;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;
 

        if ($stateParams.transferProposalId !== undefined && $stateParams.transferProposalId !== null) {
            vm.transferProposalId = $stateParams.transferProposalId;
        }

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
            proposalDetailService.getProposalDetails(vm.transferProposalId,vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.proposalDetails = data.result;
                vm.total = data.total; vm.permission = data.permission;
               
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addProposalDetail() {
            $state.go('proposal-detail-create', { transferProposalId: vm.transferProposalId });
        }

        function updateProposalDetail(proposalDetail) {
            $state.go('proposal-detail-modify', { id: proposalDetail.proposalDetailId, transferProposalId: vm.transferProposalId});
        }

        function deleteProposalDetail(proposalDetail) {
            proposalDetailService.deleteProposalDetail(proposalDetail.proposalDetailId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function getCandidates(proposalDetail) {
            $state.go('proposal-candidates', { transferProposalId: vm.transferProposalId ,proposalDetailId: proposalDetail.proposalDetailId });
        }
        

        function pageChanged() {
            $state.go('proposal-details', { transferProposalId: vm.transferProposalId, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }
        function back() {
            $state.go('transfer-proposals');
        }
        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
