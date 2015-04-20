using System.Collections.Generic;

namespace AngularAspNetMvc.Web.Core.Validation
{
    public class ValidationAttributeMetadata
    {

        public ValidationAttributeMetadata(string directiveName, string errorMessage, string attributeValue = "", Dictionary<string, string> additionalAttributes = null)
        {
            DirectiveName = directiveName;
            ErrorMessage = errorMessage;
            AttributeValue = attributeValue;
            AdditionalAttributes = additionalAttributes ?? new Dictionary<string, string>();
        }

        public string DirectiveName { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, string> AdditionalAttributes { get; set; }
        public string AttributeValue { get; set; }

    }
}
