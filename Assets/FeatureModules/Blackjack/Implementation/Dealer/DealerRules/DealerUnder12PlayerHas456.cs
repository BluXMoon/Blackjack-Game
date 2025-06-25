using System.Linq;

namespace Blackjack
{
    public class DealerUnder12PlayerHas456 : IDealerDecisionRule
    {
        public bool ShouldDrawCard(BlackjackHand dealer, BlackjackHand player)
        {
            if (dealer.GetScore() >= 12) return false;

            return player.Cards.Any(c =>
                c.card.rank is CardRank.Four or CardRank.Five or CardRank.Six);
        }
    }
}