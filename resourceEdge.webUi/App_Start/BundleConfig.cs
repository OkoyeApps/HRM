using System.Web;
using System.Web.Optimization;

namespace resourceEdge.webUi
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Scripts/angular").Include(
                "~/Scripts/angular/angular.js"
                ));

            bundles.Add(new StyleBundle("~/asset/DefaultCss").Include(
                "~/assets/AdminTemplate/assets/plugins/jquery-ui/themes/base/minified/jquery-ui.min.css",
                "~/assets/AdminTemplate/assets/plugins/bootstrap/css/bootstrap.min.css",
                "~/assets/AdminTemplate/assets/plugins/font-awesome/css/font-awesome.min.css",
                "~/assets/AdminTemplate/assets/css/animate.min.css",
                "~/assets/AdminTemplate/assets/css/style.min.css",
                "~/assets/AdminTemplate/assets/css/style-responsive.min.css",
                "~/assets/AdminTemplate/assets/css/theme/default.css"
                ));

            bundles.Add(new StyleBundle("~/asset/BaseCss").Include(
                "~/Content/jBox.css",
                "~/assets/AdminTemplate/assets/plugins/parsley/src/parsley.css"
                ));
            bundles.Add(new ScriptBundle("~/asset/HeadJs").Include(
                "~/assets/AdminTemplate/assets/plugins/pace/pace.min.js",
                "~/Scripts/jquery-3.2.1.js",
                "~/Scripts/jBox.min.js"
                ));
            bundles.Add(new ScriptBundle("~/assets/BaseJs").Include(
                "~/Scripts/jquery.blockUI.js",
                "~/assets/AdminTemplate/assets/plugins/jquery/jquery-migrate-1.1.0.min.js",
                "~/assets/AdminTemplate/assets/plugins/jquery-ui/ui/minified/jquery-ui.min.js",
                "~/assets/AdminTemplate/assets/plugins/bootstrap/js/bootstrap.min.js",
                "~/assets/AdminTemplate/assets/plugins/slimscroll/jquery.slimscroll.min.js",
                "~/assets/AdminTemplate/assets/plugins/jquery-cookie/jquery.cookie.js",
                "~/assets/AdminTemplate/assets/plugins/parsley/dist/parsley.js",
                "~/Scripts/Pages/JS/EdgeValidation.js",
                "~/assets/AdminTemplate/assets/plugins/parsley/dist/parsley.js"

                ));
            bundles.Add(new ScriptBundle("~/assets/Table").Include(
                     "~/assets/AdminTemplate/assets/plugins/DataTables/media/js/jquery.dataTables.js",
                     "~/assets/AdminTemplate/assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js",
                     "~/assets/AdminTemplate/assets/plugins/DataTables/extensions/Buttons/js/dataTables.buttons.min.js",
                     "~/assets/AdminTemplate/assets/plugins/DataTables/extensions/Buttons/js/buttons.bootstrap.min.js",
                     "~/assets/AdminTemplate/assets/plugins/DataTables/extensions/Buttons/js/buttons.flash.min.js",
                     "~/assets/AdminTemplate/assets/plugins/DataTables/extensions/Buttons/js/jszip.min.js",
                     "~/assets/AdminTemplate/assets/plugins/DataTables/extensions/Buttons/js/pdfmake.min.js",
                     "~/assets/AdminTemplate/assets/plugins/DataTables/extensions/Buttons/js/vfs_fonts.min.js",
                     "~/assets/AdminTemplate/assets/plugins/DataTables/extensions/Buttons/js/buttons.html5.min.js",
                     "~/assets/AdminTemplate/assets/plugins/DataTables/extensions/Buttons/js/buttons.print.min.js",
                     "~/assets/AdminTemplate/assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"
                ));
            bundles.Add(new StyleBundle("~/asset/tablecss").Include(
                   "~/assets/AdminTemplate/assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css",
                    "~/assets/AdminTemplate/assets/plugins/DataTables/extensions/Buttons/css/buttons.bootstrap.min.css",
                    "~/assets/AdminTemplate/assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css"
                ));
            bundles.Add(new StyleBundle("~/asset/configAppraisal").Include(

                ));
            bundles.Add(new StyleBundle("~/asset/loginCss").Include(
                 "~/assets/AdminTemplate/assets/plugins/jquery-ui/themes/base/minified/jquery-ui.min.css",
                 "~/assets/AdminTemplate/assets/plugins/bootstrap/css/bootstrap.min.css",
                 "~/assets/AdminTemplate/assets/plugins/font-awesome/css/font-awesome.min.css",
                 "~/assets/AdminTemplate/assets/css/animate.min.css",
                 "~/assets/AdminTemplate/assets/css/style.min.css",
                 "~/assets/AdminTemplate/assets/css/style-responsive.min.css",
                 "~/assets/AdminTemplate/assets/css/theme/default.css"
                ));

            bundles.Add(new ScriptBundle("~/assets/loginJs").Include(
                "~/assets/AdminTemplate/assets/plugins/jquery/jquery-1.9.1.min.js" ,
                "~/assets/AdminTemplate/assets/plugins/jquery/jquery-migrate-1.1.0.min.js" ,
                "~/assets/AdminTemplate/assets/plugins/jquery-ui/ui/minified/jquery-ui.min.js" ,
                "~/assets/AdminTemplate/assets/plugins/bootstrap/js/bootstrap.min.js",
                "~/assets/AdminTemplate/assets/plugins/slimscroll/jquery.slimscroll.min.js" ,
                "~/assets/AdminTemplate/assets/plugins/jquery-cookie/jquery.cookie.js" 
                ));
            BundleTable.EnableOptimizations = true;
        }
    }
}
