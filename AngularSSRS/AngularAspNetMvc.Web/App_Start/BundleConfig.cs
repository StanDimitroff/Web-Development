using System.Web.Optimization;

namespace AngularAspNetMvc.Web.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/bundles/thirdparty").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/angular.min.js",
                         "~/Scripts/angular-route.min.js",
                        "~/Scripts/bootstrap.min.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-{version}.js"));


            bundles.Add(new ScriptBundle("~/app/modules")
                .Include("~/app/app.js")
                .Include("~/app/core/directives/*.js")
                .Include("~/app/core/controllers/*.js")
                .Include("~/app/core/services/*.js")
                .Include("~/app/home/services/*.js")
                .Include("~/app/home/controllers/*.js")
                .Include("~/app/contactTypes/controllers/*.js")
                .Include("~/app/contacts/controllers/*.js")
                .Include("~/app/reports/services/*.js")
                .Include("~/app/reports/controllers/*.js")
                );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/font-awesome.css",
                "~/Content/style.css"));

        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        }

    }
}