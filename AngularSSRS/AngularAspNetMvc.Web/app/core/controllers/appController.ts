module app.core.controllers {

    export interface ICoreScope extends ng.IScope {
        showError?: (ngModelController: ng.INgModelController, error: string) => any;
    }

    export class CoreController {
        showError(ngModelController: ng.INgModelController, error: string) {
            if (ngModelController.$dirty) {
                return ngModelController.$error[error];
            }
            return false;
        }


        constructor($scope: ICoreScope) {
            $scope.showError = this.showError;
        }

        private cacheObject: ng.ICacheObject;

        private createCacheObjectIfNeeded() {
            if (!this.cacheObject) {
                var injector = angular.element('[data-ng-app="app"]').injector();
                var cacheService = injector.get('$cacheFactory');
                this.cacheObject = cacheService.get('controllerCache');
                if (!this.cacheObject) {
                    this.cacheObject = cacheService('controllerCache');
                }
            }
        }


        public AddToCache<T>(key: string, value: T) {
            this.createCacheObjectIfNeeded();
            this.cacheObject.put(key, value);
        }

        public GetFromCache<T>(key: string): T {
            this.createCacheObjectIfNeeded();
            return this.cacheObject.get(key);
        }

    }


}