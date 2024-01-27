(function () {

    'use strict';

    var controllerId = 'countriesController';
    angular.module('app').controller(controllerId, countriesController);
    countriesController.$inject = ['$state', 'countryService', 'notificationService', '$location'];

    function countriesController($state, countryService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.countries = [];
        vm.addCountry = addCountry;
        vm.updateCountry = updateCountry;
        vm.deleteCountry = deleteCountry;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;

        if (location.search().ps !== undefined && location.search().ps != null && location.search().ps != '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn != null && location.search().pn != '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q != null && location.search().q != '') {
            vm.searchText = location.search().q;
        }
        init();
        function init() {
            countryService.getCountries(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.countries = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addCountry() {
            $state.go('country-create');
        }

        function updateCountry(country) {
            $state.go('country-modify', { id: country.countryId});
        }

        function deleteCountry(country) {
            countryService.deleteCountry(country.countryId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });  
            });
        }
            
        function pageChanged() {
            $state.go('countries', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
