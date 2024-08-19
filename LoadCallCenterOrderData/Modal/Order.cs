using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadCallCenterOrderData.Modal
{
    public class Order
    {
        public string OrderNoPrefix { get; set; }
        public string UserId { get; set; }
        public string CuoreOrderNo { get; set; }
        public string CuoreOrderEntryLoginID { get; set; }
        public string OfferCode { get; set; }
        public string UseCode { get; set; }
        public string MailDate { get; set; }
        public string PayMethod { get; set; }
        public decimal Amount { get; set; }
        public int Date { get; set; }
        public DateTime OrderDate { get; set; }
        public int ReleaseDate { get; set; }
        public string Status { get; set; }
        public string Flag { get; set; }
        public decimal ShippingCost { get; set; }
        public Customer Customer { get; set; }
        public Customer BillingCustomer { get; set; }
        public Customer ShippingCustomer { get; set; }
        public CreditCard CreditCard { get; set; }
        public List<Item> Items { get; set; }
    }
}
