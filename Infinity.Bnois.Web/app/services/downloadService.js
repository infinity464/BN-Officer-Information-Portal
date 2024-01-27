(function () {
    'use strict';
    angular.module('app').service('downloadService', ['$http','$window', 'notificationService', downloadService]);

    function downloadService($http, $window, notificationService) {

        var service = {
            downloadFile: downloadFile,
            downloadReport: downloadReport
        };

        return service;
        function downloadReport(itemsUrl){
            var config = { responseType: 'blob' };

            $http.get(itemsUrl, config).then(function onSuccess(response) {
                var blob = response.data;
                var contentType = response.headers("content-type");
                var fileURL = URL.createObjectURL(blob);
                $window.open(fileURL);
            });
        }
     
        function downloadFile(url) {
            $http({
                method: 'GET',
                url: url,
                responseType: 'arraybuffer'

            }).success(function (data, status, headers) {
                var disposition = headers('content-disposition');

                var filename = disposition.split(';')[1].trim().split('=')[1];
                
               var name= filename.replace('"', "");
                var nameWithExtension = '';
               
                
                var contentType = headers('content-type');
                if (contentType == 'application/vnd.ms-excel') {
                    nameWithExtension = name.replace('xls"', 'xls');

                }
                else if (contentType == 'application/pdf') {
                    nameWithExtension = name.replace('pdf"', 'pdf');

                }
                else if (contentType == 'application/msword') {
                    nameWithExtension = name.replace('doc"', 'doc');

                }
                


                var linkElement = document.createElement('a');
                try {
                    var blob = new Blob([data], { type: contentType });
                    var url = window.URL.createObjectURL(blob);
                    
                    linkElement.setAttribute('href', url);
                    linkElement.setAttribute("download", nameWithExtension);
                    console.log(linkElement);
                    var clickEvent = new MouseEvent("click", {
                        "view": window,
                        "bubbles": true,
                        "cancelable": false
                    });
                    linkElement.dispatchEvent(clickEvent);
                } catch (ex) {
                    notificationService.displayError('Error on download file');
                }

            }).error(function (errorMessage) {
                notificationService.displayError('Error on download file');
            });
        }
    }
})();