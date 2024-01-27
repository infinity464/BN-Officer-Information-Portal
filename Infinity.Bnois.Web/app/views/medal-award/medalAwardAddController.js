(function () {

    'use strict';

    var controllerId = 'medalAwardAddController';

    angular.module('app').controller(controllerId, medalAwardAddController);
    medalAwardAddController.$inject = ['$stateParams', '$scope','codeValue', 'medalAwardService', 'backLogService', 'notificationService', '$state', 'FileUploader', 'OidcManager'];

    function medalAwardAddController($stateParams, $scope, codeValue, medalAwardService, backLogService, notificationService, $state, FileUploader, OidcManager) {
        var vm = this;
        vm.url = codeValue.IMAGE_URL;
        vm.manager = OidcManager.OidcTokenManager();
        vm.medalAwardId = 0;
        vm.title = 'ADD MODE';
        vm.medalAward = {};
        vm.commendations = [];
        vm.appreciations = [];
        vm.patterns = [];
        vm.offices = [];
        vm.medalAwardTypes = [];
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.medalAwardForm = {};

        vm.publicationsByCategory = publicationsByCategory;

        vm.medalAwardType = medalAwardType;
        vm.awardDisabled = false;
        vm.medalDisabled = true;
        vm.publicationDisabled = true;
        vm.publicationCategoryDisabled = true;

        vm.ranks = [];
        vm.transfers = [];
        vm.isBackLogChecked = isBackLogChecked;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.medalAwardId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            medalAwardService.getMedalAward(vm.medalAwardId).then(function (data) {
                vm.medalAward = data.result.medalAward;
                vm.awards = data.result.awards;
                vm.medals = data.result.medals;
                vm.publicationCategories = data.result.publicationCategories;
                vm.publications = data.result.publications;
                vm.medalAwardTypes = data.result.medalAwardTypes;
                vm.medalAwardComTypes = data.result.medalAwardComTypes;
                    if (vm.medalAwardId !== 0 && vm.medalAwardId !== '') {
                        vm.medalAward.date = new Date(data.result.medalAward.date);
                        medalAwardType(vm.medalAward.type);

                        isBackLogChecked(vm.medalAward.isBackLog);
                    } else {
                        vm.medalAward.type = 1;
                        medalAwardType(vm.medalAward.type);
                    }
           

               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {
            if (vm.medalAward.employee.employeeId > 0 ) {
                vm.medalAward.employeeId = vm.medalAward.employee.employeeId;
             
            } else {
                notificationService.displayError("Please Search Valid Officer by Pno!!");
            }


            if (vm.medalAwardId !== 0 && vm.medalAwardId !== '') {
                updateMedalAward();
            } else {
                insertMedalAward();

            }
        }

        function insertMedalAward() {

            medalAwardService.saveMedalAward(vm.medalAward).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateMedalAward() {
            medalAwardService.updateMedalAward(vm.medalAwardId, vm.medalAward).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('medal-awards');
        }

        function medalAwardType(type) {
            if (type == 1) {
                vm.awardDisabled = false;
                vm.medalDisabled = true;
                vm.publicationDisabled = true;
                vm.publicationCategoryDisabled = true;

                vm.medalAward.medalId = null;
                vm.medalAward.publicationId = null;
                vm.medalAward.publicationCategoryId = null;

            }
            else if (type == 2) {
                vm.awardDisabled = true;
                vm.medalDisabled = false;
                vm.publicationDisabled = true;
                vm.publicationCategoryDisabled = true;

                vm.medalAward.awardId = null;
                vm.medalAward.publicationId = null;
                vm.medalAward.publicationCategoryId = null;
           
            }
            else {
                vm.awardDisabled = true;
                vm.medalDisabled = true;
                vm.publicationDisabled = false;
                vm.publicationCategoryDisabled = false;

                vm.medalAward.awardId = null;
                vm.medalAward.medalId = null;


            }

        }


        function publicationsByCategory(categoryId) {
            medalAwardService.getPublicationsByCategory(categoryId).then(function (data) {
                vm.publications = data.result.publications;
            });
        }
        function isBackLogChecked(isBackLog) {
            if (isBackLog) {
                if (vm.medalAward.employee.employeeId > 0) {
                    backLogService.getBackLogSelectModels(vm.medalAward.employee.employeeId).then(function (data) {
                        vm.ranks = data.result.ranks;
                        vm.transfers = data.result.transfers;
                    });
                }
            }
        }


        //------------------------------------

        //--------------

        var uploader = $scope.uploader = new FileUploader({
            url: medalAwardService.fileUploadUrl(),
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
                vm.medalAward.fileName = vm.fileModel.fileName;
                notificationService.displaySuccess('File Uploaded Successfully');
            }
        };





    }

  

})();
