namespace PocketBinder.DTOs.Binder
{
    public class CardDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CardSetDto Set { get; set; }
        public string Number { get; set; }
        public string Rarity { get; set; }
        public ImageDto Images { get; set; }

        public string Supertype { get; set; }
        public List<string> Subtypes { get; set; }
        public List<string> Types { get; set; }

        public string Artist { get; set; }
        public CardMarketDto CardMarket { get; set; }
        public TcgPlayerDto TcgPlayer { get; set; }
    }
}
