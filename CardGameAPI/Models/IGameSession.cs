namespace CardGameAPI.Models
{
    public interface IGameSession
    {
        public Guid SessionId { get; set; }
        public bool IsRunning { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DeckOfCards Deck { get; set; }
        public PlayingCard CurrentCard { get; set; }
        public int score { get; set; }

        public bool StartGame();
        public bool EndGame();
    }
}