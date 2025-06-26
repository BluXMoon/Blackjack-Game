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
    }
}
