using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AngularAspNetMvc.Web.Core.Validation
{
    public class CustomRequiredAttribute : RequiredAttribute, IMetadataAware
    {
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            var additionalMetadataValue = new ValidationAttributeMetadata("required", ErrorMessageString);
            metadata.AdditionalValues.Add(Guid.NewGuid().ToString(), additionalMetadataValue);
        }
    }
}
