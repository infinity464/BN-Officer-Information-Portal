(function () {
    "use strict";

    var app = angular.module('app');

    // Side MenuBar 
    app.directive('sideNavBar', function () {
        return {
            restrict: 'E',
            scope: { model: '=' },
            templateUrl: 'panelTemplate.html',
            link: function (scope, element, attr) {
                scope.parentId = attr.id;
            }
        }
    });
    app.directive('customDatepicker',function($compile,$timeout){
        return {
            replace:true,
            templateUrl:'/app/templates/datepicker-template.html',
            scope: {
                ngModel: '=',
                dateOptions: '@',
                dateDisabled: '@',
                opened: '=',
                min: '@',
                max: '@',
                popup: '@',
                options: '@',
                name: '@',
                id: '@'
            },
            link: function($scope, $element, $attrs, $controller){

            }    
        };
    })
    app.directive("bmdatepicker", function () {
        return {
            restrict: "E",
            scope: {
                ngModel: "=",
                dateOptions: "=",
                opened: "=",
            },
            link: function ($scope, element, attrs) {
                $scope.open = function (event) {
                    event.preventDefault();
                    event.stopPropagation();
                    $scope.opened = true;
                };

                $scope.clear = function () {
                    $scope.ngModel = null;
                };
            },
            templateUrl: '/app/templates/datepicker.html'
        }
    });
    app.directive('yesNo', function () {
        return {
            restrict: 'E',
            scope: {},
            templateUrl: "/app/templates/labelYesNo.html",
            link: function ($scope, $element, $attrs) {
                $scope.getYesNoClass = function () {
                    if ($attrs.isTrue === 'true') {
                        return 'label label-success'
                    }
                    else {
                        return 'label label-default'
                    }
                };
                $scope.getYesNoValue = function () {
                    if ($attrs.isTrue === 'true') {
                        return 'Yes'
                    }
                    else {
                        return 'No'
                    }
                };
            }
        }
    });
    app.directive('emailStatus', function () {
        return {
            restrict: 'E',
            scope: {},
            templateUrl: "/app/templates/labelEmailStatus.html",
            link: function ($scope, $element, $attrs) {
                $scope.getEmailStatusClass = function () {
                    if ($attrs.status === 'NA') {
                        return 'label label-default';
                    }                    
                    else if ($attrs.status === 'H') {

                        return 'label label-warning';
                    }
                    else if ($attrs.status === 'S') {
                        return 'label label-success';
                    }
                    else if ($attrs.status === 'SS') {
                        return 'label label-info';
                    }
                    else {
                        return 'label label-primary';
                    }
                };
                $scope.getEmailStatusValue = function () {
                    if ($attrs.status === 'NA') {
                        return 'N/A';
                    }                    
                    else if ($attrs.status === 'H') {
                        return 'Hold';
                    }
                    else if ($attrs.status === 'S') {
                        return 'Send';
                    }
                    else if ($attrs.status === 'SS') {
                        return 'Sceduled Send';
                    }
                    else {
                        return 'Not Send';
                    }
                };
            }
        }
    });
    app.directive('yesNoNa', function () {
        return {
            restrict: 'E',
            scope: {},
            templateUrl: "/app/templates/labelYesNoNa.html",
            link: function ($scope, $element, $attrs) {
                $scope.getYesNoNaClass = function () {
                    if ($attrs.status === 'NO') {

                        return 'label label-warning'
                    }
                    else if ($attrs.status === 'YES') {
                        return 'label label-success'
                    }
                    else {
                        return 'label label-default'
                    }
                };
                $scope.getYesNoNaValue = function () {
                    if ($attrs.status === 'NO') {
                        return 'No'
                    }
                    else if ($attrs.status === 'YES') {
                        return 'Yes'
                    }
                    else {
                        return 'N/A'
                    }
                };
            }
        }
    });
 
 
    app.directive('activeInactive', function () {
        return {
            restrict: 'E',
            templateUrl: "/app/templates/labelActiveInactive.html",
            link: function ($scope, $element, $attrs) {
                $scope.getActiveClass = function () {
                    if ($attrs.isActive === 'true') {
                        return 'label label-success'
                    }
                    else {
                        return 'label label-danger'
                    }
                };
                $scope.getActiveValue = function () {
                    if ($attrs.isActive === 'true') {
                        return 'Active'
                    }
                    else {
                        return 'Inactive'
                    }
                };
            }
        }
    });

    app.directive('statisticsStatus', function () {
        return {
            restrict: 'E',
            templateUrl: "/app/templates/labelStatisticsStatus.html",
            link: function ($scope, $element, $attrs) {
                $scope.getStatisticsClass = function () {
                    if ($attrs.isTrue === 'true') {
                        return 'label label-success'
                    }
                    else {
                        return 'label label-info'
                    }
                };
                $scope.getStatisticsValue = function () {
                    if ($attrs.isTrue === 'true') {
                        return 'Scheduled'
                    }
                    else {
                        return 'Single'
                    }
                };
            }
        }
    });


    app.directive('passwordCheck', function () {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {
                var firstPassword = '#' + attrs.passwordCheck;
                elem.add(firstPassword).on('keyup', function () {
                    scope.$apply(function () {
                        ctrl.$setValidity('pwmatch', elem.val() === $(firstPassword).val());
                    });
                });
            }
        }
    });


    app.directive('documentType', function () {
        return {
            restrict: 'E',
            templateUrl: "/app/templates/labelDocumentType.html",
            link: function ($scope, $element, $attrs) {
                $scope.getDocumentTypeClass = function () {
                    if ($attrs.docType === 'E') {
                        return 'label label-success'
                    }
                    else if ($attrs.docType === 'S') {
                        return 'label label-warning'
                    }
                    else if ($attrs.docType === '') {
                        return 'label label-default'
                    }
                    else {
                        return 'label label-primary'
                    }
                };
                $scope.getDocumentTypeValue = function () {
                    if ($attrs.docType === 'E') {
                        return 'Letter'
                    }
                    else if ($attrs.docType === 'S') {
                        return 'SMS'
                    }
                    else if ($attrs.docType === 'ES') {
                        return 'Letter and SMS'
                    }
                    else if ($attrs.docType === '') {
                        return 'None'
                    }
                    else {
                        return 'Template'
                    }
                };
            }
        }
    });

    app.directive('mpSpinner', ['$window', function ($window) {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            scope.spinner = null;
            scope.$watch(attrs.mpSpinner, function (options) {
                if (scope.spinner) {
                    scope.spinner.stop();
                }
                scope.spinner = new $window.Spinner(options);
                scope.spinner.spin(element[0]);
            }, true);
        }
    }]);


    app.directive('workSpinner', ['requestCounter', function (requestCounter) {
        return {
            restrict: 'EA',
            transclude: true,
            scope: {},
            template: "<ng-transclude data-ng-show='requestCount'></ng-transclude>",
            link: function (scope) {
                scope.$watch(function () {
                    return requestCounter.getRequestCount();
                }, function (requestCount) {
                    scope.requestCount = requestCount;
                });
            }
        }
    }]);


    

    app.directive('ngConfirmationClick', ['$uibModal', function ($uibModal) {
        var ModalInstanceCtrl = function ($scope, $uibModalInstance) {
            $scope.ok = function () {
                $uibModalInstance.close();
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        };

        return {
            restrict: 'A',
            scope: {
                ngConfirmationClick: "&",
                ngConfirmationCancelClick: "&",
            },
            link: function (scope, element, attrs) {
                element.bind('click', function () {
                    var message = attrs.ngConfirmationMessage || "Are you sure ?";
                    var yesButtonText = attrs.ngConfirmationYesText || "Yes";
                    var cancelButtonText = attrs.ngConfirmationCancelText || "Cancel";

                    var modalHtml =
                        (attrs.ngConfirmationTitle ? ('<div class="modal-header"><h3>' + attrs.ngConfirmationTitle + '</h3></div>') : '')
                        + '<div class="modal-body"><h4>' + message + '</h4></div>';
                    modalHtml += '<div class="modal-footer"><button class="btn custom-btn btn-delete btn-danger btn-xs" ng-click="cancel()"> ' + cancelButtonText + '</button><button class="btn custom-btn btn-edit btn-info btn-xs" ng-click="ok()"> ' + yesButtonText + '</button></div>';

                    var modalInstance = $uibModal.open({
                        animation: true,
                        size: 'md',
                        overflow: scroll,
                        template: modalHtml,
                        controller: ModalInstanceCtrl,

                    });

                    modalInstance.result.then(function () {
                        scope.ngConfirmationClick({});
                    }, function () {
                        scope.ngConfirmationCancelClick && scope.ngConfirmationCancelClick();
                    });
                });

            }
        };
    }]);

    app.directive('fileModel', ['$parse', function ($parse) {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    }]);

    app.directive('showErrors', function ($timeout) {

        return {

            restrict: 'A',

            require: '^form',

            link: function (scope, el, attrs, formCtrl) {

                // find the text box element, which has the 'name' attribute

                var inputEl = el[0].querySelector("[name]");

                // convert the native text box element to an angular element

                var inputNgEl = angular.element(inputEl);

                // get the name on the text box

                var inputName = inputNgEl.attr('name');



                // only apply the has-error class after the user leaves the text box

                var blurred = false;

                inputNgEl.bind('blur', function () {

                    blurred = true;

                    el.toggleClass('has-error', formCtrl[inputName].$invalid);

                });



                scope.$watch(function () {

                    return formCtrl[inputName].$invalid

                }, function (invalid) {

                    // we only want to toggle the has-error class after the blur

                    // event or if the control becomes valid

                    if (!blurred && invalid) { return }

                    el.toggleClass('has-error', invalid);

                });



                scope.$on('show-errors-check-validity', function () {

                    el.toggleClass('has-error', formCtrl[inputName].$invalid);

                });



                scope.$on('show-errors-reset', function () {

                    $timeout(function () {

                        el.removeClass('has-error');

                    }, 0, false);

                });

            }

        }

    });

    app.directive('ngImageModalClick', ['$uibModal', function ($uibModal) {

        return {
            restrict: 'A',
            scope: {
                imgurl: "=selectedurl"
            },
            link: function (scope, element, attrs) {
                element.bind('click', function () {

                    var modalInstance = $uibModal.open({
                        animation: false,
                        templateUrl: 'app/communication/image/selectImage.html',
                        controller: 'selectImageController',
                        controllerAs: 'vm',
                        backdrop: 'static',
                        size: 'fl'
                    });

                    modalInstance.result.then(function (url) {
                        scope.imgurl = url;
                    });
                });

            }
        };
    }]);

    app.directive('ngThumb', ['$window', function ($window) {

        var helper = {
            support: !!($window.FileReader && $window.CanvasRenderingContext2D),
            isFile: function (item) {
                return angular.isObject(item) && item instanceof $window.File;
            },
            isImage: function (file) {
                var type = '|' + file.type.slice(file.type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
            }
        };

        return {
            restrict: 'A',
            template: '<canvas/>',
            link: function (scope, element, attributes) {
                if (!helper.support) return;

                var params = scope.$eval(attributes.ngThumb);

                if (!helper.isFile(params.file)) return;
                if (!helper.isImage(params.file)) return;

                var canvas = element.find('canvas');
                var reader = new FileReader();

                reader.onload = onLoadFile;
                reader.readAsDataURL(params.file);

                function onLoadFile(event) {
                    var img = new Image();
                    img.onload = onLoadImage;
                    img.src = event.target.result;
                }

                function onLoadImage() {
                    var width = params.width || this.width / this.height * params.height;
                    var height = params.height || this.height / this.width * params.width;
                    canvas.attr({ width: width, height: height });
                    canvas[0].getContext('2d').drawImage(this, 0, 0, width, height);
                }
            }
        };
    }]);
    app.directive('del', function () {
        return {
            restrict: 'A',
            scope: { ngModel: '=' },
            link: function (scope, el, attrs) {
                el.keydown((ev) => {

                    scope.$apply(() => {
                        $(el).data("kendoDatePicker").value(null);
                        scope.ngModel = null;

                    })
                })
            }
        }
    }) 

    app.directive('treeView', function ($compile) {
        return {
            restrict: 'E',
            scope: {
                localNodes: '=model',
                localClick: '&click'
            },
            link: function (scope, tElement, tAttrs, transclude) {

                var maxLevels = (angular.isUndefined(tAttrs.maxlevels)) ? 10 : tAttrs.maxlevels;
                var hasCheckBox = (angular.isUndefined(tAttrs.checkbox)) ? false : true;
                scope.showItems = [];

                scope.showHide = function (ulId) {
                    var hideThis = document.getElementById(ulId);
                    var showHide = angular.element(hideThis).attr('class');
                    angular.element(hideThis).attr('class', (showHide === 'show' ? 'hide' : 'show'));
                }

                scope.showIcon = function (node) {
                    if (!angular.isUndefined(node.children)) return true;
                }

                scope.checkIfChildren = function (node) {
                    if (!angular.isUndefined(node.children)) return true;
                }

                /////////////////////////////////////////////////
                /// SELECT ALL CHILDRENS
                // as seen at: http://jsfiddle.net/incutonez/D8vhb/5/
                function parentCheckChange(item) {
                    for (var i in item.children) {
                        item.children[i].checked = item.checked;
                        if (item.children[i].children) {
                            parentCheckChange(item.children[i]);
                        }
                    }
                }

                scope.checkChange = function (node) {
                    if (node.children) {
                        parentCheckChange(node);
                    }
                }
                /////////////////////////////////////////////////

                function renderTreeView(collection, level, max) {
                    var text = '';
                    text += '<li ng-repeat="n in ' + collection + '" >';

                    text += '<span ng-show=showIcon(n) class="show-hide" ng-click=showHide(n.id) ng-if="n.children.length>0"><i class="fa fa-plus-square"></i></span>';


                    text += '<span ng-show=!showIcon(n) style="padding-right: 13px"></span>';

                    if (hasCheckBox) {
                        text += '<input class="tree-checkbox" type=checkbox ng-model=n.checked ng-change=checkChange(n)>';
                    }


                    text += '<span class="edit" ng-click=localClick({node:n})><i class="fa fa-check"></i></span>';


                    text += '<label>{{n.name}}</label>';

                    if (level < max) {
                        text += '<ul id="{{n.id}}" class="hide" ng-if=checkIfChildren(n)>' + renderTreeView('n.children', level + 1, max) + '</ul></li>';
                    } else {
                        text += '</li>';
                    }

                    return text;
                }// end renderTreeView();

                try {
                    var text = '<ul class="tree-view-wrapper">';
                    text += renderTreeView('localNodes', 1, maxLevels);
                    text += '</ul>';
                    tElement.html(text);
                    $compile(tElement.contents())(scope);
                }
                catch (err) {
                    tElement.html('<b>ERROR!!!</b> - ' + err);
                    $compile(tElement.contents())(scope);
                }
            }
        };
    });


})();
