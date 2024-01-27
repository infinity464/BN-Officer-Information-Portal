

(function () {

    'use strict';

    var controllerId = 'dashboardStreamController';

    angular.module('app').controller(controllerId, dashboardStreamController);
    dashboardStreamController.$inject = ['$stateParams', 'dashboardService','officerStreamService','notificationService', '$state'];

    function dashboardStreamController($stateParams, dashboardService, officerStreamService,notificationService, $state) {
        var vm = this;
        vm.streamTable = false;
        vm.dashboardStreams = [];
        vm.streams = [];
        vm.getEmployeeListByOffice = getEmployeeListByOffice;
        vm.getOfficerList = getOfficerList;
        vm.streamId = 0;

        Init();
        function Init() {

            officerStreamService.getOfficerStreamSelectModels().then(function(data) {
                vm.streams = data.result;
                getEmployeeListByOffice(vm.streamId);

            });

        }

        function getEmployeeListByOffice(streamId) {
           
                vm.streamTable = true;
                dashboardService.getDashboardStream(streamId).then(function (data) {
                        vm.dashboardStreams = data.result;

                    },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
          
           
        }

        function getOfficerList(rankId, branch) {
            $state.goNewTab('stream-officer', { rankId: rankId, branch: branch, streamId:vm.streamId });

        }

    }
})();
