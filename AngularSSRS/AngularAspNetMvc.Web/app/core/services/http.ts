/// <reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../home/interfaces.ts" />


module app.core.services.http {

    import IUtilities = app.core.services.IUtilities;
    import IGlobals = interfaces.IGLobals;

    export interface IHttp {
        post(urlFragment: string, data: any, callback: IHttpCallback<IHttpResult<any>>, showPleaseWait: boolean): void;
        get(urlFragment: string, callback: IHttpCallback<IHttpResult<any>>, showPleaseWait: boolean, params?: any): void;

    }

    export interface IRequestConfig extends ng.IRequestConfig {
        showPleaseWait: boolean;
    }

    export interface IHttpCallback<IHttpResult> {
        (result: IHttpResult): void;
    }

    export interface IHttpResult<T> {
        Success: boolean;
        ErrorMessage: string;
        Result: T;
    }

    export interface IPagedResult<T> {
        TotalRows: number;
        TotalPages: number;
        Result: Array<T>;
    }

    export interface IHttpPagedResult<T> {
        Success: boolean;
        ErrorMessage: string;
        Result: IPagedResult<T>;
    }

    export interface IHttpInterceptor {
        request: (config: IRequestConfig) => IRequestConfig;
        requestError: (rejection: any) => ng.IPromise<any>;
        response: (response: ng.IPromise<any>) => ng.IPromise<any>;
        responseError: (rejection: any) => ng.IPromise<any>;
    }

    class Http implements IHttp {

        post: (urlFragment: string, data: any, callback: IHttpCallback<IHttpResult<any>>, showPleaseWait: boolean) => void;
        get: (urlFragment: string, callback: IHttpCallback<IHttpResult<any>>, showPleaseWait: boolean) => void;

        constructor(private $http: ng.IHttpService, private utilities: IUtilities, private globalsService: IGlobals) {
            var that = this;
            this.post = function (urlFragment: string, data: any, callback: IHttpCallback<IHttpResult<any>>, showPleaseWait: boolean) {
                var url = that.globalsService.webApiBaseUrl + urlFragment;
                var promise = that.$http.post(url, data, { showPleaseWait: showPleaseWait });
                promise.error(function (result: any, status: number, headers: (headerName: string) => string, config: IRequestConfig) {
                    if (status !== 0) {
                        that.utilities.showMessage('Error getting server data.');
                        callback({ Result: null, Success: false, ErrorMessage: result });
                    }
                });
                promise.success(function (result: IHttpResult<any>, status: number, headers: (headerName: string) => string, config: IRequestConfig) {
                    if (!result.Success) {
                        that.utilities.showMessage(result.ErrorMessage);
                    }
                    callback(result);
                });
                return promise;
            };

            this.get = function (urlFragment: string, callback: IHttpCallback<IHttpResult<any>>, showPleaseWait: boolean, params?: any) {
                var url = that.globalsService.webApiBaseUrl + urlFragment;
                var promise: ng.IHttpPromise<any>;
                if (params) {
                    promise = that.$http.get(url, { params: params, showPleaseWait: showPleaseWait });
                } else {
                    promise = that.$http.get(url, { showPleaseWait: showPleaseWait });
                }
                promise.error(function (result: any, status: number, headers: (headerName: string) => string, config: IRequestConfig) {
                    if (status !== 0) {
                        that.utilities.showMessage('Error getting server data.');
                        callback({ Result: null, Success: false, ErrorMessage: result });
                    }
                });
                promise.success(function (result: IHttpResult<any>, status: number, headers: (headerName: string) => string, config: IRequestConfig) {
                    if (!result.Success) {
                        that.utilities.showMessage(result.ErrorMessage);
                    }
                    callback(result);
                });
                return promise;
            };
        }
    }

    class HttpInterceptor implements IHttpInterceptor {
        request: (config: IRequestConfig) => IRequestConfig;
        requestError: (rejection: any) => ng.IPromise<any>;
        response: (response: ng.IPromise<any>) => ng.IPromise<any>;
        responseError: (rejection: any) => ng.IPromise<any>;

        constructor(private $q: ng.IQService, private utilities: IUtilities, private globalsService: IGlobals) {
            var totalRequests: number = 0;
            var that = this;
            this.request = function (config: IRequestConfig) {
                if (config.showPleaseWait) {
                    totalRequests++;
                    if (totalRequests === 1) {
                        that.utilities.showPleaseWait();
                    }
                }
                return config;
            };
            this.requestError = function (rejection: any) {
                var config: IRequestConfig = rejection['config'];
                if (config && config.showPleaseWait) {
                    totalRequests--;
                    if (totalRequests === 0) {
                        that.utilities.hidePleaseWait();
                    }
                }
                return $q.reject(rejection);
            };
            this.response = function (response: ng.IPromise<any>) {
                var config: IRequestConfig = response['config'];
                if (config && config.showPleaseWait) {
                    totalRequests--;
                    if (totalRequests === 0) {
                        that.utilities.hidePleaseWait();
                    }
                }
                return response;
            };
            this.responseError = function (rejection: any) {
                var config: IRequestConfig = rejection['config'];
                if (config && config.showPleaseWait) {
                    totalRequests--;
                    if (totalRequests === 0) {
                        that.utilities.hidePleaseWait();
                    }
                }
                return $q.reject(rejection);
            };
        }
    }

    angular.module('app.core.services.http', ['app.core.services.utilities', 'app.globalsModule'])
        .config(['$httpProvider', function ($httpProvider: ng.IHttpProvider) {
            $httpProvider.interceptors.push('httpInterceptor');
        }])
        .factory('http', ['$http', 'utilities', 'globalsService',
            function ($http: ng.IHttpService, utilities: IUtilities, globalsService: IGlobals) {
                return new Http($http, utilities, globalsService);
            }])
        .factory('httpInterceptor', ['$q', 'utilities', 'globalsService',
            function ($q: ng.IQService, utilities: IUtilities, globalsService: IGlobals) {
                return new HttpInterceptor($q, utilities, globalsService);
            }])
    ;

}