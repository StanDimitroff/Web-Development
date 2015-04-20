/// <reference path="../../../Scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="../models/models.ts" />
/// <reference path="../../home/interfaces.ts" />
/// <reference path="../../core/controllers/appController.ts" />
/// <reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
module app.contacts {

    import IUtilities = app.core.services.IUtilities;
    import IHttp = app.core.services.http.IHttp;
    import IHttpResult = app.core.services.http.IHttpResult;

    interface IContactTypesEditScope extends app.core.controllers.ICoreScope {
        save: () => void;
        cancel: () => void;
        model: app.contacts.models.IContact;
        contactTypes: Array<app.contactTypes.models.IContactType>;
    }

    interface IContactRouteParams extends ng.route.IRouteParamsService {
        id: any;
    }

    class ContactsEditController extends app.core.controllers.CoreController {

        constructor(
            private http: IHttp,
            private utilities: IUtilities,
            private $scope: IContactTypesEditScope,
            $routeParams: IContactRouteParams,
            private $location: ng.ILocationService) {
            super($scope);

            var saved = function (result: IHttpResult<app.contacts.models.IContact>) {
                if (result.Success) {
                    $location.path("/contacts");
                }
            };

            $scope.save = () => {
                http.post('/Contacts/Save', $scope.model, saved, true);
            };

            $scope.cancel = () => {
                $location.path("/contacts");
            };

            var processResults = function (result: IHttpResult<app.contacts.models.IContact>) {
                $scope.model = result.Result;
            };

            var id = $routeParams.id;
            if (id === 'add') {
                http.get('/ContactTypes/GetForSelect', (result: IHttpResult<Array<app.contactTypes.models.IContactType>>) => {
                    $scope.contactTypes = result.Result;
                    $scope.model = {
                        ContactId: 0,
                        ContactTypeId: 0,
                        FirstName: '',
                        LastName: '',
                        IsActive: true
                    };
                }, true);

            } else {
                http.get('/ContactTypes/GetForSelect', (result: IHttpResult<Array<app.contactTypes.models.IContactType>>) => {
                    $scope.contactTypes = result.Result;
                    http.get('/Contacts/GetById', processResults, true, { contactId: id });
                }, true);
            }

        }
    }

    angular.module('app.contacts.contactsEditController', ['app.core.services.utilities', 'app.core.services.http', 'ngRoute'])
        .controller('app.contacts.contactsEditController', ['http', 'utilities', '$scope', '$routeParams', '$location',
            function (http: IHttp, utilities: IUtilities, $scope: IContactTypesEditScope, $routeParams: IContactRouteParams, $location: ng.ILocationService) {
                return new ContactsEditController(http, utilities, $scope, $routeParams, $location);
            }]);
}