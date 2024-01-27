
(function () {

    'use strict';
    var controllerId = 'parentImageController';
    angular.module('app').controller(controllerId, parentImageController);
    parentImageController.$inject = ['$scope', '$state', '$stateParams', 'parentService', 'notificationService', 'OidcManager', 'FileUploader'];

    function parentImageController($scope, $state, $stateParams, parentService, notificationService, OidcManager, FileUploader) {
        var vm = this;
        vm.manager = OidcManager.OidcTokenManager();
        vm.parent= {};
        vm.parentId = 0;
        vm.employeeId = 0;
        vm.fileModel = {};
        vm.back = back;
        vm.title = '';

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        if ($stateParams.relationType !== undefined && $stateParams.relationType !== null) {
            vm.relationType = $stateParams.relationType;
            if (vm.relationType == 1) {
                vm.title = 'Father';
                vm.relationName = 'father';
            }
            else if (vm.relationType == 2) {
                vm.title = 'Mother';
                vm.relationName = 'mother';
            }
            else if (vm.relationType == 3) {
                vm.title = 'Step Father';
                vm.relationName = 'stepfather';
            }
            else if (vm.relationType == 4) {
                vm.title = 'Step Mother';
                vm.relationName = 'stepmother';
            }
            else {
                vm.title = '';
            }
        }
        Init();
        function Init() {
            parentService.getParent(vm.employeeId, vm.relationType).then(function (data) {
                vm.parent = data.result.parent;
                vm.fileModel = data.result.file;
                console.log(data);
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function back() {
            $state.go('employee-tabs.employee-' + vm.relationName, { relationType: vm.relationType });
        }     

        var uploader = $scope.uploader = new FileUploader({
            url: parentService.imageUploadUrl(vm.employeeId, vm.relationType),
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
                return item.size <= 1024000; // 100 KB
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

                notificationService.displayError('Picture size should not exceed 1000 KB');
            }
        };


        uploader.onCompleteItem = function (fileItem, response, status, headers) {
            if (response.isError) {
                notificationService.displayError(response.message);
                return;
            } else {
                console.log(response);
                vm.fileModel = response.result.file;
                notificationService.displaySuccess('Image Uploaded Successfully');
            }
        };
        console.info('uploader', uploader);
    };

})();


