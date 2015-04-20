using AngularAspNetMvc.Models.Mappings;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AngularAspNetMvc.Tests.Mapping
{
    [TestClass]
    public class AutoMapperTests
    {
        [TestMethod]
        public void TestAllMaps()
        {
            //Will throw exception if invalid
            MappingConfiguration.CreateMaps();

            Mapper.AssertConfigurationIsValid();
        }
    }
}
