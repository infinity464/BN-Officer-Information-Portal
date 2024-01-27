

(function () {

    'use strict';
    var controllerId = 'officeAppointmentsController';
    angular.module('app').controller(controllerId, officeAppointmentsController);
    officeAppointmentsController.$inject = ['$state', '$stateParams','$rootScope','officeAppointmentService', 'notificationService', '$location'];

    function officeAppointmentsController($state, $stateParams, $rootScope, officeAppointmentService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.officeId = 0;
        vm.officeAppointments = [];
        vm.addOfficeAppointment = addOfficeAppointment;
        vm.updateOfficeAppointment = updateOfficeAppointment;
        vm.deleteOfficeAppointment = deleteOfficeAppointment;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 500;
        vm.pageNumber = 1;
        vm.total = 0;
        vm.offAppId = 0;

        if ($rootScope.officeId === 0) {
            vm.addDisabled = true;
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
            officeAppointmentService.getOfficeAppointments(vm.pageSize, vm.pageNumber, vm.searchText, $rootScope.officeId,1).then(function (data) {
                vm.officeAppointments = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addOfficeAppointment() {
            $state.go('office-tabs.office-appointment-create', { id: $rootScope.officeId, appointmentId:vm.offAppId });
        }

        function updateOfficeAppointment(officeAppointment) {
            $state.go('office-tabs.office-appointment-modify', { id: $rootScope.officeId, appointmentId: officeAppointment.offAppId });
        }

        function deleteOfficeAppointment(officeAppointment) {
            officeAppointmentService.deleteOfficeAppointment(officeAppointment.offAppId).then(function (data) {
                officeAppointmentService
                    .getOfficeAppointments(vm.pageSize, vm.pageNumber, vm.searchText, $rootScope.officeId, 1).then(
                        function(data) {
                            vm.officeAppointments = data.result;
                            vm.total = data.total;
                            vm.permission = data.permission;
                        });
            });
        }

        function pageChanged() {
            $state.go('office-tabs.office-appointments', { id: $rootScope.officeId, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();

