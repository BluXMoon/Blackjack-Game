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

        private List<CardHandler> _spawnedCards = new();
        
        private int _cardCount;

        private void Awake()
        {
            _blackjackGameManager.OnDealerCardDrawn += DealerGetsNewCard;
            _blackjackGameManager.OnPlayerStands += PlayerStands;
        }

        private void OnDestroy()
        {
            _blackjackGameManager.OnDealerCardDrawn -= DealerGetsNewCard;
            _blackjackGameManager.OnPlayerStands -= PlayerStands;
        }

        private void DealerGetsNewCard(BlackjackCard card)
        {
            _cardCount++;
            var newCardGameObject = Instantiate(cardPrefab, cardSpawnParent);
            var getFaceDownCard = _cardCount == 2;

            if(!getFaceDownCard) scoreText.text = "Score: " + _blackjackGameManager.DealerScore; // Only update score when all cards are facing up!

            if (!newCardGameObject.TryGetComponent<CardHandler>(out var cardHandler)) return;

            cardHandler.SetCard(card, getFaceDownCard);
            _spawnedCards.Add(cardHandler);
        }
        
        private void PlayerStands() => ShowAllCards();

        private void ShowAllCards()
        {
            if(_spawnedCards.Count <= 0) return;
            
            _spawnedCards.ForEach(c => c.RevealCard());
            scoreText.text = "Score: " + _blackjackGameManager.DealerScore;
        }
    }
}