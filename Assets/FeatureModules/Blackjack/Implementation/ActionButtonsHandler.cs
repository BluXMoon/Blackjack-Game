using UnityEngine;
using Zenject;

namespace Blackjack
{
    public class ActionButtonsHandler : MonoBehaviour
    {
        [Inject] private IBlackjackGameManager _blackjackGameManager;
        
        public void PlayerHit() => _blackjackGameManager.PlayerHits();
        public void PlayerStand() => _blackjackGameManager.PlayerStands();
        public void Restart() => _blackjackGameManager.RestartGame();
    }
}
