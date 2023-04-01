using System.Numerics;

namespace BeerVendor.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public decimal AlcoholContent { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
