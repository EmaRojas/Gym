using System;
using System.Collections.Generic;

namespace App.Web.Models.MercadoPago
{
    public class BackUrlsPreference
    {
        public string failure { get; set; }
        public string pending { get; set; }
        public string success { get; set; }
    }

    public class ItemPreference
    {
        public string id { get; set; }
        public string currency_id { get; set; }
        public string title { get; set; }
        public string picture_url { get; set; }
        public string description { get; set; }
        public string category_id { get; set; }
        public int quantity { get; set; }
        public double unit_price { get; set; }
    }

    public class PhonePreference
    {
        public string area_code { get; set; }
        public string number { get; set; }
    }

    public class AddressPreference
    {
        public string zip_code { get; set; }
        public string street_name { get; set; }
        public object street_number { get; set; }
    }

    public class PayerPreference
    {
        public PayerPreference() { }

        public PhonePreference phone { get; set; }
        public AddressPreference address { get; set; }
        public string email { get; set; }
        public IdentificationMP identification { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string date_created { get; set; }
    }

    public class ExcludedPaymentMethodPreference
    {
        public string id { get; set; }
    }

    public class ExcludedPaymentTypePreference
    {
        public string id { get; set; }
    }

    public class PaymentMethodsPreference
    {
        public List<ExcludedPaymentMethodPreference> excluded_payment_methods { get; set; }
        public object default_installments { get; set; }
        public object default_payment_method_id { get; set; }
        public List<ExcludedPaymentTypePreference> excluded_payment_types { get; set; }
        public object installments { get; set; }
    }

    public class ReceiverAddressPreference
    {
        public string floor { get; set; }
        public string zip_code { get; set; }
        public string street_name { get; set; }
        public string apartment { get; set; }
        public object street_number { get; set; }
    }

    public class PreferenceResponse
    {
        public PreferenceResponse() { }

        public string additional_info { get; set; }
        public string auto_return { get; set; }
        public BackUrlsPreference back_urls { get; set; }
        public bool binary_mode { get; set; }
        public string client_id { get; set; }
        public int collector_id { get; set; }
        public DateTime date_created { get; set; }
        public object expiration_date_from { get; set; }
        public object expiration_date_to { get; set; }
        public bool expires { get; set; }
        public string external_reference { get; set; }
        public string id { get; set; }
        public string init_point { get; set; }
        public List<ItemPreference> items { get; set; }
        public string marketplace { get; set; }
        public int marketplace_fee { get; set; }
        public string notification_url { get; set; }
        public string operation_type { get; set; }
        public PayerPreference payer { get; set; }
        public PaymentMethodsPreference payment_methods { get; set; }
        public string sandbox_init_point { get; set; }
    }
}