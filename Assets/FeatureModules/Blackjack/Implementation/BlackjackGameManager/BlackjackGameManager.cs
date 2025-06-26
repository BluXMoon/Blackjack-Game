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
        [Inject] private IDealerRuleFactory _dealerRuleFactory;

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

            CheckForPlayerBlackJack();
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
            if (DealerHasBlackjack())
            {
                EndGame();
                return;
            }
            
            var rules = _dealerRuleFactory.GetRulesFor(_dealerHand);
            var compositeRule = new CompositeDealerRule();
            rules.ForEach(r => compositeRule.AddRule(r));

            while (compositeRule.ShouldDrawCard(_dealerHand, _playerHand) && _dealerHand.GetScore() <= 21)
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
            
            switch (_playerHand.IsBlackjack)
            {
                case true when _dealerHand.IsBlackjack:
                    result = GameOutcome.Draw;
                    break;
                case true:
                    result = GameOutcome.PlayerBlackjack;
                    break;
                default:
                {
                    if(_dealerHand.IsBlackjack) result = GameOutcome.DealerBlackjack;
                    else if (player > 21) result = GameOutcome.PlayerLost;
                    else if (dealer > 21) result = GameOutcome.DealerLost;
                    else if (player > dealer) result = GameOutcome.PlayerWon;
                    else if (dealer > player) result = GameOutcome.DealerWon;
                    break;
                }
            }
            
            OnGameOver?.Invoke(result);
            _phaseSetter.SetPhase(gameOverPhase);
        }

        private void CheckForPlayerBlackJack()
        {
            switch (_playerHand.IsBlackjack)
            {
                case false:
                    return;
                case true when _dealerHand.IsBlackjack:
                default:
                    EndGame();
                    return;
            }
        }

        private bool DealerHasBlackjack() => _dealerHand.IsBlackjack;
    }
}