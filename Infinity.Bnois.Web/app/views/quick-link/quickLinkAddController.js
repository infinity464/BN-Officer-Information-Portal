(function () {

    'use strict';

    var controllerId = 'quickLinkAddController';

    angular.module('app').controller(controllerId, quickLinkAddController);
    quickLinkAddController.$inject = ['$stateParams', '$scope','codeValue', 'quickLinkService', 'backLogService', 'notificationService', '$state', 'FileUploader', 'OidcManager'];

    function quickLinkAddController($stateParams, $scope, codeValue, quickLinkService, backLogService, notificationService, $state, FileUploader, OidcManager) {
        var vm = this;
        vm.url = codeValue.IMAGE_URL;
        vm.manager = OidcManager.OidcTokenManager();
        vm.quickLinkId = 0;
        vm.title = 'ADD MODE';
        vm.quickLink = {};
      
        vm.saveButtonText = 'Save';
        vm.save = save;
        vm.close = close;
        vm.quickLinkForm = {};


      

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.quickLinkId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }

        Init();
        function Init() {
            quickLinkService.getQuickLink(vm.quickLinkId).then(function (data) {
                vm.quickLink = data.result;
 
               },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function save() {

            if (vm.quickLinkId !== 0 && vm.quickLinkId !== '') {
                updateQuickLink();
            } else {
                insertQuickLink();

            }
        }

        function insertQuickLink() {

            quickLinkService.saveQuickLink(vm.quickLink).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function updateQuickLink() {
            quickLinkService.updateQuickLink(vm.quickLinkId, vm.quickLink).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function close() {
            $state.go('quick-links');
        }

        


        //------------------------------------

        //--------------

        var uploader = $scope.uploader = new FileUploader({
            url: quickLinkService.fileUploadUrl(),
            queueLimit: 1,
            withCredentials: true,
            queueLimit: 1,
            headers: {
                'Authorization': 'Bearer ' + vm.manager.access_token,
                'Access-Control-Allow-Credentials': true
            }
        });

        // FILTERS

//        uploader.filters.push({
//            name: 'fileFilter',
//            fn: function (item) {
//                var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
//                return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
//            }
//        });
//        uploader.filters.push({
//            name: 'enforceMaxFileSize',
//            fn: function (item) {
//                return item.size <= 10485760000; // 10 MiB to bytes
//            }
//        });


//   uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {

//            if (filter.name === 'fileFilter') {
//                notificationService.displayError('Only jpg, png, jpeg, bmp  and gif file is allowed');
//            }
//
//            if (filter.name === 'queueLimit') {
//                notificationService.displayError('Only 1(one) file allowed to upload');
//            }
//            if (filter.name === 'enforceMaxFileSize') {
//                notificationService.displayError('File size is ' + item.size + ' ' + 'byts');
//            }
//        };


        uploader.onCompleteItem = function (fileItem, response, status, headers) {
            if (response.isError) {
                notificationService.displayError(response.message);
                return;
            } else {
                vm.fileModel = response.result;
                vm.quickLink.fileName = vm.fileModel.fileName;
                vm.quickLink.extention = vm.fileModel.filePath;
                notificationService.displaySuccess('File Uploaded Successfully');
            }
        };





    }

  

})();
