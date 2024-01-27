

(function () {

    'use strict';
    var controllerId = 'officeTransfersController';
    angular.module('app').controller(controllerId, officeTransfersController);
    officeTransfersController.$inject = ['$state', '$scope', 'downloadService', 'officerTransferService', 'officeService', 'officeAppointmentService', 'employeeService', 'notificationService','codeValue', '$location','OidcManager','FileUploader'];

    function officeTransfersController($state, $scope, downloadService, officerTransferService, officeService, officeAppointmentService, employeeService, notificationService, codeValue, location, OidcManager, FileUploader) {

        /* jshint validthis:true */
        var vm = this;
        vm.URL = codeValue.FILE_URL;
        vm.manager = OidcManager.OidcTokenManager();
        vm.officerTransfers = [];
        vm.officerTemporaryTransfers = [];

        vm.officerTransferId = 0;

        vm.officerTransfer = {};
        vm.officerTransfer.attachOfficeId = 0;
        vm.transferTypes = [];
        vm.transferModes = [];
        vm.temporaryTransferTypes = [];
        vm.appointments = [];

        vm.ranks = [];
        vm.districts = [];
       
        vm.newBorns = [];
        vm.newAttaches = [];
        vm.extraOffice = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.officerTransferForm = {};
        vm.temporaryDisabled = true;
        vm.appointmentDisabled = true;
        vm.attachOffice = {};

        vm.getOfficeAppointmentByOffice = getOfficeAppointmentByOffice;
        vm.getExtraAppointmentByOffice = getExtraAppointmentByOffice;
        vm.getAdditionalAppointmentByOffice = getAdditionalAppointmentByOffice;
        vm.getOfficeSelectModelByType = getOfficeSelectModelByType;
        vm.getTransferMode = getTransferMode;
        vm.getEmployeeInfo = getEmployeeInfo;
        vm.deleteOfficerTransfer = deleteOfficerTransfer;
        vm.getOfficeTransferInfo = getOfficeTransferInfo;
        vm.updateOfficeTransfer = updateOfficeTransfer;
        vm.downloadTransferFile = downloadTransferFile;
        vm.file = {};

        vm.localSearch = localSearch;
        vm.selected = selected;
        vm.localSearchAttach = localSearchAttach;
        vm.selectedAttach = selectedAttach;
        
        $scope.itemValue = true;

        init();
        function init() {

           
            officerTransferService.getOfficerTransfer(vm.officerTransferId).then(function (data) {
               
                vm.officerTransfer = data.result.transfer;
                vm.transferTypes = data.result.transferTypes;
                vm.transferModes = data.result.transferModes;

                vm.temporaryTransferTypes = data.result.temporaryTransferTypes;
                vm.appointments = data.result.appointments;
                vm.newBorns = data.result.newBorns;
                vm.ranks = data.result.ranks;
                vm.districts = data.result.districts;
                
                vm.officerTransfer.appointmentType = 1;

                getAdditionalAppointmentByOffice(vm.officerTransfer.appointmentType);
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        
        function save() {
            vm.officerTransfer.transferFor = 1;
            if (vm.officerTransfer.employee.employeeId > 0) {
                vm.officerTransfer.employeeId = vm.officerTransfer.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }
            insertOfficerTransfer();
          
        }

        function downloadTransferFile(officerTransfer) {

            if (officerTransfer.fileName != null) {
                return true;
            }
            else {
                notificationService.displayError("File not Found!");
                return false;
            }       
        }

        function insertOfficerTransfer() {
            officerTransferService.saveOfficerTransfer(vm.officerTransfer).then(function (data) {
                    officerTransferService.getOfficerTransfers(vm.officerTransfer.employeeId, 1).then(function (data) {
                        vm.officerTransfers = data.result.officerTransfers;
                        vm.officerTemporaryTransfers = data.result.officerTemporaryTransfers;
                       
                        close();
                    });
//                    officerTransferService.getLastOfficerTransfer(vm.officerTransfer.employee.employeeId).then(
//                        function (data) {
//                            $scope.preBornOffice = data.result.preBornOffice;
//                            $scope.preAttachOffice = data.result.preAttachOffice;
//                            $scope.preAppointment = data.result.preAppointment;
//
//                        });
                  notificationService.displaySuccess("Transferred Successfully!!");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function getOfficeTransferInfo(officerTransfer) {
            officerTransferService.getOfficerTransfer(officerTransfer.transferId).then(function (data) {

                    vm.officerTransfer = data.result.transfer;
                    vm.officerTransfer.newBornId = data.result.transfer.preBornId;
                    vm.transferModes = data.result.transferModes;
                    vm.temporaryTransferTypes = data.result.temporaryTransferTypes;

                    vm.officerTransfer.fromDate = new Date(data.result.transfer.fromDate);
                    if (vm.officerTransfer.toDate != null) {
                        vm.officerTransfer.toDate = new Date(data.result.transfer.toDate);
                    }


                    //getTransferMode(vm.officerTransfer.transferMode);

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }



        function updateOfficeTransfer(transferId, officeTransfer) {
            officerTransferService.updateOfficerTransfer(transferId, officeTransfer).then(function (data) {
                    officerTransferService.getOfficerTransfers(vm.officerTransfer.employeeId, 1).then(function (data) {
                        vm.officerTransfers = data.result.officerTransfers;
                        vm.officerTemporaryTransfers = data.result.officerTemporaryTransfers;
                        close();
                    });
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }



        function close() {
            vm.officerTransfer.transferMode = null;
            vm.officerTransfer.tranferType = null;
            vm.officerTransfer.tempTransferType = null;
            vm.officerTransfer.currentBornOfficeId = null;
            vm.officerTransfer.fromDate = null;
            vm.officerTransfer.toDate = null;
            vm.officerTransfer.attachOfficeId = null;
            vm.officerTransfer.appointmentId = null;
            vm.officerTransfer.isExtraAppointment = false;
            vm.attachOffice = null;
            vm.officerTransfer.extraAppointment = null;
            vm.officerTransfer.remarks = null;
            vm.backLog = false;
            vm.officerTransfer.uploadPdf = false;
            vm.officerTransfer.appointmentType = 1;
            vm.officerTransfer.isBackLog = false;
            vm.officerTransfer.districtId = null;

            $scope.$broadcast('angucomplete-alt:changeInput', 'attachOfficeId', ' ');
            $scope.$broadcast('angucomplete-alt:changeInput', 'currentBornOfficeId', ' ');
        }

        function getEmployeeInfo(employeeId) {

            employeeService.getEmployeeByPno(employeeId).then(function (data) {
                vm.officerTransfer.employee = data.result;
                officerTransferService.getOfficerTransfers(vm.officerTransfer.employee.employeeId, 1).then(
                    function (data) {
                        vm.officerTransfers = data.result.officerTransfers;
                        vm.officerTemporaryTransfers = data.result.officerTemporaryTransfers;
                        vm.permission = data.permission;
                        if (data.result != null) {
                            $scope.itemValue = false;
                        }
                    },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });

                officerTransferService.getLastOfficerTransfer(vm.officerTransfer.employee.employeeId).then(
                    function (data) {
                        $scope.preBornOffice = data.result.preBornOffice;
                        $scope.preAttachOffice = data.result.preAttachOffice;
                        $scope.preAppointment = data.result.preAppointment;

                    });

            }, function (errorMessage) {
                notificationService.displayError(errorMessage.message);
            });
        }



        function getOfficeAppointmentByOffice(attachOffice) {
            
            officeAppointmentService.getOfficeAppointmentByOffice(attachOffice).then(function (data) {
                vm.appointments = data.result;
            });
        }
        function getOfficeSelectModelByType(type) {
            
            officeService.getOfficeSelectModelByType(type).then(function (data) {
                vm.newAttaches = data.result;
            });
        }

        function getExtraAppointmentByOffice(attachOffice) {
           
            
            officeAppointmentService.getOfficeAppointmentByOffice(attachOffice).then(function (data) {
                vm.extraAppointments = data.result;
            });
        }

        function getAdditionalAppointmentByOffice(appointmentType) {
           
            if (appointmentType == 2 || appointmentType == 3) {
                vm.appointmentDisabled = true;
                vm.officerTransfer.appointmentId = null;
            } else {
                vm.appointmentDisabled = false;
               
            }
           
        }




        function getTransferMode(transferMode) {
            if (transferMode === 2) {
                vm.temporaryDisabled = false;
                vm.officerTransfer.tempTransferType = 1;
                vm.backLog = false;
                vm.officerTransfer.isBackLog = false;

            } else {
                vm.temporaryDisabled = true;
                vm.officerTransfer.tempTransferType = null;
                vm.backLog = true;
            }
        }



        function deleteOfficerTransfer(officerTransfer) {
            officerTransferService.deleteOfficerTransfer(officerTransfer.transferId).then(function (data) {
                officerTransferService.getOfficerTransfers(officerTransfer.employeeId, 1).then(function (data) {
                    vm.officerTransfers = data.result.officerTransfers;
                    vm.officerTemporaryTransfers = data.result.officerTemporaryTransfers;
                    if (data.result != null) {
                        $scope.itemValue = false;
                    }
                });
                close();
                officerTransferService.getLastOfficerTransfer(officerTransfer.employee.employeeId).then(
                    function (data) {
                        $scope.preBornOffice = data.result.preBornOffice;
                        $scope.preAttachOffice = data.result.preAttachOffice;
                        $scope.preAppointment = data.result.preAppointment;

                    });
            });
        }


        function localSearch(str) {
            var matches = [];
            vm.newBorns.forEach(function (transfer) {

                if ((transfer.text.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0)) {
                    matches.push(transfer);

                }
            });
            return matches;
        }


        function selected(object) {
            vm.officerTransfer.currentBornOfficeId = object.originalObject.value;
          
        }



        function localSearchAttach(str) {
            var matches = [];
            vm.newAttaches.forEach(function (transfer) {

                if ((transfer.text.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0)) {
                    matches.push(transfer);

                }
            });
            return matches;
        }


        function selectedAttach(object) {
            vm.officerTransfer.attachOfficeId = object.originalObject.value;
            getOfficeAppointmentByOffice(vm.officerTransfer.attachOfficeId);
        }



        //----------------------------------Transfer Pdf File Upload----------------------------------

        var uploader = $scope.uploader = new FileUploader({
            url: officerTransferService.fileUploadUrl(),
            queueLimit: 1,
            withCredentials: true,
            queueLimit: 1,
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
                vm.file = response.result;
                vm.officerTransfer.fileName = vm.file.fileName
                notificationService.displaySuccess('Transfer File Uploaded Successfully');
            }
        };
        console.info('uploader', uploader);
    }

})();

