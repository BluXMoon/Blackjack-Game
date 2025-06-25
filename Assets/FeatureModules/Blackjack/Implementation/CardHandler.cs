using UnityEngine;
using UnityEngine.UI;

namespace Blackjack
{
    [RequireComponent(typeof(Image))]
    public class CardHandler : MonoBehaviour
    {
        private Image _cardImage;

        private void Awake() => _cardImage = GetComponent<Image>();
        
        public void SetCard(Sprite sprite) => _cardImage.sprite = sprite;
    }
}
