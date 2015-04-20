
module app.reports.reportsController {

    import IUtilities = app.core.services.IUtilities;
    import IReport = app.reports.models.IReport;
    import IReportsService = app.reports.reportsService.IReportsService;

    interface IReportsScope extends app.core.controllers.ICoreScope {
        reports: IReport[];
        viewReport: (report: IReport) => void;
    }

    class ReportsController extends app.core.controllers.CoreController {

        constructor(reportsService: IReportsService, private utilities: IUtilities, private $scope: IReportsScope, $location: ng.ILocationService) {
            super($scope);

            var getData = () => {
                reportsService.getReports((data: IReport[]) => {
                    $scope.reports = data;
                });
            };

            $scope.viewReport = (report: IReport) => {
                $location.path("/reports/view/" + report.ReportId);
            };

            getData();

        }
    }

    angular.module('app.reports.reportsController', ['app.core.services.utilities', 'app.core.services.http', 'app.reports.reportsService'])
        .controller('app.reports.reportsController', ['app.reports.reportsService', 'utilities', '$scope', '$location',
            function (reportsService: IReportsService, utilities: IUtilities, $scope: IReportsScope, $location: ng.ILocationService) {
                return new ReportsController(reportsService, utilities, $scope, $location);
            }]);
} 