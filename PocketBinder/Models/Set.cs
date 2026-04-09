namespace PocketBinder.Models
{
    public class Set
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Series { get; set; }
        public int PrintedTotal { get; set; }
        public int Total { get; set; }
        public string ReleaseDate { get; set; }
        public string SymbolUrl { get; set; }
        public string LogoUrl { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}
