using System;

namespace Blackjack
{
    public interface IBlackjackGameManager
    {
        Action<BlackjackCard> OnPlayerCardDrawn { get; set; }
        Action<BlackjackCard> OnDealerCardDrawn { get; set; }
        int PlayerScore { get; }
        int DealerScore { get; }
        void RestartGame();
        void PlayerHits();
        void PlayerStands();
    }
}