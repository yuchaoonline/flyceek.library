using System.Web;
using System.Web.Optimization;

namespace LuceneNetMvcAppTest
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Resource/Js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Resource/Js/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Resource/Js/jquery.unobtrusive*",
                        "~/Resource/Js/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Resource/Js/modernizr-*"));

            bundles.Add(new StyleBundle("~/Resource/Css").Include("~/Resource/Css/site.css"));

            bundles.Add(new StyleBundle("~/Resource/Css/themes/base/css").Include(
                        "~/Resource/Css/themes/base/jquery.ui.core.css",
                        "~/Resource/Css/themes/base/jquery.ui.resizable.css",
                        "~/Resource/Css/themes/base/jquery.ui.selectable.css",
                        "~/Resource/Css/themes/base/jquery.ui.accordion.css",
                        "~/Resource/Css/themes/base/jquery.ui.autocomplete.css",
                        "~/Resource/Css/themes/base/jquery.ui.button.css",
                        "~/Resource/Css/themes/base/jquery.ui.dialog.css",
                        "~/Resource/Css/themes/base/jquery.ui.slider.css",
                        "~/Resource/Css/themes/base/jquery.ui.tabs.css",
                        "~/Resource/Css/themes/base/jquery.ui.datepicker.css",
                        "~/Resource/Css/themes/base/jquery.ui.progressbar.css",
                        "~/Resource/Css/themes/base/jquery.ui.theme.css"));
        }
    }
}