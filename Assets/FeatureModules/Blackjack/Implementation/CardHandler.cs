using UnityEngine;
using UnityEngine.UI;

namespace Blackjack
{
    [RequireComponent(typeof(Image))]
    public class CardHandler : MonoBehaviour
    {
        private Image _cardImage;
        private BlackjackCard _card;

        private void Awake() => _cardImage = GetComponent<Image>();

        public void SetCard(BlackjackCard card, bool showBackCardSprite = false)
        {
            _card = card;
            _cardImage.sprite = _card.GetSprite(showBackCardSprite);
        }

        public void RevealCard() => _cardImage.sprite = _card.GetSprite();
    }
}
