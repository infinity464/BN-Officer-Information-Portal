(function () {

    'use strict';
    var controllerId = 'serviceExamsController';
    angular.module('app').controller(controllerId, serviceExamsController);
    serviceExamsController.$inject = ['$state', 'serviceExamService', 'notificationService', '$location'];

    function serviceExamsController($state, serviceExamService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.serviceExams = [];
        vm.addServiceExam = addServiceExam;
        vm.updateServiceExam = updateServiceExam;
        vm.deleteServiceExam = deleteServiceExam;
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
            serviceExamService.getServiceExams(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.serviceExams = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addServiceExam() {
            $state.go('service-exam-create');
        }

        function updateServiceExam(serviceExam) {
            $state.go('service-exam-modify', { id: serviceExam.serviceExamId});
        }

        function deleteServiceExam(serviceExam) {
            serviceExamService.deleteServiceExam(serviceExam.serviceExamId).then(function (data) {
                $state.go($state.current, { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('service-exams', { pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
