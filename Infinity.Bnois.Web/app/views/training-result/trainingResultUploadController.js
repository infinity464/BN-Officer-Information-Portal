
(function () {

    'use strict';
    var controllerId = 'trainingResultUploadController';
    angular.module('app').controller(controllerId, trainingResultUploadController);
    trainingResultUploadController.$inject = ['$scope', '$state', '$stateParams', 'trainingResultService', 'notificationService', 'OidcManager', 'FileUploader'];

    function trainingResultUploadController($scope, $state, $stateParams, trainingResultService, notificationService, OidcManager, FileUploader) {
        var vm = this;
        vm.manager = OidcManager.OidcTokenManager();
        vm.trainingResult = {};
        vm.fileModel = {};
        vm.trainingResultId = 0;
        vm.fileType = 1;
        vm.imageType = 2;
        vm.back = back

        if ($stateParams.id !== undefined && $stateParams.id !== null) {
            vm.trainingResultId = $stateParams.id;
        }

        Init();
        function Init() {
            trainingResultService.getTrainingResult(vm.trainingResultId).then(function (data) {
                vm.trainingResult = data.result.trainingResult;
                vm.fileModel = data.result.file;
                console.log(data);
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function back() {
            $state.go('training-results');
        }

        //-------------------------------------------Upload Main Pdf File-------------------------------
        var uploader = $scope.uploader = new FileUploader({
            url: trainingResultService.getTrainingResultUploadUrl(vm.trainingResultId,vm.fileType),
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
                vm.trainingResult = response.result;
                notificationService.displaySuccess('Result File Uploaded Successfully');
            }
        };
        console.info('uploader', uploader);


        //------------------------------------------Result Section Upload-------------------------------

        var uploader1 = $scope.uploader1 = new FileUploader({
            url: trainingResultService.getTrainingResultUploadUrl(vm.trainingResultId, vm.imageType),
            withCredentials: true,
            queueLimit: 1,
            headers: {
                'Authorization': 'Bearer ' + vm.manager.access_token,
                'Access-Control-Allow-Credentials': true
            }
        });

        // FILTERS

        uploader1.filters.push({
            name: 'fileFilter',
            fn: function (item) {
                var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
            }
        });
        uploader1.filters.push({
            name: 'enforceMaxFileSize',
            fn: function (item) {
                return item.size <= 1024000; // 100 KB
            }
        });
        // CALLBACKS

        uploader1.onAfterAddingFile = function (item) {
            var reader = new FileReader();
            reader.onload = function (event) {
                $scope.$apply(function () {
                    item.image = event.target.result;
                });
            };
            reader.readAsDataURL(item._file);
        };


        var dataURItoBlob = function (dataURI) {
            var binary = atob(dataURI.split(',')[1]);
            var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];
            var array = [];
            for (var i = 0; i < binary.length; i++) {
                array.push(binary.charCodeAt(i));
            }
            return new Blob([new Uint8Array(array)], {
                type: mimeString
            });
        };

        uploader1.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {

            if (filter.name === 'fileFilter') {
                notificationService.displayError('Only jpg, png, jpeg, bmp  and gif file is allowed');
            }

            if (filter.name === 'queueLimit') {
                notificationService.displayError('Only 1(one) file allowed to upload');
            }
            if (filter.name === 'enforceMaxFileSize') {

                notificationService.displayError('Picture size should not exceed 1000 KB');
            }
        };


        uploader1.onCompleteItem = function (fileItem, response, status, headers) {
            if (response.isError) {
                notificationService.displayError(response.message);
                return;
            } else {
                console.log(response);
                vm.fileModel = response.result.file;
                notificationService.displaySuccess('Result Section Uploaded Successfully');
            }
        };
        console.info('uploader2', uploader1);
    };

})();


