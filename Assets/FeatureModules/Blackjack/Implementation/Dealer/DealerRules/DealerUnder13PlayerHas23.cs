using System.Linq;

namespace Blackjack
{
    public class DealerUnder13PlayerHas23 : IDealerDecisionRule
    {
        public bool ShouldDrawCard(BlackjackHand dealer, BlackjackHand player)
        {
            if (dealer.GetScore() >= 13) return false;

            return player.Cards.Any(c =>
                c.card.rank == CardRank.Two || c.card.rank == CardRank.Three);
        }
    }
}