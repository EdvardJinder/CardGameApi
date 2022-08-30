using CardGameAPI.Data;
using System;
namespace CardGameAPI.Models
{
	public class GameSession : IGameSession
	{


        
        public Guid SessionId { get; set; }
        public bool IsRunning { get; set; } = false;

        public string Name { get; set; }
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public DeckOfCards Deck { get; set; } = null;
        public PlayingCard CurrentCard { get; set; } = null;
        public int score { get; set; }

        public bool StartGame(string name)
        { 
            if (!IsRunning)
            {
                Name = name;
                SessionId = Guid.NewGuid();
                IsRunning = true;
                StartTime = DateTime.Now;
                Deck = new();
                for(int i = 0; i < 3; i++)
                {
                    Deck.Shuffle();
                }

                score = 0;
                return true;
            }

            return false;
        }

        public bool EndGame()
        {
            if (IsRunning)
            {
                IsRunning = false;
                EndTime = DateTime.Now;
                return true;
            }

            return false;
        }
       

      

    }
}

