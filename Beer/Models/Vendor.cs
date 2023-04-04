﻿namespace BeerVendor.Models
{
    public class Wholesaler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<WholesalerStock> Stocks { get; set; }
    }
}
