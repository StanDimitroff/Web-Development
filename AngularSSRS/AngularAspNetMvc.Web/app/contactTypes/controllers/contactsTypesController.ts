/// <reference path="../../core/services/utilities.ts" />
/// <reference path="../models/models.ts" />
/// <reference path="../../home/interfaces.ts" />
/// <reference path="../../core/controllers/appController.ts" />
/// <reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
module app.contacts {

    import IUtilities = app.core.services.IUtilities;
    import IHttp = app.core.services.http.IHttp;
    import IHttpResult = app.core.services.http.IHttpResult;

    interface IContactTypesScope extends app.core.controllers.ICoreScope {
        contactTypes: Array<app.contactTypes.models.IContactType>;
        editType: (contactType: app.contactTypes.models.IContactType) => void;
        deleteType: (contactType: app.contactTypes.models.IContactType) => void;
        add: () => void;
    }

    class contactsTypesController extends app.core.controllers.CoreController {

        constructor(private http: IHttp, private utilities: IUtilities, private $scope: IContactTypesScope, $location: ng.ILocationService) {
            super($scope);

            var getData = () => {
                http.get('/ContactTypes/Get', (result: IHttpResult<Array<app.contactTypes.models.IContactType>>) => {
                    $scope.contactTypes = result.Result;
                }, true);
            };

            var deleted = function (result: IHttpResult<app.contactTypes.models.IContactType>) {
                if (result.Success) {
                    getData();
                }
            };

            var deleteType = (contactType: app.contactTypes.models.IContactType) => {
                http.post('/ContactTypes/Delete', contactType, deleted, true);
            };



            getData();

            $scope.editType = (contactType: app.contactTypes.models.IContactType) => {
                $location.path("/contacttype/edit/" + contactType.ContactTypeId);
            };

            $scope.add = () => {
                $location.path("/contacttype/edit/add");
            };

            $scope.deleteType = (contactType: app.contactTypes.models.IContactType) => {
                var buttons: app.core.services.IButtonForMessage[] = [
                    {
                        mehtod: () => {
                            deleteType(contactType);
                        },
                        label: 'Delete'
                    },
                    {
                        label: 'Cancel'
                    }
                ];
                utilities.showMessage('Delete type ' + contactType.Name + '?', false, buttons);
            };

        }
    }

    angular.module('app.contacts.contactsTypesController', ['app.core.services.utilities', 'app.core.services.http'])
        .controller('app.contacts.contactsTypesController', ['http', 'utilities', '$scope', '$location',
            function (http: IHttp, utilities: IUtilities, $scope: IContactTypesScope, $location: ng.ILocationService) {
                return new contactsTypesController(http, utilities, $scope, $location);
            }]);
}