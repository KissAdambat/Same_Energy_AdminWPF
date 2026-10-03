using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Same_Energy_AdminWPF.Models
{
    public class ProductsM
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string size { get; set; }
        public int stock { get; set; }
        public string image { get; set; }
        public string category { get; set; }
    }
}
