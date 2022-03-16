using System;
using System.Collections.Generic;

namespace App.Web.Models.MercadoPago
{
    public class CardholderPayment
    {
        public IdentificationMP identification { get; set; }
        public string name { get; set; }
    }

    public class CardPayment
    {
        public CardholderPayment cardholder { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_last_updated { get; set; }
        public int expiration_month { get; set; }
        public int expiration_year { get; set; }
        public string first_six_digits { get; set; }
        public string id { get; set; }
        public string last_four_digits { get; set; }
    }

    public class OrderPayment
    {
        public string id { get; set; }
        public string type { get; set; }
    }

    public class PhonePayment
    {
        public string area_code { get; set; }
        public object extension { get; set; }
        public string number { get; set; }
    }

    public class PayerPayment
    {
        public string email { get; set; }
        public object entity_type { get; set; }
        public string first_name { get; set; }
        public string id { get; set; }
        public IdentificationMP identification { get; set; }
        public string last_name { get; set; }
        public object operator_id { get; set; }
        public PhonePayment phone { get; set; }
        public string type { get; set; }
    }

    public class TransactionDetailsPayment
    {
        public object acquirer_reference { get; set; }
        public object external_resource_url { get; set; }
        public object financial_institution { get; set; }
        public double installment_amount { get; set; }
        public double net_received_amount { get; set; }
        public int overpaid_amount { get; set; }
        public object payable_deferral_period { get; set; }
        public object payment_method_reference_id { get; set; }
        public double total_paid_amount { get; set; }
    }

    public class PaymentResponse
    {
        public object acquirer { get; set; }
        public List<object> acquirer_reconciliation { get; set; }
        public string authorization_code { get; set; }
        public bool binary_mode { get; set; }
        public object call_for_authorize_id { get; set; }
        public bool captured { get; set; }
        public CardPayment card { get; set; }
        public int collector_id { get; set; }
        public object counter_currency { get; set; }
        public int coupon_amount { get; set; }
        public string currency_id { get; set; }
        public DateTime date_approved { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_last_updated { get; set; }
        public object date_of_expiration { get; set; }
        public object deduction_schema { get; set; }
        public string description { get; set; }
        public object differential_pricing_id { get; set; }
        public object external_reference { get; set; }
        public List<object> fee_details { get; set; }
        public long id { get; set; }
        public int installments { get; set; }
        public string issuer_id { get; set; }
        public bool live_mode { get; set; }
        public object merchant_account_id { get; set; }
        public object merchant_number { get; set; }
        public DateTime money_release_date { get; set; }
        public object money_release_schema { get; set; }
        public string notification_url { get; set; }
        public string operation_type { get; set; }
        public OrderPayment order { get; set; }
        public PayerPayment payer { get; set; }
        public string payment_method_id { get; set; }
        public string payment_type_id { get; set; }
        public string processing_mode { get; set; }
        public List<object> refunds { get; set; }
        public int shipping_amount { get; set; }
        public object sponsor_id { get; set; }
        public string statement_descriptor { get; set; }
        public string status { get; set; }
        public string status_detail { get; set; }
        public int taxes_amount { get; set; }
        public double transaction_amount { get; set; }
        public int transaction_amount_refunded { get; set; }
        public TransactionDetailsPayment transaction_details { get; set; }
    }
}