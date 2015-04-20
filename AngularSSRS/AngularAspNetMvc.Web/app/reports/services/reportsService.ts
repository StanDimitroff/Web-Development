
module app.reports.reportsService {

    import IUtilities = app.core.services.IUtilities;
    import IReport = app.reports.models.IReport;
    import IHttp = app.core.services.http.IHttp;
    import IHttpResult = app.core.services.http.IHttpResult;
    import IReportRequest = app.reports.models.IReportRequest;

    export interface IReportsService {
        getReports: (callback: (reports: IReport[]) => void) => void;
        getReport: (id: number, callback: (reports: IReportRequest) => void) => void;
        createRequest: (request: IReportRequest, callback: (result: string) => void) => void;
    }

    class ReportsService implements IReportsService {
        getReports: (callback: (reports: IReport[]) => void) => void;
        getReport: (id: number, callback: (reports: IReportRequest) => void) => void;
        createRequest: (request: IReportRequest, callback: (result: string) => void) => void;

        constructor(private http: IHttp, utilities: IUtilities, private globalsService: interfaces.IGLobals) {

            var getReportsFunction = (callback: (reports: IReport[]) => void) => {
                this.http.get('/report/reports', (result: IHttpResult<IReport[]>) => {
                    if (result.Success) {
                        callback(result.Result);
                    }
                }, true);
            };

            this.getReports = getReportsFunction;

            var getReportFunction = (id: number, callback: (reports: IReportRequest) => void) => {
                this.http.get('/report/report', (result: IHttpResult<IReportRequest>) => {
                    if (result.Success) {
                        callback(result.Result);
                    }
                }, true, { reportId: id });
            };

            this.getReport = getReportFunction;

            var createRequestFunction = (request: IReportRequest, callback: (result: string) => void) => {
                this.http.post('/report/createrequest', request, (result: IHttpResult<string>) => {
                    callback(result.Result);
                }, true);
            };

            this.createRequest = createRequestFunction;
        }
    }

    angular.module('app.reports.reportsService', ['app.core.services.utilities', 'app.core.services.http', 'app.globalsModule'])
        .factory('app.reports.reportsService', ['http', 'utilities', 'globalsService',
            function (http: IHttp, utilities: IUtilities, globalsService: interfaces.IGLobals) {
                return new ReportsService(http, utilities, globalsService);
            }]);
}  