using System.Collections.Generic;

namespace Blackjack
{
    public class BlackjackHand
    {
        public List<BlackjackCard> Cards { get; private set; } = new();

        public void AddCard(BlackjackCard card) => Cards.Add(card);

        public int GetValue()
        {
            int total = 0;
            int aceCount = 0;

            foreach (var card in Cards)
            {
                int value = card.GetValue();
                total += value;

                if (card.card.rank == CardRank.Ace)
                    aceCount++;
            }

            while (total > 21 && aceCount > 0)
            {
                total -= 10; // Convert Ace from 11 to 1
                aceCount--;
            }

            return total;
        }

        public bool IsBlackjack => Cards.Count == 2 && GetValue() == 21;
        public bool IsGameOver => GetValue() > 21;
    }
}