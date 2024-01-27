(function () {

    'use strict';
    var controllerId = 'pictureUploadController';
    angular.module('app').controller(controllerId, pictureUploadController);
    pictureUploadController.$inject = ['$scope', '$stateParams' , 'photoService', 'notificationService', 'codeValue','OidcManager', 'FileUploader'];

    function pictureUploadController($scope, $stateParams, photoService, notificationService, codeValue, OidcManager, FileUploader) {
        var vm = this;
        vm.manager = OidcManager.OidcTokenManager();
        vm.fileModel = {};
        vm.employeeId = 0;
        vm.photoType = '';
        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }
        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.photoType = $stateParams.type;
        }

        Init();
        function Init() {
            photoService.getPhoto(vm.employeeId, vm.photoType).then(function (data) {
                vm.fileModel = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };
    
        var uploader = $scope.uploader = new FileUploader({
            url: photoService.uplaodPhotoUrl(vm.employeeId,vm.photoType),
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
                return item.size <= 102400*10; // 10 MiB to bytes
            }
        });
        // CALLBACKS

      
        uploader.onAfterAddingFile = function (item) {
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

        uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {

            if (filter.name === 'fileFilter') {
                notificationService.displayError('Only jpg, png, jpeg, bmp  and gif file is allowed');
            }

            if (filter.name === 'queueLimit') {
                notificationService.displayError('Only 1(one) file allowed to upload');
            }
            if (filter.name === 'enforceMaxFileSize') {
                
                notificationService.displayError('Picture size should not exceed 1 MB');
            }
        };
     
      
        uploader.onCompleteItem = function (fileItem, response, status, headers) {
            if (response.isError) {
                notificationService.displayError(response.message);
                return;
            } else {
                vm.fileModel = response.result;
                notificationService.displaySuccess('Picture uploaded successfully');
            }
        };
        console.info('uploader', uploader);

    };

})();

