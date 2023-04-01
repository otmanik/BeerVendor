namespace BeerVendor.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<Beer> Beers { get; set; }
    }
}
