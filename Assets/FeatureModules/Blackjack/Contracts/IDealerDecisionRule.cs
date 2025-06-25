namespace Blackjack
{
    public interface IDealerDecisionRule
    {
        bool ShouldDrawCard(BlackjackHand dealerHand, BlackjackHand playerHand);
    }
}