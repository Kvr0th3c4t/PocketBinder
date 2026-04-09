namespace PocketBinder.DTOs.Sync.SyncSet
{
    public class SyncSetDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Series { get; set; }
        public int PrintedTotal { get; set; }
        public int Total { get; set; }
        public string ReleaseDate { get; set; }
        public SyncSetImagesDto Images { get; set; }
    }
}
