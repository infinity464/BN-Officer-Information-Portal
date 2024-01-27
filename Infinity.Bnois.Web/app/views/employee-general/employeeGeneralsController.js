(function () {

    'use strict';
    var controllerId = 'employeeGeneralsController';
    angular.module('app').controller(controllerId, employeeGeneralsController);
    employeeGeneralsController.$inject = ['$stateParams', '$state', 'employeeGeneralService', 'notificationService'];

    function employeeGeneralsController($stateParams, $state, employeeGeneralService, notificationService) {
        /* jshint validthis:true */
        var vm = this;
        vm.employeeGeneral = {};
        vm.title = 'General Information';
        vm.updateEmployeeGeneral = updateEmployeeGeneral;
        vm.getAge = getAge;

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        init();
        function init() {
           

            employeeGeneralService.getEmployeeGenerals(vm.employeeId).then(function (data) {
                vm.employeeGeneral = data.result;
                //vm.employeeGeneral.doB = new Date(vm.employeeGeneral.doB).toJSON().slice(0, 10);
                vm.employeeGeneral.doB = new Date(vm.employeeGeneral.doB);
                if (vm.employeeGeneral.doB != null) {
                    vm.employeeGeneral.age = getAge(convertDate(vm.employeeGeneral.doB));
                }

                if (vm.employeeGeneral.seniorityDate != null) {
                    vm.employeeGeneral.seniorityDate = new Date(vm.employeeGeneral.seniorityDate);
                }
                if (vm.employeeGeneral.migrationDate != null) {
                    vm.employeeGeneral.migrationDate = new Date(vm.employeeGeneral.migrationDate);
                }
                if (vm.employeeGeneral.lastRLAvailedDate != null) {
                    vm.employeeGeneral.lastRLAvailedDate = new Date(vm.employeeGeneral.lastRLAvailedDate);
                }
                
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function updateEmployeeGeneral() {
            $state.go('employee-tabs.employee-general-modify', { id: vm.employeeId});
        }
        //function save() {
        //    if (vm.employeeId !== 0 && vm.employeeId !== '') {
        //        updateemployeeGeneralUpdateUpdateUpdateUpdate();
        //    }
        //}

        //function updateEmployeeGeneral() {
        //    employeeGeneralService.updateEmployeeGeneral(vm.employeeId, vm.employeeGeneralUpdateUpdateUpdate).then(function (data) {
        //        close();
        //    },
        //        function (errorMessage) {
        //            notificationService.displayError(errorMessage.message);
        //        });
        //}

        function convertDate(inputFormat) {
            function pad(s) { return (s < 10) ? '0' + s : s; }
            var d = new Date(inputFormat);
            return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
        }

        function getAge(dateString ) {
            
            var now = new Date();

            var yearNow = now.getYear();
            var monthNow = now.getMonth();
            var dateNow = now.getDate();

            var dob = new Date(dateString.substring(6, 10),
                 dateString.substring(3, 5) - 1,
                 dateString.substring(0, 2));
            

            var yearDob = dob.getYear();
            var monthDob = dob.getMonth();
            var dateDob = dob.getDate();

           var  yearAge = yearNow - yearDob;

            if (monthNow >= monthDob)
                var monthAge = monthNow - monthDob;
            else {
                yearAge--;
                var monthAge = 12 + monthNow - monthDob;
            }

            if (dateNow >= dateDob)
                var dateAge = dateNow - dateDob;
            else {
                monthAge--;
                var dateAge = 31 + dateNow - dateDob;

                if (monthAge < 0) {
                    monthAge = 11;
                    yearAge--;
                }
            }

            return yearAge + 'y ' + monthAge + 'm ' + dateAge + 'd';
        }
    }

})();
