

(function () {

    'use strict';
    var controllerId = 'courseTransfersController';
    angular.module('app').controller(controllerId, courseTransfersController);
    courseTransfersController.$inject = ['$state', '$scope','codeValue', 'downloadService', 'officerTransferService', 'nominationService', 'nominationDetailService', 'employeeService', 'officeAppointmentService', 'notificationService', '$location', 'OidcManager', 'FileUploader'];

    function courseTransfersController($state, $scope, codeValue, downloadService, officerTransferService, nominationService, nominationDetailService, employeeService, officeAppointmentService, notificationService, $location, OidcManager, FileUploader) {

        /* jshint validthis:true */
        var vm = this;
        vm.url = codeValue.FILE_URL;
        vm.manager = OidcManager.OidcTokenManager();
        vm.officerTransfers = [];
        vm.officerTemporaryTransfers = [];
        vm.officerTransferId = 0;
        vm.officerTransfer = {};
        vm.transferModes = [];
        vm.temporaryTransferTypes = [];
        vm.nominationList = [];
        vm.nominatedList = [];
        vm.newBorns = [];
        vm.trainingPlans = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.closeAll = closeAll;
        vm.officerTransferForm = {};
        vm.temporaryDisabled = true;

        vm.getTransferMode = getTransferMode;
        vm.getNominatedList = getNominatedList;
        vm.getEmployeeInfo = getEmployeeInfo;
        vm.deleteOfficerTransfer = deleteOfficerTransfer;
        vm.getOfficeTransferInfo = getOfficeTransferInfo;
        vm.updateOfficeTransfer = updateOfficeTransfer;
        vm.localSearch = localSearch;
        vm.selected = selected;
        vm.localSearchBorn = localSearchBorn;
        vm.selectedBorn = selectedBorn;

        $scope.itemValue = true;

        init();
        function init() {

            officerTransferService.getOfficerTransfer(vm.officerTransferId).then(function (data) {
             nominationService.getNominationByType(1).then(function (data) {
                        vm.nominationList = data.result;
                    });
                vm.officerTransfer = data.result.transfer;
                vm.transferTypes = data.result.transferTypes;
                vm.transferModes = data.result.transferModes;
             vm.ranks = data.result.ranks;

                vm.temporaryTransferTypes = data.result.temporaryTransferTypes;
                vm.newBorns = data.result.newBorns;
     
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function save() {
            vm.officerTransfer.transferFor = 2;
            if (vm.officerTransfer.employee.employeeId > 0) {
                vm.officerTransfer.employeeId = vm.officerTransfer.employee.employeeId;
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }
             insertOfficerTransfer();
          
        }

        function insertOfficerTransfer() {
            officerTransferService.saveOfficerTransfer(vm.officerTransfer).then(function (data) {
                    officerTransferService.getOfficerTransfers(vm.officerTransfer.employeeId, 2).then(function (data) {
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


                    getTransferMode(vm.officerTransfer.transferMode);

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });

        }



        function updateOfficeTransfer(transferId, officeTransfer) {
            officerTransferService.updateOfficerTransfer(transferId, officeTransfer).then(function (data) {
                    officerTransferService.getOfficerTransfers(vm.officerTransfer.employeeId, 2).then(function (data) {
                        vm.officerTransfers = data.result.officerTransfers;
                        vm.officerTemporaryTransfers = data.result.officerTemporaryTransfers;
                        close();
                    });
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function closeAll() {
            vm.officerTransfer.employee = null;
            $scope.officerTransfers = null;
            $scope.preBornOffice = null;
            $scope.preAttachOffice = null;
            $scope.preAppointment = null;
            close();
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
            
            vm.officerTransfer.extraAppointment = null;
            vm.officerTransfer.remarks = null;
            vm.officerTransfer.uploadPdf = false;
            vm.backLog = false;
            vm.officerTransfer.isBackLog = false;

            $scope.$broadcast('angucomplete-alt:changeInput', 'currentBornOfficeId', ' ');

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


        function getNominatedList(nominationId) {
            closeAll();
            nominationDetailService.getNominatedList(nominationId).then(function (data) {
                vm.nominatedList = data.result;
            });

        }

        function getEmployeeInfo(employeeId) {

            employeeService.getEmployeeByPno(employeeId).then(function(data) {
                vm.officerTransfer.employee = data.result;
                officerTransferService.getOfficerTransfers(vm.officerTransfer.employee.employeeId, 2).then(
                    function(data) {
                        vm.officerTransfers = data.result.officerTransfers;
                        vm.officerTemporaryTransfers = data.result.officerTemporaryTransfers;
                        vm.permission = data.permission;
                        if (data.result != null) {
                            $scope.itemValue = false;
                        }
                    },
                    function(errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });

                officerTransferService.getLastOfficerTransfer(vm.officerTransfer.employee.employeeId).then(
                    function(data) {
                        $scope.preBornOffice = data.result.preBornOffice;
                        $scope.preAttachOffice = data.result.preAttachOffice;
                        $scope.preAppointment = data.result.preAppointment;

                    });

            });
        }


        function localSearch(str) {
            var matches = [];
            vm.nominationList.forEach(function (transfer) {

                if ((transfer.text.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0)) {
                    matches.push(transfer);

                }
            });
            return matches;
        }


        function selected(object) {
            vm.officerTransfer.nominationId = object.originalObject.value;

            getNominatedList(vm.officerTransfer.nominationId);

        }



        function localSearchBorn(str) {
            var matches = [];
            vm.newBorns.forEach(function (transfer) {

                if ((transfer.text.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0)) {
                    matches.push(transfer);

                }
            });
            return matches;
        }


        function selectedBorn(object) {
            vm.officerTransfer.currentBornOfficeId = object.originalObject.value;
           

        }


        function deleteOfficerTransfer(officerTransfer) {
                officerTransferService.deleteOfficerTransfer(officerTransfer.transferId).then(function (data) {
                    officerTransferService.getOfficerTransfers(officerTransfer.employeeId,2).then(function (data) {
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
        //----------------------------------Course Pdf File Upload----------------------------------

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
                notificationService.displaySuccess('Course File Uploaded Successfully');
            }
        };
        console.info('uploader', uploader);
      
    }

})();

