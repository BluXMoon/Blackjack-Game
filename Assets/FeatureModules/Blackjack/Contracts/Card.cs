using UnityEngine;

namespace Blackjack
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Card")]
    public class Card : ScriptableObject
    {
        public CardSuit suit;
        public CardRank rank;
        public Sprite cardSprite;
        public Sprite cardBackSprite;
    
        public int Value => rank switch
        {
            CardRank.Ace => 11,
            CardRank.King or CardRank.Queen or CardRank.Jack => 10,
            _ => (int)rank
        };
    }
}
