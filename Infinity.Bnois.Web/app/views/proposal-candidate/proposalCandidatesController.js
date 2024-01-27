(function () {

    'use strict';
    var controllerId = 'proposalCandidatesController';
    angular.module('app').controller(controllerId, proposalCandidatesController);
    proposalCandidatesController.$inject = ['$state', '$stateParams','$window', 'proposalCandidateService', 'notificationService', '$location'];

    function proposalCandidatesController($state, $stateParams, $window, proposalCandidateService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.proposalDetailId = 0;
        vm.transferProposalId = 0;
        vm.proposalCandidates = [];
        vm.deleteProposalCandidate = deleteProposalCandidate;
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.back = back;

        if ($stateParams.proposalDetailId !== undefined && $stateParams.proposalDetailId !== null) {
            vm.proposalDetailId = $stateParams.proposalDetailId;
        }


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
            proposalCandidateService.getProposalCandidates(vm.proposalDetailId).then(function (data) {
                vm.proposalCandidates = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            if (vm.proposalCandidate.employee.employeeId > 0) {
                vm.proposalCandidate.employeeId = vm.proposalCandidate.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by PNo!");
            }
            insertProposalCandidate();
        }

        function insertProposalCandidate() {
            proposalCandidateService.saveProposalCandidate(vm.proposalDetailId,vm.proposalCandidate).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function close() {
            vm.proposalCandidate = null;
            init();
        }
        function deleteProposalCandidate(proposalCandidate) {
            proposalCandidateService.deleteProposalCandidate(proposalCandidate.proposalCandidateId).then(function (data) {
                close();
            });


        }


        function back() {
            $state.go('proposal-details', { transferProposalId: vm.transferProposalId });
        }
    }

})();




//(function () {

//    'use strict';
//    var controllerId = 'proposalCandidatesController';
//    angular.module('app').controller(controllerId, proposalCandidatesController);
//    proposalCandidatesController.$inject = ['$state', '$stateParams', 'proposalCandidateService', 'notificationService', '$location'];

//    function proposalCandidatesController($state, $stateParams, proposalCandidateService, notificationService, location) {

//        /* jshint validthis:true */
//        var vm = this;
//        vm.proposalCandidates = [];
//        vm.proposalDetailId = 0;
//        vm.addProposalCandidate = addProposalCandidate;
//        vm.updateProposalCandidate = updateProposalCandidate;
//        vm.deleteProposalCandidate = deleteProposalCandidate;
//        vm.back = back;
//        vm.pageChanged = pageChanged;



//        if ($stateParams.proposalDetailId !== undefined && $stateParams.proposalDetailId !== null) {
//            vm.proposalDetailId = $stateParams.proposalDetailId;
//        }

//        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
//            vm.pageSize = location.search().ps;
//        }

//        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
//            vm.pageNumber = location.search().pn;
//        }
//        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
//            vm.searchText = location.search().q;
//        }
        

//        init();
//        function init() {
//            proposalCandidateService.getProposalCandidates(vm.proposalDetailId,vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
//                vm.proposalCandidates = data.result;
//                vm.total = data.total; vm.permission = data.permission;
//            },
//                function (errorMessage) {
//                    notificationService.displayError(errorMessage.message);
//                });
//        }

//        function addProposalCandidate() {
//            $state.go('proposal-detail-create', { proposalDetailId: vm.proposalDetailId });
//        }


//        function deleteProposalCandidate(ProposalCandidate) {
//            proposalCandidateService.deleteProposalCandidate(ProposalCandidate.ProposalCandidateId).then(function (data) {
//                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
//            });
//        }

//        function back() {
//            $state.go('transfer-proposals');
//        }
       
//    }

//})();
