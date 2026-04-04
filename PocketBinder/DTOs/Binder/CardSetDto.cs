namespace PocketBinder.DTOs.Binder
{
    public class CardSetDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Series { get; set; }
        public int PrintedTotal { get; set; }
        public int Total { get; set; }
        public string ReleaseDate { get; set; }
        public string UpdatedAt { get; set; }
        public SetImageDto Images { get; set; }
    }
}
