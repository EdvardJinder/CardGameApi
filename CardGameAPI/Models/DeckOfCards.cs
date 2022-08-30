using System;
namespace CardGameAPI.Models
{
    public class DeckOfCards
    {
        #region cards List related
        protected const int MaxNrOfCards = 52;
        protected List<PlayingCard> cards = new List<PlayingCard>(MaxNrOfCards);
        #endregion

        public int Count => cards.Count();
        public PlayingCard DealOne()
        {
            PlayingCard card = cards[^1];
            cards.RemoveAt(cards.Count - 1);

            return card;
        }

        #region Shuffle and Sorting

        public void Shuffle()
        {
            var rand = new Random();
           

            for (int i = cards.Count - 1; i > 0; i--)
            {
                int n = rand.Next(i + 1);
                SwapPlayingCards(cards[i], cards[n]);
            }

        }

        private void SwapPlayingCards(PlayingCard card1, PlayingCard card2)
        {
            int pos1 = cards.IndexOf(card1);
            cards[cards.IndexOf(card2)] = card1;
            cards[pos1] = card2;
        }

        public void Sort() => cards.Sort();

        
        #endregion

        public DeckOfCards()
        {
            for (PlayingCardColor c = PlayingCardColor.Clubs; c <= PlayingCardColor.Spades; c++)
            {
                for (PlayingCardValue v = PlayingCardValue.Two; v <= PlayingCardValue.Ace; v++)
                {
                    cards.Add(new PlayingCard { Color = c, Value = v });
                }
            }
        }
    }
}

