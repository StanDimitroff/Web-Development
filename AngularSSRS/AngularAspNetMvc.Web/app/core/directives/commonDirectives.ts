/// <reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
module app.directives {
    import IUtilities = app.core.services.IUtilities;

    export class BaseDirective implements ng.IDirective {
        public priority: number;
        public template: string;
        public templateUrl: string;
        public replace: boolean;
        public transclude: any;
        public restrict: string;
        public scope: any;
        public link: (
        scope: ng.IScope,
        instanceElement: any,
        instanceAttributes: ng.IAttributes,
        controller: any
        ) => void;
        public compile: (
        templateElement: any,
        templateAttributes: ng.IAttributes,
        transclude: (scope: ng.IScope, cloneLinkingFn: Function) => void
        ) => any;
        public controller: (...injectables: any[]) => void;
        public isEmpty: (value: any) => boolean;
        public require: string[];
        constructor() {
            this.isEmpty = function (value) {
                return angular.isUndefined(value) || value === '' || value === null || value !== value;
            };
        }

        public static getControllerFromParameterArray: (controller: any) => ng.INgModelController = function (controller: any) {
            var currentController: ng.INgModelController;
            if (angular.isArray(controller) && controller.length > 0) {
                currentController = controller[0];
            } else {
                currentController = controller;
            }
            return currentController;
        };
    }

    export class NoClickDirective extends app.directives.BaseDirective {

        constructor() {
            super();
            this.restrict = 'A';
            this.link = function (scope: ng.IScope, element: ng.IAugmentedJQuery) {
                element.click(function (eventObject: JQueryEventObject) {
                    eventObject.preventDefault();
                });
            };
        }
    }

    export class PaginationDirective extends app.directives.BaseDirective {
        currentPage; number;
        totalPages: number;
        currentlyShownPages: number[];

