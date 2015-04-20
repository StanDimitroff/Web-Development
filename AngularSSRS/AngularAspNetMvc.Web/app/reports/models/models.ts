module app.reports.models {

    export interface IReport {
        ReportId: number;
        ReportFileName: string;
        ReportName: string;
        ReportDescription: string;
        ReportParameters: IReportParameter[];
    }

    export interface IReportParameter {
        ReportParameterId: number;
        ReportId: number;
        ReportParameterName: string;
        ParameterName: string;
        ParameterViewName: string;
    }

    export interface IReportRequest
    {
        ReportRequestId: number;
        ReportId: number;
        ReportFileName: string;
        ReportName: string;
        ReportDescription: string;
        UniqueId: string;
        ReportRequestParameters: IReportRequestParameter[]
    }

    export interface IReportRequestParameter {
        ReportRequestParameterId: number;
        ReportRequestId: number;
        ParameterValue: number;
        ParameterName: number;
        ReportParameterName: string;
        ParameterViewName: string;
    }
}