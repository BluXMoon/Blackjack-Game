using System.Linq;

namespace Blackjack
{
    public class DealerUnder17PlayerHasHighCardOrAce : IDealerDecisionRule
    {
        public bool ShouldDrawCard(BlackjackHand dealer, BlackjackHand player) => 
            dealer.GetScore() < 17 && player.Cards.Any(c => c.card.rank == CardRank.Ace || c.GetValue() >= 7);
    }
}