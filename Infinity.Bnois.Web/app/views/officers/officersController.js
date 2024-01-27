(function () {

    'use strict';
    var controllerId = 'officersController';
    angular.module('app').controller(controllerId, officersController);
    officersController.$inject = ['$state', 'employeeService', 'notificationService', '$location'];

    function officersController($state, employeeService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.empoloyees = [];
        vm.officerDetails = officerDetails;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 100;
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
            employeeService.getEmployeebyName(vm.searchText).then(function (data) {
                vm.employees = data.result;             
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }




        function officerDetails(pNo) {
        $state.goNewTab('current-status-tab', { pno: pNo });

        }


        function pageChanged() {
            $state.go('officers', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
