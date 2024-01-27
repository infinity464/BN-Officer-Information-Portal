(function () {

    'use strict';
    var controllerId = 'ageServicePoliciesController';
    angular.module('app').controller(controllerId, ageServicePoliciesController);
    ageServicePoliciesController.$inject = ['$state', 'ageServicePolicyService', 'notificationService', '$location'];

    function ageServicePoliciesController($state, ageServicePolicyService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.ageServicePolicies = [];
        vm.addAgeServicePolicy = addAgeServicePolicy;
        vm.updateAgeServicePolicy = updateAgeServicePolicy;
        vm.deleteAgeServicePolicy = deleteAgeServicePolicy;
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
        init();
        function init() {
            ageServicePolicyService.getAgeServicePolicies(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.ageServicePolicies = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addAgeServicePolicy() {
            $state.go('age-service-policy-create');
        }

        function updateAgeServicePolicy(ageServicePolicy) {
            $state.go('age-service-policy-modify', { id: ageServicePolicy.ageServiceId});
        }

        function deleteAgeServicePolicy(ageServicePolicy) {
            ageServicePolicyService.deleteAgeServicePolicy(ageServicePolicy.ageServiceId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('age-service-policies', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
