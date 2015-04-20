/// <reference path="../interfaces.ts" />
/// <reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
module app.home.services {

    export interface IMenuServiceProvider extends ng.IServiceProvider {
        $get: () => IMenuService;
    }

    export interface IMenuService {
        getMenu: () => ng.IHttpPromise<Array<interfaces.IMenuItem>>;
    }

    class MenuService implements IMenuService {
        getMenu: () => ng.IHttpPromise<Array<interfaces.IMenuItem>>;
        constructor(private $http: ng.IHttpService, private globals: interfaces.IGLobals) {

            this.getMenu = function () {
                return $http.get(globals.webApiBaseUrl + '/home/menu');
            };
        }
    }

    angular.module('app.home.services', [])
        .factory('app.home.menuService', function () {
            var injector = angular.injector(['ng', 'app.globalsModule']);
            var $http: ng.IHttpService = injector.get('$http');
            var globals: interfaces.IGLobals = injector.get('globalsService');
            return new MenuService($http, globals);
        });
}