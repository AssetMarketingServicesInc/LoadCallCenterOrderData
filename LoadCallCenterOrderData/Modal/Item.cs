using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadCallCenterOrderData.Modal
{
    public class Item
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public int ItemNo { get; set; }
        public string TaxAmount { get; set; }
        public decimal ItemPrice { get; set; }
        public string Date { get; set; }
    }
}
