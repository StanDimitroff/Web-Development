/// <reference path="../../../Scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="../models/models.ts" />
/// <reference path="../../core/controllers/appController.ts" />
/// <reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
module app.contacts {

    import IUtilities = app.core.services.IUtilities;
    import IHttp = app.core.services.http.IHttp;
    import IHttpResult = app.core.services.http.IHttpResult;

    interface IContactTypesEditScope extends app.core.controllers.ICoreScope {
        save: () => void;
        cancel: () => void;
        model: app.contactTypes.models.IContactType;
    }

    interface IContactTypeRouteParams extends ng.route.IRouteParamsService {
        id: any;
    }

    class ContactsTypesEditController extends app.core.controllers.CoreController {

        constructor(
            private http: IHttp,
            private utilities: IUtilities,
            private $scope: IContactTypesEditScope,
            $routeParams: IContactTypeRouteParams,
            private $location: ng.ILocationService) {
            super($scope);

            var saved = function (result: IHttpResult<app.contactTypes.models.IContactType>) {
                if (result.Success) {
                    $location.path("/contacttypes");
                }
            };

            $scope.save = () => {
                http.post('/ContactTypes/Save', $scope.model, saved, true);
            };

            $scope.cancel = () => {
                $location.path("/contacttypes");
            };

                var processResults = function (result: IHttpResult<app.contactTypes.models.IContactType>) {
                $scope.model = result.Result;
            };

            var id = $routeParams.id;
            if (id === 'add') {
                $scope.model = {
                    ContactTypeId: 0,
                    Name: ''
                };
            } else {
                http.get('/ContactTypes/GetById', processResults, true, { contactTypeId: id });
            }

        }
    }

    angular.module('app.contacts.contactsTypesEditController', ['app.core.services.utilities', 'app.core.services.http', 'ngRoute'])
        .controller('app.contacts.contactsTypesEditController', ['http', 'utilities', '$scope', '$routeParams', '$location',
            function (http: IHttp, utilities: IUtilities, $scope: IContactTypesEditScope, $routeParams: IContactTypeRouteParams, $location: ng.ILocationService) {
                return new ContactsTypesEditController(http, utilities, $scope, $routeParams, $location);
            }]);
}