(function () {

    'use strict';

    var controllerId = 'foreignProjectAddController';

    angular.module('app').controller(controllerId, foreignProjectAddController);
    foreignProjectAddController.$inject = ['$stateParams', 'foreignProjectService', 'notificationService', '$state'];

    function foreignProjectAddController($stateParams, foreignProjectService, notificationService, $state) {
        var vm = this;
        vm.foreignProjectId = 0;
        vm.title = 'ADD MODE';
        vm.foreignProject = {};
        vm.countries = [];
      
       
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.foreignProjectForm = {};

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.foreignProjectId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            foreignProjectService.getForeignProject(vm.foreignProjectId).then(function (data) {
                vm.foreignProject = data.result.foreignProject;

                vm.countries = data.result.countries;
             
                   
                    if (vm.foreignProjectId !== 0 && vm.foreignProjectId !== '') {

                        if (vm.foreignProject.fromDate != null) {
                            vm.foreignProject.fromDate = new Date(data.result.foreignProject.fromDate);

                        }
                        if (vm.foreignProject.toDate != null) {
                            vm.foreignProject.toDate = new Date(data.result.foreignProject.toDate);

                        }


                    }
           

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.foreignProject.employee.employeeId > 0) {
                vm.foreignProject.employeeId = vm.foreignProject.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }

            if (vm.foreignProjectId !== 0 && vm.foreignProjectId !== '') {
                updateForeignProject();
            } else {
                insertForeignProject();
            }
        }

        function insertForeignProject() {
            foreignProjectService.saveForeignProject(vm.foreignProject).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateForeignProject() {
            foreignProjectService.updateForeignProject(vm.foreignProjectId, vm.foreignProject).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('foreign-projects');
        }

    }

  

})();
