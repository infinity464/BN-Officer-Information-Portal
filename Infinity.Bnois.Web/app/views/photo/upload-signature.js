(function () {

    'use strict';
    var controllerId = 'signatureUploadController';
    angular.module('app').controller(controllerId, signatureUploadController);
    signatureUploadController.$inject = ['$scope', '$stateParams', 'photoService', 'notificationService', 'OidcManager', '$location', 'FileUploader'];

    function signatureUploadController($scope, $stateParams, photoService, notificationService,OidcManager, $location, FileUploader) {
        var vm = this;
        vm.manager = OidcManager.OidcTokenManager();
        vm.saveButtonText = 'Save';
        vm.uploader = {};
        vm.fileModel = {};
        vm.employeeId = 0;
        vm.phortoType = '';
        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }
        if ($stateParams.type !== undefined && $stateParams.type !== null) {
            vm.phortoType = $stateParams.type;
        }
        Init();

        function Init() {
            photoService.getPhoto(vm.employeeId, vm.phortoType).then(function (data) {
                vm.fileModel = data.result;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };
        vm.uploader = $scope.uploader = new FileUploader({
            url: photoService.uplaodPhotoUrl(vm.employeeId, vm.phortoType),
            queueLimit: 1,
            withCredentials: true,
            headers: {
                'Authorization': 'Bearer ' + vm.manager.access_token,
                'Access-Control-Allow-Credentials': true
            }
        });

        vm.uploader.filters.push({
            name: 'fileFilter',
            fn: function (item) {
                var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
            }
        });
        vm.uploader.filters.push({
            name: 'enforceMaxFileSize',
            fn: function (item) {
                return item.size <= 61440; // 1024X60 byet
            }
        });
        // CALLBACKS

      
        vm.uploader.onAfterAddingFile = function (item) {
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

        vm.uploader.filters.push({
            name: 'asyncFilter',
            fn: function (item /*{File|FileLikeObject}*/, options, deferred) {
                setTimeout(deferred.resolve, 1e3);
                var reader = new FileReader();
                reader.onload = onLoadFile;
                reader.readAsDataURL(item);
                function onLoadFile(event) {
                    var img = new Image();
                    img.onload = function (scope) {
                        alert(img.height);
                        //console.log(img.width, img.height);
                    };
                    //if (img.height === 80 && img.width == 300) {
                    //    img.src = event.target.result;
                    //} else {
                    //    notificationService.displayError('Please Select 300X80 pixel signature');
                    //}
                   
                }
            }

        });
        vm.uploader.onWhenAddingFileFailed = function (item, filter, options) {

          
            if (filter.name === 'fileFilter') {
                notificationService.displayError('Only jpg, png, jpeg, bmp  and gif file is allowed');
            }

            if (filter.name === 'queueLimit') {
                notificationService.displayError('Only 1(one) file allowed to upload');
            }
            if (filter.name === 'enforceMaxFileSize') {
                notificationService.displayError('Signature size should not exceed 60 KB');
            }
        };

        vm.uploader.onCompleteItem = function (fileItem, response, status, headers) {
            if (response.isError) {
                notificationService.displayError(response.message);
                return;
            } else {
                vm.fileModel = response.result;
                notificationService.displaySuccess('Signature uploaded successfully');
            }
   
        };
    }

})();
