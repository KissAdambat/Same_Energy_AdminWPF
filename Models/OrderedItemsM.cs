using System;
using System.Collections.Generic;
using System.Text;

namespace Same_Energy_AdminWPF.Models
{
    public class OrderedItemsM
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string size { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
    }
}
