namespace Blackjack
{
    public class DealerStopsAt19WithAce11 : IDealerDecisionRule
    {
        public bool ShouldDrawCard(BlackjackHand dealer, BlackjackHand player) => 
            !(dealer.HasSoftAce() && dealer.GetScore() >= 19);
    }
}