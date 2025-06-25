using System;

namespace Blackjack
{
    public interface IBlackjackGameManager
    {
        Action<BlackjackCard> OnPlayerCardDrawn { get; set; }
        Action<BlackjackCard> OnDealerCardDrawn { get; set; }
        void RestartGame();
        void PlayerHits();
        void PlayerStands();
    }
}