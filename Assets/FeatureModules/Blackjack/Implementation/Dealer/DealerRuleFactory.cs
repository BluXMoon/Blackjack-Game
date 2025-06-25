using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    public class DealerRuleFactory : IDealerRuleFactory
    {
        public List<IDealerDecisionRule> GetRulesFor(BlackjackHand dealer)
        {
            var rules = new List<IDealerDecisionRule>();

            var value = dealer.GetScore();
            var aces = dealer.Cards.Count(c => c.card.rank == CardRank.Ace);
            var hasAce11 = aces > 0 && value <= 21 && value - (aces * 1) >= (aces * 10);

            if (hasAce11)
            {
                rules.Add(new DealerStopsAt19WithAce11());
                rules.Add(new DealerStopsAt18With3CardsUnlessPlayerStrong());
                rules.Add(new DealerDrawsWithAce11IfNotCovered());
            }
            else
            {
                rules.Add(new DealerUnder17PlayerHasHighCardOrAce());
                rules.Add(new DealerUnder12PlayerHas456());
                rules.Add(new DealerUnder13PlayerHas23());
            }

            return rules;
        }
    }
}