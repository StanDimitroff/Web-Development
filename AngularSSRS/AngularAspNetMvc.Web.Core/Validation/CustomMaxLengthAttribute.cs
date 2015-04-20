using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace AngularAspNetMvc.Web.Core.Validation
{
    public class CustomMaxLengthAttribute : MaxLengthAttribute, IMetadataAware
    {
        public CustomMaxLengthAttribute(int length)
            : base(length)
        {

        }
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            var additionalMetadataValue = new ValidationAttributeMetadata("maxlen", ErrorMessageString, Length.ToString(CultureInfo.InvariantCulture));
            metadata.AdditionalValues.Add(Guid.NewGuid().ToString(), additionalMetadataValue);
        }
    }
}
