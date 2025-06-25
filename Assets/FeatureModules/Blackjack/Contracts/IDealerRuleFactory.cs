using System.Collections.Generic;

namespace Blackjack
{
    public interface IDealerRuleFactory
    {
        List<IDealerDecisionRule> GetRulesFor(BlackjackHand dealer);
    }
}