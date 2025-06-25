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
        
        private void Awake() => _blackjackGameManager.OnPlayerCardDrawn += PlayerGetsNewCard;
        private void OnDestroy() => _blackjackGameManager.OnPlayerCardDrawn -= PlayerGetsNewCard;

        private void PlayerGetsNewCard(BlackjackCard card)
        {
            var newCardGameObject = Instantiate(cardPrefab, cardSpawnParent);
            scoreText.text = "Score: " +  _blackjackGameManager.PlayerScore;

            if (!newCardGameObject.TryGetComponent<CardHandler>(out var cardHandler)) return;
            
            cardHandler.SetCard(card.GetSprite());
        }
    }
}