using System.Web;
using System.Web.Optimization;

namespace Skoodle
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/lib/jquery-{version}.js",
                        "~/Scripts/lib/jquery.ui.core.1.10.3.min.js",
                        "~/Scripts/lib/jquery.ui.widget.1.10.3.min.js",
                        "~/Scripts/lib/jquery.ui.mouse.1.10.3.min.js",
                        "~/Scripts/lib/jquery.ui.draggable.1.10.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/lib/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/lib/bootstrap.js",
                      "~/Scripts/lib/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/spa-handler").Include(
                      "~/Scripts/single-page-handler.js"));

            bundles.Add(new ScriptBundle("~/bundles/wPaint").Include(
                "~/Scripts/lib/wColorPicker.min.js",
                "~/Scripts/lib/wPaint-utils.js",
                "~/Scripts/lib/wPaint.js",
                "~/Scripts/lib/wPaint.menu.main.min.js",
                "~/Scripts/lib/wPaint.menu.text.min.js",
                "~/Sctipts/lib/wPaint.menu.main.shapes.min.js",
                "~/Scripts/lib/wPaint.menu.main.file.min.js",
                "~/Scripts/gameplay.js"));

            bundles.Add(new StyleBundle("~/Content/wPaintStyles").Include(
                "~/Content/wColorPicker.min.css",
                "~/Content/wPaint.min.css",
                "~/Content/chat.css"));
        }
    }
}
