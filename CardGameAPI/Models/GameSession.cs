﻿using System;
namespace CardGameAPI.Models
{
	public class GameSession : IGameSession
	{
        public Guid SessionId { get; set; }
        public bool IsRunning { get; set; } = false;
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public DeckOfCards Deck { get; set; } = null;
        public PlayingCard CurrentCard { get; set; } = null;
        public int score { get; set; }

        public bool StartGame()
        { 
            if (!IsRunning)
            {
                SessionId = Guid.NewGuid();
                IsRunning = true;
                StartTime = DateTime.Now;
                Deck = new();
                Deck.Shuffle();
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