        static createButton(label: string, clickEvent: (eventObject: JQueryEventObject) => void): ng.IAugmentedJQuery {
            var button = angular.element('<li><a href=#>' + label + '</a></li>');
            button.click({ page: label }, clickEvent);
            return button;
        }
        constructor() {
            super();
            var that = this;
            that.currentPage = 0;
            this.restrict = 'A';
            that.currentlyShownPages = [];

            this.link = function (
                scope: ng.IScope,
                instanceElement: any,
                instanceAttributes: ng.IAttributes) {

                instanceElement.addClass('pagination');
                var searchFunc: string;
                if (instanceAttributes.$attr['gotoPageFunction']) {
                    searchFunc = instanceElement.attr(instanceAttributes.$attr['gotoPageFunction']);
                } else {
                    searchFunc = "gotoPage";
                }


                var handleButtonClick = function (eventObject: JQueryEventObject) {
                    eventObject.preventDefault();
                    var jQueryTarget = angular.element(eventObject.delegateTarget);
                    if (!jQueryTarget.hasClass("disabled")) {
                        var page = eventObject.data['page'];
                        var pageNumber: number;
                        if (page === '>') {
                            pageNumber = that.currentPage + 1;
                            if (pageNumber > that.totalPages) {
                                pageNumber = that.totalPages;
                            }
                        }
                        else if (page === '>>') {
                            pageNumber = that.totalPages;
                        }
                        else if (page === '<<') {
                            pageNumber = 1;
                        }
                        else if (page === '<') {
                            pageNumber = that.currentPage - 1;
                            if (pageNumber < 1) {
                                pageNumber = 1;
                            }
                        } else {
                            pageNumber = parseInt(page);
                        }
                        that.currentPage = pageNumber;
                        scope.$apply(searchFunc + '(' + pageNumber.toString() + ')');
                    }
                };

                var hasPageButton = function (): boolean {
                    var returnValue: boolean = false;
                    angular.forEach(instanceElement.children(), function (item: any) {
                        var jqueryObject = angular.element(item);
                        if (jqueryObject.text() === that.currentPage.toString()) {
                            returnValue = true;
                        }
                    });
                    return returnValue;
                };

                var refresh = function (goToFirstPage: boolean) {
                    if (instanceAttributes.$attr['pageCount']) {
                        that.totalPages = scope.$eval(instanceElement.attr(instanceAttributes.$attr["pageCount"]));
                    } else {
                        that.totalPages = scope.$eval("pageCount");
                    }

                    if (instanceAttributes.$attr['currentPage']) {
                        that.currentPage = scope.$eval(instanceElement.attr(instanceAttributes.$attr["currentPage"]));
                    } else {
                        that.currentPage = scope.$eval("currentPage");
                    }

                    if (that.totalPages > 0) {
                        var resetPage: boolean = goToFirstPage || (that.currentPage > that.totalPages);
                        if (resetPage) {
                            that.currentPage = 1;
                        }
                        var needToReset: boolean = (!hasPageButton()) || goToFirstPage;
                        if (needToReset) {
                            instanceElement.empty();
                            var maxButtons: number = 5;
                            var firstPageNumber: number = that.currentPage;
                            while ((firstPageNumber + 4) > that.totalPages) {
                                firstPageNumber--;
                            }
                            if (firstPageNumber < 1) {
                                firstPageNumber = 1;
                            }
                            that.currentlyShownPages = [];
                            for (var i = firstPageNumber; i <= that.totalPages; i++) {
                                if (i < firstPageNumber + maxButtons) {
                                    that.currentlyShownPages.push(i);
                                } else {
                                    break;
                                }
                            }

                            instanceElement.append(PaginationDirective.createButton('<<', handleButtonClick));
                            instanceElement.append(PaginationDirective.createButton('<', handleButtonClick));
                            for (var j = 0; j < that.currentlyShownPages.length; j++) {
                                var button = PaginationDirective.createButton(that.currentlyShownPages[j].toString(), handleButtonClick);
                                instanceElement.append(button);
                            }
                            instanceElement.append(PaginationDirective.createButton('>', handleButtonClick));
                            instanceElement.append(PaginationDirective.createButton('>>', handleButtonClick));
                        }


                        angular.forEach(instanceElement.children(), function (item: any) {
                            var jqueryObject = angular.element(item);
                            var text: string = jqueryObject.text();
                            if (that.currentPage === that.totalPages && (text === ">" || text === ">>")) {
                                jqueryObject.addClass('disabled');
                            }
                            else if (that.currentPage === 1 && (text === "<" || text === "<<")) {
                                jqueryObject.addClass('disabled');
                            } else {
                                jqueryObject.removeClass('disabled');
                            }
                            if (text === that.currentPage.toString()) {
                                jqueryObject.addClass('active');
                            } else {
                                jqueryObject.removeClass('active');
                            }
                        });
                    } else {
                        instanceElement.empty();
                    }
                };
                scope.$on('pageLoadCompleted', function () {
                    refresh(false);
                });
                scope.$on('searchCompleted', function () {
                    refresh(true);
                });
            };
        }
    }

    interface IReportScope extends ng.IScope {
        reportSource: string;
        reportName: string;
    }

    export class ReportDirective extends app.directives.BaseDirective {

        constructor(utilities: IUtilities) {
            super();
            this.restrict = 'A';
            this.scope = {
                reportSource: '=',
                reportName: '='
            };
            this.link = function (scope: IReportScope, element: ng.IAugmentedJQuery) {
                element.html('<div><iframe style="border: transparent"></iframe></div>');
                scope.$watch('reportSource', function(value) {
                    if (value) {
                        var frame = element.find('iframe');
                        frame.attr('src', value);
                        utilities.showMessage(element.html(), true, null, scope.reportName);
                    }
                });
            };
        }
    }

    angular.module('app.directives.Common', [])
        .directive('noClick', [function () {
            return new app.directives.NoClickDirective();
        }])
        .directive('pagination', [function () {
            return new app.directives.PaginationDirective();
        }])
        .directive('report', ['utilities', function (utilities: IUtilities) {
            return new app.directives.ReportDirective(utilities);
        }]);
}