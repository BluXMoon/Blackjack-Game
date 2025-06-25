using System.Linq;

namespace Blackjack
{
    public class DealerDrawsWithAce11IfNotCovered : IDealerDecisionRule
    {
        public bool ShouldDrawCard(BlackjackHand dealer, BlackjackHand player) => 
            HasAceValuedAt11(dealer);

        private bool HasAceValuedAt11(BlackjackHand hand)
        {
            var total = hand.GetScore();
            var aces = hand.Cards.Count(c => c.card.rank == CardRank.Ace);
            return aces > 0 && total <= 21 && total - (aces * 1) >= (aces * 10);
        }
    }
}