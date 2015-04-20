using AngularAspNetMvc.Models.Core;

namespace AngularAspNetMvc.Models.Common
{
    public class MenuItem : ReadOnlyModel
    {
        public string Path { get; set; }
        public string Controller { get; set; }
        public string TemplateUrl { get; set; }
        public string Title { get; set; }
        public bool IsMenu { get; set; }
    }
}
