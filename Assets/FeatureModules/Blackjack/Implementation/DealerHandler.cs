using System;
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
        
        [Inject] private IBlackjackGameManager _blackjackGameManager;

        private int _cardCount;

        private void Awake() => _blackjackGameManager.OnDealerCardDrawn += DealerGetsNewCard;
        private void OnDestroy() => _blackjackGameManager.OnDealerCardDrawn -= DealerGetsNewCard;

        private void DealerGetsNewCard(BlackjackCard card)
        {
            _cardCount++;
            var newCardGameObject = Instantiate(cardPrefab, cardSpawnParent);

            if (!newCardGameObject.TryGetComponent<CardHandler>(out var cardHandler)) return;

            var getFaceDownCard = _cardCount == 2;
            cardHandler.SetCard(card.GetSprite(getFaceDownCard));
        }
    }
}