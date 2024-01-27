(function () {

    'use strict';
    var controllerId = 'specialAppointmentsController';
    angular.module('app').controller(controllerId, specialAppointmentsController);
    specialAppointmentsController.$inject = ['$state', 'specialAppointmentService', 'notificationService', '$location'];

    function specialAppointmentsController($state, specialAppointmentService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.specialAppointmentes = [];
        vm.addSpecialAppointment = addSpecialAppointment;
        vm.updateSpecialAppointment = updateSpecialAppointment;
        vm.deleteSpecialAppointment = deleteSpecialAppointment;
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
            specialAppointmentService.getSpecialAppointments(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.specialAppointments = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addSpecialAppointment() {
            $state.go('special-appointment-create');
        }

        function updateSpecialAppointment(specialAppointment) {
            $state.go('special-appointment-modify', { id: specialAppointment.id});
        }

        function deleteSpecialAppointment(specialAppointment) {
            specialAppointmentService.deleteSpecialAppointment(specialAppointment.id).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });  
            });
        }

        function pageChanged() {
            $state.go('special-appointments', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
