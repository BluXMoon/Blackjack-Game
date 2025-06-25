using System.Linq;

namespace Blackjack
{
    public class DealerStopsAt18With3CardsUnlessPlayerStrong : IDealerDecisionRule
    {
        public bool ShouldDrawCard(BlackjackHand dealer, BlackjackHand player)
        {
            if (!HasAceValuedAt11(dealer)) return false;

            if (dealer.GetScore() != 18 || dealer.Cards.Count < 3)
                return false;

            return player.Cards.Any(c => c.card.rank == CardRank.Ace || c.GetValue() >= 9);
        }

        private bool HasAceValuedAt11(BlackjackHand hand)
        {
            var total = hand.GetScore();
            var aces = hand.Cards.Count(c => c.card.rank == CardRank.Ace);
            return aces > 0 && total <= 21 && total - (aces * 1) >= (aces * 10);
        }
    }
}