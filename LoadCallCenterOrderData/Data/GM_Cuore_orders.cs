//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoadCallCenterOrderData.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class GM_Cuore_orders
    {
        public long id { get; set; }
        public int orderid { get; set; }
        public Nullable<int> userid { get; set; }
        public string membership { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<decimal> giftcert_discount { get; set; }
        public string giftcert_ids { get; set; }
        public decimal subtotal { get; set; }
        public Nullable<decimal> discount { get; set; }
        public string coupon { get; set; }
        public Nullable<decimal> coupon_discount { get; set; }
        public Nullable<int> shippingid { get; set; }
        public string shipping { get; set; }
        public string tracking { get; set; }
        public Nullable<decimal> shipping_cost { get; set; }
        public Nullable<decimal> taxes_applied { get; set; }
        public int date { get; set; }
        public string status { get; set; }
        public string payment_method { get; set; }
        public string flag { get; set; }
        public string notes { get; set; }
        public string details { get; set; }
        public string customer_notes { get; set; }
        public string customer { get; set; }
        public string title { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string company { get; set; }
        public string b_title { get; set; }
        public string b_firstname { get; set; }
        public string b_lastname { get; set; }
        public string b_address { get; set; }
        public string b_city { get; set; }
        public string b_county { get; set; }
        public string b_state { get; set; }
        public string b_country { get; set; }
        public string b_zipcode { get; set; }
        public string b_zip4 { get; set; }
        public string b_phone { get; set; }
        public string b_fax { get; set; }
        public string s_title { get; set; }
        public string s_firstname { get; set; }
        public string s_lastname { get; set; }
        public string s_address { get; set; }
        public string s_city { get; set; }
        public string s_county { get; set; }
        public string s_state { get; set; }
        public string s_country { get; set; }
        public string s_zipcode { get; set; }
        public string s_phone { get; set; }
        public string s_fax { get; set; }
        public string s_zip4 { get; set; }
        public string CC_type { get; set; }
        public string CC_card_no { get; set; }
        public string CC_exp_year { get; set; }
        public string CC_exp_month { get; set; }
        public string url { get; set; }
        public string email { get; set; }
        public string language { get; set; }
        public Nullable<int> clickid { get; set; }
        public string extra { get; set; }
        public Nullable<int> membershipid { get; set; }
        public Nullable<int> paymentid { get; set; }
        public Nullable<decimal> payment_surcharge { get; set; }
        public string tax_number { get; set; }
        public Nullable<decimal> init_total { get; set; }
        public string access_key { get; set; }
        public Nullable<decimal> deposit_required { get; set; }
        public Nullable<decimal> deposit_paid { get; set; }
        public Nullable<int> go2mcm { get; set; }
        public Nullable<int> problem { get; set; }
        public string wcm_fraud_flag { get; set; }
        public string fraud_score { get; set; }
        public string referral_campaign { get; set; }
        public string ams_code { get; set; }
        public Nullable<decimal> print_time { get; set; }
        public string verification_hold { get; set; }
        public Nullable<int> release_time { get; set; }
        public bool processed { get; set; }
        public string created_by { get; set; }
        public Nullable<System.DateTime> created_datetime { get; set; }
        public Nullable<System.DateTime> processed_datetime { get; set; }
    }
}
