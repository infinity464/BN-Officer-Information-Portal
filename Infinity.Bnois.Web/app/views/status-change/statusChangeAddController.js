(function () {

    'use strict';

    var controllerId = 'statusChangeAddController';

    angular.module('app').controller(controllerId, statusChangeAddController);
    statusChangeAddController.$inject = ['$stateParams', 'statusChangeService', 'courseService','trainingPlanService' ,'notificationService', '$state'];

    function statusChangeAddController($stateParams, statusChangeService, courseService, trainingPlanService,notificationService, $state) {
        var vm = this;
        vm.statusChangeId = 0;
        vm.title = 'ADD MODE';
        vm.statusChange = {};
        vm.selectModels = [];
      
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.statusChangeForm = {};
  
        vm.statusTypeSelectModels = statusTypeSelectModels;
        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.statusChangeId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }
        vm.statusTypes = [
            { 'value': 1, 'text': 'Medical Category' }, { 'value': 2, 'text': 'Eye Vision' },
            { 'value': 3, 'text': 'Commission Type' }, { 'value': 4, 'text': 'Branch' }
        ];
        Init();
        function Init() {
            statusChangeService.getStatusChange(vm.statusChangeId).then(function (data) {
                vm.statusChange = data.result.statusChange;

                vm.selectModels = data.result.selectModels;

                if (vm.statusChangeId > 0) {
                    statusTypeSelectModels(vm.statusChange.statusType);

                    if (vm.statusChange.date != null) {
                        vm.statusChange.date = new Date(data.result.statusChange.date);

                    }
                }
               
           

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.statusChange.employee.employeeId > 0) {
                vm.statusChange.employeeId = vm.statusChange.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }

            if (vm.statusChangeId !== 0 && vm.statusChangeId !== '') {
                updateStatusChange();
            } else {
                insertStatusChange();
            }
        }

        function insertStatusChange() {
            statusChangeService.saveStatusChange(vm.statusChange).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateStatusChange() {
            statusChangeService.updateStatusChange(vm.statusChangeId, vm.statusChange).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('status-changes');
        }



        function statusTypeSelectModels(type, employeeId) {
            if (type != null && employeeId != null) {
                statusChangeService.getStatusChangeSelectModels(type, employeeId).then(function (data) {
                    vm.selectModels = data.result.selectModels;
                    vm.statusChange.previousId = data.result.currentStatusId;
                });
            }
          
          
        }

     
        
    }

  

})();
