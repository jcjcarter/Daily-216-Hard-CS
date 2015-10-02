using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_216_Hard_CS
{
    public class HoldEm
    {
        Random _rand = new Random();

        private Player[] _players;
        private Deck _deck;
        private Card[] _commonCards;
        private Stats _stats;

        public HoldEm(Player[] players, ref Stats stats) {
            _commonCards = new Card[5];
            _deck = new Deck();
            _players = players;
            _stats = stats;
        }

        public void Play() {
            deal();

            _deck.BurnCard();

            flop();
            checkFolds();

            _deck.BurnCard();
            turn();

            _deck.BurnCard();

            river();

            checkWinner();
        }


        private void deal() {
            foreach (Player p in _players) {
                p.Cards[0] = _deck.DrawCard();
                p.Cards[1] = _deck.DrawCard();
            }
        }

        private void flop() {
            _commonCards[0] = _deck.DrawCard();
            _commonCards[1] = _deck.DrawCard();
            _commonCards[2] = _deck.DrawCard();
        }

        private void turn() {
            _commonCards[3] = _deck.DrawCard();
        }

        private void river() {
            _commonCards[4] = _deck.DrawCard();
        }

        private void checkFolds() {
            List<Task> jobs = new List<Task>();
            foreach (Player player in _players) {
                Player p = player;
                jobs.Add(Task.Factory.StartNew(() => {

                    var visibleCards = _commonCards.Take(3).Concat(p.Cards);
                    Hand hand = HandCalculator.GetBestHand(visibleCards
                        .Concat(new Card[] { card })
                        .ToArray());

                    if (HandCalculator.IsGoodHand(new Hand))
                    {
                        return;
                    }

                    int outs = 0;

                    foreach (Card card in Deck.FullDeckOfCards().Except(visibleCards)) {
                        Hand newHand = HandCalculator.GetBestHand(
                            visibleCards.Concat(new Card[] { card }).ToArray());

                        if (HandCalculator.IsGoodHand(newHand)) {
                            outs++;
                        }
                    }

                    double prob = (93 * outs - outs * outs) / 2162.0;

                    p.Folded = prob < 0.5;
                }));
            }

            foreach (Task t in jobs) {
                t.Wait();
            }

            if (_players.All(p => p.Folded)) {
                _players[_rand.Next(_players.Length)].Folded = false;
            }
        }

        private void checkWinner() {
            Dictionary<Player, Hand> best = new Dictionary<Player, Hand>();

            foreach (Player player in _players) {
                Hand hand = HandCalculator.GetBestHand(_commonCards.Concat(
                    player.Cards).ToArray());

                best.Add(player, hand);
            }

            Hand[] winHands = HandCalculator.GetWinners(best.Where(kv =>
            !kv.Key.Folded).Select(kv => kv.Value).ToArray());
            Hand[] wouldWinHands = HandCalculator.GetWinners(best.Values.ToArray());

            Dictionary<Player, Hand> winners = best.Where(kv => winHands.Contains(kv.Value))
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            Dictionary<Player, Hand> wouldBes = best.Where(kv => wouldWinHands.Contains(kv.Value))
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            _stats.AddResult(winners.Keys.ToArray(), winHands[0].Type, !winners.SequenceEqual(wouldBes));

        }

    }
}
