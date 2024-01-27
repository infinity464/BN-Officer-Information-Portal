
(function () {

	'use strict';

    var controllerId = 'reportViwerController';
	angular.module('app').controller(controllerId, reportViwerController);
    reportViwerController.$inject = [ 'moduleService', 'notificationService', '$location','codeValue'];

    angular.module('app').filter('trustAsResourceUrl', ['$sce', function ($sce) {
        return function (val) {
            return $sce.trustAsResourceUrl(val);
        };
    }]);
    function reportViwerController(moduleService, notificationService, location,codeValue) {
        var vm = this;
        vm.url = codeValue.API_URL;
        vm.FeatureType = 2;
		vm.message = "Now viewing report!";
        vm.reports  = new kendo.data.HierarchicalDataSource({
            transport: {
                read: function (e) {
                    return moduleService.getModuleReports(vm.FeatureType).then(function (data) {
                        e.success(data.result);
					}, function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
                }
            },
            schema: {
               
                model: {
                    id: "id",
                    children: "nodes"
                }
            },

        });
	

		vm.reportId = "RankListReport";
        vm.openReport = function (dataItem) {
            if (dataItem.isChaild == true) {
                vm.reportId = dataItem.id;
                vm.reportName = dataItem.text;
            } 
			
		};
  

	}
})();


	