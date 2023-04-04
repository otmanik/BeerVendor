namespace BeerVendor.Models
{
    public class QuoteDto
    {
        public double Price { get; set; }
        public string Summary { get; set; }
    }

    public class QuoteRequestDto
    {
        public int WholesalerId { get; set; }
        public int BeerId { get; set; }
        public int Quantity { get; set; }
    }
}
