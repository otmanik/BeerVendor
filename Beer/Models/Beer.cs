using System.Numerics;

namespace BeerVendor.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AlcoholContent { get; set; }
        public decimal Price { get; set; }
        public int BreweryId { get; set; }
        public Brewery Brewery { get; set; }
    }
}
