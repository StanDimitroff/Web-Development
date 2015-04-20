/// <reference path="../models/models.ts" />
/// <reference path="../../core/services/utilities.ts" />
/// <reference path="../../home/interfaces.ts" />
/// <reference path="../../core/controllers/appController.ts" />
/// <reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
module app.contacts {

    import IUtilities = app.core.services.IUtilities;
    import IHttp = app.core.services.http.IHttp;
    import IHttpResult = app.core.services.http.IHttpResult;
    import IHttpPagedResult = app.core.services.http.IHttpPagedResult;

    interface IContactScope extends ng.IScope {
        contacts: Array<app.contacts.models.IContactInfo>;
        editContact: (contactType: app.contacts.models.IContactInfo) => void;
        deleteContact: (contactType: app.contacts.models.IContactInfo) => void;
        add: () => void;
        pageCount: number;
        pageSize: number;
        gotoPage: (pageNumber: number) => void;
        totalFound: number;
        name: string;
        search: (pageNumber: number, pageSize: number, name: string, eventToRaise: string) => void;
        performSearch: () => void;
        currentPage: number;
    }

    class ContactsController extends app.core.controllers.CoreController {

        constructor(private http: IHttp, private utilities: IUtilities, private $scope: IContactScope, $location: ng.ILocationService) {
            super($scope);
            $scope.pageSize = 10;
            $scope.pageCount = 0;
            $scope.currentPage = 1;
            $scope.search = (pageNumber: number, pageSize: number, name: string, eventToRaise: string) => {
                http.get('/Contacts/Get', (result: IHttpPagedResult<app.contacts.models.IContactInfo>) => {
                    if (result.Success) {
                        $scope.contacts = result.Result.Result;
                        $scope.pageCount = result.Result.TotalPages;
                        $scope.totalFound = result.Result.TotalRows;
                        $scope.currentPage = pageNumber;
                        $scope.$broadcast(eventToRaise);
                    }

                }, true, { pageNumber: pageNumber, pageSize: pageSize, name: name });
            };

            $scope.search(1, $scope.pageSize, $scope.name, 'searchCompleted');

            $scope.performSearch = ()=> {
                $scope.search(1, $scope.pageSize, $scope.name, 'searchCompleted');
            };
            $scope.gotoPage = function (pageNumber: number) {
                $scope.search(pageNumber, $scope.pageSize, $scope.name, 'pageLoadCompleted');
            };

            $scope.editContact = (contact: app.contacts.models.IContactInfo) => {
                $location.path("/contact/edit/" + contact.ContactId);
            };

            $scope.add = () => {
                $location.path("/contact/edit/add");
            };

            var deleted = function (result: IHttpResult<app.contacts.models.IContactInfo>) {
                if (result.Success) {
                    $scope.search($scope.currentPage, $scope.pageSize, $scope.name, 'searchCompleted');
                }
            };

            var deleteContact = (contactType: app.contacts.models.IContactInfo) => {
                http.post('/Contacts/Delete', contactType, deleted, true);
            };

            $scope.deleteContact = (contact: app.contacts.models.IContactInfo) => {
                var buttons: app.core.services.IButtonForMessage[] = [
                    {
                        mehtod: () => {
                            deleteContact(contact);
                        },
                        label: 'Delete'
                    },
                    {
                        label: 'Cancel'
                    }
                ];
                utilities.showMessage('Delete ' + contact.LastName + ', ' + contact.FirstName + '?', false, buttons);
            };
        }

    }

    angular.module('app.contacts.contactsController', ['app.core.services.utilities'])
        .controller('app.contacts.contactsController', ['http', 'utilities', '$scope', '$location',
            function (http: IHttp, utilities: IUtilities, $scope: IContactScope, $location: ng.ILocationService) {
                return new ContactsController(http, utilities, $scope, $location);
            }]);
}