using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_216_Hard_CS
{
    public class Deck
    {
        private Card[] _deck;
        private int _cardsLeft;

        public Deck() {
            _deck = Deck.FullDeckOfCards();
            _cardsLeft = _deck.Length;
            Shuffle();
        }

        public void Shuffle() {
            Deck.shuffle(_deck);
        }

        public Card DrawCard() {
            Card c = _deck[_cardsLeft - 1];
            _deck[--_cardsLeft] = null;
            return c;
        }

        public void BurnCard() {
            _deck[--_cardsLeft] = null;
        }

        public static Card FullDeckOfCards() {
            return Enumerable.Range(0, 52).Select(n =>
             new Card((n % 13) + 2, (CardSuit)(n / 13))).ToArray();
        }

        private static Random _random = new Random();

        private static void shuffle<T>(T[] array) {
            int n = array.Length;
            for (int i = 0; i < n; i++) {
                int r = i + (int)(_random.NextDouble() * (n - i));
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }
    }
}
