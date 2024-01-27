(function () {

    'use strict';

    var controllerId = 'MigrationController';

    angular.module('app').controller(controllerId, MigrationController);
    MigrationController.$inject = ['$stateParams', '$scope', 'downloadfileService', 'codeValue', 'notificationService', '$state', '$location', 'FileUploader', 'OidcManager'];

    function MigrationController($stateParams, $scope, downloadfileService, codeValue, notificationService, $state, location, FileUploader, OidcManager) {
        var vm = this;
        vm.childrens = [];
        vm.saveButtonText = 'Save';
        vm.title = 'Save MODE';
        vm.downloadId = 0;
        vm.download = {};
        vm.url = codeValue.IMAGE_URL;
        vm.manager = OidcManager.OidcTokenManager();
        vm.close = close;
        vm.downloadForm = {};
        vm.fileModel = {};
        vm.dataMigration = dataMigration;
        vm.isHide = true;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.downloadId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }


        Init();
        function Init() {

        };

        //--------------

        var uploader = $scope.uploader = new FileUploader({
            url: downloadfileService.migrationUploadUrl(),
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
                var type = item.name.split('.').pop(); //get the file extension
                return '|sql|'.indexOf(type) !== -1;
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
                notificationService.displayError('Only doc, docx, xls, xlsx, ppt, pptx, and pdf file is allowed');
            }

            if (filter.name === 'queueLimit') {
                notificationService.displayError('Only 1(one) file allowed to upload');
            }
            //if (filter.name === 'enforceMaxFileSize') {
            //    notificationService.displayError('File size is ' + item.size + ' ' + 'byts');
            //}
        };


        uploader.onCompleteItem = function (fileItem, response, status, headers) {
            if (response.isError) {
                notificationService.displayError(response.message);
                return;
            } else {
                vm.fileModel = response.result;
                vm.isHide = false;
                notificationService.displaySuccess('File Uploaded Successfully');
            }
        };

        function dataMigration() {
            vm.isHide = true;
            downloadfileService.dataMigration(vm.fileModel.fileName).then(function (data) {
                notificationService.displaySuccess('Migration Uploaded Successfully');
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };
    }
})();