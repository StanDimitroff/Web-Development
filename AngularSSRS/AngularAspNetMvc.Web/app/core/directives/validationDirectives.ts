/// <reference path="../services/utilities.ts" />
/// <reference path="../services/utilities.ts" />
/// <reference path="../controllers/appController.ts" />
/// <reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="commonDirectives.ts" />
module app.directives {

    class RequiredDirective extends BaseDirective {
        constructor() {
            super();
            this.restrict = 'A';
            this.require = ['?ngModel'];
            var that = this;
            this.link = function (scope: app.core.controllers.ICoreScope, element: ng.IAugmentedJQuery, attributes: ng.IAttributes, controller: any) {

                var currentController: ng.INgModelController = app.directives.BaseDirective.getControllerFromParameterArray(controller);
                if (!currentController) return;
                var validator = function (value) {
                    if ((that.isEmpty(value) || value === false)) {
                        currentController.$setValidity('required', false);
                        return value;
                    } else {
                        currentController.$setValidity('required', true);
                        return value;
                    }
                };

                currentController.$formatters.push(validator);
                currentController.$parsers.unshift(validator);
            };
        }
    }

    class MaxLengthDirective extends BaseDirective {
        constructor() {
            super();
            this.restrict = 'A';
            this.require = ['?ngModel'];
            var that = this;
            this.link = function (scope: app.core.controllers.ICoreScope, element: ng.IAugmentedJQuery, attributes: ng.IAttributes, controller: any) {
                var maxLength: number = parseInt(element.attr(attributes.$attr['maxlen']));
                var currentController: ng.INgModelController = app.directives.BaseDirective.getControllerFromParameterArray(controller);
                if (!currentController) return;
                var validator = function (value) {
                    if (!that.isEmpty(value) && value.length > maxLength) {
                        currentController.$setValidity('maxlen', false);
                        return value;
                    } else {
                        currentController.$setValidity('maxlen', true);
                        return value;
                    }
                };

                currentController.$formatters.push(validator);
                currentController.$parsers.unshift(validator);
            };
        }
    }

    class DropdownRequiredDirective extends BaseDirective {
        constructor() {
            super();
            this.restrict = 'A';
            this.require = ['?ngModel'];
            var that = this;
            this.link = function (scope: app.core.controllers.ICoreScope, element: ng.IAugmentedJQuery, attributes: ng.IAttributes, controller: any) {
                var currentController: ng.INgModelController = app.directives.BaseDirective.getControllerFromParameterArray(controller);
                if (!currentController) return;
                var validator = function (value) {
                    if (that.isEmpty(value)) {
                        currentController.$setValidity('dropdownRequired', false);
                        return value;
                    } else {
                        var intValue = parseInt(value);
                        if (intValue) {
                            currentController.$setValidity('dropdownRequired', true);
                        } else {
                            currentController.$setValidity('dropdownRequired', false);
                        }
                        return value;
                    }
                };

                currentController.$formatters.push(validator);
                currentController.$parsers.unshift(validator);
            };
        }
    }


    var FormSubmitDirectiveFunctionName = 'formSubmitFunction';
    export var FormSubmitDirectiveName = 'formSubmit';

    class FormSubmitDirective extends BaseDirective {

        constructor(private utilities: app.core.services.IUtilities) {
            super();
            this.restrict = 'A';
            this.link = function (scope: ng.IScope, element: ng.IAugmentedJQuery, attributes: ng.IAttributes) {
                var $element = angular.element(element);

                $element.bind('click', function (eventObject: JQueryEventObject) {
                    eventObject.preventDefault();

                    var form: ng.IFormController = scope.$eval(attributes[FormSubmitDirectiveName]);

                    // trigger all pristine fields to be dirty and run validation
                    angular.forEach(form, function (fieldController: ng.INgModelController) {
                        if (fieldController.$pristine) {
                            fieldController.$setViewValue(fieldController.$viewValue);
                        }
                    });

                    if (form.$valid) {
                        // call function defined in another attribute
                        scope.$eval(attributes[FormSubmitDirectiveFunctionName]);
                    } else {
                        // apply scope changes to show error messages
                        scope.$apply();
                        utilities.showMessage('Please correct invalid values.');
                    }
                });
            };
        }
    }

    angular.module('app.directives.validation', [])
        .directive('required', [function () {
            return new RequiredDirective();
        }])
        .directive('maxlen', [function () {
            return new MaxLengthDirective();
        }])
        .directive('dropdownRequired', [function () {
            return new DropdownRequiredDirective();
        }])
        .directive('formSubmit', ['utilities', function (utilities: app.core.services.IUtilities) {
            return new FormSubmitDirective(utilities);
        }]);


}