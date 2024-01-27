(function () {

    'use strict';
    var controllerId = 'promotionPoliciesController';
    angular.module('app').controller(controllerId, promotionPoliciesController);
    promotionPoliciesController.$inject = ['$state', 'promotionPolicyService', 'notificationService', '$location'];

    function promotionPoliciesController($state, promotionPolicyService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.promotionPolicies = [];
        vm.addPromotionPolicy = addPromotionPolicy;
        vm.updatePromotionPolicy = updatePromotionPolicy;
        vm.deletePromotionPolicy = deletePromotionPolicy;
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
            promotionPolicyService.getPromotionPolicies().then(function (data) {
                vm.promotionPolicies = data.result;
                    vm.permission = data.permission;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addPromotionPolicy() {
            $state.go('promotion-policy-create');
        }

        function updatePromotionPolicy(promotionPolicy) {
            $state.go('promotion-policy-modify', { id: promotionPolicy.promotionPolicyId });
        }

        function deletePromotionPolicy(promotionPolicy) {
            promotionPolicyService.deletePromotionPolicy(promotionPolicy.promotionPolicyId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('promotionPolicies', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
