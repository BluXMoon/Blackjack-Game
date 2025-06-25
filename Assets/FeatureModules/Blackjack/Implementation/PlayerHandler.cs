using System;
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
        
        [Inject] private IBlackjackGameManager _blackjackGameManager;

        private int _cardCount;

        private void Awake() => _blackjackGameManager.OnPlayerCardDrawn += PlayerGetsNewCard;
        private void OnDestroy() => _blackjackGameManager.OnPlayerCardDrawn -= PlayerGetsNewCard;

        private void PlayerGetsNewCard(BlackjackCard card)
        {
            _cardCount++;
            var newCardGameObject = Instantiate(cardPrefab, cardSpawnParent);

            if (!newCardGameObject.TryGetComponent<CardHandler>(out var cardHandler)) return;
            
            cardHandler.SetCard(card.GetSprite());
        }
    }
}