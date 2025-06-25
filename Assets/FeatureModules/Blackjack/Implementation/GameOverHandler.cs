using TMPro;
using UnityEngine;
using Zenject;

namespace Blackjack
{
    public class GameOverHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameOutcomeText;
        
        [Inject] private IBlackjackGameManager _blackjackGameManager;

        private void Awake() => _blackjackGameManager.OnGameOver += ShowGameResults;
        private void OnDestroy() => _blackjackGameManager.OnGameOver -= ShowGameResults;

        private void ShowGameResults(GameOutcome outcome) => gameOutcomeText.text = TranslateOutcomeToString(outcome);

        private string TranslateOutcomeToString(GameOutcome outcome) =>
            outcome switch
            {
                GameOutcome.DealerLost => "Dealer Lost",
                GameOutcome.DealerWon => "Dealer Won",
                GameOutcome.PlayerWon => "Player Won",
                GameOutcome.PlayerLost => "Player Lost",
                GameOutcome.PlayerBlackjack => "Player Blackjack",
                GameOutcome.DealerBlackjack => "Dealer Blackjack",
                _ => "Draw"
            };
    }
}
