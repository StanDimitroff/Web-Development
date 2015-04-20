
module app.reports.reportsViewController {

    import IUtilities = app.core.services.IUtilities;
    import IReportRequest = app.reports.models.IReportRequest;
    import IReportsService = app.reports.reportsService.IReportsService;
    import IGlobals = interfaces.IGLobals;

    interface IReportScope extends app.core.controllers.ICoreScope {
        report: IReportRequest;
        runReport: () => void;
        reportViewUrl: string;
        reportSource: string;
    }

    interface IReportRouteParams extends ng.route.IRouteParamsService {
        id: number;
    }

    class ReportsViewController extends app.core.controllers.CoreController {

        constructor(
            reportsService: IReportsService,
            private utilities: IUtilities,
            private $scope: IReportScope,
            $location: ng.ILocationService,
            $routeParams: IReportRouteParams,
            globalsService: IGlobals) {

            super($scope);

            var getData = (reportId: number) => {
                reportsService.getReport(reportId, (data: IReportRequest) => {
                    $scope.report = data;
                });
            };

            getData($routeParams.id);

            $scope.runReport = () => {
                reportsService.createRequest($scope.report, (result: string) => {
                    if (result) {
                        $scope.report.UniqueId = result;
                        $scope.reportSource = globalsService.baseUrl + 'ViewsStatic/ReportForm.aspx?r=' + $scope.report.UniqueId;
                    }
                });
            };
            $scope.reportViewUrl = undefined;
        }
    }

    angular.module('app.reports.reportsViewController', ['app.core.services.utilities', 'app.reports.reportsService', 'app.globalsModule'])
        .controller('app.reports.reportsViewController', [
            'app.reports.reportsService', 'utilities', '$scope', '$location', '$routeParams', 'globalsService',
            function (
                reportsService: IReportsService,
                utilities: IUtilities,
                $scope: IReportScope,
                $location: ng.ILocationService,
                $routeParams: IReportRouteParams,
                globalsService: IGlobals) {
                return new ReportsViewController(reportsService, utilities, $scope, $location, $routeParams, globalsService);
            }]);
} 