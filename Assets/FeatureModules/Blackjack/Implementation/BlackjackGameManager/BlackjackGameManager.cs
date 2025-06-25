using System;
using UnityEngine;
using Zenject;

namespace Blackjack
{
    public class BlackjackGameManager : MonoBehaviour, IBlackjackGameManager
    {
        private BlackjackHand _playerHand;
        private BlackjackHand _dealerHand;
        
        [Inject] private IDeckManager _deckManager;

        public Action<BlackjackCard> OnPlayerCardDrawn { get; set; }
        public Action<BlackjackCard> OnDealerCardDrawn { get; set; }

        private void Start() => StartGame();

        private void StartGame()
        {
            _playerHand = new BlackjackHand();
            _dealerHand = new BlackjackHand();

            // Initial 2 cards each
            DrawForPlayer();
            DrawForDealer();
            
            DrawForPlayer();
            DrawForDealer();

            CheckForBlackjack();
        }

        public void RestartGame() => StartGame();

        public void PlayerHits()
        {
            DrawForPlayer();

            if (_playerHand.IsGameOver)
                EndGame();
        }

        public void PlayerStands()
        {
            while (_dealerHand.GetValue() < 17)
            {
                DrawForDealer();
            }

            EndGame();
        }

        private void DrawForPlayer()
        {
            var card = _deckManager.DrawCard();
            _playerHand.AddCard(card);
            OnPlayerCardDrawn?.Invoke(card);
        }

        private void DrawForDealer()
        {
            var card = _deckManager.DrawCard();
            _dealerHand.AddCard(card);
            OnDealerCardDrawn?.Invoke(card);
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

        private void CheckForBlackjack()
        {
            if (_playerHand.IsBlackjack || _dealerHand.IsBlackjack)
            {
                EndGame();
            }
        }
    }
}