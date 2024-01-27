/// <reference path="../../services/spouseForeignVisitService.js" />

(function () {

    'use strict';
    var controllerId = 'spouseForeignVisitsController';
    angular.module('app').controller(controllerId, spouseForeignVisitsController);
    spouseForeignVisitsController.$inject = ['$state', '$stateParams', 'spouseForeignVisitService', 'notificationService', '$location'];

    function spouseForeignVisitsController($state, $stateParams, spouseForeignVisitService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.spouseId = 0;
        vm.spouseForeignVisitId = 0;
        vm.spouseForeignVisits = [];
        vm.addSpouseForeignVisit = addSpouseForeignVisit;
        vm.updateSpouseForeignVisit = updateSpouseForeignVisit;
        vm.backToSpouse = backToSpouse;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }

        if ($stateParams.spouseId !== undefined && $stateParams.spouseId !== null) {
            vm.spouseId = $stateParams.spouseId;
        }
        init();
        function init() {
            spouseForeignVisitService.getSpouseForeignVisits(vm.spouseId, vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
               
                vm.spouseForeignVisits = data.result;
                    vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addSpouseForeignVisit() {
            $state.go('employee-tabs.employee-spouse-foreign-visit-create', { spouseId: vm.spouseId , spouseForeignVisitId: vm.spouseForeignVisitId });
        }

        function updateSpouseForeignVisit(spouseForeignVisit) {
            console.log(spouseForeignVisit);
            $state.go('employee-tabs.employee-spouse-foreign-visit-modify', { spouseId: vm.spouseId, spouseForeignVisitId: spouseForeignVisit.spouseForeignVisitId });
        }

        
        function pageChanged() {
            $state.go('employee-tabs.employee-spouse-foreign-visits', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }

        function backToSpouse() {
            $state.go('employee-tabs.employee-spouses');
        }
    }

})();
