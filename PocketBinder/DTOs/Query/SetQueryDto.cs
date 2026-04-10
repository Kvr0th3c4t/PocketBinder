namespace PocketBinder.DTOs.Query

{
    public class SetQueryDto : PagedQueryDto
    {
        public string Name { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;

    }
}
