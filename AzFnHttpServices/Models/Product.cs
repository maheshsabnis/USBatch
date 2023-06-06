using System;
using System.Collections.Generic;

namespace AzFnHttpServices.Models
{
    public partial class Product
    {
        public int ProductRowId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Manufacturere { get; set; }
        public int Price { get; set; }
    }
}
