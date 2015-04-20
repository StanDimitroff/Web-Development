/// <reference path="../Scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="home/services/menuService.ts" />
/// <reference path="home/interfaces.ts" />
/// <reference path="../Scripts/typings/angularjs/angular.d.ts" />
module app {

    class appUtils {
        static createViewUrl(fragmnt: string, globals: interfaces.IGLobals) {
            return globals.baseUrl + fragmnt + '?v=' + globals.version;
        }
    }

    angular.module('app',
        [
            'ngRoute',
            'app.globalsModule',
            'app.directives.Common',
            'app.directives.validation',
            'app.home.controllers',
            'app.contacts.contactsController',
            'app.contacts.contactsEditController',
            'app.contacts.contactsTypesController',
            'app.contacts.contactsTypesEditController',
            'app.home.services',
            'app.reports.reportsService',
            'app.reports.reportsController',
            'app.reports.reportsViewController'
        ])
        .config(['$routeProvider', '$windowProvider', 'globalsServiceProvider', 'app.home.menuServiceProvider',
            function ($routeProvider: ng.route.IRouteProvider, $window: ng.IWindowService, globalsServiceProvider: interfaces.IGlobalsProvider, menuServiceProvider: app.home.services.IMenuServiceProvider) {
                var globals: interfaces.IGLobals = globalsServiceProvider.$get();
                var menuService: app.home.services.IMenuService = menuServiceProvider.$get();
                menuService.getMenu()
                    .success(function (data) {
                        angular.forEach(data, function (menu: interfaces.IMenuItem) {
                            $routeProvider.when(menu.Path, {
                                controller: menu.Controller,
                                templateUrl: appUtils.createViewUrl(menu.TemplateUrl, globals)
                            });

                        });
                        $routeProvider.otherwise({
                            redirectTo: '/home'
                        });
                    })
                    .error(function (error) {
                        $window.alert('Could not get the menu');
                    });
            }]);
}