using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AngularAspNetMvc.Web.Core.Validation
{
    public class DropdownRequiredAttribute : RangeAttribute, IMetadataAware
    {
        public DropdownRequiredAttribute()
            : base(1, int.MaxValue)
        {

        }
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            var additionalMetadataValue = new ValidationAttributeMetadata("dropdownRequired", ErrorMessageString);
            metadata.AdditionalValues.Add(Guid.NewGuid().ToString(), additionalMetadataValue);
        }
    }
}
