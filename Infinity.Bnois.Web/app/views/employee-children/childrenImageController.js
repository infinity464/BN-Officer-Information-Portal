
(function () {

    'use strict';
    var controllerId = 'childrenImageController';
    angular.module('app').controller(controllerId, childrenImageController);
    childrenImageController.$inject = ['$scope', '$state', '$stateParams', 'employeeChildrenService', 'notificationService', 'OidcManager', 'FileUploader'];

    function childrenImageController($scope, $state, $stateParams, employeeChildrenService, notificationService, OidcManager, FileUploader) {
        var vm = this;
        vm.manager = OidcManager.OidcTokenManager();
        vm.employeeChildren = {};
        vm.employeeChildrenId = 0;
        vm.employeeId = 0;
        vm.fileModel = {};
        vm.back = back;
        vm.title = '';

        if ($stateParams.employeeId !== undefined && $stateParams.employeeId !== null) {
            vm.employeeId = $stateParams.employeeId;
        }

        if ($stateParams.employeeChildrenId !== undefined && $stateParams.employeeChildrenId !== null) {
            vm.employeeChildrenId = $stateParams.employeeChildrenId;
        }
        Init();
        function Init() {
            employeeChildrenService.getEmployeeChildren(vm.employeeId, vm.employeeChildrenId).then(function (data) {
                vm.employeeChildren = data.result.employeeChildren;
                vm.fileModel = data.result.file;
                vm.fileModel1 = data.result.genFormName;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        function back() {
            $state.go('employee-tabs.employee-childrens');
        }     


        var uploader = $scope.uploader = new FileUploader({
            url: employeeChildrenService.imageUploadUrl(vm.employeeId, vm.employeeChildrenId),
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

                vm.fileModel = response.result.file;
                notificationService.displaySuccess('Image Uploaded Successfully');
            }
        };
   




        

        // Children Gen Form 



        var uploader1 = $scope.uploader1 = new FileUploader({
            url: employeeChildrenService.childrenGenFormUploadUrl(vm.employeeId, vm.employeeChildrenId),
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

                vm.fileModel1 = response.result.genFormName;
                notificationService.displaySuccess('Gen Form Uploaded Successfully');
            }
        };


    };

})();


