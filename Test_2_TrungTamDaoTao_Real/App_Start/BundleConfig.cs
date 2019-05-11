using System.Web;
using System.Web.Optimization;

namespace Test_2_TrungTamDaoTao_Real
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css",
                      "~/Content/css/style.css"));


            bundles.Add(new StyleBundle("~/Content/ResponsiveDataTableCSS").Include(
                      "~/Content/css/responsive.bootstrap.min.css",
                      "~/Content/css/responsive.dataTables.min.css",
                      "~/Content/css/responsive.foundation.min.css",
                      "~/Content/css/responsive.jqueryui.min.css",
                      "~/Content/css/responsive.semanticui.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/ResponsiveDataTableJS").Include(
                      "~/Scripts/js/dataTables.responsive.min.js",
                      "~/Scripts/js/responsive.bootstrap.min.js",
                      "~/Scripts/js/responsive.foundation.min.js",
                      "~/Scripts/js/responsive.jqueryui.min.js",
                      "~/Scripts/js/responsive.semanticui.min.js"));

        }
    }
}
