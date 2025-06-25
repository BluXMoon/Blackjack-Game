using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    public class CompositeDealerRule : IDealerDecisionRule
    {
        private readonly List<IDealerDecisionRule> _rules = new();

        public void AddRule(IDealerDecisionRule rule) => _rules.Add(rule);

        public bool ShouldDrawCard(BlackjackHand dealerHand, BlackjackHand playerHand) => 
            _rules.Any(rule => rule.ShouldDrawCard(dealerHand, playerHand));
    }
}