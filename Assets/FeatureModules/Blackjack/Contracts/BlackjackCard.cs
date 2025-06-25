using UnityEngine;

namespace Blackjack
{
    public class BlackjackCard
    {
        public Card card;

        public BlackjackCard(Card data) => card = data;

        public int GetValue(bool aceAsEleven = true)
        {
            return card.rank switch
            {
                CardRank.Ace => aceAsEleven ? 11 : 1,
                CardRank.King or CardRank.Queen or CardRank.Jack => 10,
                _ => (int)card.rank
            };
        }

        public Sprite GetSprite(bool faceDown = false) => 
            faceDown ? card.cardBackSprite : card.cardSprite;
    }
}