(function () {

    'use strict';

    var controllerId = 'downloadAddController';

    angular.module('app').controller(controllerId, downloadAddController);
    downloadAddController.$inject = ['$stateParams', '$scope', 'downloadfileService', 'codeValue', 'notificationService', '$state', '$location', 'FileUploader', 'OidcManager'];

    function downloadAddController($stateParams, $scope, downloadfileService, codeValue, notificationService, $state, location, FileUploader, OidcManager) {
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
        vm.save = save;

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.downloadId = $stateParams.id;
            vm.saveButtonText = 'Update';
            vm.title = 'UPDATE MODE';
        }


        Init();
        function Init() {
            downloadfileService.getDownloadById(vm.downloadId).then(function (data) {
                vm.download = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };


        function close() {
            $state.go('download-list');
        };



        function save() {
            if (vm.downloadId === 0 || vm.downloadId === '') {
                insert();
            } else {
                update();
            }
        }

        function insert() {
            var fileExtension = vm.fileModel.fileName.split('.').pop();
            vm.download.fileExtension = fileExtension;
            vm.download.source = vm.fileModel.filePath;
            vm.download.contentType = vm.fileModel.fileName;
            downloadfileService.SaveDownload(vm.download).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }
        function update() {
            if (vm.fileModel.filePath != null) {
                var fileExtension = vm.fileModel.fileName.split('.').pop();
                vm.download.fileExtension = fileExtension;
                vm.download.source = vm.fileModel.filePath;
                vm.download.contentType = vm.fileModel.fileName;
            }
            downloadfileService.updateDownload(vm.downloadId, vm.download).then(function (data) {
                close();
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        //--------------

        var uploader = $scope.uploader = new FileUploader({
            url: downloadfileService.fileUploadUrl(),
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
                //var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                var type = item.name.split('.').pop(); //get the file extension
                return '|doc|docx|xls|xlsx|ppt|pptx|pdf|'.indexOf(type) !== -1;
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
                notificationService.displaySuccess('File Uploaded Successfully');
            }
        };
    }
})();