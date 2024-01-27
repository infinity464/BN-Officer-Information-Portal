(function () {
    'use strict';
    angular.module('app').service('dailyProcessService', ['dataConstants', 'apiHttpService', dailyProcessService]);

    function dailyProcessService(dataConstants, apiHttpService) {
        var service = {
            getDailyProcesses: getDailyProcesses,
            executePromotion: executePromotion,
            executeDataBaseBackup: executeDataBaseBackup,
            executePromotionWithoutBoard: executePromotionWithoutBoard,
            executePunishment: executePunishment,
            executePromotionWithoutBoard: executePromotionWithoutBoard,
            executeSeniority: executeSeniority,
            executeTransfer: executeTransfer,
            updateSeaService: updateSeaService,
            updateSeaCmdService: updateSeaCmdService,
            updateSeaServiceDays: updateSeaServiceDays,
            updateSeaServiceYears: updateSeaServiceYears,
            executeAdvanceSearch: executeAdvanceSearch,
            executeNamingConvention: executeNamingConvention,
            updateAgeServicePolicy: updateAgeServicePolicy,
            uploadImageToFolder: uploadImageToFolder
        };
        return service;
        function getDailyProcesses() {
            var url = dataConstants.DAILY_PROCESS_URL + 'get-daily-processes';
            return apiHttpService.GET(url);
        }
        function executeDataBaseBackup() {
            var url = dataConstants.DAILY_PROCESS_URL + 'execute-data-base-backup';
            return apiHttpService.POST(url, {});
        }
        function executePromotion(data) {
           
            var url = dataConstants.DAILY_PROCESS_URL + 'execute-promotion?promotionBoardId=' + data;
            return apiHttpService.POST(url, {});
        }
 
        function executePromotionWithoutBoard() {
            var url = dataConstants.DAILY_PROCESS_URL + 'execute-promotion-without-board';
            return apiHttpService.POST(url, {});
        }

        function updateSeaServiceYears() {
            var url = dataConstants.DAILY_PROCESS_URL + 'update-sea-service-years';
            return apiHttpService.POST(url, {});
        }
        function updateSeaCmdService() {
            var url = dataConstants.DAILY_PROCESS_URL + 'update-sea-cmd-service';
            return apiHttpService.POST(url, {});
        }
        function updateSeaServiceDays() {
            var url = dataConstants.DAILY_PROCESS_URL + 'update-sea-service-days';
            return apiHttpService.POST(url, {});
        }
        function updateSeaService() {
            var url = dataConstants.DAILY_PROCESS_URL + 'update-sea-service';
            return apiHttpService.POST(url, {});
        }

        function executeTransfer() {
            var url = dataConstants.DAILY_PROCESS_URL + 'execute-transfer';
            return apiHttpService.POST(url, {});
        }
        function executeAdvanceSearch() {
            var url = dataConstants.DAILY_PROCESS_URL + 'execute-advance-search';
            return apiHttpService.POST(url, {});
        }


        function executeNamingConvention() {
            var url = dataConstants.DAILY_PROCESS_URL + 'execute-naming-convention';
            return apiHttpService.POST(url, {});
        }

        function executePunishment() {
            var url = dataConstants.DAILY_PROCESS_URL + 'get-update-punishment';
            return apiHttpService.GET(url);
        }
        function executeSeniority() {
            var url = dataConstants.DAILY_PROCESS_URL + 'get-execute-seniority';
            return apiHttpService.GET(url);
        }


        function updateAgeServicePolicy(data) {
            var url = dataConstants.DAILY_PROCESS_URL + 'update-age-service-policy';
            return apiHttpService.PUT(url, {});
        }

        function uploadImageToFolder(data) {
            var url = dataConstants.DAILY_PROCESS_URL + 'upload-image-into-folder';
            return apiHttpService.POST(url, {});
        }
        
        
    }
})();