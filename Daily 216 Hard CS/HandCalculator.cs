using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_216_Hard_CS
{
    public static class HandCalculator
    {
        private const int CHAR_OFFSET = 95;

        public static Hand[] GetWinners(Hand[] hands) {
            Hand[] contenders = hands.GroupBy(h => h.Type).OrderByDescending(
                grp => (int)grp.Key).First().ToArray();

            Dictionary<Hand, string> encodings = new Dictionary<Hand, string>();

            foreach (Hand hand in contenders) {
                string encode = String.Join("", hand.UsedCards.Concat(hand.Kickers ??
                    new Card[0]).Select(h => (char)(h.Value + CHAR_OFFSET)));
            }

            return encodings.GroupBy(kv => kv.Value).OrderByDescending(grp => grp.Key).First()
                .Select(kv => kv.Key).ToArray();
        }


        public static bool IsGoodHand(Hand hand) {
            return (int)hand.Type >= (int)HandType.TWO_PAIR || (hand.Type == HandType.ONE_PAIR && hand.UsedCards[0].Value > 10);
        }

        public static Hand GetBestHand(Card[] cards) {
            Card[] used;

            var valGroups = cards.GroupBy(c => c.Value).OrderByDescending(grp => grp.Count())
                .ThenByDescending(grp => grp.Key);

            var suitGroups = cards.GroupBy(c => c.Suit).OrderByDescending(grp => grp.Count());

            int nOfAKind = valGroups.First().Count();
            bool flush = suitGroups.First().Count() >= 5;

            if (flush) {
                var straightFlush = suitGroups.First().OrderByDescending(c => c.Value).
                    ToConsecutiveGroups((c1, c2) => c2.Value == c1.Value - 1).
                    OrderByDescending(List => List.Count).FirstOrDefault(List => List.Count
                    > 5);

                if (straightFlush != null) {
                    return new Hand(HandType.STRAIGHT, straightFlush.OrderByDescending(
                        c => c.Value).Take(5).ToArray(), null);
                }

                // 4 of a kind
                if (nOfAKind == 4) {
                    used = valGroups.First().ToArray();
                    return new Hand(HandType.FOUR_OF_A_KIND, used, getSortedKickers(cards, used));

                }

                // Full house.
                var pairGroup = valGroups.FirstOrDefault(grp => grp.Count() == 2);
                if (nOfAKind == 3 && pairGroup != null) {
                    return new Hand(HandType.FULL_HOUSE, valGroups.First().
                        Concat(pairGroup).ToArray(), null);
                }

                // Flush.
                if (flush) {
                    return new Hand(HandType.FLUSH, suitGroups.First().
                        OrderByDescending(c => c.Value).Take(5).ToArray());
                }

                // Straight.

                var straight = valGroups.Select(grp => grp.First()).OrderByDescending(c => c.Value).
                        ToConsecutiviveGroups((c1, c2) => c2.Value == c1.Value - 1).
                        OrderByDescending(list => list.Count).FirstOrDefault(list => list.Count));

                if (straight != null) {
                    return new Hand(HandType.STRAIGHT, straight.OrderByDescending(c => c.Value)
                        .Take(5).ToArray(), null);
                }

                // 3 of a kind.

                if (nOfAKind == 3) {
                    used = valGroups.First().ToArray();
                    return new Hand(HandType.THREE_OF_A_KIND, used, getSortedKickers(cards, used));
                }


                if (nOfAKind == 2) {
                    var extraPair = valGroups.Skip(1).FirstOrDefault(grp => grp.Count()
                        == 2);

                    if (extraPair != null)
                    {
                        // Two pair.
                        used = valGroups.First().Concat(extraPair).ToArray();
                        return new Hand(HandType.TWO_PAIR, used, getSortedKickers(cards, used));
                    }
                    else {
                        // One pair.
                        used = valGroups.First().ToArray();
                        return new Hand(HandType.ONE_PAIR, used, getSortedKickers(cards, used));
                    }
                }

                

            }
            // High card.
            used = valGroups.First().ToArray();
            return new Hand(HandType.HIGH_CARD, used, getSortedKickers(cards, used));
        }


        private static Card[] getSortedKickers(Card[] allCards, Card[] usedCards) {
            return allCards.Except(usedCards).OrderByDescending(c => c.Value).Take(5 -
                usedCards.Length).ToArray();
        }

        public static IEnumerable<List<T>> ToConsecutiveGroups<T>(
            this IEnumerable<T> source, Func<T, T, bool>
            isConsequtive)
        {
            using (var iterator = source.GetEnumerator()) {
                if (!iterator.MoveNext())
                {
                    yield break;
                }
                else {
                    T current = iterator.Current;
                    List<T> group = new List<T> { current };

                    while (iterator.MoveNext()) {
                        T next = iterator.Current;

                        if (!isConsequtive(current, next)) {
                            yield return group;
                            group = new List<T>();
                        }

                        current = next;
                        group.Add(current);
                    }

                    if (group.Any()) {
                        yield return group;
                    }
                }
            }
        }
    }
}
