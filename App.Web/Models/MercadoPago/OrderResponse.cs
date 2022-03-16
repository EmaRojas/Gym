using System;
using System.Collections.Generic;

namespace App.Web.Models.MercadoPago
{
    public class PayerOrder
    {
        public int id { get; set; }
        public string email { get; set; }
    }

    public class CollectorOrder
    {
        public int id { get; set; }
        public string email { get; set; }
        public string nickname { get; set; }
    }

    public class PaymentOrder
    {
        public long id { get; set; }
        public double transaction_amount { get; set; }
        public double total_paid_amount { get; set; }
        public int shipping_cost { get; set; }
        public string currency_id { get; set; }
        public string status { get; set; }
        public string status_detail { get; set; }
        public string operation_type { get; set; }
        public DateTime date_approved { get; set; }
        public DateTime date_created { get; set; }
        public DateTime last_modified { get; set; }
        public int amount_refunded { get; set; }
    }

    public class ItemOrder
    {
        public object category_id { get; set; }
        public string currency_id { get; set; }
        public object description { get; set; }
        public object id { get; set; }
        public object picture_url { get; set; }
        public int quantity { get; set; }
        public double unit_price { get; set; }
        public string title { get; set; }
    }

    public class OrderResponse
    {
        public int id { get; set; }
        public string preference_id { get; set; }
        public DateTime date_created { get; set; }
        public DateTime last_updated { get; set; }
        public object application_id { get; set; }
        public string status { get; set; }
        public string site_id { get; set; }
        public PayerOrder payer { get; set; }
        public CollectorOrder collector { get; set; }
        public object sponsor_id { get; set; }
        public List<PaymentOrder> payments { get; set; }
        public double paid_amount { get; set; }
        public int refunded_amount { get; set; }
        public int shipping_cost { get; set; }
        public bool cancelled { get; set; }
        public List<ItemOrder> items { get; set; }
        public string marketplace { get; set; }
        public List<object> shipments { get; set; }
        public object external_reference { get; set; }
        public object additional_info { get; set; }
        public string notification_url { get; set; }
        public double total_amount { get; set; }
    }
}