namespace PocketBinder.DTOs.Binder
{
    public class CardSummaryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CardSetDto Set { get; set; }
        public string Number { get; set; }
        public string Rarity { get; set; }
        public ImageDto Images { get; set; }
    }
}
