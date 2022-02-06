using System.Web.Optimization;

namespace  Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/plugins/js").Include(
                "~/Scripts/js/jquery/jquery-3.1.1.js",
                "~/Scripts/js/moment/moment.js",
                "~/Scripts/js/moment/moment-with-locales.js",
                "~/Scripts/js/bootstrap/bootstrap.js",
                "~/Scripts/js/bootstrap-table/bootstrap-table.js",
                "~/Scripts/js/bootstrap-table/bootstrap-table-es-AR.js",
                "~/Scripts/js/select2/select2.js",
                "~/Scripts/js/select2/i18n/es.js",
                "~/Scripts/js/knockout/knockout-3.4.2.js",
                "~/Scripts/js/knockout-select2/knockout-select2.js",
                "~/Scripts/js/knockout-validation/knockout.validation.js",
                "~/Scripts/js/knockout-validation/es-ES.js",
                "~/Scripts/js/toastr/toastr.js",
                "~/Scripts/js/blockUI/jquery.blockUI.js",
                "~/Scripts/js/bootstrap-datetimepicker/bootstrap-datetimepicker.js",
                "~/Scripts/js/bootstrap-datetimepicker/ko-bindingHandlers-dateTimePicker.js",
                "~/Scripts/js/numeral/numeral.js",
                "~/Scripts/js/numeral/ko-bindingHandlers-numeral.js",
                "~/Scripts/js/custom/custom.js"));

            bundles.Add(new StyleBundle("~/plugins/css").Include(
                "~/Content/css/bootstrap/bootstrap.css",
                "~/Content/css/bootstrap-datetimepicker/bootstrap-datetimepicker.css",
                "~/Content/css/bootstrap-table/bootstrap-table.css",
                "~/Content/css/select2/select2.css",
                "~/Content/css/font-awesome/font-awesome.css",
                "~/Content/css/toastr/toastr.css",
                "~/Content/css/dashboard/dashboard.css",
                "~/Content/css/text-small-devices/text-small-devices.css",
                "~/Content/css/custom/custom.css"));

            bundles.Add(new ScriptBundle("~/app/architecture").Include(
                "~/Scripts/app/architecture/Common.js",
                "~/Scripts/app/models/filterModel.js"));

            bundles.Add(new ScriptBundle("~/app/user").Include(
                "~/Scripts/app/models/UserModel.js",
                "~/Scripts/app/controllers/UserController.js"));

            bundles.Add(new ScriptBundle("~/app/role").Include(
                "~/Scripts/app/models/RoleModel.js",
                "~/Scripts/app/controllers/RoleController.js"));

            bundles.Add(new ScriptBundle("~/app/product").Include(
                "~/Scripts/app/models/ProductModel.js",
                "~/Scripts/app/controllers/ProductController.js"));

            bundles.Add(new ScriptBundle("~/app/productType").Include(
                "~/Scripts/app/models/ProductTypeModel.js",
                "~/Scripts/app/controllers/ProductTypeController.js"));

            bundles.Add(new ScriptBundle("~/app/client").Include(
                "~/Scripts/app/models/ClientModel.js",
                "~/Scripts/app/controllers/ClientController.js"));

            bundles.Add(new ScriptBundle("~/app/order").Include(
                "~/Scripts/app/models/OrderModel.js",
                "~/Scripts/app/controllers/OrderController.js"));

            bundles.Add(new ScriptBundle("~/app/cashBox").Include(
                "~/Scripts/app/models/CashBoxModel.js",
                "~/Scripts/app/controllers/CashBoxController.js"));

            bundles.Add(new ScriptBundle("~/app/deliveryMan").Include(
            "~/Scripts/app/models/DeliveryManModel.js",
            "~/Scripts/app/controllers/DeliveryManController.js"));
        }
    }
}
