

(function () {

    'use strict';
    var controllerId = 'nominationsController';
    angular.module('app').controller(controllerId, nominationsController);
    nominationsController.$inject = ['$state', '$stateParams', 'nominationService', 'reportService','downloadService','notificationService', '$location'];

    function nominationsController($state, $stateParams, nominationService, reportService, downloadService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.nominations = [];
        vm.addNomination = addNomination;
        vm.updateNomination = updateNomination;
        vm.deleteNomination = deleteNomination;
        vm.nominationDetails = nominationDetails;
        vm.nominationApprovalDetails = nominationApprovalDetails;
        vm.downlaodBroadSheetForeignCourseVisitMission = downlaodBroadSheetForeignCourseVisitMission;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;
        vm.type = 0;
        vm.typeName = null;
        vm.title = null;
        vm.appointment = false;
        vm.reportType = 1;
        vm.reportTypes = [{ text: 'PDF', value: 1 }, { text: 'WORD', value: 2 }, { text: 'EXCEL', value: 3 }];

        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.type = $stateParams.type;
            if (vm.type == 1) {
                vm.typeName = "Course";
            } else if (vm.type == 2) {
                vm.typeName = "Mission";
                vm.appointment = true;
            } else if (vm.type == 3) {
                vm.typeName = "Foreign Visit";
            }
            else {

                vm.typeName = "Other";
            }
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
            nominationService.getNominations(vm.pageSize, vm.pageNumber, vm.searchText, vm.type).then(function (data) {
                vm.nominations = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addNomination() {
            $state.go('nomination-create', {type:vm.type});
        }

        function updateNomination(nomination) {
            $state.go('nomination-modify', { type: vm.type,id: nomination.nominationId });
        }

        function deleteNomination(nomination) {
            nominationService.deleteNomination(nomination.nominationId).then(function (data) {
                $state.go($state.current, { type:vm.type,pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('nominations' , { type:vm.type, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function nominationDetails(nomination) {
            if (nomination.missionAppointment != null)
                vm.title = nomination.nomination + '[ ' + nomination.missionAppointment + ' ]';
            else
                vm.title = nomination.nomination;
            $state.go('nomination-details', { type: vm.type, id: nomination.nominationId, title: vm.title });
        }

        function nominationApprovalDetails(nomination) {
            $state.go('nomination-approval-list', { id: nomination.nominationId, title: nomination.nomination });
        }


        function downlaodBroadSheetForeignCourseVisitMission(nomination) {
            var url = reportService.downlaodBroadSheetForeignCourseVisitMission(nomination.nominationId, vm.reportType);
            downloadService.downloadReport(url);
        }
        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();

