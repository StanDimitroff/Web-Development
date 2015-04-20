/// <reference path="../../Scripts/typings/angularjs/angular.d.ts" />
module interfaces {
    export interface IGLobals {
        baseUrl: string;
        version: string;
        webApiBaseUrl: string;
        applicatioName: string;
    }

    export interface IGlobalsProvider  {
        $get: () => IGLobals;
    }

    export interface IMenuItem {
        Path: string;
        Controller: string;
        TemplateUrl: string;
        Title: string;
        IsMenu: boolean;
    }
}