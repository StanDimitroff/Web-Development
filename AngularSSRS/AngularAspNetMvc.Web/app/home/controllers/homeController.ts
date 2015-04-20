/// <reference path="../../../Scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="../services/menuService.ts" />
/// <reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../core/controllers/appController.ts" />
module app.home.controllers {

    interface homeScope extends app.core.controllers.ICoreScope {
        navigate: (path: string) => void;
        menus: Array<interfaces.IMenuItem>;
    }

    class homeController extends app.core.controllers.CoreController {
        constructor(private $location: ng.ILocationService, private $scope: homeScope, $route: ng.route.IRouteService, $timeout: ng.ITimeoutService, private menuService: app.home.services.IMenuService) {
            super($scope);
            $scope.navigate = function (path: string) {
                $location.path(path);
            };
           
            menuService.getMenu()
                .success(function (data) {
                    $scope.menus = new Array<interfaces.IMenuItem>();
                    angular.forEach(data, function (menu: interfaces.IMenuItem) {
                        if (menu.IsMenu) {
                            $scope.menus.push(menu);
                        }
                        
                    });
                    $scope.$apply();
                    var stop = $timeout(function () {
                        if ($route.routes['/home']) {
                            $route.reload();
                            $timeout.cancel(stop);
                        }
                    }, 50);
                })

                .error(function (result: any, status: number, headers: (headerName: string) => string, config: ng.IRequestConfig) {
                    if (status != 0) {
                        alert('error');
                    }

                });
        }
    }

    angular.module('app.home.controllers', ['app.home.services'])
        .controller('homeController', ['$location', '$scope', '$route', '$timeout', 'app.home.menuService',
            function ($location: ng.ILocationService, $scope: homeScope, $route: ng.route.IRouteService, $timeout: ng.ITimeoutService, menuService: app.home.services.IMenuService) {
                return new homeController($location, $scope, $route, $timeout, menuService);
            }]);
}