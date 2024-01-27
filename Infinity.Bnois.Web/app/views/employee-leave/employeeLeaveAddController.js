(function () {

    'use strict';

    var controllerId = 'employeeLeaveAddController';

    angular.module('app').controller(controllerId, employeeLeaveAddController);
    employeeLeaveAddController.$inject = ['$scope', '$stateParams', 'downloadService', 'officerTransferService','employeeLeaveService','backLogService', 'notificationService', '$state', 'codeValue', 'OidcManager', 'FileUploader'];

    function employeeLeaveAddController($scope, $stateParams, downloadService, officerTransferService, employeeLeaveService, backLogService, notificationService, $state, codeValue, OidcManager, FileUploader) {
        var vm = this;
        // Additional Value Declared 
        vm.url = codeValue.FILE_URL;
        vm.manager = OidcManager.OidcTokenManager();
        vm.plCode = codeValue.Pl_Code;


        vm.employeeLeaveId = 0;
        vm.title = 'ADD MODE';
        vm.employeeLeave = {};
        vm.fileModel = {};
        vm.leaveTypes = [];
        vm.leavePurposeList = [];
        vm.countries = [];
        vm.leaveBalances = [];
        vm.country = "";
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.SelecteleaveBalances = [];
        vm.employeeLeaveForm = {};
        vm.searchEmployee = searchEmployee;
        vm.editEmployeeLeave = editEmployeeLeave;
        vm.deleteEmployeeLeave = deleteEmployeeLeave;
        vm.getDays = getDays;
        vm.getLeaveDurationInfo = getLeaveDurationInfo;
     
        vm.ranks = [];
        vm.transfers = [];

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.employeeLeaveId = $stateParams.id;
            if (vm.employeeLeaveId > 0) {
                vm.saveButtonText = 'Update';
                vm.title = 'UPDATE MODE';
            }
          
        }

        Init();
        function Init() {
            searchEmployee();
            employeeLeaveService.getEmployeeLeave(vm.employeeLeaveId).then(function (data) {
                vm.employeeLeave = data.result.employeeLeave;
                vm.employeeLeave.createdDate = new Date(vm.employeeLeave.createdDate);
                if (vm.employeeLeave.empLeaveId > 0) {
                   
                    vm.employeeLeave.leaveDetails = data.result.leaveDetails;
                    vm.employeeLeave.fromDate = new Date(vm.employeeLeave.fromDate);
                    vm.employeeLeave.toDate = new Date(vm.employeeLeave.toDate);
                    vm.employeeLeave.leaveBalances = data.result.employeeLeave.leaveBalances;
                    vm.employeeLeave.countryIds = data.result.employeeLeave.countryIds;
                    officerTransferService.getLastOfficerTransfer(vm.employeeLeave.employee.employeeId).then(
                        function (data) {
                            vm.employeeLeave.preBornOffice = data.result.preBornOffice;
                            vm.employeeLeave.preAttachOffice = data.result.preAttachOffice;
                            vm.employeeLeave.preAppointment = data.result.preAppointment;

                        });
                }
                vm.leaveTypes = data.result.leaveTypes;
                vm.commandingOfficerList = data.result.commandingOfficerList;
                vm.countries = data.result.countries;
                vm.leavePurposeList = data.result.leavePurposeList;
                vm.fileModel = data.result.file;
                vm.permission = data.permission;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
            
     
        };

        function save() {
            if (vm.employeeLeave.employee === null) {
                notificationService.displayError('Invalid officers PNo');
            }
            vm.employeeLeave.employeeId = vm.employeeLeave.employee.employeeId;
            vm.employeeLeave.minDate = vm.minDate;
            if (vm.employeeLeaveId !== 0 && vm.employeeLeaveId !== '') {
                updateEmployeeLeave();
            } else {
                insertEmployeeLeave();
            }
        }

        function insertEmployeeLeave() {
            employeeLeaveService.saveEmployeeLeave(vm.employeeLeave).then(function (data) {
                searchEmployee();
                notificationService.displaySuccess("Leave Information Saved Successfully !");

            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateEmployeeLeave() {
            employeeLeaveService.updateEmployeeLeave(vm.employeeLeave.empLeaveId, vm.employeeLeave).then(function (data) {
                searchEmployee();
                notificationService.displaySuccess("Leave Information Updated Successfully !");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function getDays() {

            if (vm.employeeLeave.toDate && vm.employeeLeave.fromDate) {
                var d1 = new Date(vm.employeeLeave.toDate);
                var d2 = new Date(vm.employeeLeave.fromDate);
                var days = d1.getTime() - d2.getTime();
                days = parseInt((((days / 1000) / 60) / 60) / 24);
                if ((days + 1) > 0) {
                    vm.employeeLeave.duration = days + 1;
                  
                }
                else {
                    vm.employeeLeave.duration = null;
                    vm.employeeLeave.toDate = null;
                }

                if (vm.employeeLeave.leaveTypeId == null) {
                    notificationService.displayError('Invalid Leave Type.');
                }
                //else {
                //    var year = d2.getFullYear();
                //    employeeLeaveService.getPrivilegeLeaveDurationInfo(vm.employeeLeave.employee.employeeId, vm.employeeLeave.leaveTypeId, year).then(function (data) {
                //        vm.employeeLeave.leaveBalances = new Object();
                //        vm.employeeLeave.leaveBalances = data.result.leaveBalances;

                //    },
                //        function (errorMessage) {
                //            notificationService.displayError(errorMessage.message);
                //        });
                //}
               
            }

        }
        function getLeaveDurationInfo(leaveType) {

            if (vm.employeeLeave.employee == null) {
                notificationService.displayError('Invalid officers PNo');
            } else {
                vm.employeeLeave.leaveDueCount = 0;
                vm.employeeLeave.leaveBalances = null;
                employeeLeaveService.getEmployeeLeaveDurationInfo(vm.employeeLeave.employee.employeeId, leaveType).then(function (data) {
                    vm.employeeLeave.leaveBalances = data.result.leaveBalances;
                    vm.employeeLeave.leaveDueCount = data.result.employeeLeave.leaveDueCount;

                },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
            }

        }
 
        function editEmployeeLeave(id) {

            $state.go('employeeLeave-modify', { id: id });
        }

        function deleteEmployeeLeave(id) {
            employeeLeaveService.deleteEmployeeLeave(id).then(function (data) {
                searchEmployee();
                $state.go($state.current, { reload: true, inherit: false });
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function close() {
            $state.go('employeeLeave-create');
        }

        function searchEmployee() {
            var pno = "";
            vm.employeeLeave = new Object();
            employeeLeaveService.getEmployeeLeaveAndEmployeeInfo(pno).then(function (data) {
                vm.employeeLeave.employee = data.result.employee;
                vm.employeeLeave.leaveDetails = data.result.leaveDetails;
                //vm.employeeLeaveId = data.result.employee.employeeId;
                officerTransferService.getLastOfficerTransfer(data.result.employee.employeeId).then(
                    function (data) {
                        vm.employeeLeave.preBornOffice = data.result.preBornOffice;
                        vm.employeeLeave.preAttachOffice = data.result.preAttachOffice;
                        vm.employeeLeave.preAppointment = data.result.preAppointment;
                        vm.saveButtonText = 'Save';
                        vm.title = 'ADD MODE';
                    });
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
   
        }
        vm.countryOptions = {
            placeholder: "Select Country...",
            dataTextField: "text",
            dataValueField: "value",
            valuePrimitive: true,
            autoBind: false,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(vm.countries);
                    }
                }
            }
        };



        //----------------------Leave application form upload------------------------------

        var uploader = $scope.uploader = new FileUploader({
            url: employeeLeaveService.fileUploadUrl(),
            queueLimit: 1,
            withCredentials: true,
            headers: {
                'Authorization': 'Bearer ' + vm.manager.access_token,
                'Access-Control-Allow-Credentials': true
            }
        });

        // FILTERS

        uploader.filters.push({
            name: 'fileFilter',
            fn: function (item) {
                var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                return '|pdf|'.indexOf(type) !== -1;
            }
        });
        uploader.filters.push({
            name: 'enforceMaxFileSize',
            fn: function (item) {
                return item.size <= 10485760000; // 10 MiB to bytes
            }
        });


        uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {

            if (filter.name === 'fileFilter') {
                notificationService.displayError('Only pdf file is allowed');
            }

            if (filter.name === 'queueLimit') {
                notificationService.displayError('Only 1(one) file allowed to upload');
            }
            if (filter.name === 'enforceMaxFileSize') {
                notificationService.displayError('File size is ' + item.size + ' ' + 'byts');
            }
        };


        uploader.onCompleteItem = function (fileItem, response, status, headers) {
            if (response.isError) {
                notificationService.displayError(response.message);
                return;
            } else {
                vm.fileModel = response.result;
                vm.employeeLeave.fileName = vm.fileModel.fileName;
                notificationService.displaySuccess('Leave Application Form Uploaded Successfully');
            }
        };
        console.info('uploader', uploader);

    }
})();
