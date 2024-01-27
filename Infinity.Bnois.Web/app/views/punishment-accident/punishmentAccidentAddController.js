(function () {

    'use strict';

    var controllerId = 'punishmentAccidentAddController';

    angular.module('app').controller(controllerId, punishmentAccidentAddController);
    punishmentAccidentAddController.$inject = ['$stateParams', '$scope', 'codeValue','backLogService','punishmentAccidentService', 'notificationService', '$state', 'FileUploader', 'OidcManager'];

    function punishmentAccidentAddController($stateParams, $scope, codeValue, backLogService,punishmentAccidentService, notificationService, $state, FileUploader, OidcManager) {
        var vm = this;
        vm.url = codeValue.IMAGE_URL;
        vm.manager = OidcManager.OidcTokenManager();
        vm.punishmentAccidentId = 0;
        vm.title = 'ADD MODE';
        vm.punishmentAccident = {};
        vm.punishmentCategories = [];
        vm.ranks = [];
        vm.transfers = [];

        vm.punishmentSubCategories = [];
        vm.punishmentNatures = [];
        vm.accidentTypes = [];
        vm.punishmentAccidentTypes = [];

        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.punishmentAccidentForm = {};


        vm.punishmentAccidentType = punishmentAccidentType;
        vm.isBackLogChecked = isBackLogChecked;
        vm.punishmentDisabled = false;
        vm.isAccidentDisabled = true;

        vm.subCategoriesByCategory = subCategoriesByCategory;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.punishmentAccidentId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            punishmentAccidentService.getPunishmentAccident(vm.punishmentAccidentId).then(function (data) {
                vm.punishmentAccident = data.result.punishmentAccident;
                vm.punishmentCategories = data.result.punishmentCategories;
                vm.punishmentSubCategories = data.result.punishmentSubCategories;
                vm.punishmentNatures = data.result.punishmentNatures;
                vm.accidentTypes = data.result.accidentTypes;
                vm.ranks = data.result.ranks;
                vm.punishmentAccidentTypes = data.result.punishmentAccidentTypes;

                if (vm.punishmentAccidentId !== 0 && vm.punishmentAccidentId !== '') {

                        vm.punishmentAccident.date = new Date(data.result.punishmentAccident.date);

                    isBackLogChecked(vm.punishmentAccident.isBackLog);

                        punishmentAccidentType(vm.punishmentAccident.type);

                    } else {
                        vm.punishmentAccident.type = 1;
                        
                        punishmentAccidentType(vm.punishmentAccident.type);
                    }
           

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.punishmentAccident.employee.employeeId > 0 ) {
                vm.punishmentAccident.employeeId = vm.punishmentAccident.employee.employeeId;
             
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }


            if (vm.punishmentAccidentId !== 0 && vm.punishmentAccidentId !== '') {
                updatePunishmentAccident();
            } else {
                insertPunishmentAccident();

            }
        }

        function insertPunishmentAccident() {

            punishmentAccidentService.savePunishmentAccident(vm.punishmentAccident).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updatePunishmentAccident() {
            punishmentAccidentService.updatePunishmentAccident(vm.punishmentAccidentId, vm.punishmentAccident).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('punishment-accidents');
        }


        function isBackLogChecked(isBackLog) {
           
            if (isBackLog) {
                if (vm.punishmentAccident.employee.employeeId > 0) {
                    
                    backLogService.getBackLogSelectModels(vm.punishmentAccident.employee.employeeId).then(function (data) {
                        vm.ranks = data.result.ranks;
                        vm.transfers = data.result.transfers;
                    });
                }

            }
        }


        function punishmentAccidentType(type) {
            if (type == 2) {
                vm.punishmentDisabled = true;
                vm.punishmentAccident.accedentType = 1;
                vm.punishmentAccident.punishmentCategoryId = null;
                vm.punishmentAccident.punishmentSubCategoryId = null;
                vm.punishmentAccident.punishmentNatureId = null;
                vm.punishmentAccident.durationMonths = null;
                vm.punishmentAccident.durationDays = null;
                vm.isAccidentDisabled = false;
            }

            else {
                vm.punishmentDisabled = false;
                vm.isAccidentDisabled = true;
                vm.punishmentAccident.accedentType = null;

            }

        }

        function subCategoriesByCategory(categoryId) {
            punishmentAccidentService.getPunishmentSubCategoriesByCategory(categoryId).then(function (data) {
                vm.punishmentSubCategories = data.result.punishmentSubCategories;
            });
        }
        


        //------------------------------------

        //--------------

        var uploader = $scope.uploader = new FileUploader({
            url: punishmentAccidentService.fileUploadUrl(),
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
                vm.punishmentAccident.fileName = vm.fileModel.fileName;
                notificationService.displaySuccess('File Uploaded Successfully');
            }
        };



    }

  

})();
