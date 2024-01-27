(function () {

    'use strict';

    var controllerId = 'remarkAddController';

    angular.module('app').controller(controllerId, remarkAddController);
    remarkAddController.$inject = ['$stateParams', 'remarkService','employeeService' ,'notificationService', '$state'];

    function remarkAddController($stateParams, remarkService, employeeService,notificationService, $state) {
        var vm = this;
        vm.remarkId = 0;
        vm.title = 'ADD MODE';
        vm.remark = {};


       
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.remarkForm = {};
       


        vm.type = 0;
        vm.typeName = '';

        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.type = $stateParams.type;
            if (vm.type == 1) {
                vm.typeName = 'Remark';
            }
            else if (vm.type == 2) {
                vm.typeName = 'Persuasion';
            }
            else if (vm.type == 3) {
                vm.typeName = 'NS Note';
            }
        }

        if ($stateParams.pno !== undefined && $stateParams.pno !== null) {
            vm.pNo = $stateParams.pno;
        }



        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.remarkId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            remarkService.getRemark(vm.remarkId).then(function (data) {
                vm.remark = data.result;
                    employeeService.getEmployeeByPno(vm.pNo).then(function (data) {
                        vm.remark.employeeId = data.result.employeeId;
                        vm.remark.transferId = data.result.transferId;
                        },

                        function (errorMessage) {
                            notificationService.displayError(errorMessage.message);
                        });

                vm.remark.type = vm.type;

                if (vm.remarkId !== 0 && vm.remarkId !== '') {
                    if (vm.remark.date != null) {
                        vm.remark.date = new Date(data.result.date);

                    }


                } else if (vm.remarkId == 0 && vm.type==3) {
                    vm.remark.noteType = 1;
                }
           

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
           

            if (vm.remarkId !== 0 && vm.remarkId !== '') {
                updateRemark();
            } else {
                insertRemark();
            }
        }

        function insertRemark() {
            remarkService.saveRemark(vm.remark).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateRemark() {
            remarkService.updateRemark(vm.remarkId, vm.remark).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('current-status-tab.remarks', {type:vm.type});
        }



      
     
      
    }

  

})();
