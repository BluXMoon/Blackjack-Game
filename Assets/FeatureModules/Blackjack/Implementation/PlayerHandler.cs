using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Blackjack
{
    public class PlayerHandler : MonoBehaviour
    {
        [Header("Position/Parent of where new card will be spawned")]
        [SerializeField] private Transform cardSpawnParent;
        
        [Header("Card prefab")]
        [SerializeField] private GameObject cardPrefab;

        [Header("Score")] 
        [SerializeField] private TextMeshProUGUI scoreText;
        
        [Inject] private IBlackjackGameManager _blackjackGameManager;

        private List<CardHandler> _allCards = new();
        
        private void Awake()
        {
            _blackjackGameManager.OnPlayerCardDrawn += PlayerGetsNewCard;
            _blackjackGameManager.OnGameRestart += ClearAllCards;
        }

        private void OnDestroy()
        {
            _blackjackGameManager.OnPlayerCardDrawn -= PlayerGetsNewCard;
            _blackjackGameManager.OnGameRestart -= ClearAllCards;
        }

        private void PlayerGetsNewCard(BlackjackCard card)
        {
            var newCardGameObject = Instantiate(cardPrefab, cardSpawnParent);
            scoreText.text = "Score: " +  _blackjackGameManager.PlayerScore;

            if (!newCardGameObject.TryGetComponent<CardHandler>(out var cardHandler)) return;
            
            cardHandler.SetCard(card);
            _allCards.Add(cardHandler);
        }

        private void ClearAllCards()
        {
            _allCards.ForEach(c => Destroy(c.gameObject));
            _allCards.Clear();
        }
    }
}