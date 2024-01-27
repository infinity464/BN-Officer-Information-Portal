

(function () {

    'use strict';

    var controllerId = 'dashboardOfficeAppointmentController';

    angular.module('app').controller(controllerId, dashboardOfficeAppointmentController);
    dashboardOfficeAppointmentController.$inject = ['dashboardService', 'rankService', 'officerTransferService', 'notificationService', '$state'];

    function dashboardOfficeAppointmentController(dashboardService, rankService, officerTransferService, notificationService, $state) {
        var vm = this;
        vm.streamTable = false;
        vm.officeAppointments = [];
        vm.underMissions = [];
        vm.underCourses = [];
        vm.ranks = [];
        vm.officerFroms = [];
        vm.getEmployeeListByOffice = getEmployeeListByOffice;
        vm.officerDetails= officerDetails;
        vm.transferType = 0;
        vm.rankId = 0;
        vm.displayId = 0;
        vm.displayValue = [
            { 'text': 'Approved', 'value': 1 }, { 'text': 'Authorized', 'value': 0 }, { 'text': 'All', 'value': -1 }
        ];


        vm.ranks = [
            { 'text': 'Admiral', 'value': 2 }, { 'text': 'Vice Admiral', 'value': 3 }, { 'text': 'Rear Admiral', 'value': 4 }
            , { 'text': 'Cdre', 'value': 5 }, { 'text': 'Capt', 'value': 6 }, { 'text': 'Cdr', 'value': 7 }
            , { 'text': 'Lt Cdr', 'value': 8 }, { 'text': 'Lt', 'value': 9 }, { 'text': 'Sub Lt', 'value': 10 }
        ];

        Init();
        function Init() {


            officerTransferService.getOfficerFromSelectModel().then(function (data) {
                vm.officerFroms = data.result;

            });


            getEmployeeListByOffice(1, 2, 1);
        }

        function getEmployeeListByOffice(officeType, rankId, displayId) {

            dashboardService.getDashboardOfficeAppointment(officeType, rankId, displayId).then(function (data) {
                vm.officeAppointments = data.result;

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            dashboardService.getDashboardUnderCourse(rankId).then(function (data) {
                    vm.underCourses = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            dashboardService.getDashboardUnderMission( rankId).then(function (data) {
                    vm.underMissions = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }





        function officerDetails(pNo) {
            $state.goNewTab('current-status-tab', { pno: pNo });

        }


    }
})();
