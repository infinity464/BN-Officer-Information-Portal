
(function () {

    'use strict';
    var controllerId = 'dailyProcessesController';
    angular.module('app').controller(controllerId, dailyProcessesController);
    dailyProcessesController.$inject = ['$state', 'dailyProcessService', 'notificationService', '$location'];

    function dailyProcessesController($state, dailyProcessService, notificationService, location) {
        /* jshint validthis:true */
        var vm = this;
        vm.dateModel = {};
        vm.promotionBoardId = 0;
        vm.promotionBoards = [];
        vm.promotionExecutionDate = null;
        vm.executePromotion = executePromotion;
        vm.executePromotionWithoutBoard = executePromotionWithoutBoard;
        vm.executePunishment = executePunishment;
        vm.executeSeniority = executeSeniority;
        vm.executeTransfer = executeTransfer;
        vm.executeAdvanceSearch = executeAdvanceSearch;
        vm.executeNamingConvention = executeNamingConvention;
        vm.updateAgeServicePolicy = updateAgeServicePolicy;
        vm.uploadImageToFolder = uploadImageToFolder;
        vm.executeDatabaseBackup = executeDatabaseBackup;
        init();
        function init() {
            dailyProcessService.getDailyProcesses().then(function (data) {
                vm.dateModel.executionDate = new Date();
                    vm.promotionBoards = data.result;
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function executePromotion() {
            dailyProcessService.executePromotion(vm.promotionBoardId).then(function (data) {
                notificationService.displaySuccess("Promotion Executed Successfully!");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function executePromotionWithoutBoard() {
            dailyProcessService.executePromotionWithoutBoard().then(function (data) {
                notificationService.displaySuccess("Promotion Without Board Executed Successfully!");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function executePunishment() {
            dailyProcessService.executePunishment().then(function (data) {
                notificationService.displaySuccess("Punishment Executed Successfully!");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function executeSeniority() {
            dailyProcessService.executeSeniority().then(function (data) {
                notificationService.displaySuccess("Seniority Executed Successfully!");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function executeTransfer() {
            dailyProcessService.executeTransfer().then(function (data) {
                notificationService.displaySuccess("Transfer Executed Successfully!");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function executeAdvanceSearch() {
            dailyProcessService.executeAdvanceSearch().then(function (data) {
              notificationService.displaySuccess("Advance Search Executed Successfully!");

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function executeNamingConvention() {
            dailyProcessService.executeNamingConvention().then(function (data) {
                notificationService.displaySuccess("Naming Convention Executed Successfully!");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function updateAgeServicePolicy() {
            dailyProcessService.updateAgeServicePolicy().then(function (data) {
                notificationService.displaySuccess(data.result);
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function executeDatabaseBackup() {
            dailyProcessService.executeDataBaseBackup().then(function (data) {
                notificationService.displaySuccess("Database Backup Successfully!");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function uploadImageToFolder() {
            dailyProcessService.uploadImageToFolder().then(function (data) {
                notificationService.displaySuccess(data.result);
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
    }

})();
