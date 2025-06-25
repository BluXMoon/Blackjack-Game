using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Blackjack
{
    public class DeckManager : MonoBehaviour, IDeckManager
    {
        [SerializeField] private List<Card> deck;
        
        private int _currentIndex;

        private void Awake() => Shuffle();

        private void Shuffle()
        {
            for (var i = 0; i < deck.Count; i++)
            {
                var randIndex = Random.Range(i, deck.Count);
                (deck[i], deck[randIndex]) = (deck[randIndex], deck[i]);
            }

            _currentIndex = 0;
        }

        public BlackjackCard DrawCard()
        {
            if (_currentIndex >= deck.Count)
            {
                Shuffle(); // Reshuffle when empty
            }

            return new BlackjackCard(deck[_currentIndex++]);
        }
    }
}
