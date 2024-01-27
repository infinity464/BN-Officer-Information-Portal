(function () {
    'use strict';
    angular.module('app').service('certificateService', ['dataConstants', 'apiHttpService', certificateService]);

    function certificateService(dataConstants, apiHttpService) {
        var service = {
            getCertificates: getCertificates,
            getCertificate: getCertificate,
            saveCertificate: saveCertificate,
            updateCertificate: updateCertificate,
            deleteCertificate: deleteCertificate
        };

        return service;
        function getCertificates(pageSize, pageNumber, searchText) {
            var url = dataConstants.CERTIFICATE_URL + 'get-certificates?ps=' + pageSize + "&pn=" + pageNumber + "&qs=" + searchText;
            return apiHttpService.GET(url);
        }

        function getCertificate(id) {
            var url = dataConstants.CERTIFICATE_URL + 'get-certificate?id=' + id;
            return apiHttpService.GET(url);
        }

        function saveCertificate(data) {
            var url = dataConstants.CERTIFICATE_URL + 'save-certificate';
            return apiHttpService.POST(url, data);
        }

        function updateCertificate(id, data) {
            var url = dataConstants.CERTIFICATE_URL + 'update-certificate/' + id;
            return apiHttpService.PUT(url, data);
        }

        function deleteCertificate(id) {
            var url = dataConstants.CERTIFICATE_URL + 'delete-certificate/' + id;
            return apiHttpService.DELETE(url);
        }
        
    }
})();