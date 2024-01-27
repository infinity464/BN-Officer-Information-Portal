(function () {

    'use strict';

    var controllerId = 'achievementAddController';

    angular.module('app').controller(controllerId, achievementAddController);
    achievementAddController.$inject = ['$stateParams', '$scope','codeValue' ,'achievementService', 'employeeService', 'officeService', 'officeAppointmentService', 'backLogService', 'notificationService', '$state', 'FileUploader','OidcManager'];

    function achievementAddController($stateParams, $scope, codeValue, achievementService, employeeService, officeService, officeAppointmentService, backLogService, notificationService, $state, FileUploader, OidcManager) {
        var vm = this;
        vm.achievementId = 0;
        vm.url = codeValue.IMAGE_URL;
        vm.manager = OidcManager.OidcTokenManager();
        vm.title = 'ADD MODE';
        vm.achievement = {};
        vm.commendations = [];
        vm.appreciations = [];
        vm.patterns = [];
    
        vm.givenByTypes = [];
        vm.achievementComTypes = [];

        vm.insideOffices = [];
        vm.insideAppointments = [];

        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.achievementForm = {};

        vm.givenByType = givenByType;
        vm.othersDisabled = true;
        vm.ranks = [];
        vm.transfers = [];
        vm.givenTransfers = [];
        vm.isBackLogChecked = isBackLogChecked;

        vm.achievementType = achievementType;
        vm.getAppointmentSelectModelsByOffice = getAppointmentSelectModelsByOffice;
        vm.getChildOfficeSelectModel = getChildOfficeSelectModel;
        vm.commendationDisabled = false;
        vm.appreciationDisabled = true;
        vm.givenTransfer = givenTransfer;
        vm.searchEmployee = searchEmployee;



        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.achievementId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            achievementService.getAchievement(vm.achievementId).then(function (data) {
                vm.achievement = data.result.achievement;
               
                vm.commendations = data.result.commendations;
                vm.insideOffices = data.result.insideOffices;
               
                vm.appreciations = data.result.appreciations;
                vm.patterns = data.result.patterns;
                vm.givenByTypes = data.result.givenByTypes;
                vm.achievementComTypes = data.result.achievementComTypes;

                if (vm.achievementId !== 0 && vm.achievementId !== '') {
                    if (vm.achievement.patternId != null) {
                        getChildOfficeSelectModel(vm.achievement.patternId);
                       
                    }
                    if (vm.achievement.givenByType == 1) {
                        vm.achievement.navyOfficerDesignation = vm.achievement.officerDesignation;
                    } else {
                        vm.achievement.otherOfficerDesignation = vm.achievement.officerDesignation;
                    }

                    if (vm.achievement.date != null) {
                        vm.achievement.date = new Date(data.result.achievement.date);

                    }

                    isBackLogChecked(vm.achievement.isBackLog);
                  //  givenTransfer(vm.achievement.employee1.employeeId);
                    vm.insideAppointments = data.result.insideAppointments;
                    achievementType(data.result.achievement.type);
                    givenByType(data.result.achievement.givenByType);

                } else {
                    vm.achievement.type = 1;
                    vm.achievement.givenByType = 1;
                
                }


            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.achievement.givenByType == 1) {
                vm.achievement.officerDesignation = vm.achievement.navyOfficerDesignation;
            } else {
                vm.achievement.officerDesignation = vm.achievement.otherOfficerDesignation;
            }
            if (vm.achievement.employee.employeeId > 0) {
                vm.achievement.employeeId = vm.achievement.employee.employeeId;
                if (vm.achievement.employee1 !== null) {
                    vm.achievement.givenEmployeeId = vm.achievement.employee1.employeeId;
                }
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }


            if (vm.achievementId !== 0 && vm.achievementId !== '') {
                updateAchievement();
            } else {
                insertAchievement();

            }
        }

        function insertAchievement() {

            achievementService.saveAchievement(vm.achievement).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateAchievement() {
            achievementService.updateAchievement(vm.achievementId, vm.achievement).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('achievements');
        }

        function achievementType(type) {
            if (type == 2) {
                vm.commendationDisabled = true;
                vm.appreciationDisabled = false;
            }
            else if (type == 1) {
                vm.commendationDisabled = false;
                vm.appreciationDisabled = true;

            }
            else {
                vm.commendationDisabled = true;
                vm.appreciationDisabled = true;
                vm.achievement.commendationId = null;

            }

        }

        function givenByType(givenType) {
            if (givenType == 1) {
                vm.othersDisabled = true;
                vm.achievement.patternId = null;
                vm.achievement.officerName = null;
                vm.achievement.officerDesignation = null;


            } else {
                vm.othersDisabled = false;
                vm.achievement.employee1 = null;
                vm.achievement.givenEmployeeId = null;
            }

        }

        function getAppointmentSelectModelsByOffice(officeId) {
            officeAppointmentService.getOfficeAppointmentByOffice(officeId).then(function (data) {
                vm.insideAppointments = data.result;
               
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }


        function getChildOfficeSelectModel(parentId) {
            officeService.getChildOfficeSelectModels(parentId).then(function (data) {
                vm.offices = data.result;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function isBackLogChecked(isBackLog) {
            if (isBackLog) {
                if (vm.achievement.employee.employeeId > 0) {
                    backLogService.getBackLogSelectModels(vm.achievement.employee.employeeId).then(function (data) {
                        vm.ranks = data.result.ranks;
                        vm.transfers = data.result.transfers;
                    });
                }

            }
        }




        function searchEmployee(pno) {
            employeeService.getEmployeeByPno(pno).then(function (data) {
                    vm.achievement.employee1 = data.result;
             //   givenTransfer(vm.achievement.employee1.employeeId);
                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }



        function givenTransfer(employeeId) {
                    backLogService.getBackLogSelectModels(employeeId).then(function (data) {  
                        vm.givenTransfers = data.result.transfers;
                    });
               
        }







        //--------------

        var uploader = $scope.uploader = new FileUploader({
            url: achievementService.fileUploadUrl(),
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
                return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
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
                notificationService.displayError('Only jpg, png, jpeg, bmp  and gif file is allowed');
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
                vm.achievement.fileName = vm.fileModel.fileName;
                notificationService.displaySuccess('Achievement File Uploaded Successfully');
            }
        };





    }



})();
