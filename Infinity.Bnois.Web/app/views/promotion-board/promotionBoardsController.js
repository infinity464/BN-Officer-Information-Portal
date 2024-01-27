(function () {

    'use strict';
    var controllerId = 'promotionBoardsController';
    angular.module('app').controller(controllerId, promotionBoardsController);
    promotionBoardsController.$inject = ['$state', '$stateParams', 'promotionBoardService', 'downloadService', 'reportService', 'notificationService', '$location'];

    function promotionBoardsController($state, $stateParams, promotionBoardService, downloadService, reportService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.promotionBoardId = 0;
        vm.promotionBoards = [];
        vm.addPromotionBoard = addPromotionBoard;
        vm.updatePromotionBoard = updatePromotionBoard;
        vm.deletePromotionBoard = deletePromotionBoard;
        vm.getPromotionNominations = getPromotionNominations;
        vm.getBoardMembers = getBoardMembers;
        vm.downloadReport = downloadReport;
        vm.downloadPromotionBroadSheetReport = downloadPromotionBroadSheetReport;
        vm.downloadSASBBoardSheetReport = downloadSASBBoardSheetReport;
        vm.downloadPersonalReport = downloadPersonalReport;
        vm.calculateTrace = calculateTrace;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.onSearch = onSearch;
        vm.pageSize = 50;
        vm.pageNumber = 1;
        vm.total = 0;
        vm.type = 0;
        vm.promotionHide = true;
        vm.reportType = 1;
        vm.reportTypes = [{ text: 'PDF', value: 1 }, { text: 'WORD', value: 2 }, { text: 'EXCEL', value: 3 }];



        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.type = $stateParams.type;
            if (vm.type == 1) {
                vm.typeName = "Promotion";
                vm.promotionHide = false;
            }
            else {
                vm.typeName = "SASB";
                vm.promotionHide = true;
            }
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
            promotionBoardService.getPromotionBoards(vm.pageSize, vm.pageNumber, vm.searchText, vm.type).then(function (data) {
                vm.promotionBoards = data.result;
                vm.total = data.total; vm.permission = data.permission;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function addPromotionBoard() {
            $state.go('promotion-board-create', { type: vm.type });
        }

        function updatePromotionBoard(promotionBoard) {
            $state.go('promotion-board-modify', { type: vm.type, id: promotionBoard.promotionBoardId });
        }

        function deletePromotionBoard(promotionBoard) {
            promotionBoardService.deletePromotionBoard(promotionBoard.promotionBoardId).then(function (data) {
                $state.go($state.current, { type: vm.type, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
            });
        }

        function pageChanged() {
            $state.go('promotion-boards', { type: vm.type, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });

        }

        function getPromotionNominations(promotionBoard) {
            $state.go('promotion-nominations', { type: vm.type, title: promotionBoard.boardName, promotionBoardId: promotionBoard.promotionBoardId, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function getBoardMembers(promotionBoard) {
            $state.go('board-members', { type: vm.type, title: promotionBoard.boardName, promotionBoardId: promotionBoard.promotionBoardId, pn: vm.pageNumber, ps: vm.pageSize, q: vm.searchText }, { reload: true, inherit: false });
        }

        function downloadReport(promotionBoard) {
            var url = reportService.downloadReportUrl(promotionBoard.promotionBoardId, vm.reportType, promotionBoard.type);
            downloadService.downloadFile(url);
        }

        function downloadPromotionBroadSheetReport(promotionBoard) {
            var url = reportService.downloadPromotionBroadSheetReportUrl(promotionBoard.promotionBoardId, vm.reportType);
            downloadService.downloadFile(url);
        }

        function downloadPersonalReport(promotionBoard) {
            var url = reportService.downloadPersonalReport(promotionBoard.promotionBoardId, vm.reportType);
            downloadService.downloadFile(url);
        }


        function downloadSASBBoardSheetReport(promotionBoard) {
            var url = reportService.downloadSASBBoardSheetReportUrl(promotionBoard.promotionBoardId, vm.reportType);
            downloadService.downloadFile(url);
        }
        
      

        function calculateTrace(promotionBoard) {
            promotionBoardService.calculateTrace(promotionBoard.promotionBoardId).then(function (data) {
                notificationService.displaySuccess("Trace Calculated Successfully.");
            },
                function (errorMessage) {
                    notificationService.displayError("Trace not Calculated.");
                });
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
