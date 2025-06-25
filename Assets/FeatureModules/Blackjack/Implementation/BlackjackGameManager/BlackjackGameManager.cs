using UnityEngine;
using Zenject;

namespace Blackjack
{
    public class BlackjackGameManager : MonoBehaviour, IBlackjackGameManager
    {
        private BlackjackHand _playerHand;
        private BlackjackHand _dealerHand;
        
        [Inject] private IDeckManager _deckManager;

        private void Start() => StartGame();

        private void StartGame()
        {
            _playerHand = new BlackjackHand();
            _dealerHand = new BlackjackHand();

            // Initial 2 cards each
            _playerHand.AddCard(_deckManager.DrawCard());
            _dealerHand.AddCard(_deckManager.DrawCard());
            
            _playerHand.AddCard(_deckManager.DrawCard());
            _dealerHand.AddCard(_deckManager.DrawCard());

            ShowHands();
            CheckForBlackjack();
        }

        public void RestartGame() => StartGame();

        public void PlayerHits()
        {
            _playerHand.AddCard(_deckManager.DrawCard());
            ShowHands();

            if (_playerHand.IsGameOver)
                EndGame();
        }

        public void PlayerStands()
        {
            while (_dealerHand.GetValue() < 17)
            {
                _dealerHand.AddCard(_deckManager.DrawCard());
            }

            ShowHands();
            EndGame();
        }

        private void EndGame()
        {
            var player = _playerHand.GetValue();
            var dealer = _dealerHand.GetValue();

            var result = "Draw!";
            
            if (player > 21) result = "Player Busts!";
            else if (dealer > 21) result = "Dealer Busts!";
            else if (player > dealer) result = "Player Wins!";
            else if (dealer > player) result = "Dealer Wins!";

            Debug.Log(result);
        }

        private void ShowHands()
        {
            Debug.Log($"Player: {_playerHand.GetValue()}");
            Debug.Log($"Dealer: {_dealerHand.GetValue()}");
        }

        private void CheckForBlackjack()
        {
            if (_playerHand.IsBlackjack || _dealerHand.IsBlackjack)
            {
                EndGame();
            }
        }
    }
}