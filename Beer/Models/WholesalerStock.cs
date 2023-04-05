namespace BeerVendor.Models
{
    public class WholesalerStock
    {
        public int BeerId { get; set; }
        public int WholesalerId { get; set; }
        public int Quantity { get; set; }
        public Beer Beer { get; set; }
        public Wholesaler Wholesaler { get; set; }
    }
}
