using LoadCallCenterOrderData.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadCallCenterOrderData.Utils
{
    public class OrderUtils
    {
        public List<Order> ProcessFile(List<ImportOrderFile> importOrderItems)
        {
            List<Order> orders = new List<Order>();
            EmailUtils emailUtils = new EmailUtils();

            try
            {
                importOrderItems = importOrderItems.Skip(1).ToList();

                foreach (var importOrderItem in importOrderItems)
                {
                    Order order = orders.Where(r => r.CuoreOrderNo == importOrderItem.orderid).FirstOrDefault();

                    if (order == null)
                    {
                        order = new Order();
                        orders.Add(order);
                        order.Amount = ConvertToDecimal(importOrderItem.total);
                        order.CuoreOrderEntryLoginID = importOrderItem.userid;
                        order.CuoreOrderNo = importOrderItem.orderid;
                        order.UserId = importOrderItem.userid;
                        order.Date = importOrderItem.date;
                        order.OrderDate = importOrderItem.order_date;
                        order.ReleaseDate = importOrderItem.release_time;
                        order.Status = importOrderItem.status;
                        order.Flag = importOrderItem.flag;
                        //order.EnteredDate = importOrderItem.EnteredDate;
                        //order.EnteredTime = importOrderItem.EnteredTime;
                        //order.MailDate = importOrderItem.MailDate;
                        //order.OfferCode = importOrderItem.OfferCode;
                        if (string.IsNullOrEmpty(importOrderItem.ams_code))
                        {
                            order.UseCode = "TV242";
                        }
                        else
                        {
                            order.UseCode = importOrderItem.ams_code;
                        }
                        //order.OrderNoPrefix = importOrderItem.OrderNoPrefix;
                        order.PayMethod = importOrderItem.payment_method;
                        order.ShippingCost = ConvertToDecimal(importOrderItem.shipping);
                        //order.UseCode = importOrderItem.UseCode;

                        order.Customer = new Customer();
                        //order.Customer.Address1 = importOrderItem.Address1;
                        //order.Customer.Address2 = importOrderItem.Address2;
                        //order.Customer.City = importOrderItem.City;
                        order.Customer.CompanyName = importOrderItem.company;
                        //order.Customer.Country = importOrderItem.Country;
                        //order.Customer.DayPhone = importOrderItem.DayPhone;
                        order.Customer.EmailAddress = importOrderItem.email;
                        order.Customer.FirstName = importOrderItem.firstname;
                        order.Customer.LastName = importOrderItem.lastname;
                        //order.Customer.MiddleName = importOrderItem.MiddleName;
                        //order.Customer.NightPhone = importOrderItem.NightPhone;
                        //order.Customer.State = importOrderItem.State;
                        //order.Customer.Street = importOrderItem.Street;
                        //order.Customer.Title = importOrderItem.Title;
                        //order.Customer.ZipCode = importOrderItem.ZipCode;

                        order.BillingCustomer = new Customer();
                        //order.BillingCustomer.Address1 = importOrderItem.b_address;
                        //order.BillingCustomer.Address2 = importOrderItem.BillingAddress2;
                        order.BillingCustomer.City = importOrderItem.b_city;
                        order.BillingCustomer.CompanyName = importOrderItem.company;
                        order.BillingCustomer.Country = importOrderItem.b_country;
                        order.BillingCustomer.DayPhone = importOrderItem.b_phone;
                        order.BillingCustomer.EmailAddress = importOrderItem.email;
                        order.BillingCustomer.FirstName = importOrderItem.b_firstname;
                        order.BillingCustomer.LastName = importOrderItem.b_lastname;
                        //order.BillingCustomer.MiddleName = importOrderItem.BillingMiddleName;
                        order.BillingCustomer.NightPhone = importOrderItem.b_phone;
                        order.BillingCustomer.State = importOrderItem.b_state;
                        order.BillingCustomer.Street = importOrderItem.b_address;
                        order.BillingCustomer.Title = importOrderItem.b_title;
                        order.BillingCustomer.ZipCode = importOrderItem.b_zipcode;

                        order.ShippingCustomer = new Customer();
                        //order.ShippingCustomer.Address1 = importOrderItem.ShippingAddress1;
                        //order.ShippingCustomer.Address2 = importOrderItem.ShippingAddress2;
                        order.ShippingCustomer.City = importOrderItem.s_city;
                        order.ShippingCustomer.CompanyName = importOrderItem.company;
                        order.ShippingCustomer.Country = importOrderItem.s_country;
                        order.ShippingCustomer.DayPhone = importOrderItem.s_phone;
                        order.ShippingCustomer.EmailAddress = importOrderItem.email;
                        order.ShippingCustomer.FirstName = importOrderItem.s_firstname;
                        order.ShippingCustomer.LastName = importOrderItem.s_lastname;
                        //order.ShippingCustomer.MiddleName = importOrderItem.ShippingMiddleName;
                        order.ShippingCustomer.NightPhone = importOrderItem.s_phone;
                        order.ShippingCustomer.State = importOrderItem.s_state;
                        order.ShippingCustomer.Street = importOrderItem.s_address;
                        order.ShippingCustomer.Title = importOrderItem.s_title;
                        order.ShippingCustomer.ZipCode = importOrderItem.s_zipcode;

                        order.CreditCard = new CreditCard();
                        order.CreditCard.CCCardNumber = importOrderItem.CC_card_no;
                        order.CreditCard.CCExpiryMonth = importOrderItem.CC_exp_month;
                        order.CreditCard.CCExpiryYear = importOrderItem.CC_exp_year;
                        order.CreditCard.CCType = importOrderItem.CC_type;

                        order.Items = new List<Item>();
                    }

                    Item item = new Item();
                    order.Items.Add(item);
                    item.ItemNo = ConvertToInt(importOrderItem.itemid);
                    item.ItemPrice = ConvertToDecimal(importOrderItem.price);
                    item.Quantity = ConvertToInt(importOrderItem.amount);
                    item.Product = importOrderItem.product;
                    item.Date = order.OrderDate.ToString("yyyyMMdd");
                    //item.Quantity = ConvertToInt(importOrderItem.Quantity);
                    //item.TaxAmount = importOrderItem.TaxAmount;
                }
            }
            catch (Exception ex)
            {
                string subject = "Error in Application LoadCallCenterOrderData.Utils.ProcessFile";
                string body = string.Format("There was an error in the application: {0}", ex.ToString());

                emailUtils.SendMail(subject, body);
            }

            return orders;
        }

        private int ConvertToInt(string stringValue)
        {
            int returnValue = 0;

            try
            {
                if (!string.IsNullOrEmpty(stringValue))
                {
                    returnValue = Convert.ToInt32(stringValue);
                }
            }
            catch (Exception)
            {

            }

            return returnValue;
        }

        private decimal ConvertToDecimal(string stringValue)
        {
            decimal returnValue = 0;

            try
            {
                if (!string.IsNullOrEmpty(stringValue))
                {
                    returnValue = Convert.ToDecimal(stringValue);// / 100;
                }
            }
            catch (Exception)
            {

            }

            return returnValue;
        }
    }
}
