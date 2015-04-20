using System.Linq;
using AngularAspNetMvc.Data.Models;
using AngularAspNetMvc.Data.ExtendedModels;
using AutoMapper;


namespace AngularAspNetMvc.Models.Mappings
{
    public static class MappingConfiguration
    {
        public static void CreateMaps()
        {
            CreateContactMaps();
            CreateReportMaps();
#if DEBUG
            Mapper.AssertConfigurationIsValid();
#endif
        }

        private static void CreateContactMaps()
        {
            CreateTwoWayMapping<ContactType, Contacts.ContactType>();
            CreateTwoWayMapping<Contact, Contacts.Contact>();
            CreateOneWayMapping<ContactInfo, Contacts.ContactInfo>();
            CreateOneWayMapping<PagedResult<ContactInfo>, Common.PagedResult<Contacts.ContactInfo>>();
        }

        private static void CreateReportMaps()
        {
            CreateOneWayMapping<Report, Reports.Report>();
            CreateOneWayMapping<ReportParameter, Reports.ReportParameter>();
            CreateOneWayMapping<ReportParameter, Reports.ReportRequestParameter>();
            CreateOneWayMapping<Report, Reports.ReportRequest>()
                .ForMember(dest => dest.ReportRequestParameters, options => options.MapFrom(source => source.ReportParameters));
            CreateTwoWayMapping<Reports.ReportRequestParameter, ReportRequestParameter>();
            CreateTwoWayMapping<Reports.ReportRequest, ReportRequest>();

        }

        private static void CreateTwoWayMapping<TSource, TDestination>()
        {
            Mapper.CreateMap<TDestination, TSource>()
                .IgnoreAllNonExisting();

            Mapper.CreateMap<TSource, TDestination>()
                .IgnoreAllNonExisting();
        }

        private static IMappingExpression<TSource, TDestination> CreateOneWayMapping<TSource, TDestination>()
        {
            return Mapper.CreateMap<TSource, TDestination>()
                .IgnoreAllNonExisting();

        }

        private static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(
                x => x.SourceType == sourceType && x.DestinationType == destinationType);
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }
    }
}
