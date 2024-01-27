(function () {

    'use strict';

    var controllerId = 'oprEntryAddController';

    angular.module('app').controller(controllerId, oprEntryAddController);
    oprEntryAddController.$inject = ['$stateParams', '$scope','oprEntryService','backLogService', 'officeAppointmentService','employeeService', 'notificationService', '$state'];

    function oprEntryAddController($stateParams, $scope, oprEntryService, backLogService,officeAppointmentService, employeeService, notificationService, $state) {
        var vm = this;
        vm.oprEntryId = 0;
        vm.title = 'ADD MODE';
        vm.oprEntry = {};
        vm.oprEntry.employee = {};

        vm.ranks = [];
        vm.specialAptTypes = [];
        vm.suitabilities = [];
        vm.transfers = [];
        vm.occasions = [];
        vm.oprGradings = [];
        vm.recommendationTypes = [];
        vm.attachOffices = [];
        vm.newattachOffices = [];
        vm.selectedAttach = selectedAttach;
        vm.localSearchAttach = localSearchAttach;
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.oprEntryForm = {};
        vm.oprEntry.gradingStatus = "";
        vm.getOfficeAppointmentByOffice = getOfficeAppointmentByOffice;
        vm.getGradingStatus = getGradingStatus;
        vm.searchEmployee = searchEmployee;

        vm.localSearch = localSearch;
        vm.selected = selected;
        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.oprEntryId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            oprEntryService.getoprEntry(vm.oprEntryId).then(function (data) {
                vm.oprEntry = data.result.employeeOpr;

                vm.recommendationTypes = data.result.recommendationTypes;
                vm.oprGradings = data.result.oprGradings.result;
                vm.occasions = data.result.occasions;  
                vm.bornOffices = data.result.bornOffices;  
                vm.attachOffices = data.result.attachOffices;  
                vm.newattachOffices = data.result.attachOffices;                  
                vm.appointments = data.result.appointments;  
                vm.suitabilities = data.result.suitabilities;  
                vm.specialAptTypes = data.result.specialAptTypes;  
                vm.ranks = data.result.ranks;  
                vm.getOfficeAppointmentByOffice = getOfficeAppointmentByOffice;         
                if (vm.oprEntry != null) {
                    getGradingStatus(vm.oprEntry.oprGrade);
                    getTransferHistory(vm.oprEntry.employee.employeeId);
                }
                if (vm.oprEntry.oprFromDate != null && vm.oprEntry.oprToDate != null) {
                    vm.oprEntry.oprFromDate = new Date(data.result.employeeOpr.oprFromDate);
                    vm.oprEntry.oprToDate = new Date(data.result.employeeOpr.oprToDate);
                }
                changeInputValue();

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };



       
        function searchEmployee() {
            employeeService.getEmployeeByPno(vm.oprEntry.employee.pNo).then(function (data) {
              
                    vm.oprEntry.employee = data.result;;
         
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }



        function getTransferHistory(employeeId) {

            backLogService.getBackLogSelectModels(employeeId).then(function (data) {
                vm.transfers = data.result.transfers;
            });


        }



        function localSearch(str) {
            var matches = [];
            vm.attachOffices.forEach(function (transfer) {

                if ((transfer.text.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0)) {
                    matches.push(transfer);

                }
            });
            return matches;
        }


        function selected(object) {
            vm.oprEntry.bOffCId = object.originalObject.value;
     
        }
        function selectedAttach(object) {
            vm.oprEntry.officeId = object.originalObject.value;
            getOfficeAppointmentByOffice(vm.oprEntry.officeId);
        }

        function getOfficeAppointmentByOffice(attachOffice) {

            officeAppointmentService.getOfficeAppointmentByOffice(attachOffice).then(function (data) {
                vm.newattachOffices = data.result;
            });
        }

        function localSearchAttach(str) {
            var matches = [];
            vm.newattachOffices.forEach(function (transfer) {

                if ((transfer.text.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0)) {
                    matches.push(transfer);

                }
            });
            return matches;
        }
        function save() {
            if (vm.oprEntry.employee.employeeId > 0) {
                vm.oprEntry.employeeId = vm.oprEntry.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }

            if (vm.oprEntryId !== 0 && vm.oprEntryId !== '') {
                updateoprEntry();
            } else {
                insertoprEntry();
            }
        }

        function insertoprEntry() {
            oprEntryService.saveoprEntry(vm.oprEntry).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateoprEntry() {
            oprEntryService.updateoprEntry(vm.oprEntryId, vm.oprEntry).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('opr-entries');
        }


        function changeInputValue() {
            if (vm.oprEntryId > 0) {
                if (vm.oprEntry.officeId != null) {
                    var obj = { value: vm.oprEntry.officeId, text: vm.oprEntry.office.shortName };
                    $scope.$broadcast('angucomplete-alt:changeInput', 'officeId', obj);
                }
               

                var obj = { value: vm.oprEntry.bOffCId, text: vm.oprEntry.office1.shortName };
                $scope.$broadcast('angucomplete-alt:changeInput', 'bOffCId', obj);


            }
        }



        function getOfficeAppointmentByOffice(attachOffice) {
            officeAppointmentService.getOfficeAppointmentByOffice(attachOffice).then(function (data) {
                vm.appointments = data.result;
            });
        }

        function getGradingStatus(grading) {
            if (grading>=0 && grading<10) {
                for (var i = 0; i < vm.oprGradings.length; i++) {
                    if ((grading >= vm.oprGradings[i].minGrade) && (grading <= vm.oprGradings[i].maxGrade)) {
                        vm.oprEntry.gradingStatus = vm.oprGradings[i].remark;

                    }
                }
            } else {
                vm.oprEntry.gradingStatus = "";
            }
            
        }



    }

  

})();
