using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_216_Hard_CS
{
    class Card
    {
        public int Value { get; private set; }
        public CardSuit Suit { get; private set; }

        public Card(int value, CardSuit suit) {
            this.Value = value;
            this.Suit = suit;
        }

        public static Dictionary<CardSuit, string> _suitToChar = new Dictionary<CardSuit, string>()
        {
            {CardSuit.HEARTS, "\u2665"},
            { CardSuit.DIAMONDS, "\u2666"},
            { CardSuit.CLUBS, "\u2663"},
            { CardSuit.SPADES, "\u2660"}
        };

        private static Dictionary<int, string> _valToChar = new Dictionary<int, string>()
        {
            { 11,"J"},
            { 12, "Q"},
            { 13, "K"},
            { 14, "A"}
        };

        public override string ToString()
        {
            return string.Format(
                "{0}{1}", 
                _valToChar.ContainsKey(Value) ? _valToChar[Value]
                : Value.ToString(), _suitToChar[Suit]);
        }
    }
}
