using System.Web.Optimization;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Bundles;

namespace WorkFlowBilling
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            BundleResolver.Current = new CustomBundleResolver();
            var nullOrderer = new NullOrderer();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js",
                        "~/Scripts/jquery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.js",
                      "~/Scripts/bootstrap/respond.js"));
            
            // Less Bundle
            var lessBundle = new CustomStyleBundle("~/bundles/less");
            lessBundle.Include("~/Content/Less/Site.less",
                "~/Content/Less/bootstrap.less");
            lessBundle.Orderer = nullOrderer;
            bundles.Add(lessBundle); 
        }
    }
}
