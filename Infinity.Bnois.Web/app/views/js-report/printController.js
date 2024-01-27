

(function () {

    'use strict';

    var controllerId = 'printController';

    angular.module('app').controller(controllerId, printController);
    printController.$inject = ['$stateParams', 'codeValue','employeeLeaveService', 'employeeService', 'notificationService', 'currentStatusService', '$state'];

    function printController($stateParams, codeValue, employeeLeaveService, employeeService, notificationService, currentStatusService, $state) {
        var vm = this;
        //vm.pNo = '';
        vm.officerName = '';
        vm.currentStatus = {};
        vm.permanentTransferHistories = {};
        vm.courseAttendeds = {};
        vm.leaveInfos = [];
        vm.leaveInfo = {};
        vm.publications = [];
        vm.commendations = [];
        vm.appreciations = [];
        vm.today = new Date();
        vm.date = vm.today.getFullYear() + '-' + (vm.today.getMonth() + 1) + '-' + vm.today.getDate();
        Init();
        function Init() {
            employeeService.getEmployeeByPno(vm.pNo).then(function (data) {
                vm.officerName = data.result.fullNameEng;
                vm.pNo = data.result.pNo;

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
            currentStatusService.getCurrentStatus(vm.pNo).then(function (data) {
                vm.currentStatus = data.result;
                if (vm.currentStatus.photo != null) {
                    vm.currentStatus.photo = codeValue.API_OFFICER_PICTURE_URL + vm.currentStatus.photo;
                } else {
                    vm.currentStatus.photo = "../../../Images/officer.png";
                }
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
            currentStatusService.getTransferHistory(vm.pNo).then(function (data) {
                vm.permanentTransferHistories = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getCourseAttended(vm.pNo).then(function (data) {
                vm.courseAttendeds = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getLeaveInfo(vm.pNo).then(function (data) {
                vm.leaveInfo = data.result;
                vm.plAvailable = Math.abs(vm.leaveInfo.plAvailable);
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            employeeLeaveService.getEmployeeLeaveAndEmployeeInfo(vm.pNo).then(function (data) {
                vm.leaveInfos = data.result.leaveDetails;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getPublication(vm.pNo).then(function (data) {
                vm.publications = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getCommendationAppreciation(vm.pNo, 1).then(function (data) {
                vm.commendations = data.result;
                console.log(vm.commendations);
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

            currentStatusService.getCommendationAppreciation(vm.pNo, 2).then(function (data) {
                vm.appreciations = data.result;
                console.log(vm.appreciations);
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };
    }
})();
