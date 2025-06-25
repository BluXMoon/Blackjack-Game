using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Blackjack
{
    public class DealerHandler : MonoBehaviour
    {
        [Header("Position/Parent of where new card will be spawned")]
        [SerializeField] private Transform cardSpawnParent;
        
        [Header("Card prefab")]
        [SerializeField] private GameObject cardPrefab;
        
        [Header("Score")] 
        [SerializeField] private TextMeshProUGUI scoreText;
        
        [Inject] private IBlackjackGameManager _blackjackGameManager;

        private List<CardHandler> _allCards = new();
        
        private int _cardCount;

        private void Awake()
        {
            _blackjackGameManager.OnDealerCardDrawn += DealerGetsNewCard;
            _blackjackGameManager.OnPlayerStands += PlayerStands;
            _blackjackGameManager.OnGameOver += OnGameOver;
            _blackjackGameManager.OnGameRestart += ClearAllCardsAndRestart;
        }

        private void OnDestroy()
        {
            _blackjackGameManager.OnDealerCardDrawn -= DealerGetsNewCard;
            _blackjackGameManager.OnPlayerStands -= PlayerStands;
            _blackjackGameManager.OnGameOver -= OnGameOver;
            _blackjackGameManager.OnGameRestart -= ClearAllCardsAndRestart;
        }

        private void DealerGetsNewCard(BlackjackCard card)
        {
            _cardCount++;
            var newCardGameObject = Instantiate(cardPrefab, cardSpawnParent);
            var getFaceDownCard = _cardCount == 2;

            if(!getFaceDownCard) scoreText.text = "Score: " + _blackjackGameManager.DealerScore; // Only update score when all cards are facing up!

            if (!newCardGameObject.TryGetComponent<CardHandler>(out var cardHandler)) return;

            cardHandler.SetCard(card, getFaceDownCard);
            _allCards.Add(cardHandler);
        }
        
        private void PlayerStands() => ShowAllCards();
        private void OnGameOver(GameOutcome outcome) => ShowAllCards();

        private void ShowAllCards()
        {
            if(_allCards.Count <= 0) return;
            
            _allCards.ForEach(c => c.RevealCard());
            scoreText.text = "Score: " + _blackjackGameManager.DealerScore;
        }
        
        private void ClearAllCardsAndRestart()
        {
            _allCards.ForEach(c => Destroy(c.gameObject));
            _allCards.Clear();
            _cardCount = 0;
        }
    }
}