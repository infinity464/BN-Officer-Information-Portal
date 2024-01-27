

(function () {

    'use strict';

    var controllerId = 'dashboardCategoryController';

    angular.module('app').controller(controllerId, dashboardCategoryController);
    dashboardCategoryController.$inject = ['$stateParams', 'dashboardService','subCategoryService','notificationService', '$state'];

    function dashboardCategoryController($stateParams, dashboardService, subCategoryService,notificationService, $state) {
        var vm = this;



        vm.dashboardCategories = [];
        vm.categories = [
            { 'value': 1, 'text': 'BNVR Officer' },
            { 'value': 1, 'text': 'Deputed Officer (Surg)' },
            { 'value': 1, 'text': 'Deputed Officer (AFNS)' },
            { 'value': 1, 'text': 'Direct Entry Officer (L & E)' },
            { 'value': 1, 'text': 'HON Officer' },
            { 'value': 1, 'text': 'Instr Officer' },
            { 'value': 1, 'text': 'Permanent Comm Cadet Entry Officer' },
            { 'value': 1, 'text': 'SD Officer' }

            ];
        vm.getEmployeeListByOffice = getEmployeeListByOffice;
        vm.getOfficerList = getOfficerList;
        vm.categoryId = -1;

        Init();
        function Init() {
            subCategoryService.getSubCategorySelectModels().then(function (data) {
                vm.categories = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            getEmployeeListByOffice(vm.categoryId);

        }

        function getEmployeeListByOffice(categoryId) {
            if (categoryId == null) {
                categoryId = -1;
            }

                
                dashboardService.getDashboardCategory(categoryId).then(function (data) {
                        vm.dashboardCategories = data.result;

                    },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
          
           
        }

        function getOfficerList(rankId, branch) {
            $state.goNewTab('category-officer', { rankId: rankId, branch: branch, categoryId:vm.categoryId });

        }

    }
})();
