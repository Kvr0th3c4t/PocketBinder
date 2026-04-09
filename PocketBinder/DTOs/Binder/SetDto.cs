namespace PocketBinder.DTOs.Binder
{
    public class SetDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public int PrintedTotal { get; set; }
        public int Total { get; set; }
        public string ReleaseDate { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public string SymbolUrl { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
    }
}
