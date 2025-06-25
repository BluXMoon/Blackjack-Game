using System;
using Phases;
using UnityEngine;
using Zenject;

namespace Blackjack
{
    public class BlackjackGameManager : MonoBehaviour, IBlackjackGameManager
    {
        [SerializeField] private Phase gamePhase;
        [SerializeField] private Phase gameOverPhase;
        
        private BlackjackHand _playerHand;
        private BlackjackHand _dealerHand;
        
        [Inject] private IDeckManager _deckManager;
        [Inject] private IPhaseSetter _phaseSetter;

        public Action<BlackjackCard> OnPlayerCardDrawn { get; set; }
        public Action OnPlayerStands { get; set; }
        public Action<BlackjackCard> OnDealerCardDrawn { get; set; }
        public Action<GameOutcome> OnGameOver { get; set; }
        public Action OnGameRestart { get; set; }
        public int PlayerScore => _playerHand.GetScore();
        public int DealerScore => _dealerHand.GetScore();

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

        public void RestartGame()
        {
            _phaseSetter.SetPhase(gamePhase);
            OnGameRestart?.Invoke();
            StartGame();
        }

        public void PlayerHits()
        {
            DrawForPlayer();

            if (_playerHand.IsGameOver)
                EndGame();
        }

        public void PlayerStands()
        {
            OnPlayerStands?.Invoke();
            
            while (_dealerHand.GetScore() < 17)
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
            var player = _playerHand.GetScore();
            var dealer = _dealerHand.GetScore();

            var result = GameOutcome.Draw;
            
            if (player > 21) result = GameOutcome.PlayerLost;
            else if (dealer > 21) result = GameOutcome.DealerLost;
            else if (player > dealer) result = GameOutcome.PlayerWon;
            else if (dealer > player) result = GameOutcome.DealerWon;
            
            OnGameOver?.Invoke(result);
            _phaseSetter.SetPhase(gameOverPhase);
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