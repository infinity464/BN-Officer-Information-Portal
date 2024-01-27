(function () {
    "use strict";

    var app = angular.module('app');
    app.directive('employeeSearch', function () {
        var controller = ['$scope', 'employeeService', 'notificationService', 'officerTransferService',
            function ($scope, employeeService, notificationService, officerTransferService) {
            $scope.employee = angular.copy($scope.employee);
            $scope.searchEmployee = searchEmployee;
            function searchEmployee() {
                employeeService.getEmployeeByPno($scope.employee.pNo).then(function (data) {
					$scope.employee = data.result;

//                        officerTransferService.getLastOfficerTransfer(data.result.employeeId).then(
//                            function (data) {
//                                $scope.preBornOffice = data.result.preBornOffice;
//                                $scope.preAttachOffice = data.result.preAttachOffice;
//                                $scope.preAppointment = data.result.preAppointment;
//
//                            });
	                },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
            }
        }];

        return {
            restrict: 'E',
            scope: { employee: '=ngModel' },
            templateUrl: '/app/templates/employeeSearch.html',
            controller: controller,
        }
    });

})();