

(function () {

    'use strict';

    var controllerId = 'advanceSearchResultController';

    angular.module('app').controller(controllerId, advanceSearchResultController);
    advanceSearchResultController.$inject = ['$stateParams', '$rootScope', 'codeValue', 'advanceSearchService', 'downloadService', 'reportService', 'notificationService', '$state', '$sce','$filter'];

    function advanceSearchResultController($stateParams, $rootScope, codeValue, advanceSearchService, downloadService, reportService, notificationService, $state, $sce, $filter) {
        var vm = this;
        vm.officerDetails = officerDetails;
        vm.downloadSearchResult = downloadSearchResult;
       vm.fnExcelReport = fnExcelReport;
        vm.searchOfficers = searchOfficers;
        vm.length = 0;
        vm.searchResults = [];
        vm.url = codeValue.APPLICATION_URL;
        vm.singlerow = ['pNo', 'officerName', 'presentBillet', 'since', 'age', 'adminAuthority', 'ageLimit', 'batch', 'bloodGroup', 'branch', 'commService', 'commissionType', 'currentStatus', 'district', 'drivingLicenseNo', 'gender', 'height', 'hobby', 'joiningDate', 'lprDate', 'maritalStatus', 'medicalCategory', 'officerCategory', 'officerEmail', 'officerServiceFrom', 'passportNo', 'permanentAddress', 'phoneNo', 'photo', 'presentAddress', 'promotionStatus', 'religion', 'retirementDate', 'spouseName', 'subBranch', 'subjectName', 'seaService', 'seaCmdService', 'freedomFighterCertificate', 'sectorNo', 'issb', 'issbResult', 'batchPosition', 'svcExamName', 'svcExamResult', 'fatherName', 'motherName', 'dateOfBirth', 'rankService', 'terminationDate', 'promotionDate', 'specialNotification', 'extendedDate', 'extendedDuration', 'officerSubCategory', 'preCommssionRank', 'tyReason', 'commissionDate','rlDueDate','average','seaCmdService','oprAverage']
        vm.columnlengthset = columnlengthset;
        vm.reportType = 3;
        vm.orientation = 2;
        vm.reportTypes = [{ text: 'EXCEL', value: 3 },{ text: 'PDF', value: 1 }, { text: 'WORD', value: 2 }];
        vm.pageOrientations = [{ text: 'Landscape', value: 2 },{ text: 'Potrait', value: 1 }];

        Init();
        function Init() {
            searchOfficers();
        }

        function searchOfficers() {
            
            advanceSearchService.searchOfficersResult().then(function (data) {

                vm.searchResults = data.result;

                vm.totalResult = vm.searchResults.length;

                },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function downloadSearchResult(header, type, orientation) {
            var url = reportService.downloadSearchResult(header, type, orientation);
            downloadService.downloadReport(url);
        }


        function officerDetails(pNo) {
            $state.goNewTab('current-status-tab', { pno: pNo });

        }


function fnExcelReport(header)
{
    
    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
    var base64 = function (s) {
        return window.btoa(unescape(encodeURIComponent(s)))
    };

    var format = function (s, c) {
        return s.replace(/{(\w+)}/g, function (m, p) {
            return c[p];
        })
    };


    var html = "<h2 style='text-decoration:underline;font-family:arial;'>" + vm.header+"</h2>"
     html += "<table class='table table-bordered pdfReportTable' style='font-family:arial;font-size:13px;' id='headerTable' >"
   
    html += "<thead class='reportThead'>"



    html += "<tr>"
    var index = 0
    angular.forEach(vm.searchResults, function (value, key) {
        if (index != 0)
            return false
        html += "<th>SL </th >"
        angular.forEach(value[0], function (v, k) {
            if (k != "$id" && k != "0") {
                k = k[0].toUpperCase() + k.slice(1);
                html += "<th  >" + k + " </th >"
            }
            

        });
        index = index + 1
    });


    html += "</tr>"
    html += "</thead>"

    html += "<tbody>"
    vm.totalResult = 0
    angular.forEach(vm.searchResults, function (value, key) {

        if (key == "$id")
            return false
        html += "<tr class='reportTR'>"

        
        html += "<td style='vertical-align:middle;' rowspan =" + value.length + ">" + ++vm.totalResult + "</td>"
        angular.forEach(value[0], function (valu, key) {

            if (vm.singlerow.indexOf(key) !== -1) {  
               if (key != "$id")
                   html += "<td style='vertical-align:middle;' rowspan =" + value.length + "> " + valu + "</td >"

            }

            else {
                if (key != "$id")
                    html += " <td >" + valu + "</td >"
            }

        });
        html += "</tr>"

        angular.forEach(value.slice(1), function (value, key1) {
            html += "<tr class='reportTR'>"

            angular.forEach(value, function (value, key) {
                if (vm.singlerow.indexOf(key) === -1) {
                    if (key != "$id")
                        html += " <td > " + value + "</td >"
                }


            });
            html += "</tr>"


        });

    });


    html += "</tbody>"
    html += "</table>"    

    var ctx = {
        worksheet: 'Worksheet',
        table: html
    }


    var link = document.createElement("a");
    link.download = header+".xls";
    link.href = uri + base64(format(template, ctx));
    link.click();
   
}
 
        function columnlengthset() {


            if (vm.searchResults) {

          
                var html = "<table class='table table-bordered pdfReportTable' id='headerTable' border='1'>"
                    html  +="<thead class='reportThead'>"
               
                    
           
                    html += "<tr>"
                    //html += "<th>Sl</th >"
                var index =0
                angular.forEach(vm.searchResults, function (value, key) {
                    if (index != 0)
                        return false
                    html+="<th>SL </th >"
                angular.forEach(value[0], function (v, k) {
                    if (k != "$id" && k != "0")
                    html += "<th  style='text-transform:capitalize; '>"+k+" </th >"
                    
                        });
                    index =index+1
                    });
                          

                        html  += "</tr>"
                        html  += "</thead>"
                  
                        html += "<tbody>"
                vm.totalResult = 0
                //vm.searchResults = $filter('orderBy')(vm.searchResults, ['pNo']);
            angular.forEach(vm.searchResults, function (value, key) {               
                
                if (key == "$id")
                    return false
                html += "<tr class='reportTR'>"
                html += "<td style='vertical-align:middle;text-align: center;'  rowspan =" + value.length + "><strong>" + ++vm.totalResult + "</strong></td>"
                angular.forEach(value[0], function (valu, key) {
                  
                        if (vm.singlerow.indexOf(key) !== -1) {
                            if (key != "$id" && key == 'officerName')
                                html += "<td rowspan =" + value.length + " style='vertical-align : middle; '><a href='" + vm.url + "current-status-tab/current-status?pno=" + value[0].pNo+ "' target='_Blank'> <strong> " + valu + "</strong></a></td >"
                            else if(key != "$id" && key != 'officerName')
                                html += "<td rowspan =" + value.length + " style='vertical-align : middle; '>  <strong> " + valu + "</strong></td >"

                        }
                        else {
                            if (key != "$id")
                                html += " <td style='vertical-align : middle;' ><strong> " + valu + "</strong></td >"
                        }
                           
                    });
                html += "</tr>"
                
                angular.forEach(value.slice(1), function (value, key1) {
                    html += "<tr class='reportTR'>"
                  
                    angular.forEach(value, function (value, key) {
                        if (vm.singlerow.indexOf(key) === -1) {
                            if (key != "$id")
                                html += " <td ><strong> " + value + "</strong></td >"
                        }


                    });
                    html += "</tr>"


                });
                //angular.forEach(value.slice(1), function (value, key1) {                   
                //    html += "<tr class='reportTR'>"
                   
                //        angular.forEach(value, function (value, key) {
                //            if (vm.singlerow.indexOf(key) === -1) {
                //                if (key != "$id")
                //                    html += " <td ><strong> " + value + "</strong></td >"
                //            }


                //        });
                //        html += "</tr>"
                  
                 
                //});                
              
                });           
         
                    
            html +="</tbody>"
            html += "</table>"               
        
         
            return $sce.trustAsHtml(html);

            }
        }
    }
})();
