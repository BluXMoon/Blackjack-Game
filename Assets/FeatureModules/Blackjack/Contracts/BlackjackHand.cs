using System.Collections.Generic;
using UnityEngine;

namespace Blackjack
{
    public class BlackjackHand
    {
        public List<BlackjackCard> Cards { get; private set; } = new();

        public void AddCard(BlackjackCard card) => Cards.Add(card);

        public int GetScore()
        {
            var total = 0;
            var aceCount = 0;

            foreach (var card in Cards)
            {
                var value = card.GetValue();
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
        
        public bool HasSoftAce()
        {
            var score = 0;
            var aceCount = 0;

            foreach (var card in Cards)
            {
                if (card.card.rank == CardRank.Ace)
                    aceCount++;
                else
                    score += Mathf.Min(card.GetValue(), 10);
            }

            return aceCount > 0 && score + 11 + (aceCount - 1) <= 21;
        }

        public bool IsBlackjack => Cards.Count == 2 && GetScore() == 21;
        public bool IsGameOver => GetScore() > 21;
    }
}