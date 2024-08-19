using LoadCallCenterOrderData.Modal;
using LoadCallCenterOrderData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadCallCenterOrderData.Utils
{
    public class DBUtils
    {
        public void InsertData(List<Order> orders)
        {
            EmailUtils emailUtils = new EmailUtils();
            AppSettinsUtils appSettinsUtils = new AppSettinsUtils();

            bool isOrderProcessed = Convert.ToBoolean(appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.IsOrderProcessed));

            try
            {
                MCMEntities entities = new MCMEntities();

                GM_Cuore_orders gmOrder = new GM_Cuore_orders();

                foreach (var order in orders)
                {
                    gmOrder.access_key = string.Empty;
                    gmOrder.orderid = Convert.ToInt32(order.CuoreOrderNo);
                    gmOrder.userid = Convert.ToInt32(order.UserId);
                    gmOrder.ams_code = order.UseCode;
                    gmOrder.b_address = order.BillingCustomer.Street;
                    gmOrder.b_city = order.BillingCustomer.City;
                    gmOrder.b_country = order.BillingCustomer.Country;
                    gmOrder.b_county = string.Empty;
                    gmOrder.b_fax = string.Empty;
                    gmOrder.b_firstname = order.BillingCustomer.FirstName;
                    gmOrder.b_lastname = order.BillingCustomer.LastName;
                    gmOrder.b_phone = order.BillingCustomer.DayPhone;
                    gmOrder.b_state = order.BillingCustomer.State;
                    gmOrder.b_title = order.BillingCustomer.Title;
                    gmOrder.b_zip4 = string.Empty;
                    gmOrder.b_zipcode = order.BillingCustomer.ZipCode;
                    gmOrder.clickid = 0;
                    gmOrder.CC_type = order.CreditCard.CCType;
                    gmOrder.CC_card_no = order.CreditCard.CCCardNumber;
                    gmOrder.CC_exp_year = order.CreditCard.CCExpiryYear;
                    gmOrder.CC_exp_month = order.CreditCard.CCExpiryMonth;
                    gmOrder.company = order.Customer.CompanyName;
                    gmOrder.coupon = string.Empty;
                    gmOrder.coupon_discount = 0.00M;
                    gmOrder.customer = string.Empty;
                    gmOrder.customer_notes = string.Empty;
                    gmOrder.date = order.Date;
                    gmOrder.deposit_paid = 0.00M;
                    gmOrder.deposit_required = 0.00M;
                    gmOrder.details = string.Empty;
                    gmOrder.discount = 0.00M;
                    gmOrder.email = order.Customer.EmailAddress;
                    gmOrder.extra = string.Empty;
                    gmOrder.firstname = order.Customer.FirstName;
                    gmOrder.flag = order.Flag;
                    gmOrder.fraud_score = "N/A";
                    gmOrder.giftcert_discount = 0.00M;
                    gmOrder.giftcert_ids = string.Empty;
                    gmOrder.go2mcm = 0;
                    gmOrder.init_total = order.Amount;
                    gmOrder.language = "en";
                    gmOrder.lastname = order.Customer.LastName;
                    gmOrder.membership = string.Empty;
                    gmOrder.membershipid = 0;
                    gmOrder.notes = string.Empty;
                    //gmOrder.orderid
                    gmOrder.paymentid = 28;
                    gmOrder.payment_method = order.PayMethod;
                    gmOrder.payment_surcharge = 0.00M;
                    //gmOrder.print_time
                    gmOrder.problem = 0;
                    gmOrder.referral_campaign = string.Empty;
                    gmOrder.release_time = order.ReleaseDate;
                    gmOrder.shipping = string.Empty;
                    gmOrder.shippingid = 0;
                    gmOrder.shipping_cost = order.ShippingCost;
                    gmOrder.status = order.Status;
                    gmOrder.subtotal = order.Amount;
                    gmOrder.s_address = order.ShippingCustomer.Street;
                    gmOrder.s_city = order.ShippingCustomer.City;
                    gmOrder.s_country = order.ShippingCustomer.Country;
                    gmOrder.s_county = string.Empty;
                    gmOrder.s_fax = string.Empty;
                    gmOrder.s_firstname = order.ShippingCustomer.FirstName;
                    gmOrder.s_lastname = order.ShippingCustomer.LastName;
                    gmOrder.s_phone = order.ShippingCustomer.DayPhone;
                    gmOrder.s_state = order.ShippingCustomer.State;
                    gmOrder.s_title = order.ShippingCustomer.Title;
                    gmOrder.s_zip4 = string.Empty;
                    gmOrder.s_zipcode = order.ShippingCustomer.ZipCode;
                    gmOrder.taxes_applied = 0.00M;
                    gmOrder.tax_number = string.Empty;
                    gmOrder.title = string.Empty;
                    gmOrder.total = order.Amount;
                    gmOrder.tracking = string.Empty;
                    gmOrder.url = string.Empty;
                    gmOrder.verification_hold = "N";
                    gmOrder.wcm_fraud_flag = "0";
                    gmOrder.processed = isOrderProcessed;
                    gmOrder.created_by = "CallCenterApp";
                    gmOrder.created_datetime = DateTime.Now;

                    entities.GM_Cuore_orders.Add(gmOrder);
                    entities.SaveChanges();

                    foreach (var item in order.Items)
                    {
                        GM_Cuore_Order_Details gmOrderDetail = new GM_Cuore_Order_Details();
                        gmOrderDetail.amount = item.Quantity;
                        gmOrderDetail.date = Convert.ToInt32(item.Date);
                        gmOrderDetail.itemid = item.ItemNo;
                        gmOrderDetail.orderid = (int)gmOrder.orderid;
                        gmOrderDetail.price = item.ItemPrice;
                        gmOrderDetail.product = item.Product;
                        gmOrderDetail.productcode = item.ItemNo.ToString();
                        gmOrderDetail.productid = item.ItemNo;
                        gmOrderDetail.product_options = string.Empty;
                        gmOrderDetail.provider = 0;

                        entities.GM_Cuore_Order_Details.Add(gmOrderDetail);
                        entities.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                string subject = "Error in Application LoadCallCenterOrderData.Utils.InsertData";
                string body = string.Format("There was an error in the application: {0}", ex.ToString());

                emailUtils.SendMail(subject, body);
            }            
        }

        public void CleanCCData()
        {
            MCMEntities entities = new MCMEntities();
            ECOMEntities ecomEntities = new ECOMEntities();

            var orders = entities.GM_Cuore_orders.Where(r => r.processed == true && r.CC_card_no != null).ToList();

            foreach(var order in orders)
            {
                string orderNumber = order.orderid.ToString().PadLeft(7, '0');
                orderNumber = string.Format("C{0}", orderNumber);

                var ecomOrder = ecomEntities.ORDERHEADERs.Where(r => r.FULLORDERNO.Contains(orderNumber)).FirstOrDefault();

                if(ecomOrder != null)
                {
                    order.CC_card_no = null;
                }
            }

            entities.SaveChanges();
        }
    }
}
