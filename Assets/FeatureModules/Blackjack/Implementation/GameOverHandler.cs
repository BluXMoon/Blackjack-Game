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
                GameOutcome.DealerLost => "Dealer Lost With Total Points Over 21",
                GameOutcome.DealerWon => "Dealer Won With More Points Than Player",
                GameOutcome.PlayerWon => "Player Won With More Points Than Dealer",
                GameOutcome.PlayerLost => "Player Lost With Total Points Over 21",
                GameOutcome.PlayerBlackjack => "Player Won With Blackjack",
                GameOutcome.DealerBlackjack => "Dealer Won With Blackjack",
                _ => "Draw"
            };
    }
}
